using System;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class GameController : MonoBehaviour
    {
        public static int EnemiesKilledCount { get; private set; }

        public static void NotifyEnemyKilled()
        {
            EnemiesKilledCount++;
            OnEnemyKilled?.Invoke(EnemiesKilledCount);
        }

        public static event Action<int> OnEnemyKilled;
    }
}