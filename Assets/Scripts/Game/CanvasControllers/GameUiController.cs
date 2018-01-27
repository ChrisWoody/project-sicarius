using UnityEngine.UI;

namespace Assets.Scripts.Game.CanvasControllers
{
    public class GameUiController : CanvasBase
    {
        private Text _enemiesKilledValue;
        private Text _enemySoulsCollectedValue;

        protected override void OnAwake()
        {
            _enemiesKilledValue = Canvas.transform.Find("EnemiesKilledValue").GetComponent<Text>();
            _enemySoulsCollectedValue = Canvas.transform.Find("EnemySoulsCollectedValue").GetComponent<Text>();

            GameController.OnEnemyKilled += SetEnemyKilledCount;
            GameController.OnEnemySoulCollected += SetEnemySoulsCollectedCount;
        }

        private void SetEnemyKilledCount(int count)
        {
            _enemiesKilledValue.text = $"{count}";
        }

        private void SetEnemySoulsCollectedCount(int count)
        {
            _enemySoulsCollectedValue.text = $"{count}";
        }

        public override void OnShowCanvas()
        {
            SetEnemyKilledCount(0);
            SetEnemySoulsCollectedCount(0);
            Canvas.enabled = true;
        }

        public override void OnHideCanvas() // should handle a 'reset' as well?
        {
            Canvas.enabled = false;
        }
    }
}