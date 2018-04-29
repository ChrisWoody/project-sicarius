namespace Assets.Scripts.Game.CanvasControllers
{
    public class IntroUiController : CanvasBase
    {
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