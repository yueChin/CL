using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon :IGear{
    WeaponType SetWeaponType(WeaponType weaponType);
    int SetDamage();
}
