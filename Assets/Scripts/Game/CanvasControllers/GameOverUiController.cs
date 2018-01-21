namespace Assets.Scripts.Game.CanvasControllers
{
    public class GameOverUiController : CanvasBase
    {
        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        public void Yes()
        {
            GameController.RestartGame();
        }

        public void No()
        {
            // go to main menu
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