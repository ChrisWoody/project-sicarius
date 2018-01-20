using UnityEngine;

namespace Assets.Scripts.Gun
{
    public class GunShotImpact : MonoBehaviour
    {
        public void Setup(RaycastHit2D hit)
        {
            transform.position = hit.point;
            transform.forward = hit.normal;
            Destroy(transform.gameObject, 0.2f);
        }
    }
}