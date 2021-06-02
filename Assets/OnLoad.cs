using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("coins") == false) //true resets everytime editor is opened and closed, false is it is never reset
        {
            PlayerPrefs.SetInt("coins", 0);

            PlayerPrefs.SetInt("val1", 0);
            PlayerPrefs.SetInt("val2", 200);
            PlayerPrefs.SetInt("val3", 400);
            PlayerPrefs.SetInt("val4", 750);
            PlayerPrefs.SetInt("val5", 1000);
            PlayerPrefs.SetInt("val6", 1250);
            PlayerPrefs.SetInt("val7", 2000);

            //0 is false, 1 is true
            PlayerPrefs.SetInt("buy1", 1);
            PlayerPrefs.SetInt("buy2", 0);
            PlayerPrefs.SetInt("buy3", 0);
            PlayerPrefs.SetInt("buy4", 0);
            PlayerPrefs.SetInt("buy5", 0);
            PlayerPrefs.SetInt("buy6", 0);
            PlayerPrefs.SetInt("buy7", 0);

            PlayerPrefs.SetInt("mat", 1);


            PlayerPrefs.SetInt("highscore", 0);
            PlayerPrefs.SetInt("shield", 0);
            PlayerPrefs.SetInt("freeze", 0);
            PlayerPrefs.SetInt("gamesPlayed", 0);


        }
    }
	
}
