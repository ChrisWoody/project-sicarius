using Assets.Scripts.Gun;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.CanvasControllers
{
    public class GameUiController : CanvasBase
    {
        public Image Health1;
        public Image Health2;
        public Image Health3;

        public Text KilledTextAmount;
        public Text SoulsTextAmount;

        public Image FireRate1;
        public Image FireRate2;
        public Image FireRate3;

        public Image Spread1;
        public Image Spread2;
        public Image Spread3;

        public Image Damage1;
        public Image Damage2;
        public Image Damage3;

        public Image Penetrate;
        public Text PenetrateText;

        public Image AutoFire;
        public Text AutoFireText;

        private Player.Player _player;
        private Gun.Gun _gun;
        private readonly Color _greyTransparentColor = new Color(0.5f, 0.5f, 0.5f, 0.75f);
        private readonly Color _whiteTransparentColor = new Color(1f, 1f, 1f, 0.75f);

        protected override void OnAwake()
        {
            KilledTextAmount.text = "0";
            SoulsTextAmount.text = "0";

            GameController.OnEnemyKilled += count => KilledTextAmount.text = $"{count}";
            GameController.OnEnemySoulCollected += count => SoulsTextAmount.text = $"{count}";
            GameController.OnPlayerHit += OnPlayerHit;
            GameController.OnGunChange += OnGunChange;
            GameController.OnRestartGame += OnResetGame;
            Health1.color = Health2.color = Health3.color = _whiteTransparentColor;

            _player = FindObjectOfType<Player.Player>();
            _gun = FindObjectOfType<Gun.Gun>();
        }

        private void OnPlayerHit()
        {
            if (_player.CurrentHealth == 2)
                Health3.color = _greyTransparentColor;

            if (_player.CurrentHealth == 1)
                Health2.color = Health3.color = _greyTransparentColor;

            if (_player.CurrentHealth == 0)
                Health1.color = Health2.color = Health3.color = _greyTransparentColor;
        }

        private void OnGunChange()
        {
            if (_gun.CurrentGunType.Firerate == Firerate.Fast)
            {
                FireRate1.color = FireRate2.color = FireRate3.color = _whiteTransparentColor;
            }

            if (_gun.CurrentGunType.Firerate == Firerate.Medium)
            {
                FireRate1.color = FireRate2.color = _whiteTransparentColor;
                FireRate3.color = _greyTransparentColor;
            }

            if (_gun.CurrentGunType.Firerate == Firerate.Slow)
            {
                FireRate1.color = _whiteTransparentColor;
                FireRate2.color = FireRate3.color = _greyTransparentColor;
            }


            if (_gun.CurrentGunType.HasSpread) // will update to an enum
            {
                Spread1.color = Spread2.color = Spread3.color = _whiteTransparentColor;
            }
            else
            {
                Spread1.color = Spread2.color = Spread3.color = _greyTransparentColor;
            }


            if (_gun.CurrentGunType.Damage == Damage.High)
            {
                Damage1.color = Damage2.color = Damage3.color = _whiteTransparentColor;
            }

            if (_gun.CurrentGunType.Damage == Damage.Medium)
            {
                Damage1.color = Damage2.color = _whiteTransparentColor;
                Damage3.color = _greyTransparentColor;
            }

            if (_gun.CurrentGunType.Damage == Damage.Low)
            {
                Damage1.color = _whiteTransparentColor;
                Damage2.color = Damage3.color = _greyTransparentColor;
            }


            if (_gun.CurrentGunType.CanShootThroughEverything)
            {
                Penetrate.color = PenetrateText.color = _whiteTransparentColor;
            }
            else
            {
                Penetrate.color = PenetrateText.color = _greyTransparentColor;
            }


            if (_gun.CurrentGunType.HasAutoFire)
            {
                AutoFire.color = AutoFireText.color = _whiteTransparentColor;
            }
            else
            {
                AutoFire.color = AutoFireText.color = _greyTransparentColor;
            }
        }

        public override void OnShowCanvas()
        {
            Canvas.enabled = true;
        }

        public override void OnHideCanvas()
        {
            Canvas.enabled = false;
        }

        public void OnResetGame()
        {
            Health1.color = Health2.color = Health3.color = _whiteTransparentColor;
            KilledTextAmount.text = "0";
            SoulsTextAmount.text = "0";
            OnGunChange();
        }
    }
}