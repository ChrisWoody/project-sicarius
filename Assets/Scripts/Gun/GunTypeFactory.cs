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