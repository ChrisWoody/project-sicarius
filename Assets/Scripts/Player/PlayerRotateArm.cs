using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerRotateArm : MonoBehaviour
    {
        public Transform Crosshair;

        private void Update()
        {
            var crosshairPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            crosshairPos.z = transform.position.z;

            var dir = transform.position - crosshairPos;
            transform.up = dir.normalized;
            if (Input.GetKeyDown(KeyCode.Escape))
                EditorApplication.isPaused = true;
            Crosshair.position = crosshairPos;
        }
    }
}