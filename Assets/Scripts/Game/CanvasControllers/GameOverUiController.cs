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
            GameObject.Find("GameOverEnemiesSoulsCollectedText").GetComponent<Text>().text = $"Souls Collected: {GameController.EnemySoulsCollectedCount}";
            GameObject.Find("GameOverHighscoreEnemiesSoulsCollectedText").GetComponent<Text>().text = $"Most Souls Collected: {GameController.HighscoreEnemySoulsCollectedCount}";

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
            if (_canResetGame)
            {
                GameController.NotifyReturnToMainMenu();
                _canResetGame = false;
            }
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