using UnityEngine;

namespace Scriptable_Objects.Weapons
{
    public abstract class WeaponData : ScriptableObject
    {
        [Header("Info")]
        public new string name;
        public WeaponType weaponType;

        [Tooltip("rps")]
        public float attackRate;
        
        public enum WeaponType
        {
            Hands,
            Knife,
            Pistol,
            Rifle
        }
    }
}
