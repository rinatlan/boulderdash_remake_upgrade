using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _shared;
    
    public static float xPos = -0.501f;
    public static float yPos = -0.5f;
    public GameObject wallPrefab;
    public GameObject sandPrefab;
    public GameObject playerPrefab;
    public GameObject diamondPrefab;
    public GameObject rockPrefab;
    public GameObject secondWallPrefab;
    public GameObject doorPrefab;
    public GameObject live1Prefab;

    private int _points = 0;
    private int _numDiamondsEaten = 0;
    private GameObject playerObj;
    private GameObject _canvas;
    private int _numLives = 3;
    
    //public int amountOfDiamond = 0;
    

    private Vector3[] _lifePointsPositions = new Vector3[] {new Vector3(-226.419998f,120.389999f,0)
        , new Vector3(-210.600006f,123.910004f,0),new Vector3(-193.300003f,123.910004f,0) };

    private int wall = 1;
    private int sand = 2;
    private int player = 3;
    private int diamond = 4;
    private int rock = 5;
    private int secondWall = 6;

    private int[][] board = 
    {
        new int[40] {
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
        new int[40] {
            1,2,2,2,2,2,2,2,0,2,5,5,2,2,2,2,2,2,5,2,2,4,2,2,2,2,2,0,2,2,5,2,2,2,2,2,5,2,5,1 },
        new int[40] {
            1,2,2,3,2,2,2,2,5,2,5,5,2,2,2,2,2,2,2,2,2,5,2,2,2,5,2,2,2,2,2,2,2,2,4,2,2,2,2,1 },
        new int[40] {
            1,4,2,5,2,2,2,2,5,2,2,5,2,2,2,2,0,2,2,2,5,2,2,4,2,5,2,2,5,2,5,2,2,2,2,2,2,2,2,1},
        new int[40] {
            1,4,2,5,2,2,2,2,5,2,2,5,2,2,5,2,2,2,2,2,2,2,2,5,2,2,2,2,2,5,2,2,2,2,2,2,2,2,5,1},
        new int[40] {
            1,5,2,2,5,2,2,2,5,0,2,2,2,0,2,2,2,2,5,2,2,4,2,5,2,2,2,2,2,2,2,2,2,2,2,2,2,2,5,1},
        new int[40] {
            1,5,2,2,5,2,2,2,2,5,2,2,2,5,2,2,5,2,2,2,2,2,5,2,2,2,2,2,2,2,2,5,2,0,2,2,2,2,2,1},
        new int[40] {
            1,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,5,2,5,4,2,2,0,2,1},
        new int[40] {
            1,2,5,2,2,2,5,2,2,4,2,4,2,2,5,2,0,5,2,2,2,2,2,2,2,2,2,2,2,2,2,2,0,5,5,2,2,4,2,1},
        new int[40] {
            1,2,2,2,2,2,4,2,2,2,2,2,5,0,2,2,2,2,2,2,2,2,2,2,2,4,0,2,2,2,2,2,5,2,5,2,2,5,2,1},
        new int[40] {
            1,2,2,2,5,2,2,5,2,5,2,2,2,5,2,0,0,2,2,2,2,2,2,2,2,2,5,2,2,2,4,2,2,2,2,5,2,2,2,1},
        new int[40] {
            1,5,2,2,2,5,2,2,2,2,2,2,2,5,2,5,0,2,2,2,2,2,2,2,2,2,5,5,2,2,5,2,2,2,2,2,2,2,2,1},
        new int[40] {
            1,2,2,2,2,2,5,2,5,5,2,2,2,2,2,2,5,2,2,5,2,2,2,2,2,2,2,2,5,2,2,2,2,2,5,2,0,0,2,1},
        new int[40] {
            1,2,0,2,2,2,7,2,5,5,2,2,2,2,2,2,2,2,2,5,4,2,2,5,2,2,2,2,2,2,2,2,2,5,2,2,2,2,2,1},
        new int[40] {
            1,5,4,2,2,2,2,2,2,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,1},
        new int[40] {
            1,5,4,2,2,2,2,2,2,0,2,2,5,2,2,4,4,4,2,5,2,2,5,2,2,2,2,0,2,2,2,2,2,2,2,2,5,0,0,1},
        new int[40] {
            1,2,4,2,2,2,2,5,2,2,2,5,2,2,2,2,5,2,2,5,2,4,2,2,2,2,2,2,2,2,2,2,2,2,2,2,5,5,1,1},
        new int[40] {
            1,2,5,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,5,2,2,2,2,2,2,2,2,5,5,0,2,2,2,2,2,2,2,1},
        new int[40] {
            1,2,5,2,4,2,2,2,2,0,2,2,2,2,5,2,2,2,2,2,5,2,0,2,2,2,2,2,2,5,2,5,4,2,2,4,2,2,2,1},
        new int[40] {
            1,2,2,2,5,2,2,4,2,5,2,2,5,2,4,5,2,2,2,2,2,2,2,2,2,2,2,2,2,2,5,5,5,2,2,5,2,2,2,1},
        new int[40] {
            1,2,2,2,5,2,2,2,2,2,5,2,2,2,2,2,2,2,2,2,2,2,4,4,5,2,2,2,2,2,2,2,5,2,2,2,2,5,2,1},
        new int[40] {
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
    };
    
    private void Awake()
    {
        if ( _shared == null )
        {
            _shared = this;
            LoadLP();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            _shared.InitBoard();
            xPos = -0.501f;
            yPos = -0.5f;
            _shared.LoadLP();
            Destroy(gameObject);
            
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        InitBoard();
    }

    private void InitBoard()
    {
        if (_shared._numLives == 0)
        {
            SceneManager.LoadScene("Game Over", LoadSceneMode.Single);
        }
        else
        {
            Instantiate(doorPrefab);
            Vector2 originalPos = new Vector2(-9f, 3.2f);
            int counterX = 0;
            int counterY = 0;
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    Vector2 newPos = new Vector2(counterX, counterY);
                    switch (board[i][j])
                    {
                        case 1:
                            Instantiate(wallPrefab, originalPos + newPos, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(sandPrefab, originalPos + newPos, Quaternion.identity);
                            break;
                        case 3:
                            playerObj = Instantiate(playerPrefab, originalPos + newPos, Quaternion.identity);
                            break;
                        case 4:
                            Instantiate(diamondPrefab, originalPos + newPos, Quaternion.identity);
                            break;
                        case 5:
                            Instantiate(rockPrefab, originalPos + newPos, Quaternion.identity);
                            break;
                        case 6:
                            Instantiate(secondWallPrefab, originalPos + newPos, Quaternion.identity);
                            break;
                    }

                    counterX += 1;
                }

                counterX = 0;
                counterY -= 1;
            }
        }
    }

    public void LoadLP()
    {
        var canvas = GameObject.FindGameObjectWithTag("canvas");
        foreach (var live in GameObject.FindGameObjectsWithTag("live"))
        {
            Destroy(live);
        }
        for (var i = 0; i < _numLives; i++)
        {
            var curLive = Instantiate(live1Prefab, canvas.transform);
            var livePos = curLive.transform.position;
            livePos.x += (_numLives + 1 - i) * 20 - 35;
            curLive.transform.position = livePos;
        }
    }
    
    public static int GetPoints()
    {
        return _shared._points;
    }
    public static void AddPoints()
    {
        _shared._points += 10;
    }

    public static void MinusLive()
    {
        _shared._numLives -= 1;
        SceneManager.LoadScene("Game");
        
    }

    public static int GetDiamonds()
    {
        return _shared._numDiamondsEaten;
    }
    
    public static void AddDiamond()
    {
        _shared._numDiamondsEaten += 1;
    }
}
