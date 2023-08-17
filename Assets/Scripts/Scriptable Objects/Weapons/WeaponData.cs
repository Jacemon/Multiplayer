using UnityEngine;

namespace Scriptable_Objects.Weapons
{
    public abstract class WeaponData : ScriptableObject
    {
        [Header("Info")]
        public new string name;
    }
}
