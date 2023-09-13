using Entities.Bullets;
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
                
                var cameraTransform = PlayerController.camera.transform;
                var ray = new Ray(cameraTransform.position, cameraTransform.forward);
                
                var hitDistance = gunData.bullet.bulletData.maxDistance;
                
                Debug.DrawRay(cameraTransform.position, cameraTransform.forward * hitDistance, Color.blue);
                
                Vector3 hitPoint;

                Color color;
                
                if (Physics.Raycast(ray, out var hit, hitDistance))
                {
                    hitPoint = hit.point;
            
                    var objectHit = hit.transform;
                    
                    color = Color.red;
                    Debug.Log($"{objectHit.name} : {hitPoint}");
                }
                else
                {
                    hitPoint = cameraTransform.position + cameraTransform.forward * hitDistance;
                    
                    color = Color.yellow;
                    Debug.Log($"None : {hitPoint}");
                }
                
                Debug.DrawRay(muzzlePosition, hitPoint - muzzlePosition, color);
                
                BulletShoot(gunData.bullet, muzzlePosition, hitPoint - muzzlePosition);
                
                timeSinceLastAttack = 0;
            }
            else
            {
                Debug.Log($"Gun {name} can not fire");
            }
        }
        public void BulletShoot(Bullet bulletPrefab, Vector3 start, Vector3 dir)
        {
            var bullet = Instantiate(bulletPrefab, start, Quaternion.identity);
            
            bullet.transform.LookAt(dir + start);
            bullet.transform.Rotate(bulletPrefab.additionalRotation);
            
            if (bullet.bulletData == null)
            {
                Debug.Log("Bullet data was not set!");
                return;
            }
            
            bullet.GetComponent<Rigidbody>().AddForce(dir.normalized * bullet.bulletData.speed, ForceMode.Impulse);
            
            Debug.Log($"Bullet {name} was fired");
        }
        
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            Debug.DrawRay(muzzle.position, -transform.right, Color.yellow);
        }
    }
}