namespace Assets.Scripts.Game.CanvasControllers
{
    public class GameOverUiController : CanvasBase
    {
        private void Start()
        {
            GameController.OnPlayerKilled += CanRestartGame;
        }

        private bool _canResetGame;

        private void CanRestartGame()
        {
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