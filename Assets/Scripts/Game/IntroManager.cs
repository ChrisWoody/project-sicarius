using UnityEngine;

namespace Assets.Scripts.Game
{
    public class IntroManager : MonoBehaviour
    {
        // Assumes that player is in correct starting position already

        public Transform EndPoint;
        public Transform Player;
        private readonly Vector3 _fallSpeed = new Vector3(0f, -20f, 0f);

        private void Awake()
        {
            GameController.OnPlayIntro += PlayIntro;
            GameController.OnShowMainMenu += PreparePlayer;
        }

        public void PlayIntro()
        {
            PreparePlayer();
            Player.gameObject.SetActive(true);
            Player.Find("ShootingArmShoulder").gameObject.SetActive(false);
            Player.GetComponent<Animator>().SetBool("Dive", true);
            Player.GetComponent<Animator>().SetFloat("IntroSpeed", 0f);
        }

        private void PreparePlayer()
        {
            Player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            Player.GetComponent<Rigidbody2D>().isKinematic = true;
            Player.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!GameController.IsPlayingIntro)
                return;

            var newPosition = _fallSpeed * Time.deltaTime;
            if ((Player.position + newPosition).y <= EndPoint.position.y)
            {
                Player.GetComponent<Animator>().SetBool("Dive", false);
                Player.GetComponent<Animator>().SetFloat("vSpeed", 1f);
                Player.GetComponent<Animator>().SetFloat("vSpeed", 0f);
                Player.GetComponent<Animator>().SetFloat("IntroSpeed", 0f); // need?
                Player.GetComponent<Rigidbody2D>().isKinematic = false;
                Player.Find("ShootingArmShoulder").gameObject.SetActive(true);
                GameController.IntroFinished();
                return;
            }
            
            Player.position += newPosition;
        }
    }
}