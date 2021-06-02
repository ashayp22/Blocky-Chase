using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMovement : MonoBehaviour {

    public GameObject player;
	
	// Update is called once per frame
	void LateUpdate () {
        //setCamera();
        
    }


    private void setCamera()
    {
        Vector3 camPos = new Vector3(0, 5, 0);
        camPos.x = player.transform.localPosition.x;
        camPos.z = (float)(player.transform.localPosition.z - 2.5);
        transform.position = camPos;
        Debug.Log(transform.position);
    }
}
