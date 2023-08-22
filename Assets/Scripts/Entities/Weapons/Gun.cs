using System;
using Entities.Bullets;
using Scriptable_Objects.Weapons;
using UnityEngine;

namespace Entities.Weapons
{
    public class Gun : Weapon
    {
        [Header("References")]
        public GunData gunData;
        [Space]
        public Transform muzzle;
        
        [Header("Reloading")]
        public bool reloading;
        [SerializeField]
        private float timeSinceLastShoot;

        private bool CanShoot() => !reloading && timeSinceLastShoot >= 1f / gunData.fireRate;

        public override void Attack()
        {
            if (gunData == null)
            {
                Debug.Log("Gun data was not set!");
                return;
            }
            
            if (gunData.ammoSize > 0 && CanShoot())
            {
                if (Physics.Raycast(muzzle.position, transform.forward, out var hit))
                {
                    Debug.Log(hit.transform.name);
                    Debug.DrawRay(muzzle.position, -transform.right, Color.red);
                    
                    Debug.Log($"Gun {name} has fired");
                    
                    var bullet = Instantiate(gunData.bullet, muzzle.position, muzzle.rotation);
                    bullet.Shoot();

                    timeSinceLastShoot = 0;
                }
            }
            else
            {
                Debug.Log($"Gun {name} can not fire");
            }
        }

        private void Update()
        {
            timeSinceLastShoot += Time.deltaTime;
            Debug.DrawRay(muzzle.position, -transform.right, Color.yellow);
        }
    }
}