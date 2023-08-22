using Entities.Bullets;
using UnityEngine;

namespace Scriptable_Objects.Weapons
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Custom/Weapon/Gun")]
    public class GunData : WeaponData
    {
        [Header("Shooting")]
        public Bullet bullet;
        
        [Header("Reloading")]
        public int ammoSize;
        public int magazineSize;
        [Tooltip("rps")]
        public float fireRate;
        [Tooltip("seconds")]
        public float reloadTime;
    }
}