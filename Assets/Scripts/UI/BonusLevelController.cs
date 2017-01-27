using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;

public class BonusLevelController : MonoBehaviour {

    private int scoreForOpen;
    public float moveSpeed;
    public float waitTime;
    public GameObject line;
    public GameObject moveSprite;
    public GameObject settings;
    public GameObject bonusLevelButton;

    public List<GameObject> hideObjects = new List<GameObject>();
    private Vector2 oldPos;
    private Vector2 newPos;
    private bool moveState = false;

    private int score = 0;
    private bool inited = false;
	void Start () {        
	}
		
	void Update () {
	    if(moveState == true){
            moveSprite.transform.localPosition = Vector2.MoveTowards(moveSprite.transform.localPosition, newPos, Time.deltaTime * moveSpeed);
            if (moveSprite.transform.localPosition.x == newPos.x)
            {
                StopMove();
            }
        }
	}

    public void InitPosition()
    {
        DeactivateGift();
        scoreForOpen = settings.GetComponent<Settings>().gamePointCountForGift;
        score = settings.GetComponent<Settings>().GamePoint;
        float width = line.GetComponent<RectTransform>().sizeDelta.x;
        float spriteW = moveSprite.GetComponent<RectTransform>().sizeDelta.x;

        float dlt = (float)score / (float)scoreForOpen;
        if (dlt > 1)
        {
            dlt = 1;
        }
        moveSprite.transform.localPosition = new Vector2(width * dlt - width / 2.0f - spriteW / 4.0f, moveSprite.transform.localPosition.y);
    }

    public void InitPlusScore(int s)
    {
        if (inited == false)
        {
            inited = true;
            scoreForOpen = settings.GetComponent<Settings>().gamePointCountForGift;
            score = settings.GetComponent<Settings>().GamePoint;

            settings.GetComponent<Settings>().GamePoint = s + score;
            int actual = settings.GetComponent<Settings>().GamePoint;

            float width = line.GetComponent<RectTransform>().sizeDelta.x;
            float spriteW = moveSprite.GetComponent<RectTransform>().sizeDelta.x;

            float dlt = (float)actual / (float)scoreForOpen;
            if (dlt > 1)
            {
                dlt = 1;
            }

            newPos = new Vector2(width * dlt - width / 2.0f - spriteW / 4.0f, moveSprite.transform.localPosition.y);
            if (scoreForOpen <= actual)
            {
                foreach (GameObject o in hideObjects)
                {
                    o.SetActive(false);
                }
            }
            if (score != actual)
            {
                StartCoroutine(MoveBonusLevel());
            }
        }
    }

    private IEnumerator MoveBonusLevel()
    {
        yield return new WaitForSeconds(waitTime);
        Move();
    }

    public void Move()
    {
        moveState = true;
    }

    public void StopMove()
    {
        moveState = false;        
        if(settings.GetComponent<Settings>().GamePoint >= scoreForOpen){
            ActivateGift();
        }
    }

    void ActivateGift()
    {
        moveSprite.SetActive(false);
        bonusLevelButton.GetComponent<Button>().enabled = true;
        bonusLevelButton.GetComponent<Animation>().enabled = true;
    }

    void DeactivateGift()
    {
        moveSprite.SetActive(true);
        bonusLevelButton.GetComponent<Button>().enabled = false;
        bonusLevelButton.GetComponent<Animation>().enabled = false;
        bonusLevelButton.transform.localRotation = Quaternion.EulerRotation(new Vector3(0,0,0));
        foreach (GameObject o in hideObjects)
        {
            o.SetActive(true);
        }
    }
}
