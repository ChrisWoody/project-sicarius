using UnityEngine;

namespace Assets.Scripts.Game.CanvasControllers
{
    public abstract class CanvasBase : MonoBehaviour
    {
        protected Canvas Canvas;

        private void Awake()
        {
            Canvas = GetComponent<Canvas>();
            OnAwake();
        }

        protected virtual void OnAwake(){}

        public abstract void OnShowCanvas(); // might rename these
        public abstract void OnHideCanvas();
    }
}