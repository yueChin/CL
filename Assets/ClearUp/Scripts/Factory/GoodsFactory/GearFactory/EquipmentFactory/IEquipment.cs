using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipment:IGear,IProtection {
    EquipmentType SetEquipment(EquipmentType equipmentType);
}
