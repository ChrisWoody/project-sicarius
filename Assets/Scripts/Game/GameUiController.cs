using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
    public class GameUiController : CanvasBase
    {
        private Canvas _canvas;
        private Text _enemiesKilledValue;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _enemiesKilledValue = _canvas.transform.Find("EnemiesKilledValue").GetComponent<Text>();

            GameController.OnEnemyKilled += OnEnemyKilled;
        }

        private void OnEnemyKilled(int count)
        {
            _enemiesKilledValue.text = $"{count}";
        }

        private void Update()
        {
            
        }

        public override void OnShowCanvas()
        {
            _canvas.enabled = true;
        }

        public override void OnHideCanvas() // should handle a 'reset' as well?
        {
            _canvas.enabled = false;
        }
    }
}