using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class Stats : MonoBehaviour {


    public Text hs;
    public Text f;
    public Text s;
    public Text gP;
	// Use this for initialization
	void Start () {
        hs.text = "Highscore - " + PlayerPrefs.GetInt("highscore").ToString(); ;
        f.text = "Freezes Used - " + PlayerPrefs.GetInt("freeze").ToString();
        s.text = "Shields Used - " + PlayerPrefs.GetInt("shield").ToString();
        gP.text = "Games Played - " + PlayerPrefs.GetInt("gamesPlayed").ToString();

    }


}
