using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.CanvasControllers
{
    public class GameOverUiController : CanvasBase
    {
        private void Start()
        {
            GameController.OnPlayerKilled += OnPlayerKilled;
        }

        private bool _canResetGame;

        private void OnPlayerKilled()
        {
            // set enemies killed and souls collected
            GameObject.Find("GameOverEnemiesKilledText").GetComponent<Text>().text = $"Demons Killed: {GameController.EnemiesKilledCount}";
            GameObject.Find("GameOverEnemiesSoulsCollectedText").GetComponent<Text>().text = $"Souls Collected: {GameController.EnemySoulsCollectedCount}";

            _canResetGame = true;
        }

        private void Update()
        {
            
        }

        public void Yes()
        {
            if (_canResetGame)
            {
                GameController.RestartGame();
                _canResetGame = false;
            }
        }

        public void No()
        {
            // go to main menu
            _canResetGame = false;
        }


        public override void OnShowCanvas()
        {
            Canvas.enabled = true;
        }

        public override void OnHideCanvas()
        {
            Canvas.enabled = false;
        }
    }
}