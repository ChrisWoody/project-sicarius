using Assets.Scripts.Game;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemySoul : MonoBehaviour
    {
        private void Awake()
        {
            GameController.OnRestartGame += DestroyEnemySoul;
        }

        private void DestroyEnemySoul()
        {
            GameController.OnRestartGame -= DestroyEnemySoul;
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Player.Player>())
            {
                GameController.NotifySoulCollected();
                DestroyEnemySoul();
            }
        }
    }
}