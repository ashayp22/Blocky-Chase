using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour {
    public Maze mazePrefab; //prefab for maze

    private Maze mazeInstance; //instance of the maze prefab

    public Camera cam;
    // Use this for initialization
    void Start () {
        SetUpMaze();
	}

    private void SetUpMaze()
    {
        Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
        //instaniates maze
        mazeInstance = Instantiate(mazePrefab) as Maze;
        //generates maze
        StartCoroutine(mazeInstance.Generate2());
    }
    public static bool Bool = false;
    private void checkMaze()
    {
        
        if (Bool == true)
        {
         
            Destroy(mazeInstance.gameObject);
            SetUpMaze();
            Bool = false;
        }
        
    }

    public Color lerpedColor = new Color(0, 223, 255, 1);
    
    
    private void Update()
    {
        checkMaze();
        //lerpedColor = Color.Lerp(Color.blue, Color.green, Mathf.PingPong(Time.time, 1));
        //Camera.main.backgroundColor = lerpedColor;
    }

   

    
}
