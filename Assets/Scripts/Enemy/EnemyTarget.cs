using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyTarget : MonoBehaviour
    {
        private Transform _player;
        private LayerMask _worldLayerMask;

        private void Awake()
        {
            _player = FindObjectOfType<Player.Player>().transform;
            _worldLayerMask = LayerMask.GetMask("World");
        }

        private void Update()
        {
            var pos = _player.position;

            var hit = Physics2D.Raycast(_player.position, Vector2.down, 1000f, _worldLayerMask);
            pos.y = hit.point.y + 1f;
            
            transform.position = pos;
        }
    }
}