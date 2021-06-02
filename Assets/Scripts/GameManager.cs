
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool gameStart = false; //game starts or not
    private bool gameOver = false;

    private int score = 0; //keeps score
    public Text countText; //displays score
    public Text spaceText; //displays game over text
    public Maze mazePrefab; //prefab for maze

    private Maze mazeInstance; //instance of the maze prefab

    public Player playerPrefab; //instance of player
    public Food foodPrefab; //instance of food
    public Agent agentPrefab; //instance of agent
    public Chaser chaserPrefab; //instance of chaser

    public Material pathMaterial; //material for the correct path

    private Player playerInstance; //instance for player
    private Food foodInstance; //food instance

    private Agent agentInstance; //agents instance
    private Chaser chaserInstance; //chaser instance

    public static int Size = 4;

    private float pathNeg1; //for path ex: -10.5 for 20*20
    private float pathNeg2; //for path ex: -9.5 for 20*20

    private float pathPos1; //for path ex: 9.5 for 20*20

    private int endCor; //end coordinate


    public Camera cam; //main camera

    public AudioSource win;
    public AudioSource lose;
    public AudioSource collectCoin;
    public AudioSource shieldSound;
    public AudioSource freezeSound;

    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;
    public Material mat5;
    public Material mat6;
    public Material mat7;

    public Coins coinsPrefab;
    private List<Coins> coinsInstances = new List<Coins>();

    public Shield shieldPrefab;
    private List<Shield> shieldInstances = new List<Shield>();


    public Freeze freezePrefab;
    private List<Freeze> freezeInstances = new List<Freeze>();

    private bool Invincible = false;


    public Text timerText;
    public Text timerText2;


    public Text plusText;

    // Use this for initialization


    void Start()
    {
        setSize();
        //adds two paths where it will start
        path.Add(new Vector2(pathNeg1, pathNeg1)); //need first to be different from second, else will backtrack 
        path.Add(new Vector2(pathNeg2, pathNeg2)); // starting position

        
        //Cursor.visible = false;
        BeginGame(); //starts game, runs for certain amount of seconds


        matList.Add(mat1);
        matList.Add(mat2);
        matList.Add(mat3);
        matList.Add(mat4);
        matList.Add(mat5);
        matList.Add(mat6);
        matList.Add(mat7);

        

        setMat(PlayerPrefs.GetInt("mat"));



    }

    private bool WaitForSpace = false;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && gameStart == false && canAdvance) //restart the game
        {
            canAdvance = false;
            spaceText.text = "";
            WaitForSpace = false;
            if (gameOver == false) //next round
            {
                PrintPath2();
                remove = true; //needed for removing first value from path list
                CancelInvoke();
                Size += 2;
                setChaseTimes();
                userPath = new List<Vector3>();
                chaseIndex = 0;
                RestartGame();

                
            }
            else if (gameOver == true) //game is over
            {
                gameOver = false;
                remove = true; //needed for removing first value from path list


                score = 0;
                Size = 4;
                speed = 1.9f;
                time = 3f;
                userPath = new List<Vector3>();
                chaseIndex = 0;
                CancelInvoke();
                RestartGame();
            }
        }

        if (gameStart == true) //if game is going can check for win
        {
            checkWin();
            
        }
        if (gameOver != true && WaitForSpace == false)
        {
            checkCoins();
            checkShield();
            checkFreeze();
            checkAquaOrange();
            checkYellow();
        }
    }

    //CHECKING FUNCTIONS

    private bool Freeze = false;

    private void checkFreeze()
    {
        for (int i = 0; i < freezeInstances.Count; i++)
        {
            if (freezeInstances[i].transform.position == playerInstance.transform.position)
            {
                if (freezeInstances[i].gameObject.active == true)
                {
                    freezeSec = 5;
                    CancelInvoke("freezeTimer");
                    Freeze = true;
                    InvokeRepeating("freezeTimer", 0f, 1f);
                    freezeInstances[i].gameObject.active = false;
                    freezeSound.Play();
                    PlayerPrefs.SetInt("freeze", PlayerPrefs.GetInt("freeze") + 1);
                }
            }
        }
    }


    private int freezeSec = 5;
    private void freezeTimer()
    {
        timerText.text = "FREEZE: " + freezeSec.ToString();
        freezeSec -= 1;
        if (freezeSec == -1)
        {
            Freeze = false;
            CancelInvoke("freezeTimer");
            timerText.text = "";
            freezeSec = 5;
        }
    }









    private void checkShield()
    {
        for (int i = 0; i < shieldInstances.Count; i++)
        {
            if (shieldInstances[i].transform.position == playerInstance.transform.position)
            {
                if (shieldInstances[i].gameObject.active == true)
                {
                    shieldSec = 5;
                    CancelInvoke("invincibleTimer");
                    Invincible = true;
                    InvokeRepeating("invincibleTimer", 0f, 1f);
                    shieldInstances[i].gameObject.active = false;
                    shieldSound.Play();
                    PlayerPrefs.SetInt("shield", PlayerPrefs.GetInt("shield") + 1);
                }
            }
        }
    }


    private int shieldSec = 5;
    private void invincibleTimer()
    {
        timerText2.text = "SHIELD: " + shieldSec.ToString();
        shieldSec -= 1;
        if(shieldSec == -1)
        {
            Invincible = false;
            CancelInvoke("invincibleTimer");
            timerText2.text = "";
            shieldSec = 5;
        }
        
    }

    
    private void checkCoins()
    {
        for(int i = 0; i < coinsInstances.Count; i++)
        {
            if(coinsInstances[i].transform.position == playerInstance.transform.position)
            {
                if(coinsInstances[i].gameObject.active == true)
                {
                    int prevCoins = PlayerPrefs.GetInt("coins");
                    PlayerPrefs.SetInt("coins", prevCoins + 10);
                    coinsInstances[i].gameObject.active = false;
                    collectCoin.Play();
                    plusText.gameObject.active = true;
                    InvokeRepeating("plusCoin", 1f, 1f);
                    
                }
            }
        }
    }


    private void plusCoin()
    {
        plusText.gameObject.active = false;
        CancelInvoke("plusCoin");

    }

    private void checkYellow()
    {
        for (int i = 0; i < yellow.Count; i++)
        {
            MazeCell cell = mazeInstance.GetCell(yellow[i]);
            if (cell.gameObject.transform.position == playerInstance.transform.position && ya == true)
            {

                //sets player
                int rand = randInt(0, path.Count - 1);
                Vector2 intCoor = path[rand];
                IntVector2 newCoor = new IntVector2(Mathf.RoundToInt(intCoor.x + pathPos1) , Mathf.RoundToInt(intCoor.y + pathPos1));
                //get old position
                Vector2 oldPos = new Vector2(playerInstance.transform.position.x, playerInstance.transform.position.z);


                playerInstance.SetLocation(mazeInstance.GetCell(newCoor));

                //userPath.Add(playerInstance.transform.position);
                //sets userPath

                
                Vector2 newPos = new Vector2(playerInstance.transform.position.x, playerInstance.transform.position.z);

                int oldInd = path.IndexOf(oldPos);
                int newInd = path.IndexOf(newPos);

              

                if (oldInd > newInd) //player went back
                {
                    


                    for (i = 0; i < oldInd - newInd + 1; i++)
                    {
                        userPath.RemoveAt(userPath.Count - 1);
                    }
                    
                } else if(newInd > oldInd) //player went forwards
                {
                    


                    for (int l = 0; l < newInd - oldInd + 1; l++)
                    {
                        Vector3 tile = new Vector3(path[oldInd + l].x, 0, path[oldInd + l].y);
                        userPath.Add(tile);
                    }
                   

                }


            }
        }
    }


    private void checkAquaOrange()
    {
        for (int b = 0; b < aquaorange.Count; b++)
        {
            MazeCell cell = mazeInstance.GetCell(aquaorange[b]);
            if (cell.gameObject.transform.position == playerInstance.transform.position && oa == true && Invincible == false)
            {
                if (remove)
                {
                    CancelInvoke();
                    gameStart = false;
                    gameOver = true;
                    path.RemoveAt(0);
                    PrintPath(); //prints correct path
                    SpaceText(2);
                    remove = false;
                    lose.Play();
                    canAdvance = true;
                    WaitForSpace = true;
                    PlayerPrefs.SetInt("gamesPlayed", PlayerPrefs.GetInt("gamesPlayed") + 1);
                    if (score > PlayerPrefs.GetInt("highscore"))
                    {
                        PlayerPrefs.SetInt("highscore", score);
                    }
                }
            }
        }
    }


    private void LateUpdate()
    {
        updatePlayerPath();
    }

    bool remove = true; //for removing first value in path
    bool canAdvance = false;


    //CHECKING WIN
    private void checkWin() //checks win
    {
        //checks player
        if (GameObject.FindGameObjectWithTag("Player").transform.position == GameObject.FindGameObjectWithTag("Target").transform.position)
        {

            
            //does only once
            if (remove) //advance round
            {
                CancelInvoke();
                PrintPath2();
                score += 1;
                path.RemoveAt(0);
                SpaceText(1);
                remove = false;
                //PrintPath();
                win.Play();
                canAdvance = true;
                gameStart = false;
                WaitForSpace = true;
            }
            


        }
        //checks chaser
        else if (GameObject.FindGameObjectWithTag("Player").transform.position == GameObject.FindGameObjectWithTag("Chaser").transform.position && Invincible == false)
        {
            
            //does only once
            if (remove)
            {
                CancelInvoke();
                gameStart = false;
                gameOver = true;
                path.RemoveAt(0);
                PrintPath(); //prints correct path
                SpaceText(2);
                remove = false;
                lose.Play();
                canAdvance = true;
                WaitForSpace = true;
                PlayerPrefs.SetInt("gamesPlayed", PlayerPrefs.GetInt("gamesPlayed") + 1);
                if (score > PlayerPrefs.GetInt("highscore"))
                {
                    PlayerPrefs.SetInt("highscore", score);
                }
            }


        }

    }


    public static List<Vector3> userPath = new List<Vector3>(); //actual path
    private int index = -5;
    
    public void updatePlayerPath()
    {
        if (Player.canSet == true)
        {
            index = -5; //random num that can't be index


            Vector3 posOfPlayer = playerInstance.gameObject.transform.position;
            userPath.Add(posOfPlayer);
            
            for (int i = 0; i < userPath.Count - 1; i++)
            {
                if (userPath[userPath.Count - 1] == userPath[i])
                {
                    index = (i); 
                }
            }
            if (index != -5)
            {
                userPath.RemoveAt(index);
                userPath.RemoveAt(index);
            }
            
            Player.canSet = false;
        }
    }

    private void setGameStart()
    {
        
        gameStart = true;
    }


    private void setSize()
    {
        pathNeg1 = (float)(-1 * ((Size / 2) + .5)); //for path ex: -10.5 for 20*20
        pathNeg2 = (float)(-1 * ((Size / 2) - .5)); //for path ex: -9.5 for 20*20

        pathPos1 = (float)(((Size / 2) - .5)); //for path ex: 9.5 for 20*20

        endCor = Size - 1;
    }


    private void createFreeze()
    {
        int i = 0;
        if (Size >= 8 && Size <= 12)
        {
            i = 1;
        }
        else if (Size >= 12 && Size <= 14)
        {
            i = 2;
        }
        else if (Size >= 14 && Size <= 18)
        {
            i = 3;
        }
        else if(Size >= 18)
        {
            i = 4;
        }


        List<IntVector2> coorList = new List<IntVector2>();
        for (int j = 0; j < i; j++)
        {
            freezeInstances.Add(Instantiate(freezePrefab) as Freeze);

            IntVector2 coor = RandomCoordinates;

            bool same = true;

            while (same)
            {
                bool once = false;
                for (int z = 0; z < coorList.Count; z++)
                {
                    if (coor.x == coorList[z].x && coor.z == coorList[z].z)
                    {
                        once = true;
                    }
                }

                for (int z = 0; z < coinCoorList.Count; z++)
                {
                    if (coor.x == coinCoorList[z].x && coor.z == coinCoorList[z].z)
                    {
                        once = true;
                    }
                }

                for (int z = 0; z < shieldCoor.Count; z++)
                {
                    if (coor.x == shieldCoor[z].x && coor.z == shieldCoor[z].z)
                    {
                        once = true;
                    }
                }



                if ((coor.x == 0 && coor.z == 0) || (coor.x == Size-1 && coor.z == Size-1))
                {
                    once = true;
                }

                if (once == true)
                {
                    coor = RandomCoordinates;
                }
                else
                {
                    same = false;
                }
            }

            coorList.Add(coor);
            freezeInstances[j].SetLocation(mazeInstance.GetCell(coor));



        }
    }



    private List<IntVector2> shieldCoor = new List<IntVector2>();
    private void createShield()
    {
        int i = 0;
        if (Size >= 8 && Size <= 12)
        {
            i = 1;
        }
        else if (Size >= 12 && Size <= 14)
        {
            i = 2;
        }
        else if (Size >= 14 && Size <= 18)
        {
            i = 3;
        } else if(Size >= 18)
        {
            i = 4;
        }


        shieldCoor = new List<IntVector2>();
        for (int j = 0; j < i; j++)
        {
            shieldInstances.Add(Instantiate(shieldPrefab) as Shield);

            IntVector2 coor = RandomCoordinates;

            bool same = true;

            while (same)
            {
                bool once = false;
                for (int z = 0; z < shieldCoor.Count; z++)
                {
                    if (coor.x == shieldCoor[z].x && coor.z == shieldCoor[z].z)
                    {
                        once = true;
                    }
                }

                for (int z = 0; z < coinCoorList.Count; z++)
                {
                    if (coor.x == coinCoorList[z].x && coor.z == coinCoorList[z].z)
                    {
                        once = true;
                    }
                }

                if ((coor.x == 0 && coor.z == 0) || (coor.x == Size-1 && coor.z == Size-1))
                {
                    once = true;
                }

                if (once == true)
                {
                    coor = RandomCoordinates;
                }
                else
                {
                    same = false;
                }
            }

            shieldCoor.Add(coor);
            shieldInstances[j].SetLocation(mazeInstance.GetCell(coor));



        }
    }

    private List<IntVector2> coinCoorList = new List<IntVector2>();

    private void createCoins()
    {
        int i = 0;
        if(Size <= 6) // 4, 6, 8, 10, 12, 14
        {
            i = 2;
        } else if(Size >= 6 && Size <= 10)
        {
            i = 4;
        } else if(Size >= 10)
        {
            i = 8;
        }


        coinCoorList = new List<IntVector2>();
        for(int j = 0; j < i; j++)
        {
            coinsInstances.Add(Instantiate(coinsPrefab) as Coins);

            IntVector2 coor = RandomCoordinates;

            bool same = true;

            while (same)
            {
                bool once = false;
                for (int z = 0; z < coinCoorList.Count; z++)
                {
                    if(coor.x == coinCoorList[z].x && coor.z == coinCoorList[z].z)
                    {
                        once = true;
                    }
                }

               
                

                if((coor.x == 0 && coor.z == 0) || (coor.x == Size-1 && coor.z == Size-1))
                {
                    once = true;
                }

                if(once == true)
                {
                    coor = RandomCoordinates;
                } else
                {
                    same = false;
                }
            }

            coinCoorList.Add(coor);
            coinsInstances[j].SetLocation(mazeInstance.GetCell(coor));



            

        }

    }


    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(Random.Range(0, Size), Random.Range(0, Size));
        }
    }

    private List<IntVector2> yellow = new List<IntVector2>();

    private void randomYellow()
    {
        int i = 0;
        
        if (Size >= 8 && Size <= 12)
        {
            i = 2;
        }
        else if (Size >= 12 && Size <= 16)
        {
            i = 4;
        }
        else if (Size >= 16 && Size <= 18)
        {
            i = 6;
        }
        else if (Size >= 18)
        {
            i = 8;
        }



        for (int j = 0; j < i; j++)
        {
            //IntVector2 coor = RandomCoordinates;
            
            int rand = randInt(1, path.Count - 2);

            IntVector2 coor = new IntVector2(Mathf.RoundToInt(path[rand].x + pathPos1), Mathf.RoundToInt(path[rand].y + pathPos1));

            

            bool same = true;

            while (same)
            {
                bool once = false;
                for (int z = 0; z < yellow.Count; z++)
                {
                    if (coor.x == yellow[z].x && coor.z == yellow[z].z)
                    {
                        once = true;
                    }
                }

                if ((coor.x == 0 && coor.z == 0) || (coor.x == Size - 1 && coor.z == Size - 1))
                {
                    once = true;
                }


                for (int z = 0; z < aquaorange.Count; z++)
                {
                    if (coor.x == aquaorange[z].x && coor.z == aquaorange[z].z)
                    {
                        once = true;
                    }
                }

                if (once == true)
                {
                    // coor = RandomCoordinates;
                    int rand2 = randInt(1, path.Count - 2);
                    coor = new IntVector2(Mathf.RoundToInt(path[rand2].x + pathPos1), Mathf.RoundToInt(path[rand2].y + pathPos1));
                }
                else
                {
                    same = false;
                }
            }

            yellow.Add(coor);
            
            MazeCell cell = mazeInstance.GetCell(coor); //gets gameobject representing cell from path 
            GameObject quad = cell.transform.GetChild(0).gameObject; //gets actual physical plane of gameobject
            Color whateverColor = new Color32(43, 237, 245, 1); //color aqua

            MeshRenderer cellRenderer = quad.GetComponent<MeshRenderer>();

            Material material = new Material(Shader.Find("Standard"));

            material.color = whateverColor;
            cellRenderer.material = material; //adds color

        }

    }



    private List<IntVector2> aquaorange = new List<IntVector2>();

    private void randomAquaOrange()
    {
        int i = 0;
        if (Size <= 6) // 4, 6, 8, 10, 12, 14
        {
            i = 2;
        }
        else if (Size >= 6 && Size <= 10)
        {
            i = 6;
        }
        else if (Size >= 10 && Size <= 14)
        {
            i = 10;
        } else if(Size >= 14 && Size <= 18)
        {
            i = 14;
        } else if(Size >= 18)
        {
            i = 20;
        }
        
        for(int j = 0; j < i; j++)
        {
            IntVector2 coor = RandomCoordinates;

            bool same = true;

            while (same)
            {
                bool once = false;
                for (int z = 0; z < aquaorange.Count; z++)
                {
                    if (coor.x == aquaorange[z].x && coor.z == aquaorange[z].z)
                    {
                        once = true;
                    }
                }

                if ((coor.x == 0 && coor.z == 0) || (coor.x == Size-1 && coor.z == Size-1))
                {
                    once = true;
                }

                if (once == true)
                {
                    coor = RandomCoordinates;
                }
                else
                {
                    same = false;
                }
            }

            aquaorange.Add(coor);

            MazeCell cell = mazeInstance.GetCell(coor); //gets gameobject representing cell from path 
            GameObject quad = cell.transform.GetChild(0).gameObject; //gets actual physical plane of gameobject
            Color whateverColor = new Color32(43, 237, 245, 1); //color aqua

            MeshRenderer cellRenderer = quad.GetComponent<MeshRenderer>();

            Material material = new Material(Shader.Find("Standard"));

            material.color = whateverColor;
            cellRenderer.material = material; //adds color

        }
    }

    public bool ya = false;

    public void changeYellowAqua()
    {
        if (ya == false)
        {
            for (int i = 0; i < yellow.Count; i++)
            {
                MazeCell cell = mazeInstance.GetCell(yellow[i]); //gets gameobject representing cell from path 
                GameObject quad = cell.transform.GetChild(0).gameObject; //gets actual physical plane of gameobject
                Color whateverColor = new Color32(255, 255, 0, 1); //color yellow

                MeshRenderer cellRenderer = quad.GetComponent<MeshRenderer>();

                Material material = new Material(Shader.Find("Standard"));

                material.color = whateverColor;
                cellRenderer.material = material; //adds color
            }
            ya = true;
        }
        else if (ya == true)
        {
            for (int i = 0; i < yellow.Count; i++)
            {
                MazeCell cell = mazeInstance.GetCell(yellow[i]); //gets gameobject representing cell from path 
                GameObject quad = cell.transform.GetChild(0).gameObject; //gets actual physical plane of gameobject
                Color whateverColor = new Color32(43, 237, 245, 1); //color aqua

                MeshRenderer cellRenderer = quad.GetComponent<MeshRenderer>();

                Material material = new Material(Shader.Find("Standard"));

                material.color = whateverColor;
                cellRenderer.material = material; //adds color
            }
            ya = false;
        }
    }





    public bool oa = false; //false - aqua(move) true - orange(no move)
    private void changeOrangeAqua()
    {
        
        if(oa == false)
        {
            for(int i = 0; i < aquaorange.Count; i++)
            {
                MazeCell cell = mazeInstance.GetCell(aquaorange[i]); //gets gameobject representing cell from path 
                GameObject quad = cell.transform.GetChild(0).gameObject; //gets actual physical plane of gameobject
                Color whateverColor = new Color32(248, 174, 69, 1); //color orange

                MeshRenderer cellRenderer = quad.GetComponent<MeshRenderer>();

                Material material = new Material(Shader.Find("Standard"));

                material.color = whateverColor;
                cellRenderer.material = material; //adds color
            }
            oa = true;
        } else if(oa == true)
        {
            for (int i = 0; i < aquaorange.Count; i++)
            {
                MazeCell cell = mazeInstance.GetCell(aquaorange[i]); //gets gameobject representing cell from path 
                GameObject quad = cell.transform.GetChild(0).gameObject; //gets actual physical plane of gameobject
                Color whateverColor = new Color32(43, 237, 245, 1); //color aqua

                MeshRenderer cellRenderer = quad.GetComponent<MeshRenderer>();

                Material material = new Material(Shader.Find("Standard"));

                material.color = whateverColor;
                cellRenderer.material = material; //adds color
            }
            oa = false;
        }
    }






    private void BeginGame()
    {
        //sets the count text
        SetCountText();
        //Camera.main.clearFlags = CameraClearFlags.Skybox;
        //sets main camera
        Camera.main.rect = new Rect(0f, 0f, 1f, 1f);

        //sets position of camera
        float yPos = 5 + (score * 2);
        cam.transform.position = new Vector3(6, yPos, -2);
        //instaniates maze
        mazeInstance = Instantiate(mazePrefab) as Maze;
        //generates maze
        mazeInstance.Generate();
        //creates other gameobjects
        playerInstance = Instantiate(playerPrefab) as Player;
        playerInstance.SetLocation(mazeInstance.GetCell(StartCoordinates));
        

        foodInstance = Instantiate(foodPrefab) as Food;
        foodInstance.SetLocation(mazeInstance.GetCell(EndCoordinates));
        agentInstance = Instantiate(agentPrefab) as Agent;
        agentInstance.SetLocation(mazeInstance.GetCell(StartCoordinates));

        chaserInstance = Instantiate(chaserPrefab) as Chaser;
        chaserInstance.SetLocation(mazeInstance.GetCell(StartCoordinates));

        createCoins();
        createShield();
        createFreeze();

        randomAquaOrange();
        

        //sets box camera in corner
        Camera.main.clearFlags = CameraClearFlags.Depth;
        Camera.main.rect = new Rect(-5f, 0f, 0.5f, 0.5f);
        
        //game starts
        //gameStart = true;
        //sets agent to insisible
        agentInstance.gameObject.active = false;

        solveMaze();

        randomYellow();

        //InvokeRepeating("chasePlayer", )
        //game start is set .5 seconds later, every 100 seconds(can be any seconds)
        InvokeRepeating("chasePlayer", time + 4f, speed);
        InvokeRepeating("setGameStart", time + 4f, 100f); //this is here because there was an issue with gameStart.
        //it would be set to true before the maze was done being built, thus making the player 
        //and food objects be in the same position and make next level occur.
        //solution was to set gamestart to true 3 seconds later
        InvokeRepeating("displayStart", 0f, 1f);
        InvokeRepeating("gameTimer", 0f, 1f);

        InvokeRepeating("changeOrangeAqua", 4f, 3f);
        InvokeRepeating("changeYellowAqua", 4f, 3f);

    }

    private void RestartGame()
    {

        //resets variables
        path = new List<Vector2>();
        corners = new List<Vector2>();
        cornerDirections = new List<List<MazeDirection>>();
        chosenDirections = new List<MazeDirection>();

        aquaorange = new List<IntVector2>();
        yellow = new List<IntVector2>();

        Invincible = false;
        Freeze = false;

        oa = false;

        canGo = false;
        timeOf = 0;
        plusText.gameObject.active = false;
        timerText.text = "";
        timerText2.text = "";

        setSize();
        //adds path start
        path.Add(new Vector2(pathNeg1, pathNeg1)); //need first to be different from second, else will backtrack 
        path.Add(new Vector2(pathNeg2, pathNeg2)); // starting position

        //stops all coroutines
        //StopAllCoroutines();
        //destroys instances
        Destroy(mazeInstance.gameObject);
        if (playerInstance != null)
        {
            Destroy(playerInstance.gameObject);
        }
        if (foodInstance != null)
        {
            Destroy(foodInstance.gameObject);
        }
        if (agentInstance != null)
        {
            Destroy(agentInstance.gameObject);
        }
        if (chaserInstance != null)
        {
            Destroy(chaserInstance.gameObject);
        }



        for (int i = 0; i < coinsInstances.Count; i++)
        {
           Destroy(coinsInstances[i].gameObject);
            
        }

        coinsInstances = new List<Coins>();


        for (int i = 0; i < shieldInstances.Count; i++)
        {
            Destroy(shieldInstances[i].gameObject);

        }

        shieldInstances = new List<Shield>();

        for (int i = 0; i < freezeInstances.Count; i++)
        {
            Destroy(freezeInstances[i].gameObject);

        }

        freezeInstances = new List<Freeze>();




        BeginGame();
        setMat(PlayerPrefs.GetInt("mat"));
    }

    public static bool canGo = false;
    private int ct = 3;
    public Text ctText;


    

    private void displayStart()
    {
       
            ctText.gameObject.active = true;
            if (ct == 0)
            {
                ctText.text = "GO";
            }
            else
            {
                ctText.text = ct.ToString();
            }
            ct -= 1;
            if (ct == -1)
            {
                ct = 3;
                canGo = true;
                CancelInvoke("displayStart");
            }
        
    }

    private int timeOf = 0;
    private void gameTimer()
    {
        timeOf += 1;
        if(timeOf == 5)
        {
            ctText.gameObject.active = false;
        }
    }

    void SetCountText() //displays score
    {
        countText.text = "Score: " + score.ToString();
    }

    private void SpaceText(int i)
    {
        if (i == 1) //won, next level
        {
            spaceText.text = "Press Space to Continue";
        }
        else if (i == 2) //lost, restart
        {
            spaceText.text = "Press Space to Play Again";
        }
    }

    private float speed = 1.9f;
    private float time = 3f;

    private int chaseIndex = 0;

    private void chasePlayer()
    {
        //Debug.Log("chasing");
        if (userPath.Count != 0 && Player.canSet == false && Freeze == false)
        {
            chaserInstance.gameObject.transform.position = userPath[chaseIndex];
            chaseIndex += 1;
        }
    }

    private void setChaseTimes()
    {
        speed -= .2f; //starts at 1.9
        time -= .4f; //starts at 3
        if(speed <= .4)
        {
            speed = 0.4f;

        }

        if(time <= .5f)
        {
            time = .5f;
        }
    }





    private List<MazeDirection> Shuffle(List<MazeDirection> arr) //shuffles list
    { //fisher yates shuffle
        for (var i = arr.Count - 1; i > 0; i--)
        {
            var r = Random.Range(0, i);
            var tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
        return arr;
    }








    private void PrintPath() //prints path
    {
        for (int i = 0; i < path.Count; i++)
        {



            MazeCell cell = mazeInstance.GetCell(convertVector3(path[i])); //gets gameobject representing cell from path 
            GameObject quad = cell.transform.GetChild(0).gameObject; //gets actual physical plane of gameobject
            Color whateverColor = new Color(256, 0, 0, 1); //color red

            MeshRenderer cellRenderer = quad.GetComponent<MeshRenderer>();

            Material material = new Material(Shader.Find("Transparent/Diffuse"));

            material.color = whateverColor;
            cellRenderer.material = material; //adds color
        }
    }


    private void PrintPath2() //prints path
    {
        
        for (int z = 0; z < userPath.Count; z++)
        {
            IntVector2 cCell = new IntVector2(Mathf.RoundToInt(userPath[z].x + pathPos1), Mathf.RoundToInt(userPath[z].z + pathPos1));
            
            MazeCell cell = mazeInstance.GetCell(cCell); //gets gameobject representing cell from path 
            GameObject quad = cell.transform.GetChild(0).gameObject; //gets actual physical plane of gameobject
            Color whateverColor = new Color(0, 256, 0, 1); //color green

            MeshRenderer cellRenderer = quad.GetComponent<MeshRenderer>();

            Material material = new Material(Shader.Find("Transparent/Diffuse"));

            material.color = whateverColor;
            cellRenderer.material = material; //adds color
        }
    }
    








    private List<Vector2> path = new List<Vector2>(); //actual path
    private List<Vector2> corners = new List<Vector2>(); //spaces with multiple directions
    private List<List<MazeDirection>> cornerDirections = new List<List<MazeDirection>>(); //directions to go in that space
    private List<MazeDirection> chosenDirections = new List<MazeDirection>(); //direction currently going

    public void solveMaze()
    {

        while (path[path.Count - 1] != new Vector2(pathPos1, pathPos1)) //as long as the agent isn't at the destination spot
        {
            Vector3 agentPos = agentInstance.transform.position; //vector 3 of agent position
            List<MazeDirection> direction = new List<MazeDirection>(); //all directions 
            direction.Add(MazeDirection.North);
            direction.Add(MazeDirection.East);
            direction.Add(MazeDirection.South);
            direction.Add(MazeDirection.West);

            List<MazeDirection> straight = new List<MazeDirection>(); //directions possible

            int numOfPaths = 0;
            for (int i = 0; i < 4; i++) //checks if the agent can go in each direction
            {
                double x = agentPos.x + pathPos1;
                float x1 = (float)x;
                int x2 = Mathf.RoundToInt(x1);

                double y = agentPos.z + pathPos1;
                float y1 = (float)y;
                int y2 = Mathf.RoundToInt(y1);
                IntVector2 agentVect1 = new IntVector2(x2, y2);
                MazeCell current1 = mazeInstance.GetCell(agentVect1);
                MazeCellEdge edge = current1.GetEdge(direction[i]);
                if (edge is MazePassage)
                {
                    numOfPaths += 1;
                    straight.Add(direction[i]);
                }
            }


            if (numOfPaths > 2) //has 2 or more ways to go
            {
                //Debug.Log("corner");
                Corners(straight);

            }
            else if (path[path.Count - 1] != path[path.Count - 2]) //can move straight
            {
                //Debug.Log("move straight");
                moveStraight();
            }
            else //is stuck and has to backtrack
            {

                //Debug.Log("backtrack");
                BackTrack();
            }

        }
    }


    private void Corners(List<MazeDirection> straight) //corner algorithm
    {
        Vector3 agentPos = agentInstance.transform.position; //position of agent

        corners.Add(convertVector(agentPos)); //adds the position to the corner list


        List<MazeDirection> straight2 = new List<MazeDirection>(); //new list for possible qays to go

        for (int i = 0; i < straight.Count; i++) //this basically checks if the direction was added, then whether the new position of the agent would be the same as the previous to the previous move
        {
            Vector3 agentPos2 = agentPos;

            if (MazeDirection.North == straight[i])
            {
                agentPos2.z += 1;
            }
            else if (MazeDirection.East == straight[i])
            {
                agentPos2.x += 1;
            }
            else if (MazeDirection.West == straight[i])
            {
                agentPos2.x -= 1;
            }
            else if (MazeDirection.South == straight[i])
            {
                agentPos2.z -= 1;
            }

            if (convertVector(agentPos2) != path[path.Count - 2]) //checks if agent wouldn't move backward
            {
                straight2.Add(straight[i]);
            }

        }


        cornerDirections.Add(straight2); //all possible directions for agent at that corner
        chosenDirections.Add(straight2[randInt(0, straight.Count - 1)]); //adds a random direction

        //applies that chosen direction
        if (MazeDirection.North == chosenDirections[chosenDirections.Count - 1])
        {
            agentPos.z += 1;
        }
        else if (MazeDirection.East == chosenDirections[chosenDirections.Count - 1])
        {
            agentPos.x += 1;
        }
        else if (MazeDirection.West == chosenDirections[chosenDirections.Count - 1])
        {
            agentPos.x -= 1;
        }
        else if (MazeDirection.South == chosenDirections[chosenDirections.Count - 1])
        {
            agentPos.z -= 1;
        }

        path.Add(convertVector(agentPos));
        agentInstance.transform.position = agentPos;
    }

    private int randInt(int min, int max) //min and max included
    {
        int random = Random.Range(min, max);
        return random;
    }

    private void moveStraight() //for moving straight
    {
        Vector3 agentPos = agentInstance.transform.position; //gets position

        List<MazeDirection> direction = new List<MazeDirection>(); //all directions
        direction.Add(MazeDirection.North);
        direction.Add(MazeDirection.East);
        direction.Add(MazeDirection.South);
        direction.Add(MazeDirection.West);


        for (int i = 0; i < 4; i++) //checks which direction works, making sure it won't move the agent backwards
        {
            double x = agentPos.x + pathPos1;
            float x1 = (float)x;
            int x2 = Mathf.RoundToInt(x1);

            double y = agentPos.z + pathPos1;
            float y1 = (float)y;
            int y2 = Mathf.RoundToInt(y1);
            IntVector2 agentVect1 = new IntVector2(x2, y2);
            MazeCell current1 = mazeInstance.GetCell(agentVect1);
            MazeCellEdge edge = current1.GetEdge(direction[i]);

            if (edge is MazePassage)
            {
                if (MazeDirection.North == direction[i])
                {
                    agentPos.z += 1;
                }
                else if (MazeDirection.East == direction[i])
                {
                    agentPos.x += 1;
                }
                else if (MazeDirection.West == direction[i])
                {
                    agentPos.x -= 1;
                }
                else if (MazeDirection.South == direction[i])
                {
                    agentPos.z -= 1;
                }

                if (convertVector(agentPos) != path[path.Count - 2]) //won't go back in path
                {
                    //Debug.Log("good way");

                    break;
                }
                else
                {
                    agentPos = agentInstance.transform.position;
                }

            }
        }
        //adds new position
        path.Add(convertVector(agentPos));
        agentInstance.transform.position = agentPos;
    }

    private void BackTrack() //backtracking when stuck
    {

        while (path[path.Count - 1] != corners[corners.Count - 1]) //will continue to remove the path until it gets to previous corner
        {
            path.RemoveAt(path.Count - 1);
            agentInstance.transform.position = convertVector2(path[path.Count - 1]);
        }
        //removes the current chosen direction
        int indexOfDirection = FindIndex(cornerDirections[cornerDirections.Count - 1], chosenDirections[chosenDirections.Count - 1]);
        cornerDirections[cornerDirections.Count - 1].RemoveAt(indexOfDirection);

        if (cornerDirections[cornerDirections.Count - 1].Count == 0) //if there are no more directions left
        {
            while (cornerDirections[cornerDirections.Count - 1].Count == 0) //until it gets to a corner with a direction available
            {
                //Debug.Log("go back to previous corner");
                //removes corner
                corners.RemoveAt(corners.Count - 1);
                cornerDirections.RemoveAt(cornerDirections.Count - 1);
                chosenDirections.RemoveAt(chosenDirections.Count - 1);

                while (path[path.Count - 1] != corners[corners.Count - 1]) //moves back on path
                {
                    path.RemoveAt(path.Count - 1);
                    agentInstance.transform.position = convertVector2(path[path.Count - 1]);
                }


                int indexOfDirection2 = FindIndex(cornerDirections[cornerDirections.Count - 1], chosenDirections[chosenDirections.Count - 1]);
                cornerDirections[cornerDirections.Count - 1].RemoveAt(indexOfDirection2); //remove previous direction from choice

                agentInstance.transform.position = convertVector2(corners[corners.Count - 1]); //updates agent position
            }

            path.Add(convertVector(agentInstance.transform.position)); //adds new path


        }

        int newIndex = randInt(0, cornerDirections[cornerDirections.Count - 1].Count);
        List<MazeDirection> listOfCurrentDirections = cornerDirections[cornerDirections.Count - 1];

        chosenDirections[chosenDirections.Count - 1] = listOfCurrentDirections[newIndex]; //get new direction


        Vector3 agentPos = agentInstance.transform.position;
        //new passage
        if (MazeDirection.North == chosenDirections[chosenDirections.Count - 1])
        {
            agentPos.z += 1;
        }
        else if (MazeDirection.East == chosenDirections[chosenDirections.Count - 1])
        {
            agentPos.x += 1;
        }
        else if (MazeDirection.West == chosenDirections[chosenDirections.Count - 1])
        {
            agentPos.x -= 1;
        }
        else if (MazeDirection.South == chosenDirections[chosenDirections.Count - 1])
        {
            agentPos.z -= 1;
        }

        path.Add(convertVector(agentPos));
        agentInstance.transform.position = agentPos;

    }


    private int FindIndex(List<MazeDirection> list, MazeDirection direction) //find index within list
    {
        int index = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == direction)
            {
                index = i;
                break;
            }
        }
        return index;
    }

    private Vector2 convertVector(Vector3 vector3) //converts vector3 to vector2
    {
        Vector2 vector2 = new Vector2(0, 0);
        vector2.x = vector3.x;
        vector2.y = vector3.z;
        return vector2;
    }

    private Vector3 convertVector2(Vector2 vector2) //converts vector2 to vector3
    {
        Vector3 vector3 = new Vector3(0, 0, 0);
        vector3.x = vector2.x;
        vector3.z = vector2.y;
        return vector3;
    }

    private IntVector2 convertVector3(Vector2 vector2) //converts vector2 to intvector2
    {
        double x = vector2.x + pathPos1;
        float x1 = (float)x;
        int x2 = Mathf.RoundToInt(x1);

        double y = vector2.y + pathPos1;
        float y1 = (float)y;
        int y2 = Mathf.RoundToInt(y1);
        IntVector2 intvector = new IntVector2(x2, y2);
        return intvector;
    }

    public IntVector2 StartCoordinates
    {
        get
        {
            return new IntVector2(0, 0);
        }
    }


    public IntVector2 EndCoordinates
    {
        get
        {
            return new IntVector2(endCor, endCor);
        }
    }

    public List<Material> matList = new List<Material>();

    public void setMat(int i)
    {

        MeshRenderer playerRenderer = playerInstance.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();

        Material material = matList[i-1];


        playerRenderer.material = material; //adds color
    }


    

}









