using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

    public GameObject mainScene;
    public GameObject gameScene;
    public GameObject endScene;
    public GameObject[] off;

	void Start () {
        mainScene.gameObject.SetActive(true);
        gameScene.gameObject.SetActive(true);
        endScene.gameObject.SetActive(true);
        mainScene.gameObject.SetActive(false);
        gameScene.gameObject.SetActive(false);
        endScene.gameObject.SetActive(false);

        foreach (GameObject o in off)
        {
            o.SetActive(false);
        }

        mainScene.GetComponent<IScreenController>().View();
	}
}
