using System;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class GameController : MonoBehaviour
    {
        public bool StartGameImmediatelly;
        public Transform PlayerIntroStart;
        public Transform PlayerIntroEnd;
        public Transform Player;

        public static bool IsPlayingIntro { get; private set; }

        public static int EnemiesKilledCount { get; private set; } // thinking not as important
        public static int EnemySoulsCollectedCount { get; private set; }
        public static int HighscoreEnemySoulsCollectedCount { get; private set; }
        public static bool IsPlayerDead { get; private set; }

        private void Start()
        {
            if (StartGameImmediatelly)
            {
                Player.position = PlayerIntroEnd.position;
                OnShowGameMenu?.Invoke();
            }
            else
            {
                IsPlayerDead = true;
                Player.position = PlayerIntroStart.position;
                OnShowMainMenu?.Invoke();
            }
        }

        public static void NotifyEnemyKilled()
        {
            EnemiesKilledCount++;
            OnEnemyKilled?.Invoke(EnemiesKilledCount);
        }

        public static void NotifyPlayerKilled()
        {
            if (HighscoreEnemySoulsCollectedCount < EnemySoulsCollectedCount)
                HighscoreEnemySoulsCollectedCount = EnemySoulsCollectedCount;

            IsPlayerDead = true;
            OnPlayerKilled?.Invoke();
        }

        public static void NotifyPlayerHit()
        {
            OnPlayerHit?.Invoke();
        }

        public static void NotifyGunChange()
        {
            OnGunChange?.Invoke();
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
            EnemySoulsCollectedCount = 0;
            OnRestartGame?.Invoke();
        }

        public static void PlayIntro()
        {
            IsPlayingIntro = true;
            OnPlayIntro?.Invoke();
        }

        public static void IntroFinished()
        {
            IsPlayerDead = false;
            IsPlayingIntro = false;
            OnIntroFinished?.Invoke();
        }

        public static event Action<int> OnEnemyKilled;
        public static event Action<int> OnEnemySoulCollected;
        public static event Action OnPlayerKilled;
        public static event Action OnPlayerHit;
        public static event Action OnGunChange;
        public static event Action OnRestartGame;
        public static event Action OnShowMainMenu;
        public static event Action OnShowGameMenu;
        public static event Action OnPlayIntro;
        public static event Action OnIntroFinished;
    }
}