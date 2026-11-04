using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    private TMP_InputField NameInput;
    public Text BestScore;
    public Text ScoreText;
    public GameObject GameOverText;

    static string ActualPlayerName;
    static int ActualBestScore;

    [SerializeField] private bool m_Started = false;
    private int m_Points;

    [SerializeField] private bool m_GameOver = false;

    public bool m_Menu;

    public static MainManager Instance;

    private void Start()
    {
        ActualPlayerName = "";
        ActualBestScore = 0;
    }

    void Awake()
    {
        BestScore = GameObject.Find("BestScoreText").GetComponent<Text>();
        

        GameOverText.SetActive(false);
        BestScore.text = "Best Score : " + ActualPlayerName + " : " + ActualBestScore;

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (m_Menu == false)
        {
            const float step = 0.6f;
            int perLine = Mathf.FloorToInt(4.0f / step);

            int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
            for (int i = 0; i < LineCount; ++i)
            {
                for (int x = 0; x < perLine; ++x)
                {
                    Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                    var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                    brick.PointValue = pointCountArray[i];
                    brick.onDestroyed.AddListener(AddPoint);
                }
            }
        }
    }


    private void Update()
    {
        if (!m_Started && !m_Menu && !m_GameOver)
        {
            BestScore.text = "Best Score : " + ActualPlayerName + " : " + ActualBestScore;
            if (Input.GetKeyDown(KeyCode.Space) && Ball != null)
            {
                m_Started = true;
                float randomDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (!m_Menu && m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_GameOver = false;
                LoadGame();
                StartGame();
            }

            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                m_Menu = true;
                m_GameOver = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_Started = false;
        m_GameOver = true;
        Ball = null;
        GameOverText.SetActive(true);
        LoadGame();
    }

    public void StartGame()
    {
        Debug.Log("Jugador actual en StartGame(): " + ActualPlayerName);
        m_Points = 0;
        m_Menu = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    [Serializable]
    public class PlayerData
    {
        public string name;
        public int score;
    }

    public void SaveGame()
    {
        PlayerData actualPlayer = new PlayerData();
        actualPlayer.name = ActualPlayerName;
        if (actualPlayer.name == "Guest" || actualPlayer.name == "")
        {
            actualPlayer.score = 0;
        }
        else
        {
            actualPlayer.score = ActualBestScore;
        }

        string json = JsonUtility.ToJson(actualPlayer);
        File.WriteAllText(Application.persistentDataPath + "/" + actualPlayer.name + ".json", json);
    }

    public void LoadGame()
    {
        if (ActualPlayerName != "" && ActualPlayerName != "Guest")
        {
            string path = Application.persistentDataPath + "/" + ActualPlayerName + ".json";

            if (File.Exists(path))
            {
                Debug.Log("Data: " + path + " Loaded");
                string jsonDocument = File.ReadAllText(path);
                PlayerData data = JsonUtility.FromJson<PlayerData>(jsonDocument);

                if (data.score < m_Points)
                {
                    SaveGame();
                    BestScore.text = "Best Score : " + data.name + " : " + m_Points;
                    ActualBestScore = m_Points;
                }

                else
                {
                    BestScore.text = "Best Score : " + data.name + " : " + data.score;
                    ActualBestScore = data.score;
                }

            }

            else
            {
                ActualBestScore = 0;
                BestScore.text = "Best Score : " + ActualPlayerName + " : " + ActualBestScore;
                SaveGame();
            }
        }
        else if (ActualPlayerName == "Guest" || ActualPlayerName == "")
        {
            BestScore.text = "Best Score : Guest : 0";
            ActualBestScore = 0;
        }
    }

    public void LoadData()
    {
        if (ActualPlayerName != "" && ActualPlayerName != "Guest")
        {
            string path = Application.persistentDataPath + "/" + ActualPlayerName + ".json";

            if (File.Exists(path))
            {
                Debug.Log("Data: " + path + " Loaded");
                string jsonDocument = File.ReadAllText(path);
                PlayerData data = JsonUtility.FromJson<PlayerData>(jsonDocument);

                    BestScore.text = "Best Score : " + data.name + " : " + data.score;
                    ActualBestScore = data.score;
            }

            else
            {
                BestScore.text = "Best Score : " + ActualPlayerName + " : " + 0;
            }
        }
        else if (ActualPlayerName == "Guest" || ActualPlayerName == "")
        {
            BestScore.text = "Best Score : Guest : 0";
            ActualBestScore = 0;
        }

    }
    public void GetUserName()
    {
        NameInput = GameObject.Find("NameInput").GetComponent<TMP_InputField>();
        if(NameInput.text != "")
        {
            ActualPlayerName = NameInput.text;
        }

        else
        {
            ActualPlayerName = "Guest";
        }
    }
}
