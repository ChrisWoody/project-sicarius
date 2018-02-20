using Assets.Scripts.Game;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        public const int OriginalHealth = 3;
        public int CurrentHealth { get; private set; } = OriginalHealth;

        private void Awake()
        {
            GameController.OnRestartGame += () => CurrentHealth = OriginalHealth;
        }

        private void Update()
        {
            
        }

        public void Hit()
        {
            CurrentHealth--;
            GameController.NotifyPlayerHit();

            if (CurrentHealth <= 0)
            {
                GameController.NotifyPlayerKilled();
            }
        }
    }
}