using System;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class GameController : MonoBehaviour
    {
        public static int EnemiesKilledCount { get; private set; }
        public static int EnemySoulsCollectedCount { get; private set; }
        public static bool IsPlayerDead { get; private set; }

        public static void NotifyEnemyKilled()
        {
            EnemiesKilledCount++;
            OnEnemyKilled?.Invoke(EnemiesKilledCount);
        }

        public static void NotifyPlayerKilled()
        {
            IsPlayerDead = true;
            OnPlayerKilled?.Invoke();
        }

        public static void NotifySoulCollected()
        {
            EnemySoulsCollectedCount++;
            OnEnemySoulCollected?.Invoke(EnemySoulsCollectedCount);
        }

        public static void RestartGame()
        {
            IsPlayerDead = false;
            EnemiesKilledCount = 0;
            OnRestartGame?.Invoke();
        }

        public static event Action<int> OnEnemyKilled;
        public static event Action<int> OnEnemySoulCollected;
        public static event Action OnPlayerKilled;
        public static event Action OnRestartGame;
    }
}