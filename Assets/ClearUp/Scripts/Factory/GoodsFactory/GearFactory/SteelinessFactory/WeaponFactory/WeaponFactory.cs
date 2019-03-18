using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory  {

    public BaseWeapon GetWeapon(int price)
    {
        return new BaseWeapon(price);
    }

}
