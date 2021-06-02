using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class PlayMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //AudioSource audio = GetComponent<AudioSource>();
        Scene currentScene = SceneManager.GetActiveScene();

        int buildIndex = currentScene.buildIndex;
        if (buildIndex == 1)
        {
            PlayerPrefs.SetInt("music", 0);
        }
        setSound();
    }

    public static void setSound()
    {
        if (PlayerPrefs.GetInt("music") == 1)
        {
            Debug.Log("yes");
            AudioListener.volume = 0;
            //audio.Play();
        } else if(PlayerPrefs.GetInt("music") == 0)
        {
            AudioListener.volume = 0.5f;
        }
    }
}
