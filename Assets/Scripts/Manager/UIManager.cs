using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Utilities;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private RectTransform endPanel;
        [SerializeField] private TextMeshProUGUI sumScore;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private RectTransform playerLife;
        [SerializeField] private Image[] heart;
        [SerializeField] private RectTransform startPannel;
        [SerializeField] private Button startButton;
        

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            ScoreManager.Instance.ScoreUpdated += ScoreUpdated;
            GameManager.Instance.PlayerDie += MinusLife;
            
            restartButton.onClick.AddListener(OnRestart);
            exitButton.onClick.AddListener(ExitGame);
            startButton.onClick.AddListener(OnStart);
        }
        
        private void OnStart()
        {
            startPannel.gameObject.SetActive(false);
            GameManager.Instance.StartGame();
        }
        
        public void OnRestart()
        {
            playerLife.gameObject.SetActive(true);
            for (int i = 0; i < heart.Length; i++)
            {
                heart[i].gameObject.SetActive(true);
            }
            endPanel.gameObject.SetActive(false);
            GameManager.Instance.StartGame();
            scoreText.text = $"Score : {ScoreManager.Instance.Score}";
        }

        public void OpenEndPanel()
        {
            endPanel.gameObject.SetActive(true);
            playerLife.gameObject.SetActive(false);
            sumScore.text = $"Your Score : {ScoreManager.Instance.Score}";
        }

        private void ScoreUpdated()
        {
            scoreText.text = $"Score : {ScoreManager.Instance.Score}";
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        private void MinusLife()
        {
            if (GameManager.Instance.playerLife == 2)
            {
                heart[0].gameObject.SetActive(false);
            }
            else if (GameManager.Instance.playerLife == 1)
            {
                heart[1].gameObject.SetActive(false);
            }
            else if (GameManager.Instance.playerLife == 0)
            {
                playerLife.gameObject.SetActive(false);
            }
        }
    }
}
