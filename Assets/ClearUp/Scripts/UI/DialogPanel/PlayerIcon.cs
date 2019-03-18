using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class PlayerIcon : MonoBehaviour {
    private DialogPanel mDialogPanel;
    private Button mButton;
    private float mCountDown;
    private bool mIsTiming;
    private void Awake()
    {
        mDialogPanel = this.transform.parent.parent.GetComponent<DialogPanel>();
        mDialogPanel.DialogEnterEventHandle += new DialogPanel.DialogEnter(IconEnter);
    }
    // Use this for initialization
    void Start () {
        mButton = this.GetComponent<Button>();
        mButton.onClick.AddListener(ButtonClick);
	}

    private void OnDestroy()
    {
        mDialogPanel.DialogEnterEventHandle -= new DialogPanel.DialogEnter(IconEnter);
    }

    // Update is called once per frame
    void Update () {
        if (mIsTiming) //如果 IsTiming 为 true 
        {
            if ((Time.time - mCountDown) > 1.0) //如果 两次点击时间间隔大于2秒 
            {
                mCountDown = 0; //倒计时时间归零 
                mIsTiming = false; //关闭倒计时 
            }
        }
    }

    private void ButtonClick()
    {
        if (mCountDown == 0) //当倒计时时间等于0的时候 
        {
            mCountDown = Time.time; //把游戏开始时间，赋值给 CountDown
            mIsTiming = true; //开始计时                
        }
        else
        {
            mCountDown = 0; //倒计时时间归零 
            mIsTiming = false; //关闭倒计时 
            GameControl.GetGameControl.PlayerRequestExitDialog();
        }
    }

    private void IconEnter()
    {
        //Debug.Log(vector3 + "主角");
        this.transform.parent.localPosition = new Vector3(this.transform.parent.localPosition.x - 500, this.transform.parent.localPosition.y, 0);
        this.transform.parent.DOLocalMoveX(this.transform.parent.localPosition.x + 500, 1f).SetEase(Ease.InCirc);
    }
}
