
using UnityEngine;

public class BattleBall : MonoBehaviour
{
    private void Update()
    {
        RaycastHit hit;
        bool grounded = Physics.Raycast(transform.position, Vector3.right, out hit);
        // 可控制投射距离bool grounded = Physics.Raycast(transform.position, -Vector3.up, out hit,100.0);
        if (grounded && hit.distance < 0.8f)
        {
            GameControl.GetGameControl.BattlePunch(this.transform.position);
        }
    }
}
