using UnityEngine.UI;

namespace Assets.Scripts.Game.CanvasControllers
{
    public class GameUiController : CanvasBase
    {
        private Text _enemiesKilledValue;

        protected override void OnAwake()
        {
            _enemiesKilledValue = Canvas.transform.Find("EnemiesKilledValue").GetComponent<Text>();

            GameController.OnEnemyKilled += SetEnemyKilledCount;
        }

        private void SetEnemyKilledCount(int count)
        {
            _enemiesKilledValue.text = $"{count}";
        }

        private void Update()
        {
            
        }

        public override void OnShowCanvas()
        {
            SetEnemyKilledCount(0);
            Canvas.enabled = true;
        }

        public override void OnHideCanvas() // should handle a 'reset' as well?
        {
            Canvas.enabled = false;
        }
    }
}