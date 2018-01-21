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

        public void Hit(Vector2 hitPoint, Damage damage)
        {
            _health -= DamageMap[damage];
            if (_health > 0)
                return;

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