using UnityEngine;
using System;
using System.Collections.Generic;

public class ColorSettings : MonoBehaviour {

	public List<Colors> colors = new List<Colors>();

    public List<GradientColors> grColors = new List<GradientColors>();

    public Color GetRandom()
    {
        System.Random r = new System.Random();
        return colors[r.Next(0, colors.Count)].color;
    }

    public GradientColors GetRandomGradient()
    {
        System.Random r = new System.Random();
        return grColors[r.Next(0, grColors.Count)];
    }
}

[Serializable]
public struct Colors
{
    public Color color;
}

[Serializable]
public struct GradientColors
{
    public Color colorTop;
    public Color colorMid;
    public Color colorBot;
}
