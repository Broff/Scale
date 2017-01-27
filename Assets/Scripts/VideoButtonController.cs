using UnityEngine;
using System.Collections;

public class VideoButtonController : MonoBehaviour {

    public GameObject button;
    public GameObject settings;
    public int countGames;

    public void CheckState()
    {
        if(settings.GetComponent<Settings>().Games % countGames == 0){
            button.SetActive(true);
        }
    }
}
