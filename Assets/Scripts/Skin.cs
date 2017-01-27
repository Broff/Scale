using UnityEngine;
using System;


[Serializable]
public struct Skin
{
    public int number;
    public Texture2D texture;
    public GameObject prefab;
    public GameObject prefabStartPlatform;
    public int cost;
    public bool open;
    public string name;
}
