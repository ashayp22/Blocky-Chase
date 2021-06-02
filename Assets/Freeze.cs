using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour {

    private MazeCell currentCell;

    public void SetLocation(MazeCell cell)
    {
        currentCell = cell;
        transform.localPosition = cell.transform.localPosition;
    }
}
