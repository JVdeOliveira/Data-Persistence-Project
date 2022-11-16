using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private int m_score;
    private int m_highScore;

    private bool m_lose;

    public int Score => m_score;
    public int MaxScore => m_highScore;

    public event Action<int> OnScoreChanged;
    public event Action<bool> OnHighScoreChanged;

    public event Action OnLose;

    private string saveFilePath;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        saveFilePath = $"{Application.persistentDataPath}/SaveFile.json";
        LoadHighScore();
    }

    private void Update()
    {
        if (m_lose && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void IncreaseScore(int amountScore)
    {
        m_score += amountScore;

        OnScoreChanged?.Invoke(m_score);
    }

    public void CheckNewRecord()
    {
        if (m_score > m_highScore)
        {
            m_highScore = m_score;
            SaveHighScore();
        }

        OnHighScoreChanged?.Invoke(m_score > m_highScore);
    }

    public void Lose()
    {
        m_lose = true;
        CheckNewRecord();

        OnLose?.Invoke();
    }

    #region Save end Load
    [Serializable]
    private class SaveData
    {
        public int HighScore;
    }

    public void SaveHighScore()
    {
        var data = new SaveData()
        {
            HighScore = m_highScore
        };

        var json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json);
    }

    public void LoadHighScore()
    {
        if (!File.Exists(saveFilePath)) return;

        var json = File.ReadAllText(saveFilePath);
        var data = JsonUtility.FromJson<SaveData>(json);

        m_highScore = data.HighScore;
    }
    #endregion
}
