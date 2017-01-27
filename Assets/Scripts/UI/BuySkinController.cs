using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuySkinController : MonoBehaviour {

    public GameObject coinShop;
    public GameObject shop;

    public GameObject enviroments;
    public GameObject NonActiveLayer;
    public GameObject skinController;
    public GameObject TextController;
    public GameObject NotBuyLayer;
    public GameObject Skin;
    public Text skinName;
    public int cost = 0;
    public int index = 0;

    private bool buyState = true;

	void Start () {
        if (buyState == true)
        {
            NotBuyLayer.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void InitObject(int index, int cost, bool buy, Texture2D texture)
    {
        this.index = index;
        Skin.GetComponent<Image>().sprite = Sprite.Create(texture,new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        this.cost = cost;
        TextController.GetComponent<CenterCostController>().SetCost(cost);
        buyState = buy;
        if (buyState == true)
        {
            NotBuyLayer.SetActive(false);
        }
        else
        {
            NotBuyLayer.SetActive(true);
        }
    }

    public void Activate()
    {
        skinController.GetComponent<SkinsController>().SetActive(index);
        NonActiveLayer.SetActive(false);
        foreach(Transform t in transform.parent){
            if (t.gameObject.GetComponent<BuySkinController>() && t.gameObject.GetComponent<BuySkinController>().index != index)
            {
                t.gameObject.GetComponent<BuySkinController>().NonActiveLayer.SetActive(true);
            }
        }

        enviroments.GetComponent<EnviromentController>().initAll();
    }

    public void Buy()
    {
        int c = PlayerPrefs.GetInt("coins");
        if (c >= cost)
        {
            PlayerPrefs.SetInt("coins", (c-cost));
            skinController.GetComponent<SkinsController>().BuySkin(index);
            buyState = true;
            if (buyState == true)
            {
                NotBuyLayer.SetActive(false);
            }
            Activate();
        }
        else
        {
            coinShop.GetComponent<CoinShopScreen>().InitPrevScreen(shop.GetComponent<ShopController>().prevScreen);
            shop.GetComponent<IScreenController>().Skip();
            coinShop.GetComponent<CoinShopScreen>().View();
        }
    }
}
