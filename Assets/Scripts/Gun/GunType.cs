using System.Collections.Generic;

namespace Assets.Scripts.Gun
{
    public class GunType
    {
        public readonly float Cooldown;
        public readonly Damage Damage;
        public readonly bool HasSpread;
        public readonly bool HasAutoFire;
        public readonly bool CanShootThroughEverything;

        private GunType(Firerate firerate, Damage damage, bool spread, bool autoFiring, bool shootThroughEverything)
        {
            Cooldown = FirerateMap[firerate];
            Damage = damage;

            HasSpread = spread;
            HasAutoFire = autoFiring;
            CanShootThroughEverything = shootThroughEverything;
        }

        public static GunType Create(Firerate firerate, Damage damage, bool spread, bool autoFiring,
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
    }
}