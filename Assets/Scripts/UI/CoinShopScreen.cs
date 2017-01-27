using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinShopScreen : MonoBehaviour, IScreenController
{
    public GameObject prevScreen;
    public GameObject shopScreen;
    public GameObject bg;
    public Text coinsScore;

    void Start()
    {
        coinsScore.text = PlayerPrefs.GetInt("coins") + "";
    }

    public void View()
    {
        gameObject.SetActive(true);
        bg.SetActive(true);
    }

    public void Skip()
    {
        gameObject.SetActive(false);
        bg.SetActive(false);
    }

    public void InitPrevScreen(GameObject o)
    {
        prevScreen = o;
    }

    public void Shop()
    {
        shopScreen.GetComponent<ShopController>().InitPrevScreen(prevScreen);        
        Skip();
        shopScreen.GetComponent<ShopController>().View();
    }

    public void Back()
    {
        prevScreen.GetComponent<IScreenController>().View();
        Skip();
    }

    public void Plus(int p)
    {
        int c = PlayerPrefs.GetInt("coins");
        PlayerPrefs.SetInt("coins", (c + p));
        coinsScore.text = PlayerPrefs.GetInt("coins") + "";
    }

    public void NoAds()
    {

    }
}