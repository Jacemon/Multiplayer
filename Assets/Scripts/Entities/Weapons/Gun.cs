using Scriptable_Objects.Weapons;
using UnityEngine;

namespace Entities.Weapons
{
    public class Gun : Weapon
    {
        [Header("Additional")]
        public Transform muzzle;
        
        [Header("Reloading")]
        public bool reloading;

        public override bool CanAttack() => !reloading && timeSinceLastAttack >= 1f / data.attackRate;

        public override void Attack()
        {
            var gunData = data as GunData;
            
            if (gunData == null)
            {
                Debug.Log("Gun data was not set!");
                return;
            }
            
            if (gunData.ammoSize > 0 && CanAttack())
            {
                var muzzlePosition = muzzle.position;
                
                if (Physics.Raycast(muzzlePosition, transform.forward, out var hit))
                {
                    Debug.Log(hit.transform.name);
                    Debug.DrawRay(muzzlePosition, -transform.right, Color.red);
                    
                    Debug.Log($"Gun {name} has fired");
                    
                    var bullet = Instantiate(gunData.bullet, muzzlePosition, muzzle.rotation);
                    bullet.Shoot();

                    timeSinceLastAttack = 0;
                }
            }
            else
            {
                Debug.Log($"Gun {name} can not fire");
            }
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            Debug.DrawRay(muzzle.position, -transform.right, Color.yellow);
        }
    }
}