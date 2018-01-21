using UnityEngine;
using Assets.Scripts.Game;

namespace Assets.Scripts.Gun
{
    public class Gun : MonoBehaviour
    {
        public GunShotTrail GunShotTrail;
        public GunShotImpact GunShotImpact;

        public GunType CurrentGunType { get; private set; }

        private float _cooldownElapsed;
        private float _cooldownTimeout;
        private bool _coolingDown;

        private void Start()
        {
            //Cursor.lockState = CursorLockMode.Confined;
            //Cursor.visible = false;
            UpdateCurrentGunType();
        }

        private void Update()
        {
            if (GameController.IsPlayerDead)
                return;

            if (_coolingDown)
            {
                _cooldownElapsed += Time.deltaTime;
                if (_cooldownElapsed >= _cooldownTimeout)
                {
                    _cooldownElapsed = 0f;
                    _coolingDown = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
                UpdateCurrentGunType();

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
                            ShowGunShotImpact(hit);
                            hit.transform.GetComponent<Enemy.Enemy>()?.Hit(hit.point, CurrentGunType.Damage);
                        }
                    }

                    ShowMuzzleFlash(fireDir, firePos);
                    ShowShotTrail(firePos, firePos + (fireDir * 1000f), CurrentGunType.Damage);
                }
                else
                {
                    var hit = Physics2D.Raycast(firePos, dirToFire, 100f, LayerMask.GetMask("World", "Enemy"));
                    Vector2 pointToFireTo;
                    if (hit.collider)
                    {
                        pointToFireTo = hit.point;
                        ShowGunShotImpact(hit);
                        hit.transform.GetComponent<Enemy.Enemy>()?.Hit(hit.point, CurrentGunType.Damage);
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
            CurrentGunType = GunTypeFactory.GetNextGunType();
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

        private void ShowShotTrail(Vector3 shotStartPos, Vector3 shotEndPos, Damage damage)
        {
            var gunShotTrail = Instantiate(GunShotTrail);
            gunShotTrail.SetupShotTrail(shotStartPos, shotEndPos, damage);
        }

        private void ShowGunShotImpact(RaycastHit2D hit)
        {
            var gunShotImpact = Instantiate(GunShotImpact);
            gunShotImpact.Setup(hit);
        }

        //private void OnRequiredSoulsCollected()
        //{
        //    UpdateCurrentGunType();
        //    _cooldownElapsed = 0f;
        //    _coolingDown = false;
        //}
    }
}