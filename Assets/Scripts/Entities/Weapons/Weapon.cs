using Scriptable_Objects.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [Header("References")]
        public WeaponData data;
        
        [Header("Reloading")]   
        [SerializeField]
        protected float timeSinceLastAttack;
        
        public abstract bool CanAttack();
        public abstract void Attack();
    }
}