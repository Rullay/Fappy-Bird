using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] private GameObject character;
    [SerializeField] public Sprite[] skins;

    [Header("Column and Spawner")]
    [SerializeField] private GameObject column;
    [SerializeField] private List<GameObject> Сolumns;
    [SerializeField] GameObject StartingPoint;
    [SerializeField] private float distanceBetweenColumn;
    private int hightColomn;
    private int random;
    public float calibrator;
    private int lastColumn;

    [Header("Stars and Fon")]
    [SerializeField] private GameObject moveFon;
    [SerializeField] private List<GameObject> Stars;
    [SerializeField] private GameObject star;

    [Header("Canvas")]
    [SerializeField] private Text counterPoints;
    [SerializeField] private Text counterStars;
    [SerializeField] public int numberOfStars;
    private int Points;
    private int PointsStars;
    [SerializeField] private Text bestScore;
    private int best_Score;






    void Start()
    {
        LoadGame();
        bestScore.text = "" + best_Score;
        Points = 0;
        hightColomn = 0;
        star.GetComponent<StarMove>().speed = column.GetComponent<MoveСolumn>().const_speed;
        calibrator = column.GetComponent<MoveСolumn>().columnSpacing;
        if (calibrator != 0)
        {
            calibrator = calibrator * 0.5f;
        }
        character.tag = "Death";

    }


    void Update()
    {
        СolumnsSpawner();
        StopGame();
        Сounter();
        StarsDestroy();
        counterStars.text = "" + numberOfStars;
    }

    void CreationOfTheFirstColumn()
    {
        Сolumns.Add(Instantiate(column, new Vector2(StartingPoint.transform.position.x + 4.5f, Random.Range(-4.4f + calibrator, 4.4f - calibrator)), Quaternion.identity));
        Debug.Log(StartingPoint.transform.position.x);
    }

    void СolumnsSpawner()
    {
        if (Сolumns.Count != 0)
        {

            for (int i = 0; i < Сolumns.Count; i++)
            {
                if (Сolumns[i].GetComponent<Transform>().position.x > Сolumns[hightColomn].GetComponent<Transform>().position.x)
                {
                    hightColomn = i;
                }

            }


            if (StartingPoint.GetComponent<Transform>().position.x + distanceBetweenColumn >= Сolumns[hightColomn].GetComponent<Transform>().position.x)
            {
                SpawnColumns();
                ColumnsDestroy();
            }
        }
    }

    void SpawnColumns()
    {

        Сolumns.Add(Instantiate(column, new Vector2(StartingPoint.transform.position.x + distanceBetweenColumn * 2, Random.Range(-4.4f + calibrator, 4.4f - calibrator)), Quaternion.identity));
        if (Random.Range(1, 100) >= 40)
        {
            for (int i = 0; i < Сolumns.Count; i++)
            {
                if (i + 1 == Сolumns.Count)
                {
                    lastColumn = i;
                }
            }
            Stars.Add(Instantiate(star, new Vector2(Сolumns[lastColumn].transform.position.x, Random.Range(Сolumns[lastColumn].transform.position.y + calibrator - 0.15f, Сolumns[lastColumn].transform.position.y - calibrator + 0.15f)), Quaternion.identity));
        }

    }

    void ColumnsDestroy()
    {
        for (int i = 0; i < Сolumns.Count; i++)
        {
            if (Сolumns[i].GetComponent<Transform>().position.x + 3f <= StartingPoint.GetComponent<Transform>().position.x)
            {
                Destroy(Сolumns[i]);
                Сolumns.Remove(Сolumns[i]);
            }
        }
    }
    void StarsDestroy()
    {
        for (int i = 0; i < Stars.Count; i++)
        {
            if (Stars[i].tag == "Сollected")
            {
                Destroy(Stars[i]);
                Stars.Remove(Stars[i]);
            }
        }
    }

    void StopMoveColumn()
    {
        for (int i = 0; i < Сolumns.Count; i++)
        {
            Сolumns[i].GetComponent<MoveСolumn>().speed = 0;
        }
    }

    void StartMoveColumn()
    {
        for (int i = 0; i < Сolumns.Count; i++)
        {
            Сolumns[i].GetComponent<MoveСolumn>().speed = Сolumns[i].GetComponent<MoveСolumn>().const_speed;
        }
    }

    void StopMOveStars()
    {
        for (int i = 0; i < Stars.Count; i++)
        {
            Stars[i].GetComponent<StarMove>().speed = 0;
        }
    }
    void StartMoveStars()
    {
        for (int i = 0; i < Stars.Count; i++)
        {
            Stars[i].GetComponent<StarMove>().speed = Сolumns[i].GetComponent<MoveСolumn>().const_speed;
        }
    }

    void StopGame()
    {

        if (character.tag == "Death")
        {

            SaveGame();
            StopMoveColumn();
            StopMOveStars();
            moveFon.GetComponent<MoveFon>().StopMove();

        }
    }

    void Сounter()
    {
        for (int i = 0; i < Сolumns.Count; i++)
        {
            if (Сolumns[i].GetComponent<Transform>().position.x + 0.3f <= character.GetComponent<Transform>().position.x)
            {
                if (Сolumns[i].GetComponent<MoveСolumn>().point == true)
                {
                    Сolumns[i].GetComponent<MoveСolumn>().point = false;
                    Points++;
                    counterPoints.text = "\n  " + Points;
                }
            }

        }
        if (best_Score < Points)
        {
            best_Score = Points;
            bestScore.text = "" + best_Score;
        }


    }

    public void CounterStars()
    {
        numberOfStars++;
    }

    void SaveGame()
    {
        PlayerPrefs.SetInt("SavedInteger", numberOfStars);
        PlayerPrefs.SetInt("BestScore", best_Score);
    }
    void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedInteger"))
        {
            numberOfStars = PlayerPrefs.GetInt("SavedInteger");
            best_Score = PlayerPrefs.GetInt("BestScore");
        }
    }

    public void StartGame()
    {
        for (int i = 0; i < Сolumns.Count; i++)
        {
            Destroy(Сolumns[i]);
        }
        Сolumns.Clear();
        for (int i = 0; i < Stars.Count; i++)
        {
            Destroy(Stars[i]);

        }
        Stars.Clear();

        character.GetComponent<Move>().StartPosition();
        moveFon.transform.position = new Vector3(-2.5f, -2.42f, -15);
        Points = 0;

        CreationOfTheFirstColumn();
        hightColomn = 0;
        character.tag = "Player";

        StartMoveColumn();
        StartMoveStars();
        moveFon.GetComponent<MoveFon>().StartMove();

    }

    public void Buy()
    {
        numberOfStars -= 20;
    }




}


