using UnityEngine;
using System.Collections;

public class GradientController : MonoBehaviour {

    public GameObject colors;
    public float speed;

    bool finalColors = true;

    Color cB;
    Color cT;
    Color cM;

    Material m;

	void Start () {
        GradientColors col = colors.GetComponent<ColorSettings>().GetRandomGradient();

        m = GetComponent<Renderer>().material;
        m.SetColor("_ColorBot", col.colorBot);
        m.SetColor("_ColorTop", col.colorTop);
        m.SetColor("_ColorMid", col.colorMid);

        col = colors.GetComponent<ColorSettings>().GetRandomGradient();
        cB = col.colorBot;
        cM = col.colorMid;
        cT = col.colorTop;
        
	}
	
	// Update is called once per frame
	void Update () {
        Color cTact = m.GetColor("_ColorTop");
        Color cMact = m.GetColor("_ColorMid");
        Color cBact = m.GetColor("_ColorBot");

        //Color cBnew = Color.LerpUnclamped(cBact, cB, Time.deltaTime * speed);
        //Color cTnew = Color.LerpUnclamped(cTact, cT, Time.deltaTime * speed);
        //Color cMnew = Color.LerpUnclamped(cMact, cM, Time.deltaTime * speed);

        Color cBnew = CalculateColor(cBact, cB);
        Color cTnew = CalculateColor(cTact, cT);
        Color cMnew = CalculateColor(cMact, cM);

        m.SetColor("_ColorBot", cBnew);
        m.SetColor("_ColorTop", cTnew);
        m.SetColor("_ColorMid", cMnew);

        if(cBnew == cB && cTnew == cT && cMnew == cM){
            GradientColors col = colors.GetComponent<ColorSettings>().GetRandomGradient();

            cB = col.colorBot;
            cM = col.colorMid;
            cT = col.colorTop;
        }
	}

    Color CalculateColor(Color act, Color newC)
    {
        Vector3 c1 = new Vector3(act.r, act.g, act.b);
        Vector3 c2 = new Vector3(newC.r, newC.g, newC.b);
        Vector3 res = Vector3.MoveTowards(c1, c2, Time.deltaTime * speed);
        Color c = new Color(res.x, res.y, res.z);
        return c;
    }
}
