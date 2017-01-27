using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {

    public int gamePointCountForGift;
    private bool gameState = false;
    private int score = 0;

    private int highscore;
    private int gameSessions;
    private int games;
    private static bool firstLaunch = true;

    public void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        highscore = PlayerPrefs.GetInt("highscore");
        games = PlayerPrefs.GetInt("games")+1;
        PlayerPrefs.SetInt("games", games);
        if(firstLaunch == true){
            firstLaunch = false;
            gameSessions = PlayerPrefs.GetInt("sessions") + 1;
            PlayerPrefs.SetInt("sessions", gameSessions);
            
            if(PlayerPrefs.GetString("skins") == ""){
                PlayerPrefs.SetString("skins", "0!0");
                Debug.Log("skins initialized");
            }
        }
    }

    public bool GameState
    {
        get 
        {
            return gameState;
        }

        set
        {
            gameState = value;
        }

    }

    public int GamePoint
    {
        get { return PlayerPrefs.GetInt("gamePoint"); }
        set
        {
            if (value >= gamePointCountForGift)
            {
                Gift = true;
            }
            PlayerPrefs.SetInt("gamePoint", value);
        }
    }

    public bool Gift{
        get { return PlayerPrefs.GetInt("gift")>0?true:false; }
        set { PlayerPrefs.SetInt("gift", value ? 1 : 0); }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            if(score > highscore){
                highscore = score;
                PlayerPrefs.SetInt("highscore", highscore);
            }
        }
    }

    public int Highscore
    {
        get
        {
            return PlayerPrefs.GetInt("highscore");
        }
    }

    public int Sessions
    {
        get
        {
            return gameSessions;
        }
    }

    public int Games
    {
        get
        {
            return games;
        }
    }
	
}
