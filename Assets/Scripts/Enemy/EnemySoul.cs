using System.Collections;
using Assets.Scripts.Game;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemySoul : MonoBehaviour
    {
        public EnemySoulParticles EnemySoulParticles;

        private SpriteRenderer _ps;

        private void Awake()
        {
            GameController.OnRestartGame += () =>
            {
                DestoryOnRestart();
                Destroy(gameObject);
            };
            _ps = GetComponent<SpriteRenderer>();
        }

        private static void DestoryOnRestart()
        {
            GameController.OnRestartGame -= DestoryOnRestart;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Player.Player>())
            {
                Instantiate(EnemySoulParticles, transform.position, transform.rotation);
                GetComponent<ParticleSystem>().Stop(false, ParticleSystemStopBehavior.StopEmitting);
                StartCoroutine(FadeOutTrail());
                DestoryOnRestart();
                Destroy(gameObject, DieInTime);
            }
        }

        private const float DieInTime = 3f; // Time for the particle system to finish
        private const float FadeOutSpeed = 0.05f;

        private IEnumerator FadeOutTrail()
        {
            for (var i = 1f; i > -FadeOutSpeed; i -= FadeOutSpeed)
            {
                var color = _ps.color;
                color.a = i;
                _ps.color = color;

                yield return new WaitForSeconds(FadeOutSpeed);
            }
        }
    }
}