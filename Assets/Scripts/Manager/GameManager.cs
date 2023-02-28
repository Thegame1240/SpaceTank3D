using System;
using System.Resources;
using Car;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;


namespace Manager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private PlayerCar playerCar;
        [SerializeField] private EnemyCar enemyCar;
        [SerializeField] private UIManager uiManger;
        [SerializeField] private int playerHp = 100;
        [SerializeField] private int enemyHp = 100;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private Transform enemySpawnPoint;
        
        public int playerLife = 3;

        public event Action PlayerDie;

        public void StartGame()
        {
            ScoreManager.Instance.Init();
            SpawnPlayer();
            SpawnEnemy();
            playerLife = 3;
        }

        //private void PlayerInit()
        //{
        //    var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCar>();
        //    player.Init(playerHp);
        //    player.OnExplode += OnPlayerCarExplode;
        //}

        private void SpawnPlayer()
        {
            var player = Instantiate(playerCar, playerSpawnPoint.position, Quaternion.identity);
            player.Init(playerHp);
            player.OnExplode += OnPlayerCarExplode;
        }
        
        private void OnPlayerCarExplode()
        {
            playerLife -= 1;

            if (playerLife > 0)
            {
                PlayerDie?.Invoke();
                SpawnPlayer();
            }
            else if (playerLife <= 0)
            {
                EndGame();
            }
        }

        //private void EnemyInit()
        //{
        //    var enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyCar>();
        //    enemy.Init(enemyHp);
        //    enemy.OnExplode += OnEnemyCarExplode;
        //}

        private void OnEnemyCarExplode()
        {
            ScoreManager.Instance.AddScore(1);
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            var enemy = Instantiate(enemyCar, enemySpawnPoint.position, Quaternion.identity);
            enemy.Init(enemyHp);
            enemy.OnExplode += OnEnemyCarExplode;
        }

        public void EndGame()
        {
            uiManger.OpenEndPanel();
            DestroyRemaining();
        }

        private void DestroyRemaining()
        {
            var remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in remainingEnemies)
            {
                Destroy(enemy);
            }

            var remainingPlayers = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in remainingPlayers)
            {
                Destroy(player);
            }
        }
    }
}
