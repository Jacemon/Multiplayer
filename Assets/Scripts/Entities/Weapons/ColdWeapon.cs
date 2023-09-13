using UnityEngine;

namespace Entities.Weapons
{
    public class ColdWeapon : Weapon
    {
        public override bool CanAttack() => timeSinceLastAttack >= 1f / data.attackRate;

        public override void Attack()
        {
            if (data == null)
            {
                Debug.Log("Cold Weapon data was not set!");
                return;
            }
            
            timeSinceLastAttack = 0;
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
        }
    }
}