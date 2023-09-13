using System;
using Scriptable_Objects.Bullets;
using UnityEngine;

namespace Entities.Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour, IDamageable
    {
        [Header("References")]
        public BulletData bulletData;

        [Header("Settings")]
        [SerializeField]
        private float delayBeforeDestroy = 20.0f;
        
        [Header("Special settings")]
        public Vector3 additionalRotation;

        private void Awake()
        {
            Debug.Log($"Bullet {name} was instantiated");
            
            Destroy(gameObject, delayBeforeDestroy);
        }

        public void Damage(float damage)
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.transform.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.Damage(bulletData.damage);
            }
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            Debug.Log($"Bullet {name} was destroyed");
        }
    }
}