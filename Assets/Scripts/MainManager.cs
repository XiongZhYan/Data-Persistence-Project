using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public Text PlayerNameText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    private static int BestScore;
    private string PlayerName;
    private static string BestOne;
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        LoadNameAndScore();
        PlayerName = DataToSave.instance.Name;
        PlayerNameText.text = PlayerName;
        BestScoreText.text = "Best Score " + BestOne + ":" + BestScore;
       
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
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

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
                
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        CheckBestScore();
        DisplayScore();

        ScoreText.text = $"Score : {m_Points}";
        
    }
    void DisplayScore()
    {
        BestScoreText.text = "Best Score " + BestOne + ":" + BestScore;
    }


    void CheckBestScore()
    {
       if(BestScore<m_Points)
        {
            BestScore= m_Points;
            BestOne=PlayerName;
            SaveNameAndScore();
        }
    }

    public void GameOver()
    {
        CheckBestScore();
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    [System.Serializable]
    class SaveData
    {
        public string playerNameToSave;
        public int bestScoreToSave;
    }
    private void SaveNameAndScore()
    {
        SaveData data = new SaveData();
        data.playerNameToSave = BestOne;
        data.bestScoreToSave = BestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    private void LoadNameAndScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestOne = data.playerNameToSave;
            BestScore = data.bestScoreToSave;
        }
    }
}
