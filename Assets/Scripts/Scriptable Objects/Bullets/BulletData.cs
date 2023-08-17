using UnityEngine;

namespace Scriptable_Objects.Bullets
{
    [CreateAssetMenu(fileName = "Bullet", menuName = "Custom/Bullet")]
    public class BulletData : ScriptableObject
    {
        [Header("Info")]
        public new string name;
        
        [Header("Bullet parameters")]
        public float speed;
        public float damage;
        public float maxDistance;
    }
}