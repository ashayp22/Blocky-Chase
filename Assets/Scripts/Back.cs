using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Back : MonoBehaviour {
    public Button play;
    public Button music;
    public Button help;
    public Text helptext;
    public Text helptext2;

    public Button back;
    public Button shop;
    public Button stats;
    public GameObject shops;

    public GameObject stat; 

    public void GoBack()
    {
        play.gameObject.active = true;
        music.gameObject.active = true;
        help.gameObject.active = true;
        shop.gameObject.active = true;
        shops.active = false;
        stats.gameObject.active = true;
        helptext.gameObject.active = false;
        helptext2.gameObject.active = false;
        stat.active = false;
        back.gameObject.active = false;

    }
}
