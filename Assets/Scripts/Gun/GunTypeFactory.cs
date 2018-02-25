namespace Assets.Scripts.Gun
{
    public static class GunTypeFactory
    {
        private static readonly GunType[] GunTypes =
        {
            GunType.Create(Firerate.Medium, Damage.Medium, spread: Spread.None, autoFiring: false, shootThroughEverything: false),
            GunType.Create(Firerate.Slow, Damage.Low, spread: Spread.Small, autoFiring: false, shootThroughEverything: false),
            GunType.Create(Firerate.Slow, Damage.High, spread: Spread.None, autoFiring: false, shootThroughEverything: false),
            GunType.Create(Firerate.Fast, Damage.Low, spread: Spread.None, autoFiring: true, shootThroughEverything: false),
            GunType.Create(Firerate.Medium, Damage.Medium, spread: Spread.None, autoFiring: false, shootThroughEverything: true),
            GunType.Create(Firerate.Slow, Damage.Low, spread: Spread.Wide, autoFiring: true, shootThroughEverything: false),
            GunType.Create(Firerate.Slow, Damage.Medium, spread: Spread.None, autoFiring: true, shootThroughEverything: false),
            GunType.Create(Firerate.Slow, Damage.Low, spread: Spread.Small, autoFiring: false, shootThroughEverything: true),
        };

        private static int _currentGunTypeIndex = -1;

        public static GunType GetNextGunType()
        {
            return _currentGunTypeIndex + 1 < GunTypes.Length
                ? GunTypes[++_currentGunTypeIndex]
                : GunTypes[GunTypes.Length - 1];
        }

        public static void OnRestartGame()
        {
            _currentGunTypeIndex = -1;
        }
    }
}