using System;
using UnityEngine;
using Assets.Scripts.Game;

namespace Assets.Scripts.Gun
{
    public class Gun : MonoBehaviour
    {
        public GunShotTrail GunShotTrail;
        public GunShotImpact GunShotImpactWorld;
        public GunShotImpact GunShotImpactEnemy;
        public Transform ShotMuzzleFlash;
        public ParticleSystem GunUpgradeFlash;

        public GunType CurrentGunType { get; private set; }

        private float _cooldownElapsed;
        private bool _coolingDown;
        private LayerMask _enemyLayerMask;
        private LayerMask _enemyAndWorldLayerMask;

        private void Awake()
        {
            _enemyLayerMask = LayerMask.GetMask("Enemy");
            _enemyAndWorldLayerMask = LayerMask.GetMask("World", "Enemy");

            //Cursor.lockState = CursorLockMode.Confined;
            //Cursor.visible = false;
            UpdateCurrentGunType(0);
            GameController.OnEnemySoulCollected += UpdateCurrentGunType;
            GameController.OnRestartGame += () =>
            {
                GunTypeFactory.OnRestartGame();
                UpdateCurrentGunType(0);
            };
        }

        private void Update()
        {
            if (GameController.IsPlayerDead || GameController.IsPlayingIntro)
                return;

            if (_coolingDown)
            {
                _cooldownElapsed += Time.deltaTime;
                if (_cooldownElapsed >= CurrentGunType.Cooldown)
                {
                    _cooldownElapsed = 0f;
                    _coolingDown = false;
                }
            }

            if (!_coolingDown && (CurrentGunType.HasAutoFire || Input.GetButtonDown("Fire1")))
            {
                Fire();
                _coolingDown = true;
            }
        }

        private void Fire()
        {
            var firePos = transform.position;
            var fireDir = transform.up;

            for (var i = 0; i < CurrentGunType.SpreadCount; i++)
            {
                var dirToFire = GetDirectionToFire(fireDir);
                if (CurrentGunType.CanShootThroughEverything)
                {
                    var hits = Physics2D.RaycastAll(firePos, dirToFire, 100f, _enemyLayerMask);
                    foreach (var hit in hits)
                    {
                        if (hit.collider)
                        {
                            ShowGunShotImpact(hit);
                            hit.transform.GetComponent<Enemy.Enemy>()?.Hit(hit.point, CurrentGunType.Damage);
                        }
                    }

                    ShowShotTrail(firePos, firePos + (dirToFire * 1000f), CurrentGunType.Damage);
                }
                else
                {
                    var hit = Physics2D.Raycast(firePos, dirToFire, 100f, _enemyAndWorldLayerMask);
                    Vector2 pointToFireTo;
                    if (hit.collider)
                    {
                        pointToFireTo = hit.point;
                        ShowGunShotImpact(hit);
                        hit.transform.GetComponent<Enemy.Enemy>()?.Hit(hit.point, CurrentGunType.Damage);
                    }
                    else
                    {
                        pointToFireTo = firePos + (dirToFire * 1000f);
                    }

                    ShowShotTrail(firePos, pointToFireTo, CurrentGunType.Damage);
                }
            }

            ShowMuzzleFlash(fireDir, firePos);
        }

        private Vector3 GetDirectionToFire(Vector3 fireDir)
        {
            if (CurrentGunType.Spread == Spread.None)
                return fireDir;

            var randomSpread = UnityEngine.Random.value * CurrentGunType.SpreadAmount - CurrentGunType.SpreadAmount / 2;
            return Quaternion.Euler(0, 0, randomSpread) * fireDir;
        }

        private void UpdateCurrentGunType(int soulCollectedCount)
        {
            if (soulCollectedCount % 10 != 0)
                return;

            CurrentGunType = GunTypeFactory.GetNextGunType();
            _cooldownElapsed = 0f;
            _coolingDown = false;
            var gunUpgradeFlash = Instantiate(GunUpgradeFlash);
            gunUpgradeFlash.GetComponent<Transform>().position = transform.position;
            GameController.NotifyGunChange();
        }

        private void ShowMuzzleFlash(Vector3 dir, Vector3 shotStartPosition)
        {
            var muzzleFlash = Instantiate(ShotMuzzleFlash);
            var muzzleFlashPos = shotStartPosition;
            muzzleFlash.position = muzzleFlashPos;
            muzzleFlash.up = dir;
            Destroy(muzzleFlash.gameObject, 0.3f);
        }

        private void ShowShotTrail(Vector3 shotStartPos, Vector3 shotEndPos, Damage damage)
        {
            var gunShotTrail = Instantiate(GunShotTrail);
            gunShotTrail.SetupShotTrail(shotStartPos, shotEndPos, damage);
        }

        private void ShowGunShotImpact(RaycastHit2D hit)
        {
            var gunShotImpact = hit.transform.GetComponent<Enemy.Enemy>()
                ? Instantiate(GunShotImpactEnemy)
                : Instantiate(GunShotImpactWorld);

            gunShotImpact.Setup(hit);
        }
    }
}