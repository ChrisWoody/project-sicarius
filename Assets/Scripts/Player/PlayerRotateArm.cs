using Assets.Scripts.Game;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerRotateArm : MonoBehaviour
    {
        public Transform Crosshair;

        private void Update()
        {
            if (GameController.IsPlayerDead || GameController.IsPlayingIntro)
                return;

            var crosshairPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            crosshairPos.z = transform.position.z;

            var dir = transform.position - crosshairPos;
            transform.up = dir.normalized;
            //if (Input.GetKeyDown(KeyCode.Escape))
            //    EditorApplication.isPaused = true;
            Crosshair.position = crosshairPos;
        }
    }
}