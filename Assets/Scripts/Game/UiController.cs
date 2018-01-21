using Assets.Scripts.Game.CanvasControllers;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class UiController : MonoBehaviour
    {
        private CanvasBase _gameUiController;
        private CanvasBase _gameOverUiController;

        private void Awake()
        {
            _gameUiController = GetComponentInChildren<GameUiController>();
            _gameOverUiController = GetComponentInChildren<GameOverUiController>();

            GameController.OnPlayerKilled += ShowGameOverUi;
            GameController.OnRestartGame += ShowGameUi;

            ShowGameUi();
        }

        private void HideAllUiControllers()
        {
            foreach (var canvasBase in GetComponentsInChildren<CanvasBase>())
                canvasBase.OnHideCanvas();
        }

        private void ShowGameOverUi()
        {
            HideAllUiControllers();
            _gameOverUiController.OnShowCanvas();
        }

        private void ShowGameUi()
        {
            HideAllUiControllers();
            _gameUiController.OnShowCanvas();
        }
    }
}