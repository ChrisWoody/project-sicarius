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

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _canResetGame = true;
        }

        private void Update()
        {
            
        }

        public void Yes()
        {
            if (_canResetGame)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
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