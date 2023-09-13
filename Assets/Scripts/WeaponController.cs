using System;
using System.Collections.Generic;
using Entities.Weapons;
using Scriptable_Objects.Weapons;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public List<Weapon> weapons;

    [SerializeField]
    private GameInput gameInput;

    private int _currentWeapon;
    
    private Animator _animator;
    private static readonly int ArmsState = Animator.StringToHash("ArmsState");
    private static readonly int IsAiming = Animator.StringToHash("IsAiming");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        
        SetWeapon();
    }

    private void OnEnable()
    {
        GameInput.onAttack += Attack;
    }
    
    private void OnDisable()
    {
        GameInput.onAttack -= Attack;
    }

    private void Update()
    {
        SwitchWeapon();

        var aim = gameInput.GetAim();
        if (aim)
        {
            Aim();
        }
        _animator.SetBool(IsAiming, aim);
    }

    private void SetWeapon()
    {
        if (weapons == null || weapons.Count == 0) return;
        
        weapons.ForEach(w => w.gameObject.SetActive(false));
        weapons[_currentWeapon].gameObject.SetActive(true);
        
        _animator.SetFloat(ArmsState, (float)WeaponTypeToArmsState(weapons[_currentWeapon].data.weaponType));
    }
    
    private void SwitchWeapon()
    {
        if (weapons == null) return;
        
        var switchWeapon = gameInput.GetSwitch();
        if (switchWeapon == 0) return;

        _currentWeapon += switchWeapon;
        
        if (_currentWeapon > weapons.Count - 1) _currentWeapon = 0;
        else if (_currentWeapon < -1) _currentWeapon = weapons.Count - 1;
        
        SetWeapon();
    }

    private void Attack()
    {
        if (weapons.Count == 0) return;
        
        weapons[_currentWeapon].Attack();
    }

    private void Aim()
    {
        if (weapons.Count == 0) return;
        
        // TODO:
    }

    private GameInput.ArmsState WeaponTypeToArmsState(WeaponData.WeaponType weaponType)
    {
        return (GameInput.ArmsState)weaponType;
    }
}
