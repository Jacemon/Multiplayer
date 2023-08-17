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
        public int currentAmmo;
        public int magazineSize;
        public float fireRate;
        public float reloadTime;
        [HideInInspector]
        public bool reloading;
    }
}