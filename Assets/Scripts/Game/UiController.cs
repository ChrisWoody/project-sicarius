using Assets.Scripts.Game.CanvasControllers;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class UiController : MonoBehaviour
    {
        private CanvasBase _gameUiController;
        private CanvasBase _gameOverUiController;
        private CanvasBase _mainMenuController;
        private CanvasBase _introUiController;

        private void Awake()
        {
            _gameUiController = GetComponentInChildren<GameUiController>();
            _gameOverUiController = GetComponentInChildren<GameOverUiController>();
            _mainMenuController = GetComponentInChildren<MainMenuController>();
            _introUiController = GetComponentInChildren<IntroUiController>();

            GameController.OnPlayerKilled += ShowGameOverUi;
            GameController.OnRestartGame += ShowGameUi;
            GameController.OnShowMainMenu += ShowMainMenuUi;
            GameController.OnPlayIntro += ShowIntroUi;
            GameController.OnIntroFinished += ShowGameUi;
        }

        private void Start()
        {
            ShowMainMenuUi();
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

        private void ShowMainMenuUi()
        {
            HideAllUiControllers();
            _mainMenuController.OnShowCanvas();
        }

        private void ShowIntroUi()
        {
            HideAllUiControllers();
            _introUiController.OnShowCanvas();
        }
    }
}