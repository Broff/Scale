using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScreenController : MonoBehaviour, IScreenController {
        
    public Text score;
    public Text coinsScore;
    public GameObject settings;
    public GameObject gameController;

	void Start () {
        coinsScore.text = PlayerPrefs.GetInt("coins") + "";
        gameController.GetComponent<GameController>().StartGame();
	}
	
	// Update is called once per frame
	void Update () {
	    score.text = settings.GetComponent<Settings>().Score+"";
        coinsScore.text = PlayerPrefs.GetInt("coins")+"";
	}

    public void View()
    {
        gameObject.SetActive(true);
        GetComponent<ShowEffect>().StartAnimation();
        GetComponent<ShowEffect>().StartAnimation();
    }

    private IEnumerator WaitAndHide()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    public void Skip()
    {
        gameObject.SetActive(false);
    }    
}
