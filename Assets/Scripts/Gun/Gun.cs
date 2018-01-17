using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enemy;

namespace Assets.Scripts.Gun
{
    public class Gun : MonoBehaviour
    {
        public GunType CurrentGunType { get; private set; }

        private float _cooldownElapsed;
        private float _cooldownTimeout;
        private bool _coolingDown;

        private void Start()
        {
            //Cursor.lockState = CursorLockMode.Confined;
            //Cursor.visible = false;
        }

        private void Update()
        {
            if (_coolingDown)
            {
                _cooldownElapsed += Time.deltaTime;
                if (_cooldownElapsed >= _cooldownTimeout)
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

            var shots = CurrentGunType.HasSpread ? 10 : 1;
            for (var i = 0; i < shots; i++)
            {
                var randVal = (UnityEngine.Random.value * 10f) - 5f;
                var dirToFire = CurrentGunType.HasSpread ? Quaternion.Euler(0, 0, randVal) * fireDir : fireDir;
                if (CurrentGunType.CanShootThroughEverything)
                {
                    var hits = Physics2D.RaycastAll(firePos, fireDir, 100f, LayerMask.GetMask("Enemy"));

                    if (hits.Length == 0)
                    {
                        ShowMuzzleFlash(fireDir, firePos);
                        ShowShotTrail(firePos, firePos + (fireDir * 1000f), CurrentGunType.Damage);
                        return;
                    }

                    foreach (var hit in hits)
                    {
                        if (hit.collider)
                        {
                            ShotImpact(hit);
                            hit.transform.GetComponent<Enemy.Enemy>()?.Hit(hit.point, CurrentGunType.DamageAmount);
                        }
                    }

                    ShowMuzzleFlash(fireDir, firePos);
                    ShowShotTrail(firePos, firePos + (fireDir * 1000f), CurrentGunType.Damage);
                }
                else
                {
                    var hit = Physics2D.Raycast(firePos, dirToFire, 100f, LayerMask.GetMask("Platform", "Enemy"));
                    Vector2 pointToFireTo;
                    if (hit.collider)
                    {
                        pointToFireTo = hit.point;
                        ShotImpact(hit);
                        hit.transform.GetComponent<Enemy.Enemy>()?.Hit(hit.point, CurrentGunType.DamageAmount);
                    }
                    else
                    {
                        pointToFireTo = firePos + (fireDir * 1000f);
                    }

                    ShowShotTrail(firePos, pointToFireTo, CurrentGunType.Damage);
                }
            }
        }

        private void UpdateCurrentGunType()
        {
            //CurrentGunType = GunTypeFactory.GetNextGunType();
            _cooldownTimeout = CurrentGunType.Cooldown;
        }


        private void ShowMuzzleFlash(Vector3 dir, Vector3 shotStartPosition)
        {
            //var muzzleFlash = Instantiate(ShotMuzzleFlash);
            //var muzzleFlashPos = shotStartPosition;
            //muzzleFlashPos.z = -1f;
            //muzzleFlash.position = muzzleFlashPos;
            //muzzleFlash.up = dir;
            //Destroy(muzzleFlash.gameObject, 0.2f);
        }

        private void ShowShotTrail(Vector3 shotStartPosition, Vector2 pointToFire, Assets.Scripts.Gun.Damage damage)
        {
            //var shotTrail = Instantiate(ShotTrail);
            //shotTrail.SetupShotTrail(shotStartPosition, pointToFire, DamageLineWidthMap[damage]);
        }

        private static readonly Dictionary<Damage, float> DamageLineWidthMap = new Dictionary<Damage, float>
        {
            {Damage.Low, 0.05f},
            {Damage.Medium, 0.15f},
            {Damage.High, 0.25f},
        };

        private void ShotImpact(RaycastHit2D hit)
        {
            //var ps = Instantiate(BulletImpact);
            //ps.position = hit.point;
            //ps.transform.up = hit.normal;
            //Destroy(ps.gameObject, 1f);
        }

        //private void OnRequiredSoulsCollected()
        //{
        //    UpdateCurrentGunType();
        //    _cooldownElapsed = 0f;
        //    _coolingDown = false;
        //}
    }
}