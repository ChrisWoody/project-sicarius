﻿using System;
using Assets.Scripts.Game;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemySoulParticles : MonoBehaviour
    {
        private static Transform _gun;

        private float _elapsed;
        private bool _dead;
        private const float TimeToTravel = 0.5f;

        private void Start()
        {
            if (!_gun)
                _gun = FindObjectOfType<Gun.Gun>().transform;

            GameController.OnRestartGame += () =>
            {
                if (_dead)
                    return;

                try
                {
                    DestoryOnRestart();
                    Destroy(gameObject);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            };
        }

        private static void DestoryOnRestart()
        {
            GameController.OnRestartGame -= DestoryOnRestart;
        }

        private void Update()
        {
            if (_dead)
                return;

            _elapsed += Time.deltaTime;
            if (_elapsed >= TimeToTravel)
            {
                _dead = true;
                GetComponent<ParticleSystem>().Stop(false, ParticleSystemStopBehavior.StopEmitting);
                GameController.NotifySoulCollected();
                DestoryOnRestart();
                Destroy(gameObject, 0.5f);
            }

            var toMove = Vector3.Lerp(transform.position, _gun.position, _elapsed / TimeToTravel);
            toMove.z = 1f;
            transform.position = toMove;
        }
    }
}