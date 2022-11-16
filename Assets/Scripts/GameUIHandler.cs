using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject loseUI;

    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI HighText;

    private void Start()
    {
        if (!GameController.Instance) return;

        GameController.Instance.OnScoreChanged += Instance_OnScoreChanged;
        GameController.Instance.OnHighScoreChanged += Instance_OnHighScoreChanged;

        ScoreText.text = $"Score\n{GameController.Instance.Score}";
    }

    private void Instance_OnHighScoreChanged(bool newScore)
    {
        gameUI.SetActive(false);
        loseUI.SetActive(true);

        string highScoreText;
        
        if (newScore)
            highScoreText = $"NEW\nHighScore\n{GameController.Instance.MaxScore}";
        else
            highScoreText = $"HighScore\n{GameController.Instance.MaxScore}";

        HighText.text = highScoreText;
    }

    private void Instance_OnScoreChanged(int score)
    {
        ScoreText.text = $"Score\n{score}";
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
