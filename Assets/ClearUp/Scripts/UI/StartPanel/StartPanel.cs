using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class StartPanel : BasePanel {    

    public override void EnterPanel()
    {
        base.EnterPanel();        
        EnterAnimation();                                    
    }

    public override void ExitPanel()
    {
        base.ExitPanel();
        ExitAnimation();       
    }

    #region 动画相关
    /// <summary>
    /// 进入面板动画
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
    /// 推出面板动画
    /// </summary>
    private void ExitAnimation()
    {
        transform.DOScale(0, 0.3f);
        transform.DOLocalMoveX(-1000, 0.3f).OnComplete(() => gameObject.SetActive(false));
    }
    #endregion
}
