using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goHome : MonoBehaviour {

	public void Home()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
