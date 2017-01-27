using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndScreenController : MonoBehaviour, IScreenController {

    public GameObject settings;
    public GameObject giftScene;
    public Text score;
    public Text highScore;
    public Text coinsScore;

    public GameObject videoButton;
    public GameObject newHighscore;
    public GameObject progressBar;
    public bool showNewHighscore = false;

	void Start () {
        coinsScore.text = PlayerPrefs.GetInt("coins") + "";
	}
    void Update()
    {
        coinsScore.text = PlayerPrefs.GetInt("coins") + "";
    }

    public void View()
    {
        gameObject.SetActive(true);
        progressBar.GetComponent<BonusLevelController>().InitPosition();
        progressBar.GetComponent<BonusLevelController>().InitPlusScore(settings.GetComponent<Settings>().Score);
        score.text = settings.GetComponent<Settings>().Score + "";
        highScore.text = "BEST "+settings.GetComponent<Settings>().Highscore;
        if (showNewHighscore == true)
        {
            newHighscore.GetComponent<NewHighscore>().Play();
        }
        videoButton.GetComponent<VideoButtonController>().CheckState();
    }

    public void Skip()
    {
        gameObject.SetActive(false);
    }
    
    private IEnumerator WaitAndHide()
    {
        yield return new WaitForSeconds(0.5f);   
        Skip();
    }

    public void OpenGiftScreen()
    {
        settings.GetComponent<Settings>().Gift = false;
        settings.GetComponent<Settings>().GamePoint = 0;
        Skip();
        int c = UnityEngine.Random.Range(0,5);
        giftScene.GetComponent<GiftScreenController>().gemsCount = getGiftCount(c);
        giftScene.GetComponent<GiftScreenController>().prevScene = gameObject;
        giftScene.GetComponent<GiftScreenController>().View();
    }

    public void WatchVideo(int c)
    {
        Skip();
        giftScene.GetComponent<GiftScreenController>().prevScene = gameObject;
        giftScene.GetComponent<GiftScreenController>().gemsCount = c;
        giftScene.GetComponent<GiftScreenController>().View();
    }


    int getGiftCount(int i)
    {
        switch (i)
        {
            case 0:
                return 10;
            case 1:
                return 20;
            case 2:
                return 30;
            case 3:
                return 40;
            case 4:
                return 50;
            default :
                return 35;
        }
    }
    public void PlayAgain()
    {
        Application.LoadLevel("main");
    }

}
