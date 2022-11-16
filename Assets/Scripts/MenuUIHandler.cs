using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;

    public void Start()
    {
        if (GameController.Instance)
        {
            highScoreText.text = $"HighScore\n{GameController.Instance.MaxScore}";
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
