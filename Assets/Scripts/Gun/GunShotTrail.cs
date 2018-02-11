using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gun
{
    public class GunShotTrail : MonoBehaviour
    {
        private const float DieInTime = 1f;
        private const float FadeOutSpeed = 0.05f;

        private LineRenderer _lr;

        private void Awake()
        {
            _lr = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            Destroy(gameObject, DieInTime);
            StartCoroutine(FadeOutTrail());
        }

        private IEnumerator FadeOutTrail()
        {
            for (var i = DieInTime; i >= 0; i -= FadeOutSpeed)
            {
                var color = _lr.material.color;
                color.a = i;
                _lr.material.color = color;

                yield return new WaitForSeconds(FadeOutSpeed);
            }
        }

        public void SetupShotTrail(Vector3 shotStartPos, Vector3 shotEndPos, Damage damage)
        {
            _lr.SetPositions(new []{shotStartPos, shotEndPos});
            _lr.startWidth = _lr.endWidth = DamageLineWidthMap[damage];
        }

        private static readonly Dictionary<Damage, float> DamageLineWidthMap = new Dictionary<Damage, float>
        {
            {Damage.Low, 0.05f},
            {Damage.Medium, 0.15f},
            {Damage.High, 0.25f},
        };
    }
}