using System;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController Instance;

    private int m_score;
    private int m_highScore;

    public int Score => m_score;
    public int MaxScore => m_highScore;

    public Action<int> OnScoreChanged;
    public Action<int> OnHighScoreChanged;

    private string saveFilePath;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        saveFilePath = $"{Application.persistentDataPath}/SaveFile.json";
        LoadHighScore();
    }

    public void IncreaseScore()
    {
        m_score++;

        OnScoreChanged?.Invoke(m_score);
    }

    public void CheckNewRecord()
    {
        if (m_score <= m_highScore) return;

        m_highScore = m_score;

        OnHighScoreChanged?.Invoke(m_highScore);
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
