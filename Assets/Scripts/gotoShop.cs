using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class gotoShop : MonoBehaviour {

    public Button play;
    public Button music;
    public Button help;
    public Text helptext;
    public Button back; 
    public Button shop;
    public Button stats;
    public GameObject shops;
    
    public void GoBack()
    {
        play.gameObject.active = false;
        music.gameObject.active = false;
        help.gameObject.active = false;
        shop.gameObject.active = false;
        stats.gameObject.active = false;
        helptext.gameObject.active = false;
        back.gameObject.active = true;
        shops.gameObject.active = true;
    }
}
