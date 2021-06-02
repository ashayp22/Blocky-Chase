using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour {

	public void setSelect()
    {
        Shop.Buy = false;
        Shop.Select = true;
        Debug.Log("Setted select");
    }
}
