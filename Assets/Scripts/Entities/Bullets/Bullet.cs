using System;
using Scriptable_Objects.Bullets;
using UnityEngine;

namespace Entities.Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [Header("References")]
        public BulletData bulletData;

        [Header("Settings")]
        [SerializeField]
        private float delayBeforeDestroy = 2.0f;
        
        private Rigidbody _rigidbody;

        private void Awake()
        {
            Debug.Log($"Bullet {name} was instantiated");
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Shoot()
        {
            if (bulletData == null)
            {
                Debug.Log("Bullet data was not set!");
                return;
            }
            
            _rigidbody.AddForce(-transform.right * bulletData.speed, ForceMode.Impulse);
            
            Debug.Log($"Bullet {name} was fired");
            
            Destroy(gameObject, delayBeforeDestroy);
        }

        private void OnDestroy()
        {
            Debug.Log($"Bullet {name} was destroyed");
        }
    }
}