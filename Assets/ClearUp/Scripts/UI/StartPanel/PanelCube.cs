using UnityEngine;

public class PanelCube : MonoBehaviour {

    private bool mUpDown = false;
    private bool Up = false;
    private Ray mRay;

    private void FixedUpdate()
    {
        JudgeDistance();
        if (mUpDown)
        {
            Down();
            if (transform.localPosition.y <= -0.8)
            {
                mUpDown = false;
                Up = true;
            }
        }
        if (Up)
        {
            UP();
            if (transform.localPosition.y >= 0)
            {
                Up = false;
            }
        }        
    }

    private void JudgeDistance()
    {
        mRay = new Ray(this.transform.position,this.transform.up);
        RaycastHit raycastHit;        
        if (Physics.Raycast(mRay, out raycastHit, 1f))
        {
            if (raycastHit.transform.CompareTag("Player"))
            {
                if (raycastHit.transform.position.y - this.transform.position.y <= 1.25f)
                {
                    mUpDown = true;
                }
            }
        }
        
    }

    private void Down()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 2.5f * Time.deltaTime, 0);
    }

    private void UP()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 2.5f * Time.deltaTime, 0);
    }

}
