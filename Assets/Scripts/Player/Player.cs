using System.Collections;
using Assets.Scripts.Game;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        public const int OriginalHealth = 3;
        private const float FadeOutSeconds = 0.1f;
        public int CurrentHealth { get; private set; } = OriginalHealth;

        private void Awake()
        {
            GameController.OnRestartGame += () =>
            {
                CurrentHealth = OriginalHealth;
                StopAllCoroutines();
                Time.timeScale = 1f;
            };
        }

        public void Hit()
        {
            CurrentHealth--;
            GameController.NotifyPlayerHit();

            if (CurrentHealth <= 0)
            {
                GameController.NotifyPlayerKilled();
                StartCoroutine(FadeOutTrail());
            }
        }

        private static IEnumerator FadeOutTrail()
        {
            for (var i = 1f; i > -FadeOutSeconds; i -= FadeOutSeconds)
            {
                if (i < 0f) i = 0f;

                Time.timeScale = i;

                yield return new WaitForSeconds(FadeOutSeconds);
            }
        }
    }
}