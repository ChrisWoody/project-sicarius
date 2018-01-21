using UnityEngine;

namespace Assets.Scripts.Game
{
    public abstract class CanvasBase : MonoBehaviour
    {
        public abstract void OnShowCanvas();
        public abstract void OnHideCanvas();
    }
}