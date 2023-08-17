using System.Collections.Generic;
using Entities.Weapons;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponController : MonoBehaviour
{
    public List<Weapon> weapons;

    [SerializeField]
    private GameInput gameInput;

    private void Awake()
    {
        if (weapons.Count == 0) return;

        weapons.ForEach(w => w.gameObject.SetActive(false));
    }

    private void Update()
    {
        if (gameInput.GetShoot())
        {
            if(weapons.Count == 0) return;

            weapons.ForEach(w => w.gameObject.SetActive(false));
            weapons[Random.Range(0, weapons.Count)].gameObject.SetActive(true);
        }
    }
}
