using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventBuilder  {
    void SetCubetag(string cube);
    void RandomBuildingEvent();
    void OnComplete(System.Action<Vector3> action = null);
}
