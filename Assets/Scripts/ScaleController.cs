using UnityEngine;
using System.Collections;

public class ScaleController : MonoBehaviour {

    public bool scaleX = true;
    [SerializeField]
    private float minScale;
    public float maxScaleX;
    public float maxScaleZ;

    [SerializeField]
    public float speed;
    private bool scaleUp = true;    
    private bool isAlive = true;
    private Transform transformR;

    public GameObject[] planes;

	void Start () {
        transformR = gameObject.transform;
        if (scaleX == true)
        {
            transformR.localScale = new Vector3(0, transformR.localScale.y, transformR.localScale.z);
        }
        else
        {
            transformR.localScale = new Vector3(transformR.localScale.x, transformR.localScale.y, 0);
        }
	}
	
	void Update () {
        if (isAlive == false)
        {
            //return;
        }
        else
        {
            float ScaleVal = 0;
            float maxScale = 0;
            Vector3 target = new Vector3(0,0,0);
            if (scaleX == true)
            {
                ScaleVal = transformR.localScale.x;
                maxScale = maxScaleX;
                if (scaleUp == true)
                {
                    target = new Vector3(maxScale, transformR.localScale.y, transformR.localScale.z);
                }
                else
                {
                    target = new Vector3(minScale, transformR.localScale.y, transformR.localScale.z);
                }
            }
            else
            {
                ScaleVal = transformR.localScale.z;
                maxScale = maxScaleZ;
                if (scaleUp == true)
                {
                    target = new Vector3(transformR.localScale.x, transformR.localScale.y, maxScale);
                }
                else
                {
                    target = new Vector3(transformR.localScale.x, transformR.localScale.y, minScale);
                }
            }

            if (scaleUp == true)
            {
                if (ScaleVal >= maxScale)
                {
                    scaleUp = !scaleUp;
                }
                else
                {
                    transformR.localScale = Vector3.MoveTowards(transformR.localScale, target, Time.deltaTime * speed);
                }
            }
            else
            {
                if (ScaleVal <= minScale)
                {
                    scaleUp = !scaleUp;
                }
                else
                {
                    transformR.localScale = Vector3.MoveTowards(transformR.localScale, target, Time.deltaTime * speed);
                }
            }
        }
	}

    public void stopScaling()
    {
        isAlive = false;
    }

    public void startScaling()
    {
        isAlive = true;
    }

    public Vector3 Scale {
        get { return transformR.localScale; }
        set { transformR.localScale = value; }
    }
    public Vector3 Position
    {
        get { return transformR.position; }
    }
}
