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
        private Vector3 _homePortalPos;

        private void Awake()
        {
            GameController.OnRestartGame += OnRestartGame;
            _homePortalPos = new Vector3(39.5f, 0.5f, 0f);
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

        public void Init(Vector3 homePortalPos)
        {
            _homePortalPos = homePortalPos;
        }

        private void Die()
        {
            var enemySoul = Instantiate(EnemySoul);
            enemySoul.transform.position = transform.position;
            enemySoul.Init(_homePortalPos);

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