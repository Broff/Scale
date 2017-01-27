using UnityEngine;
using System;
using System.Collections.Generic;

public class SkinsController : MonoBehaviour {

    public List<Skin> skins = new List<Skin>();
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    public bool CheckPurchased(int num)
    {
        //Debug.Log(PlayerPrefs.GetString("skins"));
        string[] va = (PlayerPrefs.GetString("skins").Split('!'));
        //Debug.Log(va.Length);
        string[] val = va[1].Split(',');
        int[] b = new int[val.Length];
        for (int i = 0; i < b.Length; i++)
        {
            b[i] = Convert.ToInt32(val[i]);
        }

        for (int i = 0; i < b.Length; i++ )
        {
            if(b[i] == num){
                return true;
            }
        }
        return false;
    }

    public Texture2D GetActiveSkin()
    {
        int i = GetActiveIndex();
        if (i == -1)
        {
            return null;
        }
        return skins[i].texture;
    }

    public void SetActive(int i)
    {
        if(i >=0 && i < skins.Count){
            string[] val = PlayerPrefs.GetString("skins").Split('!');
            string[] buying = val[1].Split(',');
            bool buy = false;
            for (int j = 0; j < buying.Length; j++ )
            {
                if(i == Convert.ToInt32(buying[j])){
                    buy = true;
                    break;
                }
            }

            if(buy == true){
                PlayerPrefs.SetString("skins",i+"!"+val[1]);
                ///player.GetComponent<PlayerController>().UpdateSkin();   NEED SET INITIALIZE SKIN!!!
            }
        }
    }

    public void BuySkin(int i)
    {
        if (i >= 0 && i < skins.Count)
        {
            string[] val = PlayerPrefs.GetString("skins").Split('!');
            string[] buying = val[1].Split(',');
            bool buy = true;
            for (int j = 0; j < buying.Length; j++)
            {
                if (i == Convert.ToInt32(buying[j]))
                {
                    buy = false;
                    break;
                }
            }

            if (buy == true)
            {
                PlayerPrefs.SetString("skins", val[0] + "!" + val[1]+","+i);
            }
        }
    }

    public int GetActiveIndex()
    {
        string s = PlayerPrefs.GetString("skins");
        int num = 0;
        try
        {
            num = Convert.ToInt32(s.Split('!')[0]);
            if(num < 0 || num >= skins.Count ){
                num = -1;
            }
        }
        catch(FormatException e){}
        return num;
    }

    public Skin GetActive()
    {
        int i = GetActiveIndex();
        Skin o = skins[0];
        foreach(Skin s in skins){
            if (s.number == i)
            {
                o = s;
                break;
            }
        }
        return o;
    }
}
