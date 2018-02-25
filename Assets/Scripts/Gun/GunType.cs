using System.Collections.Generic;

namespace Assets.Scripts.Gun
{
    public class GunType
    {
        public readonly Firerate Firerate;
        public readonly float Cooldown;
        public readonly Damage Damage;
        public readonly Spread Spread;
        public readonly int SpreadCount;
        public readonly float SpreadAmount;
        public readonly bool HasAutoFire;
        public readonly bool CanShootThroughEverything;

        private GunType(Firerate firerate, Damage damage, Spread spread, bool autoFiring, bool shootThroughEverything)
        {
            Firerate = firerate;
            Cooldown = FirerateMap[firerate];
            Damage = damage;

            Spread = spread;
            SpreadCount = SpreadCountMap[spread];
            SpreadAmount = SpreadAmountMap[spread];
            HasAutoFire = autoFiring;
            CanShootThroughEverything = shootThroughEverything;
        }

        public static GunType Create(Firerate firerate, Damage damage, Spread spread, bool autoFiring,
            bool shootThroughEverything)
        {
            return new GunType(firerate, damage, spread, autoFiring, shootThroughEverything);
        }

        private static readonly Dictionary<Firerate, float> FirerateMap = new Dictionary<Firerate, float>
        {
            {Firerate.Slow, 0.75f},
            {Firerate.Medium, 0.25f},
            {Firerate.Fast, 0.1f},
        };

        private static readonly Dictionary<Spread, int> SpreadCountMap = new Dictionary<Spread, int>
        {
            {Spread.None, 1},
            {Spread.Small, 5},
            {Spread.Wide, 15},
        };

        private static readonly Dictionary<Spread, int> SpreadAmountMap = new Dictionary<Spread, int>
        {
            {Spread.None, 0},
            {Spread.Small, 5},
            {Spread.Wide, 15},
        };
    }
}