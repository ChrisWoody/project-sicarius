using System.Collections.Generic;
using Assets.Scripts.Game;
using Assets.Scripts.Gun;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public EnemySoul EnemySoul;

        private int _health = 100;

        private void Awake()
        {
            GameController.OnRestartGame += OnRestartGame;
        }

        private void OnRestartGame()
        {
            DestroyEnemy();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (GameController.IsPlayerDead)
                return;

            var player = other.GetComponent<Player.Player>();
            if (player)
            {
                player.Hit();
                Die();
            }
        }

        public void Hit(Vector2 hitPoint, Damage damage)
        {
            _health -= DamageMap[damage];
            if (_health > 0)
                return;

            Die();
        }

        private void Die()
        {
            var enemySoul = Instantiate(EnemySoul);
            enemySoul.transform.position = transform.position;

            GameController.NotifyEnemyKilled();
            DestroyEnemy();
        }

        private void DestroyEnemy()
        {
            GameController.OnRestartGame -= OnRestartGame;
            Destroy(gameObject);
        }

        private static readonly Dictionary<Damage, int> DamageMap = new Dictionary<Damage, int>
        {
            {Damage.Low, 15},
            {Damage.Medium, 40},
            {Damage.High, 100},
        };
    }
}