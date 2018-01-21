using System.Collections.Generic;
using Assets.Scripts.Game;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        public Enemy Enemy;
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

        private Transform[] _spawners;

        private void Awake()
        {
            GameController.OnRestartGame += OnRestartGame;
            _spawners = GetComponentsInChildren<Transform>();
        }

        private void OnRestartGame()
        {
            _elapsed = 0f;
            _spawnedEnemies = 0;
        }

        void Update()
        {
            if (GameController.IsPlayerDead)
                return;

            _elapsed += Time.deltaTime;
            if (_elapsed >= _spawnFrequency)
            {
                _elapsed = 0f;

                var enemy = Instantiate(Enemy);
                var index = UnityEngine.Random.Range(0, _spawners.Length);
                enemy.transform.position = _spawners[index].position;
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