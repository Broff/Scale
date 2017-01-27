using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CenterCostController : MonoBehaviour {

    public Text txt;

	void Start () {
        SetCenter();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void SetCost(int i)
    {
        txt.text = i + "  X  ";
        SetCenter();
    }

    private void SetCenter()
    {
        float imgWidth = transform.GetComponent<RectTransform>().sizeDelta.x;
        float txtWidth = txt.transform.GetComponent<RectTransform>().sizeDelta.x;
        txtWidth = txt.preferredWidth;//txt.fontSize * txt.text.Length;
        float newCnter = ((imgWidth + txtWidth) / 2.0f - txtWidth / 4.0f);
        transform.localPosition = new Vector2(newCnter, transform.localPosition.y);
    }
}
