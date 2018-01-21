using Assets.Scripts.Game;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        private const int OriginalHealth = 3;
        private int _currentHealth = OriginalHealth;

        private void Awake()
        {
            GameController.OnRestartGame += () => _currentHealth = OriginalHealth;
        }

        private void Update()
        {
            
        }

        public void Hit()
        {
            _currentHealth--;

            if (_currentHealth <= 0)
            {
                GameController.NotifyPlayerKilled();
            }
        }
    }
}