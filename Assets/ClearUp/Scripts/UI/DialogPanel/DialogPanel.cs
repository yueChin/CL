
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogPanel : BasePanel {
    private Image mPlayerIcon;
    private Image mShoperIcon;
    private Transform mDialog;
    private RectTransform mShop;
    private RectTransform mView;
    public delegate void DialogEnter();
    public event DialogEnter DialogEnterEventHandle;
    // Use this for initialization
    private void Awake()
    {
        mDialog = UITool.FindChild<Transform>(this.transform, "Dialog");
        mPlayerIcon = UITool.FindChild<Image>(this.transform, "IconPlayer");
        mShoperIcon = UITool.FindChild<Image>(this.transform, "IconCharcter");
        mShop = this.transform.Find("Shop").GetComponent<RectTransform>();
        mView = UnityTool.FindOneOfAllChild(mShop, "View").GetComponent<RectTransform>();
        EventCenter.AddListener<Sprite,Sprite>(EventType.EnterDialog,OnDialogEnter);
        EventCenter.AddListener<BaseGoods>(EventType.ShowShopGood, OnShowShopInventory);     
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<Sprite, Sprite>(EventType.EnterDialog, OnDialogEnter);
        EventCenter.RemoveListener<BaseGoods>(EventType.ShowShopGood, OnShowShopInventory);
    }

    public override void EnterPanel()
    {
        base.EnterPanel();
        EnterAnimation();
        _UIManager.ShowPlayerPlane();
    }

    public override void ExitPanel()
    {
        base.ExitPanel();
        ExitAnimation();
        _UIManager.HidePlayerPanel();
    }

    private void OnDialogEnter(Sprite spriteLeft,Sprite spriteRight)
    {
        mPlayerIcon.sprite = spriteLeft;
        mShoperIcon.sprite = spriteRight;
        DialogContextEnter();
        ShowShopPanel();
        
    }

    private void OnShowShopInventory(BaseGoods baseGoods)
    {                
        BussinessSlot bussinessSlot = FactoryManager.SlotFactory.GetSlot<BussinessSlot>(baseGoods);
        bussinessSlot.transform.SetParent(mView);
        bussinessSlot.transform.localScale = Vector3.one;       
    }

    

    #region 各类动画
    /// <summary>
    /// 面板进入动画
    /// </summary>
    private void EnterAnimation()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.3f).OnComplete(()=> { DialogEnterEventHandle(); });
    }
    /// <summary>
    /// 面板退出动画
    /// </summary>
    private void ExitAnimation()
    {
        transform.DOScale(0, 0.3f).OnComplete(() => gameObject.SetActive(false));
    }

    /// <summary>
    /// 对面框进入
    /// </summary>
    private void DialogContextEnter()
    {       
        Vector3 vector3 = mDialog.localPosition;
        mDialog.localPosition = new Vector3(0,-1000,0);
        mDialog.DOLocalMove(vector3,1f);
    }   

    /// <summary>
    /// 显示商店面板动画
    /// </summary>
    private void ShowShopPanel()
    {
        Vector3 vector3 = mPlayerIcon.transform.localPosition;
        mPlayerIcon.transform.localPosition = new Vector3(vector3.x, 1500, 0);
        mPlayerIcon.transform.DOLocalMove(vector3, 0.8f).SetEase(Ease.InBounce).SetDelay(0.5f);
    }
    #endregion
}
