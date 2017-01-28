using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    [SerializeField]
    private GameObject envController;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    public GameObject platformPrefab;

    [SerializeField]
    public GameObject activePlatform = null;
    private GameObject unactivePlatform = null;

    public float minDeltaScale;
    public float loseScale;
    public int countOverlap = 4;
    private int overlap = 0;
    public float plusScale = 0.5f;
    public float maxScaleX = 4;
    public float maxScaleZ = 4;
    public float coff = 0.05f;

    [Header("Speed scaling settings")]
    public float minSpeed;
    public float maxSpeed;
    private float speedScale;
    public float stepSpeed;
    public int countPlatform;
    private int countP = 0;

    private float height;

    [Header("Other settings")]
    public GameObject animationStack;
    public GameObject animationPlusStack;
    public GameObject gameScreen;
    public GameObject endScreen;
    public GameObject settings;
    public GameObject colors;
    public float speedPlatformColor = 1;
    Color nextPlatformColor = Color.red;

    public GameObject highscoreEffect;

    public List<GameObject> coloredObj = new List<GameObject>();

    bool scaleX = true;
    int highscore;


    void Start()
    {
        highscore = settings.GetComponent<Settings>().Highscore;
        envController.GetComponent<EnviromentController>().initAll();
        height = platformPrefab.gameObject.GetComponent<MeshRenderer>().bounds.size.y;
        SetCameraPos();
        GetRandomColorSwitch();
        SetStartPlatformColor();
        speedScale = minSpeed;
        nextPlatformColor = colors.GetComponent<ColorSettings>().GetRandom();
    }

    void SetStartPlatformColor()
    {
        Color c = colors.GetComponent<ColorSettings>().GetRandom();
        foreach(GameObject o in coloredObj){
            o.GetComponent<Renderer>().material.SetColor("_Color", c);
            //o.GetComponent<Renderer>().material.SetColor("_EMISSION", c);
            foreach (GameObject ob in o.GetComponent<ScaleController>().planes)
            {
                ob.GetComponent<Renderer>().material.SetColor("_Color", c);
                //o.GetComponent<Renderer>().material.SetColor("_EMISSION", c);
            }
        }
    }

    public void NewPlatform()
    {
        if (activePlatform == null || (activePlatform.transform.localScale.x > loseScale &&
            activePlatform.transform.localScale.z > loseScale))
        {
            createPlatform();
        }
        else
        {
            activePlatform.GetComponent<ScaleController>().stopScaling();
            GameOver();
        }
    }

    public void StartGame()
    {
        activePlatform.gameObject.SetActive(true);
    }

    void createPlatform()
    {
        float maxScaleX = 0;
        float maxScaleZ = 0;
        
        if (unactivePlatform != null)
        {
            if (activePlatform != null)
            {
                if (scaleX == true)
                {
                    float scaleXUnactive = unactivePlatform.GetComponent<ScaleController>().Scale.x;
                    float scaleXActive = activePlatform.GetComponent<ScaleController>().Scale.x;
                    if (scaleXUnactive - scaleXActive <= minDeltaScale)
                    {
                        activePlatform.transform.localScale = unactivePlatform.transform.localScale;
                        overlap++;                        
                        EqualsScale();
                    }
                    else
                    {
                        overlap = 0;
                    }
                }
                else
                {
                    float scaleZUnactive = unactivePlatform.GetComponent<ScaleController>().Scale.z;
                    float scaleZActive = activePlatform.GetComponent<ScaleController>().Scale.z;
                    if (scaleZUnactive - scaleZActive <= minDeltaScale)
                    {
                        activePlatform.transform.localScale = unactivePlatform.transform.localScale;
                        overlap++;
                        EqualsScale();
                    }
                    else
                    {
                        overlap = 0;
                    }
                }                
            }
        }
        maxScaleX = activePlatform.transform.localScale.x;
        maxScaleZ = activePlatform.transform.localScale.z;
        unactivePlatform = activePlatform;
        unactivePlatform.GetComponent<ScaleController>().stopScaling();
        
        activePlatform = Instantiate(platformPrefab);
        if (unactivePlatform != null)
        {
            if (scaleX == true)
            {
                if (unactivePlatform.transform.localScale.z > loseScale)
                {
                    scaleX = !scaleX;
                }
            }
            else
            {
                if (unactivePlatform.transform.localScale.x > loseScale)
                {
                    scaleX = !scaleX;
                }
            }
        }
        else
        {
            scaleX = !scaleX;
        }
        if (scaleX == true)
        {
            if (unactivePlatform != null)
            {
                if (overlap >= countOverlap)
                {
                    activePlatform.transform.localScale = new Vector3(0, unactivePlatform.transform.localScale.y, targetScale.z);
                }
                else
                {
                    activePlatform.transform.localScale = new Vector3(0, unactivePlatform.transform.localScale.y, unactivePlatform.transform.localScale.z);
                }
            }
            else
            {
                if (overlap >= countOverlap)
                {
                    activePlatform.transform.localScale = new Vector3(0, unactivePlatform.transform.localScale.y, targetScale.z);
                }
                else
                {
                    activePlatform.transform.localScale = new Vector3(0, activePlatform.transform.localScale.y, activePlatform.transform.localScale.z);
                }
            }
        }
        else
        {
            if (unactivePlatform != null)
            {
                if (overlap >= countOverlap)
                {
                    activePlatform.transform.localScale = new Vector3(targetScale.x, unactivePlatform.transform.localScale.y, 0);
                }
                else
                {
                    activePlatform.transform.localScale = new Vector3(unactivePlatform.transform.localScale.x, unactivePlatform.transform.localScale.y, 0);
                }
            }
            else
            {
                if (overlap >= countOverlap)
                {
                    activePlatform.transform.localScale = new Vector3(targetScale.x, unactivePlatform.transform.localScale.y, 0);
                }
                else
                {
                    activePlatform.transform.localScale = new Vector3(unactivePlatform.transform.localScale.x, activePlatform.transform.localScale.y, 0);
                }
            }
        }
        activePlatform.GetComponent<ScaleController>().scaleX = scaleX;
        if (unactivePlatform != null)
        {
            activePlatform.transform.position = new Vector3(unactivePlatform.transform.position.x,
                unactivePlatform.transform.position.y + height, unactivePlatform.transform.position.z);
            SetPlatformColor(unactivePlatform, activePlatform);
        }
        if (overlap >= countOverlap)
        {
            activePlatform.GetComponent<ScaleController>().maxScaleX = targetScale.x;
            activePlatform.GetComponent<ScaleController>().maxScaleZ = targetScale.z;
        }
        else
        {
            activePlatform.GetComponent<ScaleController>().maxScaleX = maxScaleX;
            activePlatform.GetComponent<ScaleController>().maxScaleZ = maxScaleZ;
        }
        countP++;
        float newScaleSpeed = 0;
        if (countP % countPlatform == 0 && speedScale + stepSpeed <= maxSpeed)
        {
            speedScale += stepSpeed;
        }
        System.Random r = new System.Random();
        double d = r.NextDouble();
        newScaleSpeed = (speedScale - minSpeed) * (float)d + minSpeed;
        activePlatform.GetComponent<ScaleController>().speed = newScaleSpeed;

        SetCameraPos();
        settings.GetComponent<Settings>().Score++;        
        CheckNewBest();
    }

    bool newHighscore = false;
    void CheckNewBest()
    {        
        if (settings.GetComponent<Settings>().Score > highscore
            && newHighscore == false
            && highscore >= 1)
        {
            newHighscore = true;
            highscoreEffect.GetComponent<NewHighscore>().Play();
            endScreen.GetComponent<EndScreenController>().showNewHighscore = true;
            Debug.Log("NEW HIGHSCORE");
        }
    }

    void SetCameraPos()
    {
        cam.GetComponent<CameraController>().SetY(activePlatform.transform.position.y);
    }

    void EqualsScale()
    {
        if (overlap >= countOverlap)
        {
            PlusScale();
            activePlatform.GetComponent<PlusScaleAnimation>().Animate(targetScale);
            GameObject ps = Instantiate(animationPlusStack);
            ps.transform.position = activePlatform.transform.position;
            ps.transform.position = new Vector3(ps.transform.position.x, ps.transform.position.y - height / 2.0f, ps.transform.position.z);
            ps.SetActive(true);
        }
        else
        {
            PlusScaleAnimations();
        }
    }

    Vector3 targetScale;
    void PlusScale()
    {        
        int c = PlayerPrefs.GetInt("coins");
        c++;
        PlayerPrefs.SetInt("coins", c);

        targetScale = new Vector3(0,0,0);
        float x = activePlatform.transform.localScale.x;
        float z = activePlatform.transform.localScale.z;

        if (x < z)
        {
            if (x + plusScale > maxScaleX)
            {
                targetScale = new Vector3(maxScaleX, activePlatform.transform.localScale.y, z);
            }
            else
            {
                targetScale = new Vector3(x + plusScale, activePlatform.transform.localScale.y, z);
            }
        }
        else
        {
            if (z + plusScale > maxScaleZ)
            {
                targetScale = new Vector3(x, activePlatform.transform.localScale.y, maxScaleZ);
            }
            else
            {
                targetScale = new Vector3(x, activePlatform.transform.localScale.y, z + plusScale);
            }
        }       
    }

    void PlusScaleAnimations()
    {        
        GameObject ps = Instantiate(animationStack);
        ps.transform.position = activePlatform.transform.position;
        ps.transform.position = new Vector3(ps.transform.position.x, ps.transform.position.y - height / 2.0f, ps.transform.position.z);
        ps.SetActive(true);
    }

    void GameOver()
    {
        gameScreen.GetComponent<IScreenController>().Skip();
        endScreen.GetComponent<IScreenController>().View();
        cam.GetComponent<CameraController>().GameOver();
    }

    ColorSwitch colorST = ColorSwitch.RedM;

    private void GetRandomColorSwitch()
    {
        System.Random rnd = new System.Random();
        colorST = (ColorSwitch)rnd.Next(0, 5);
    }

    void SetColor(GameObject oOld, GameObject oNew)
    {        
        Color newColor = new Color(0,0,0);

        Color old = oOld.GetComponent<Renderer>().material.GetColor("_Color");
        switch (colorST)
        {
            case ColorSwitch.RedM :
                if (old.r > 0)
                {
                    newColor = new Color(old.r - coff, old.g, old.b);
                }
                else
                {
                    GetRandomColorSwitch();
                }
                break;
            case ColorSwitch.RedP:
                if (old.r < 1)
                {
                    newColor = new Color(old.r + coff, old.g, old.b);
                }
                else
                {
                    GetRandomColorSwitch();
                }
                break;
            case ColorSwitch.GreenM:
                if (old.g > 0)
                {
                    newColor = new Color(old.r, old.g - coff, old.b);
                }
                else
                {
                    GetRandomColorSwitch();
                }
                break;
            case ColorSwitch.GreenP:
                if (old.g < 1)
                {
                    newColor = new Color(old.r, old.g + coff, old.b);
                }
                else
                {
                    GetRandomColorSwitch();
                }
                break;
            case ColorSwitch.BlueM:
                if (old.b > 0)
                {
                    newColor = new Color(old.r, old.g, old.b - coff);
                }
                else
                {
                    GetRandomColorSwitch();
                }
                break;
            case ColorSwitch.BlueP:
                if (old.b < 1)
                {
                    newColor = new Color(old.r, old.g, old.b + coff);
                }
                else
                {
                    GetRandomColorSwitch();
                }
                break;
        }
        if (newColor.r == 0 && newColor.g == 0 && newColor.b ==0)
        {
            newColor = old;
            GetRandomColorSwitch();
        }
        oNew.GetComponent<Renderer>().material.SetColor("_Color", newColor);
        //oNew.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(newColor.r / 4.0f, newColor.g / 4.0f, newColor.b / 4.0f));
        foreach (GameObject o in oNew.GetComponent<ScaleController>().planes)
        {
            o.GetComponent<Renderer>().material.SetColor("_Color", newColor);
            //o.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(newColor.r / 4.0f, newColor.g / 4.0f, newColor.b / 4.0f));
        }
    }    

    void SetPlatformColor(GameObject oOld, GameObject oNew)
    {
        Color newColor = new Color(0, 0, 0);

        Color old = oOld.GetComponent<Renderer>().material.GetColor("_Color");

        newColor = CalculateColor(old, nextPlatformColor);

        if (newColor == nextPlatformColor)
        {
            nextPlatformColor = colors.GetComponent<ColorSettings>().GetRandom();
        }

        oNew.GetComponent<Renderer>().material.SetColor("_Color", newColor);
        //oNew.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(newColor.r / 4.0f, newColor.g / 4.0f, newColor.b / 4.0f));
        foreach (GameObject o in oNew.GetComponent<ScaleController>().planes)
        {
            o.GetComponent<Renderer>().material.SetColor("_Color", newColor);
           // o.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(newColor.r / 4.0f, newColor.g / 4.0f, newColor.b / 4.0f));
        }
    }

    Color CalculateColor(Color act, Color newC)
    {        
        Vector3 c1 = new Vector3(act.r, act.g, act.b);
        Vector3 c2 = new Vector3(newC.r, newC.g, newC.b);
        Vector3 res = Vector3.MoveTowards(c1,c2,speedPlatformColor);
        Color c = new Color(res.x, res.y, res.z);
        return c;
    }

}

public enum ColorSwitch
{
    RedM,
    RedP,
    GreenM,
    GreenP,
    BlueM,
    BlueP
}
