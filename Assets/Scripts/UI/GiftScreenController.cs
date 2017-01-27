using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GiftScreenController : MonoBehaviour, IScreenController {

    public GameObject prevScene;
    public GameObject bg;
    public GameObject gift;
    public GameObject gem;

    public GameObject open;
    public GameObject close;
    public Text plusCoinsText;
    public int gemsCount;

    public float flashTime;
    private float flashDlt = 0;
    public GameObject flashBG;
    public Color start;
    public Color finish;
    private bool startFlash = false;

    GameObject cam;

	void Start () {
        startFlash = false;
        cam = GameObject.Find("Main Camera");
        gift.transform.position = new Vector3(gift.transform.position.x, cam.transform.position.y - 2, cam.transform.position.z + 3.5f);
	}

    void Update()
    {
        if(startFlash == true){
            tweenColor(flashDlt, flashTime, start, finish);
            flashDlt += Time.deltaTime;
        }
    }

    void tweenColor(float t, float time, Color start, Color finish)
    {
        flashBG.GetComponent<Image>().color = Color.Lerp(start, finish, t / time);
    }

    public void View()
    {
        startFlash = false;
        flashDlt = 0;
        gameObject.SetActive(true);
        bg.SetActive(true);
        gift.SetActive(true);
        open.SetActive(true);
        int c = PlayerPrefs.GetInt("coins");
        PlayerPrefs.SetInt("coins", (c + gemsCount));
        plusCoinsText.text = "+ " + gemsCount;
        gift.transform.position = new Vector3(gift.transform.position.x, cam.transform.position.y - 2.0f, cam.transform.position.z + 3.5f);
        
    }

    public void Skip()
    {
        gameObject.SetActive(false);
        bg.SetActive(false);
        gift.SetActive(false);
        close.SetActive(false);
        open.SetActive(false);
        gem.SetActive(false);
    }

    public void Back()
    {
        Skip();
        prevScene.GetComponent<IScreenController>().View();
    }

    public void Open()
    {
        gem.transform.position = new Vector3(0, cam.transform.position.y - 1.0f, cam.transform.position.z + 3.5f);
        startFlash = true;
        open.SetActive(false);
        gift.SetActive(false);
        close.SetActive(true);
        gem.SetActive(true);
    }
}
