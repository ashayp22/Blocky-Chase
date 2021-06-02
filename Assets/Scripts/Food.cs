using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    private MazeCell currentCell;

    public void SetLocation(MazeCell cell)
    {
        currentCell = cell;
        transform.localPosition = cell.transform.localPosition;
    }

}
