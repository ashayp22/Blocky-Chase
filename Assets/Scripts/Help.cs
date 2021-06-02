using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Help : MonoBehaviour {
    public Button play;
    public Button music;
    public Button help;
    public Text helptext;
    public Text helptext2;
    public Button back;
    public Button shop;
    public Button stats;

    public void GoToHelp()
    {
        play.gameObject.active = false;
        music.gameObject.active = false;
        help.gameObject.active = false;
        shop.gameObject.active = false;
        stats.gameObject.active = false;
        helptext.gameObject.active = true;

        helptext2.gameObject.active = true;

        back.gameObject.active = true;

    }

}
