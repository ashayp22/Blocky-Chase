using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Music : MonoBehaviour {

    public static bool music = false;
    public Button button;
    public Sprite on;
    public Sprite off;

    


    public void changeMusic()
    {
        if (PlayerPrefs.GetInt("music") == 0)

        {
            PlayerPrefs.SetInt("music", 1);
            button.image.sprite = off;
            button.image.GetComponent<Image>().color = new Color32(0, 181, 255, 255);
            PlayMusic.setSound();
        } else if(PlayerPrefs.GetInt("music") == 1)
        {
            PlayerPrefs.SetInt("music", 0);
            button.image.sprite = on;
            button.image.GetComponent<Image>().color = new Color32(0, 181, 255, 255);
            PlayMusic.setSound();

        }
    }
}
