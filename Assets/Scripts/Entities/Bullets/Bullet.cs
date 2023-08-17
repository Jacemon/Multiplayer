using Scriptable_Objects.Bullets;
using UnityEngine;

namespace Entities.Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [Header("References")]
        public BulletData bulletData;
        
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Shoot()
        {
            if (bulletData == null)
            {
                Debug.Log("Bullet data was not set!");
                return;
            }
            
            _rigidbody.AddForce(transform.forward * bulletData.speed, ForceMode.Impulse);
            
            Debug.Log($"Bullet {name} was fired");
        }
    }
}