using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class gotoStats : MonoBehaviour {

    public Button play;
    public Button music;
    public Button help;
    

    public Button back;
    public Button shop;
    public Button stats;

    public GameObject stat;



    public void gotostats() {

        play.gameObject.active = false;
        music.gameObject.active = false;
        help.gameObject.active = false;
        back.gameObject.active = true;
        shop.gameObject.active = false;
        stats.gameObject.active = false;
        stat.active = true;


    }
}
