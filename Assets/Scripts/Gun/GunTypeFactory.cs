namespace Assets.Scripts.Gun
{
    public static class GunTypeFactory
    {
        private static readonly GunType[] GunTypes =
        {
            GunType.Create(Firerate.Medium, Damage.Medium, spread: false, autoFiring: false, shootThroughEverything: false),
            GunType.Create(Firerate.Slow, Damage.Low, spread: true, autoFiring: false, shootThroughEverything: false),
            GunType.Create(Firerate.Slow, Damage.High, spread: false, autoFiring: false, shootThroughEverything: false),
            GunType.Create(Firerate.Fast, Damage.Low, spread: false, autoFiring: true, shootThroughEverything: false),
            GunType.Create(Firerate.Medium, Damage.Medium, spread: false, autoFiring: false, shootThroughEverything: true),
            GunType.Create(Firerate.Slow, Damage.Low, spread: true, autoFiring: true, shootThroughEverything: false),
            GunType.Create(Firerate.Slow, Damage.Medium, spread: false, autoFiring: true, shootThroughEverything: false),
            GunType.Create(Firerate.Slow, Damage.Low, spread: true, autoFiring: false, shootThroughEverything: true),
        };

        private static int _currentGunTypeIndex = 3;

        public static GunType GetNextGunType()
        {
            if (_currentGunTypeIndex + 1 < GunTypes.Length)
                return GunTypes[++_currentGunTypeIndex];

            return GunTypes[GunTypes.Length - 1];
        }

        // i manually call this as the subscribe to onrestartgame wasn't in the order i expected
        public static void OnRestartGame()
        {
            _currentGunTypeIndex = -1;
        }
    }
}