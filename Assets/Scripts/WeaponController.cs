using System.Collections.Generic;
using Entities.Weapons;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public List<Weapon> weapons;

    [SerializeField]
    private GameInput gameInput;

    private int _currentWeapon;
    
    private void Awake()
    {
        // if (weapons.Count == 0) return;
        //
        // weapons.ForEach(w => w.gameObject.SetActive(false));
    }

    private void Update()
    {
        SwitchWeapon();

        if (gameInput.GetShoot())
        {
            Shoot();
        }
    }

    private void SwitchWeapon()
    {
        if (weapons.Count == 0) return;
        
        var switchWeapon = gameInput.GetSwitch();
        if (switchWeapon == 0) return;

        _currentWeapon += switchWeapon;

        if (_currentWeapon > weapons.Count - 1) _currentWeapon = 0;
        else if (_currentWeapon < 0) _currentWeapon = weapons.Count - 1;
        
        weapons.ForEach(w => w.gameObject.SetActive(false));
        weapons[_currentWeapon].gameObject.SetActive(true);
    }

    private void Shoot()
    {
        
    }
}
