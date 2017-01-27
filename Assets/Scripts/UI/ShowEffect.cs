using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShowEffect : MonoBehaviour {

    public List<GameObject> top = new List<GameObject>();
    private float newY;
    public float topSpeed;
    private List<Vector3> topPos = new List<Vector3>();
    private bool moveTop = false;

    public List<GameObject> bot = new List<GameObject>();
    private float newYBot;
    public float botSpeed;
    private List<Vector3> botPos = new List<Vector3>();
    private bool moveBot = false;

    public List<GameObject> left = new List<GameObject>();
    private float newXLeft;
    public float leftSpeed;
    private List<Vector3> leftPos = new List<Vector3>();
    private bool moveLeft = false;

    public List<GameObject> right = new List<GameObject>();
    private float newXRight;
    public float rightSpeed;
    private List<Vector3> rightPos = new List<Vector3>();
    private bool moveRight = false;

    public List<Image> alpha = new List<Image>();
    public float alphaSpeed;
    private bool alphaState;

	
    public void StartAnimation()
    {

        newY = Camera.main.pixelHeight * 2;
        newYBot = -Camera.main.pixelHeight;
        newXRight = Camera.main.pixelWidth;
        newXLeft = -Camera.main.pixelWidth;
        foreach (GameObject t in top)
        {
            topPos.Add(t.transform.position);
        }
        foreach (GameObject t in top)
        {
            t.transform.position = new Vector3(t.transform.position.x, newY, t.transform.position.z);
        }
        moveTop = true;
        ////////////////////
        foreach (GameObject t in bot)
        {
            botPos.Add(t.transform.position);
        }
        foreach (GameObject t in bot)
        {
            t.transform.position = new Vector3(t.transform.position.x, newYBot, t.transform.position.z);
        }
        moveBot = true;
        ////////////////////
        foreach (GameObject t in left)
        {
            leftPos.Add(t.transform.position);
        }
        foreach (GameObject t in left)
        {
            t.transform.position = new Vector3(newXLeft, t.transform.position.y, t.transform.position.z);
        }
        moveLeft = true;

        foreach (GameObject t in right)
        {
            rightPos.Add(t.transform.position);
        }
        foreach (GameObject t in right)
        {
            t.transform.position = new Vector3(newXRight, t.transform.position.y, t.transform.position.z);
        }
        moveRight = true;
        //////////////////////
        foreach (Image i in alpha)
        {
            Color c = i.color;
            c.a = 0;
            i.color = c;
        }
        alphaState = true;


    }

    public void StartAnimationHide()
    {

        newY = Camera.main.pixelHeight * 2;
        newYBot = -Camera.main.pixelHeight / 10;
        newXRight = Camera.main.pixelWidth*2;
        newXLeft = -Camera.main.pixelWidth;
       
        moveTop = false;

        moveBot = false;

        moveLeft = false;

        moveRight = false;

        foreach (Image i in alpha)
        {
            Color c = i.color;
            c.a = 0.5f;
            i.color = c;
        }
        alphaState = false;


    }

	void Update () {

        if (alphaState == true)
        {
            foreach (Image i in alpha)
            {
                Color c = i.color;
                c.a += alphaSpeed;
                i.color = c;
            }
        }
        else
        {
            foreach (Image i in alpha)
            {
                Color c = i.color;
                c.a -= alphaSpeed;
                i.color = c;
            }
        }

        if (moveTop == true)
        {
            for (int i = 0; i < top.Count; i++)
            {
                top[i].transform.position = Vector3.Lerp(top[i].transform.position, topPos[i], Time.deltaTime * topSpeed);
            }
        }
        else
        {
            for (int i = 0; i < top.Count; i++)
            {
                top[i].transform.position =
                    Vector3.Lerp(top[i].transform.position,
                    new Vector3(top[i].transform.position.x, newY, top[i].transform.position.z), Time.deltaTime * 1);
            }
        }
        if (moveBot == true)
        {
            for (int i = 0; i < bot.Count; i++)
            {
                bot[i].transform.position = Vector3.Lerp(bot[i].transform.position, botPos[i], Time.deltaTime * botSpeed);
            }
        }
        else
        {
            for (int i = 0; i < bot.Count; i++)
            {
                bot[i].transform.position =
                    Vector3.Lerp(bot[i].transform.position,
                    new Vector3(bot[i].transform.position.x, newYBot, bot[i].transform.position.z), Time.deltaTime * 1);
            }
        }

        if (moveLeft == true)
        {
            for (int i = 0; i < left.Count; i++)
            {
                left[i].transform.position = Vector3.Lerp(left[i].transform.position, leftPos[i], Time.deltaTime * leftSpeed);
            }
        }
        else
        {
            for (int i = 0; i < left.Count; i++)
            {
                left[i].transform.position =
                    Vector3.Lerp(left[i].transform.position,
                    new Vector3(newXLeft, left[i].transform.position.y, left[i].transform.position.z), Time.deltaTime * 1);
            }
        }
        if (moveRight == true)
        {
            for (int i = 0; i < right.Count; i++)
            {
                right[i].transform.position = Vector3.Lerp(right[i].transform.position, rightPos[i], Time.deltaTime * rightSpeed);
            }
        }
        else
        {
            for (int i = 0; i < right.Count; i++)
            {
                right[i].transform.position =
                    Vector3.Lerp(right[i].transform.position,
                    new Vector3(newXRight, right[i].transform.position.y, right[i].transform.position.z), Time.deltaTime * 1);
            }
        }

        
	}
}
