using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private MazeCell currentCell;
    private float cameraX;
    private float cameraY;
    private Vector2 pos;
    public Camera cam;
    public AudioSource move;

    public void SetLocation(MazeCell cell)
    {
        currentCell = cell;
        transform.localPosition = cell.transform.localPosition;
    }

    public static bool canSet = false;

    private void Move(MazeDirection direction)
    {
        MazeCellEdge edge = currentCell.GetEdge(direction);
        if (edge is MazePassage)
        {
            SetLocation(edge.otherCell);
            canSet = true;
            move.Play();
        }
    }

    private MazeDirection currentDirection;

    private void Look(MazeDirection direction)
    {
        transform.localRotation = direction.ToRotation();
        currentDirection = direction;
    }

    private void Update()
    {

        //camera
        //Direction();

        //movement
        if (GameManager.canGo == true)
        {
            Movement();
            userClick();
        }

       
    }


    private void userClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pos = Input.mousePosition;
            int y = Screen.height;
            int x = Screen.width;

            if(pos.x > x/4 && pos.x < (3*x)/4)
            {
                if (pos.y > y/2) {
                    Debug.Log("U");
                    Move(currentDirection);

                }
                else if(pos.y < y/2)
                {
                    Debug.Log("D");
                    Move(currentDirection.GetOpposite());



                }
            } else if(pos.x > (3*x)/4)
            {
                Debug.Log("R");
                Move(currentDirection.GetNextClockwise());


            }
            else if(pos.x < (x/4))
            {
                Debug.Log("L");
                Move(currentDirection.GetNextCounterclockwise());


            }

        }
    }
    


    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) //up
        {
            Move(currentDirection);

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) //right
        {
            Move(currentDirection.GetNextClockwise());
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) //down
        {
            Move(currentDirection.GetOpposite());

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))//left
        {
            Move(currentDirection.GetNextCounterclockwise());
        }
         
    }

    private void Direction()
    {
        //camera
        pos = Input.mousePosition;

        cameraX = (Screen.height / 2) - pos.y;
        
        cameraY = pos.x - (Screen.width / 2);

        if (-45 < cameraY && cameraY < 45) //north
        {
            currentDirection = MazeDirection.North;
        }
        else if (-135 < cameraY && cameraY < -45) //west
        {
            currentDirection = MazeDirection.West;
        }
        else if (45 < cameraY && cameraY < 135) //east
        {
            currentDirection = MazeDirection.East;
        }
        else if ((135 < cameraY && cameraY <= 180) || (-180 <= cameraY && cameraY < -135)) //south
        {
            currentDirection = MazeDirection.South;
        }


        //constraints
        if (cameraX >= 90)
        {
            cameraX = 90;
        }
        else if (cameraX <= -90)
        {
            cameraX = -90;
        }

        if(cameraY >= 180)
        {
            cameraY = 180;
        } else if(cameraY <= -180)
        {
            cameraY = -180;
        } 


        cam.transform.rotation = Quaternion.Euler(cameraX, cameraY, 0);
    }



}