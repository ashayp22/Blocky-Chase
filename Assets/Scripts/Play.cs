using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour {

    public void NewScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
