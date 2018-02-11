using UnityEngine;

namespace Assets.Scripts.Gun
{
    public class GunShotImpact : MonoBehaviour
    {
        public void Setup(RaycastHit2D hit)
        {
            transform.position = hit.point + (hit.normal * 0.25f);
            transform.up = hit.normal;
            Destroy(transform.gameObject, 2f);
        }
    }
}