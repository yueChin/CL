using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayPanel : BasePanel {
    private Toggle mCameraToggle;
    private Text mHighestScore;
    private void Awake()
    {
        mCameraToggle = UnityTool.FindOneOfAllChild(this.transform, "CameraToggle").GetComponent<Toggle>();
        mCameraToggle.onValueChanged.AddListener(OnCameraToggleToggleOn);
        mHighestScore = this.transform.Find("HighestScore").Find("Field").GetComponent<Text>();
    }

    public override void EnterPanel()
    {
        base.EnterPanel();
        //Debug.Log("游戏界面");
        EnterAnimation();
        if (_GameControl.PlayScene != null) { _GameControl.PlayScene.gameObject.SetActive(true); }
        _UIManager.ShowPlayerPlane();
        mCameraToggle.isOn = _GameControl.IsCameraFollow;       
    }

    public override void ExitPanel()
    {
        base.ExitPanel();
        ExitAnimation();
        _GameControl.PlayScene.gameObject.SetActive(false);
        _UIManager.HidePlayerPanel();
        mCameraToggle.isOn = false;
    }

    private void OnCameraToggleToggleOn(bool ison)
    {
        _GameControl.IsCameraFollow = ison;
    }

    #region 各类动画
    /// <summary>
    /// 面板进入动画
    /// </summary>
    private void EnterAnimation()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.2f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.2f);
    }
    /// <summary>
    /// 面板退出动画
    /// </summary>
    private void ExitAnimation()
    {
        transform.DOScale(0, 0.3f);
        transform.DOLocalMoveX(-1000, 0.3f).OnComplete(() => gameObject.SetActive(false));
    }  

    public void ShowPlayerPanel(GameObject gameObject)
    {
        gameObject.SetActive(true);        
        gameObject.transform.localScale = Vector3.zero;
        gameObject.transform.DOScale(1, 0);
        gameObject.transform.SetParent(this.transform, false);
    }
    #endregion
}
