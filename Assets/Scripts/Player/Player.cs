using Assets.Scripts.Game;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        private int _health = 3;

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        public void Hit()
        {
            _health--;

            if (_health <= 0)
            {
                GameController.NotifyPlayerKilled();
                Destroy(gameObject);
            }
        }
    }
}