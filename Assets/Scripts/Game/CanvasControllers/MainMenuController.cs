using UnityEngine;

namespace Assets.Scripts.Game.CanvasControllers
{
    public class MainMenuController : CanvasBase
    {
        public void StartGame()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            GameController.PlayIntro();
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