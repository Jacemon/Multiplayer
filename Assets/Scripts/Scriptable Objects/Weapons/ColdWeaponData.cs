using UnityEngine;

namespace Scriptable_Objects.Weapons
{
    [CreateAssetMenu(fileName = "ColdWeapon", menuName = "Custom/Weapon/ColdWeapon")]
    public class ColdWeaponData : WeaponData
    {
        [Header("Attack")]
        public float damage;
        
        [Tooltip("rps")]
        public float fireRate;
    }
}