using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public static bool Select = false;
    public static bool Buy = false;
    public Text coins;
    public Text notAble;

    public void Start()
    {
        showBorder();
        coins.text = PlayerPrefs.GetInt("coins").ToString();
    }

    

    private void doneText()
    {
        notAble.text = "";
        CancelInvoke("doneText");
    }
    
    public void p1()
    {
        if(Select == true && PlayerPrefs.GetInt("buy1") == 1)
        {
            PlayerPrefs.SetInt("mat", 1);
            
        } else if (Select == true)
        {
            notAble.text = "YOU CANNOT SELECT THAT";
            InvokeRepeating("doneText", 2f, 2f);
        }


        if (Buy == true && PlayerPrefs.GetInt("buy1") == 0)
        {
            int val = PlayerPrefs.GetInt("val1");
            int coin = PlayerPrefs.GetInt("coins");
            if(coin >= val)
            {
                PlayerPrefs.SetInt("buy1", 1);
                PlayerPrefs.SetInt("coins", coin - val);
                coins.text = PlayerPrefs.GetInt("coins").ToString();
            } else
            {
                notAble.text = "YOU CANNOT BUY THAT";
                InvokeRepeating("doneText", 4f, 4f);

            }
        } else if(Buy == true && PlayerPrefs.GetInt("buy1") == 1)
        {
            notAble.text = "YOU ALREADY BOUGHT THAT";
            InvokeRepeating("doneText", 4f, 4f);

        }


        showBorder();
    }

    public void p2()
    {
        if (Select == true && PlayerPrefs.GetInt("buy2") == 1)
        {
            PlayerPrefs.SetInt("mat", 2);

        }
        else if(Select == true)
        {
            notAble.text = "YOU CANNOT SELECT THAT";
            InvokeRepeating("doneText", 2f, 2f);
        }


        if (Buy == true && PlayerPrefs.GetInt("buy2") == 0)
        {
            int val = PlayerPrefs.GetInt("val2");
            int coin = PlayerPrefs.GetInt("coins");
            if (coin >= val)
            {
                PlayerPrefs.SetInt("buy2", 1);
                PlayerPrefs.SetInt("coins", coin - val);
                coins.text = PlayerPrefs.GetInt("coins").ToString();
            }
            else
            {
                notAble.text = "YOU CANNOT BUY THAT";
                InvokeRepeating("doneText", 4f, 4f);

            }
        }
        else if (Buy == true && PlayerPrefs.GetInt("buy2") == 1)
        {
            notAble.text = "YOU ALREADY BOUGHT THAT";
            InvokeRepeating("doneText", 4f, 4f);

        }


        showBorder();
    }


    public void p3()
    {
        if (Select == true && PlayerPrefs.GetInt("buy3") == 1)
        {
            PlayerPrefs.SetInt("mat", 3);

        }
        else if (Select == true)
        {
            notAble.text = "YOU CANNOT SELECT THAT";
            InvokeRepeating("doneText", 2f, 2f);
        }


        if (Buy == true && PlayerPrefs.GetInt("buy3") == 0)
        {
            int val = PlayerPrefs.GetInt("val3");
            int coin = PlayerPrefs.GetInt("coins");
            if (coin >= val)
            {
                PlayerPrefs.SetInt("buy3", 1);
                PlayerPrefs.SetInt("coins", coin - val);
                coins.text = PlayerPrefs.GetInt("coins").ToString();
            }
            else
            {
                notAble.text = "YOU CANNOT BUY THAT";
                InvokeRepeating("doneText", 4f, 4f);

            }
        }
        else if (Buy == true && PlayerPrefs.GetInt("buy3") == 1)
        {
            notAble.text = "YOU ALREADY BOUGHT THAT";
            InvokeRepeating("doneText", 4f, 4f);

        }


        showBorder();
    }

    public void p4()
    {
        if (Select == true && PlayerPrefs.GetInt("buy4") == 1)
        {
            PlayerPrefs.SetInt("mat", 4);

        }
        else if (Select == true)
        {
            notAble.text = "YOU CANNOT SELECT THAT";
            InvokeRepeating("doneText", 2f, 2f);
        }


        if (Buy == true && PlayerPrefs.GetInt("buy4") == 0)
        {
            int val = PlayerPrefs.GetInt("val4");
            int coin = PlayerPrefs.GetInt("coins");
            if (coin >= val)
            {
                PlayerPrefs.SetInt("buy4", 1);
                PlayerPrefs.SetInt("coins", coin - val);
                coins.text = PlayerPrefs.GetInt("coins").ToString();
            }
            else
            {
                notAble.text = "YOU CANNOT BUY THAT";
                InvokeRepeating("doneText", 4f, 4f);

            }
        }
        else if (Buy == true && PlayerPrefs.GetInt("buy4") == 1)
        {
            notAble.text = "YOU ALREADY BOUGHT THAT";
            InvokeRepeating("doneText", 4f, 4f);

        }


        showBorder();
    }

    public void p5()
    {
        if (Select == true && PlayerPrefs.GetInt("buy5") == 1)
        {
            PlayerPrefs.SetInt("mat", 5);

        }
        else if (Select == true)
        {
            notAble.text = "YOU CANNOT SELECT THAT";
            InvokeRepeating("doneText", 2f, 2f);
        }


        if (Buy == true && PlayerPrefs.GetInt("buy5") == 0)
        {
            int val = PlayerPrefs.GetInt("val5");
            int coin = PlayerPrefs.GetInt("coins");
            if (coin >= val)
            {
                PlayerPrefs.SetInt("buy5", 1);
                PlayerPrefs.SetInt("coins", coin - val);
                coins.text = PlayerPrefs.GetInt("coins").ToString();
            }
            else
            {
                notAble.text = "YOU CANNOT BUY THAT";
                InvokeRepeating("doneText", 4f, 4f);

            }
        }
        else if (Buy == true && PlayerPrefs.GetInt("buy5") == 1)
        {
            notAble.text = "YOU ALREADY BOUGHT THAT";
            InvokeRepeating("doneText", 4f, 4f);

        }


        showBorder();

    }

    public void p6()
    {
        if (Select == true && PlayerPrefs.GetInt("buy6") == 1)
        {
            PlayerPrefs.SetInt("mat", 6);

        }
        else if (Select == true)
        {
            notAble.text = "YOU CANNOT SELECT THAT";
            InvokeRepeating("doneText", 2f, 2f);
        }


        if (Buy == true && PlayerPrefs.GetInt("buy6") == 0)
        {
            int val = PlayerPrefs.GetInt("val6");
            int coin = PlayerPrefs.GetInt("coins");
            if (coin >= val)
            {
                PlayerPrefs.SetInt("buy6", 1);
                PlayerPrefs.SetInt("coins", coin - val);
                coins.text = PlayerPrefs.GetInt("coins").ToString();
            }
            else
            {
                notAble.text = "YOU CANNOT BUY THAT";
                InvokeRepeating("doneText", 4f, 4f);

            }
        }
        else if (Buy == true && PlayerPrefs.GetInt("buy6") == 1)
        {
            notAble.text = "YOU ALREADY BOUGHT THAT";
            InvokeRepeating("doneText", 4f, 4f);

        }


        showBorder();
    }

    public void p7()
    {
        if (Select == true && PlayerPrefs.GetInt("buy7") == 1)
        {
            PlayerPrefs.SetInt("mat", 7);

        }
        else if (Select == true)
        {
            notAble.text = "YOU CANNOT SELECT THAT";
            InvokeRepeating("doneText", 2f, 2f);
        }


        if (Buy == true && PlayerPrefs.GetInt("buy7") == 0)
        {
            int val = PlayerPrefs.GetInt("val7");
            int coin = PlayerPrefs.GetInt("coins");
            if (coin >= val)
            {
                PlayerPrefs.SetInt("buy7", 1);
                PlayerPrefs.SetInt("coins", coin - val);
                coins.text = PlayerPrefs.GetInt("coins").ToString();
            }
            else
            {
                notAble.text = "YOU CANNOT BUY THAT";
                InvokeRepeating("doneText", 4f, 4f);

            }
        }
        else if (Buy == true && PlayerPrefs.GetInt("buy7") == 1)
        {
            notAble.text = "YOU ALREADY BOUGHT THAT";
            InvokeRepeating("doneText", 4f, 4f);

        }


        showBorder();
    }


    public Button f1;
    public Button f2;
    public Button f3;
    public Button f4;
    public Button f5;
    public Button f6;
    public Button f7;



    public void showBorder()
    {
        List<GameObject> list = new List<GameObject>();
        list.Add(f1.gameObject);
        list.Add(f2.gameObject);
        list.Add(f3.gameObject);
        list.Add(f4.gameObject);
        list.Add(f5.gameObject);
        list.Add(f6.gameObject);
        list.Add(f7.gameObject);

        //orange(hasn't bought) - buy is 0
        //purple(selected) - mat
        //invisible(available) - buy is 1 but not mat
        
        for (int i = 0; i < 7; i++)
        {
            string key = "buy" + (i + 1);
            //makes every border visible just in case it shouldn't be
            GameObject border1 = list[i].transform.GetChild(1).gameObject;
            border1.active = true;

            if (PlayerPrefs.GetInt(key) == 0) //orange
            {
                

                GameObject border = list[i].transform.GetChild(1).gameObject;
                border.GetComponent<Image>().color = new Color32(255, 127, 80, 255);
            }
            else if (PlayerPrefs.GetInt("mat") == i + 1) //purple
            {
               
                GameObject border = list[i].transform.GetChild(1).gameObject;
                border.GetComponent<Image>().color = new Color32(148, 0, 211, 255);

            }
            else if (PlayerPrefs.GetInt(key) == 1 && PlayerPrefs.GetInt("mat") != i + 1) //invisible
            {
                GameObject border = list[i].transform.GetChild(1).gameObject;
                border.active = false;

                
            }
        }
    }

}
