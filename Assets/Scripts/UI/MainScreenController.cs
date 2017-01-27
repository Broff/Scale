using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class MainScreenController : MonoBehaviour, IScreenController{

    public GameObject gameScreen;
    public GameObject shopScreen;
    public Text coinsScore;

    void Start()
    {
        coinsScore.text = PlayerPrefs.GetInt("coins") + "";
        //GetComponent<ShowEffect>().StartAnimation();
    }
    
    
    public void View()
    {
        gameObject.SetActive(true);
        GetComponent<ShowEffect>().StartAnimation();
    }

    public void Skip()
    {
        GetComponent<ShowEffect>().StartAnimationHide();
        StartCoroutine(WaitAndHide());
    }

    private IEnumerator WaitAndHide()
    {
        yield return new WaitForSeconds(0.8f);
        gameObject.SetActive(false);
    }

    public void Click()
    {
        Skip();
        gameScreen.GetComponent<IScreenController>().View();
    }

    public void OpenShop()
    {
        shopScreen.GetComponent<ShopController>().prevScreen = gameObject;
        shopScreen.GetComponent<ShopController>().View();
        gameObject.SetActive(false);
    }
}
