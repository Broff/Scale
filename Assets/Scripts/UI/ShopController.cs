using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopController : MonoBehaviour, IScreenController
{
    public GameObject coinShop;
    public GameObject prevScreen;

    public GameObject bg;
    public Text coinsScore;    
    public GameObject skinsController;
    public GameObject skinPref;
    public GameObject content;
    public float hDlt;
    public float wDlt;


    void Start()
    {
        coinsScore.text = PlayerPrefs.GetInt("coins") + "";
        InitSkins();
    }

    void Update()
    {
        coinsScore.text = PlayerPrefs.GetInt("coins") + "";
    }

    private void InitSkins()
    {
        float w = skinPref.GetComponent<BuySkinController>().NonActiveLayer.transform.GetComponent<RectTransform>().sizeDelta.x;
        float h = skinPref.GetComponent<BuySkinController>().NonActiveLayer.transform.GetComponent<RectTransform>().sizeDelta.y;

        float cW = transform.GetComponent<RectTransform>().sizeDelta.x;
        Debug.Log(cW);
        int skinCount = skinsController.GetComponent<SkinsController>().skins.Count;
        int height = skinCount;
        if (skinCount % 2 != 0)
        {
           // height++;
        }
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, height * (h+hDlt));//;
        List<Skin> skins = skinsController.GetComponent<SkinsController>().skins;

        int i = 1;
        int j = 0;

        //float x1 = cW / 2 + w / 2 + wDlt;
        //float x2 = cW / 2 - w / 2 - wDlt;

        float firstY = -(h /2.0f);
        foreach(Skin s in skins){
            
            GameObject o = Instantiate(skinPref);
            o.transform.SetParent(content.transform, false);

            if (s.number == skinsController.GetComponent<SkinsController>().GetActiveIndex())
            {
                o.GetComponent<BuySkinController>().NonActiveLayer.SetActive(false);
            }
            //if (i % 2 == 0)
            //{
            //    o.transform.localPosition = new Vector2(x1, firstY);
            //    j++;
            //    i = 0;
            //    firstY -= hDlt + h;
            //}
            //else
            //{
            //    o.transform.localPosition = new Vector2(x2, firstY);
            //}
            o.transform.localPosition = new Vector2(o.transform.localPosition.x, firstY);
            firstY -= hDlt + h;
            bool buyState = skinsController.GetComponent<SkinsController>().CheckPurchased(s.number);
            o.GetComponent<BuySkinController>().InitObject(s.number,s.cost, buyState,s.texture);
            o.GetComponent<BuySkinController>().skinName.text = s.name;
            o.GetComponent<BuySkinController>().shop = gameObject;
            o.GetComponent<BuySkinController>().coinShop = coinShop;
            o.SetActive(true);
            i++;
        }
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

    public void CoinShop()
    {
        coinShop.GetComponent<CoinShopScreen>().InitPrevScreen(prevScreen);        
        Skip();
        coinShop.GetComponent<CoinShopScreen>().View();
    }

    public void Back()
    {
        prevScreen.GetComponent<IScreenController>().View();
        Skip();
    }
}