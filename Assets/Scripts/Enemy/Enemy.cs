using System.Collections.Generic;
using Assets.Scripts.Game;
using Assets.Scripts.Gun;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class Enemy : MonoBehaviour
    {
        private int _health = 100;

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.GetComponent<Player.Player>();
            if (player)
            {
                player.Hit();
                Die(spawnSoul: false);
            }
        }

        public void Hit(Vector2 hitPoint, Damage damage)
        {
            _health -= DamageMap[damage];
            if (_health > 0)
                return;

            Die(spawnSoul: true);
        }

        private void Die(bool spawnSoul)
        {
            GameController.NotifyEnemyKilled();
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