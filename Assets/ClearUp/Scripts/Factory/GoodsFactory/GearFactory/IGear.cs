using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGear  {
    DurabilityType SetDurabilityType(DurabilityType durabilityType);
    CategoryType SetCategoryType(CategoryType categoryType);
    PartOfBodyType SetPartOfBodyType(PartOfBodyType partOfBodyType);
    int GetArmamentValue();
}
