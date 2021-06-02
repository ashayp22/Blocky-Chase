using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buy : MonoBehaviour {

    public void setBuy()
    {
        Shop.Buy = true;
        Shop.Select = false;
    }
}
