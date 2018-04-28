using System.Collections.Generic;
using Assets.Scripts.Game;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        public Enemy Enemy;
        public Transform[] Spawners;
        private float _spawnFrequency = 2f;

        // Something like this, could be a calculated thing instead of an explicit map
        private readonly Dictionary<int, float> _enemyCountToFrequencyMap = new Dictionary<int, float>()
        {
            {10, 1.5f},
            {60, 1f},
            {100, 0.5f},
        };

        private float _elapsed;
        private int _spawnedEnemies;
        private int _currentNumberOfEnemies;
        private const int TotalCurrentEnemies = 10;

        private void Awake()
        {
            GameController.OnRestartGame += OnRestartGame;
            GameController.OnEnemyKilled += OnEnemyKilled;
        }

        private void OnRestartGame()
        {
            _elapsed = 0f;
            _spawnedEnemies = 0;
            _currentNumberOfEnemies = 0;
        }

        private void OnEnemyKilled(int obj)
        {
            _currentNumberOfEnemies--;
        }

        void Update()
        {
            if (GameController.IsPlayerDead || GameController.IsPlayingIntro)
                return;

            _elapsed += Time.deltaTime;
            if (_elapsed >= _spawnFrequency)
            {
                _elapsed = 0f;

                if (_currentNumberOfEnemies >= TotalCurrentEnemies)
                    return;

                _currentNumberOfEnemies++;
                var enemy = Instantiate(Enemy);
                var index = UnityEngine.Random.Range(0, Spawners.Length);
                enemy.transform.position = Spawners[index].position;
                _spawnedEnemies++;
                UpdateSpawnFrequency();
            }
        }

        private void UpdateSpawnFrequency()
        {
            var minKey = 0;
            foreach (var item in _enemyCountToFrequencyMap)
            {
                if (_spawnedEnemies > item.Key && minKey < item.Key)
                {
                    minKey = item.Key;
                }
            }

            if (minKey != 0)
            {
                _spawnFrequency = _enemyCountToFrequencyMap[minKey];
            }
        }
    }
}