using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager {

    private static UIManager mUIManager;
    public static UIManager GetUIManager
    {
        get
        {
            if (mUIManager == null)
            {
                mUIManager = new UIManager();
            }
            return mUIManager;
        }
    }

    private Transform mCanvasTransform;
    public Transform GetCanvasTransform
    {
        get
        {
            if (mCanvasTransform == null)
            {
                mCanvasTransform = GameObject.Find("Canvas").transform;
            }
            return mCanvasTransform;
        }
    }
    public PlayerPanel PlayerPanel { get { return mPlayerPanel; } }
    public float Record { get { return GameControl.GetGameControl.Record; } }
    public float Time { get { return GameControl.GetGameControl.GameTime; } }
    public float Score { get { return GameControl.GetGameControl.CurrentScore; } }
    private Dictionary<UIPanelType, BasePanel> dUIPanelPrefabDict;
    private Stack<BasePanel> mPanelStack;
    private UIPanelType mNextUIPanelType = UIPanelType.None;
    private GameData mGameData;
    private SlotInfoPanel mSlotInfoPanel;
    private PlayerPanel mPlayerPanel;
    private Text mText;
    public void Init()
    {
        ParseUIPanelTypeJson();
        //UIManager.GetUIManager.PushPanel(UIPanelType.Start);
    }

    public void Update()
    {
        if (mNextUIPanelType != UIPanelType.None)
        {
            PushPanel(mNextUIPanelType);
            mNextUIPanelType = UIPanelType.None;
        }
    }

    /// <summary>
    /// 显示技能黑屏面板
    /// </summary>
    public void ShowSkillPausePanel()
    {
        GetPanel(UIPanelType.ReleaseSkill).EnterPanel();
    }

    /// <summary>
    /// 可以的话，可以写一个消息管理
    /// </summary>
    /// <param name="message"></param>
    public void ShowMessage(string message,float delayTime = 0)
    {
        FactoryManager.AssetFactory.LoadGameObject("UI/Bars/MessageBar").GetComponent<MessageUI>().SetMessage(message, GetCanvasTransform,delayTime);
    }

    /// <summary>
    /// 显示主角的面板
    /// </summary>
    /// <param name="action"></param>
    public void ShowPlayerPlane()
    {
        mPlayerPanel = GetPanel(UIPanelType.Player) as PlayerPanel;
        //Debug.Log("显示主角面板"+ mPlayerPanel);
        mPlayerPanel.EnterPanel();
    }

    public void HidePlayerPanel()
    {
        if (mPlayerPanel != null) mPlayerPanel.ExitPanel();
    }

    /// <summary>
    /// 显示Slot移动的动画
    /// </summary>
    /// <param name="action"></param>
    public void SlotMove(Action<PlayerPanel> action)
    {
        if (mPlayerPanel == null) { Debug.Log("玩家界面不存在，无法移动slot");return; }
        action(mPlayerPanel);
    }

    /// <summary>
    /// 外部的ui面板切换
    /// </summary>
    /// <param name="panelType"></param>
    public void PushPanelSync(UIPanelType panelType)
    {
        mNextUIPanelType = panelType;
    }

    /// <summary>
    /// 退出游戏面板
    /// </summary>
    /// <param name="panelType"></param>
    public void ShowExitPanel(UIPanelType panelType)
    {
        BasePanel exitPanel = GetPanel(panelType);
        exitPanel.gameObject.SetActive(true);
        int count = exitPanel.transform.parent.childCount;
        exitPanel.transform.SetSiblingIndex(count - 1);
    }

    #region 显示格子信息
    /// <summary>
    /// 显示物品信息
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="desc"></param>
    /// <param name="price"></param>
    public void ShowItemSlotInfo(Vector3 pos, string desc)
    {
        if (mSlotInfoPanel == null)
        {
            mSlotInfoPanel = GetPanel(UIPanelType.SlotInfo) as SlotInfoPanel;
        }
        mSlotInfoPanel.ShowItemInfo(pos, desc);
    }

    /// <summary>
    /// 显示技能信息
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="desc"></param>
    public void ShowSkillSlotInfo(Vector3 pos, string desc)
    {
        if (mSlotInfoPanel == null)
        {
            mSlotInfoPanel = GetPanel(UIPanelType.SlotInfo) as SlotInfoPanel;
        }
        mSlotInfoPanel.ShowSkillInfo(pos, desc);
    }

    /// <summary>
    /// 显示装备信息
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="desc"></param>
    public void ShowGearSlotInfo(Vector3 pos, string desc)
    {
        if (mSlotInfoPanel == null)
        {
            mSlotInfoPanel = GetPanel(UIPanelType.SlotInfo) as SlotInfoPanel;
        }
        mSlotInfoPanel.gameObject.SetActive(true);
        mSlotInfoPanel.ShowGearInfo(pos, desc);
    }

    /// <summary>
    /// 影藏slotInfo面板，不过自己也会隐藏
    /// </summary>
    public void HideSlotInfo()
    {
        if (mSlotInfoPanel == null)
        {
            return;
        }
        mSlotInfoPanel.HideInfo();
    }
    #endregion

    #region 面板切换
    /// <summary>
    /// 把某个页面入栈，把某个页面显示在界面上
    /// </summary>
    public BasePanel PushPanel(UIPanelType panelType)
    {
        //Debug.Log(panelType + "界面转换");
        if (mPanelStack == null)
            mPanelStack = new Stack<BasePanel>();

        //判断一下栈里面是否有页面,把栈顶的推出
        if (mPanelStack.Count > 0)
        {
            BasePanel topPanel = mPanelStack.Peek(); //返回栈顶界面
            topPanel.ExitPanel();
        }

        BasePanel panel = GetPanel(panelType);
        panel.EnterPanel();
        mPanelStack.Push(panel);
        return panel;
    }

    /// <summary>
    /// 出栈 ，把页面从界面上移除
    /// </summary>
    public void PopPanel()
    {
        if (mPanelStack == null)
            mPanelStack = new Stack<BasePanel>();

        if (mPanelStack.Count <= 0) return;

        //关闭栈顶页面的显示
        BasePanel topPanel = mPanelStack.Pop();
        topPanel.ExitPanel();

        if (mPanelStack.Count <= 0) return;
        BasePanel topPanel2 = mPanelStack.Peek();
        topPanel2.EnterPanel();

    }
    #endregion

    #region 获取面板预制件
    /// <summary>
    /// 根据面板类型 得到实例化的面板
    /// </summary>
    /// <returns></returns>
    private BasePanel GetPanel(UIPanelType panelType)
    {
        if ( dUIPanelPrefabDict== null)
        {
            dUIPanelPrefabDict = new Dictionary<UIPanelType, BasePanel>();
        }

        BasePanel panel = dUIPanelPrefabDict.TryGet(panelType);

        if (panel == null)
        {
            //如果找不到，那么就找这个面板的prefab的路径，然后去根据prefab去实例化面板
            //Debug.Log(panelType);
            string path = UIPanelPathDict.TryGet(panelType);
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            instPanel.transform.SetParent(GetCanvasTransform, false);
            instPanel.transform.localScale = Vector3.one;
            instPanel.GetComponent<BasePanel>().UIManager= this;
            instPanel.GetComponent<BasePanel>().GameControl = GameControl.GetGameControl;
            dUIPanelPrefabDict.Add(panelType, instPanel.GetComponent<BasePanel>());
            return instPanel.GetComponent<BasePanel>();
        }
        else
        {
            return panel;
        }

    }
    #endregion

    public Dictionary<UIPanelType, string> UIPanelPathDict { get; private set; }

    [Serializable]
    class UIPanelTypeJson
    {
        public List<UIPanelInfo> infoList;
    }

    /// <summary>
    /// 获取面板的路径，存入字典
    /// </summary>
    private void ParseUIPanelTypeJson()
    {
        UIPanelPathDict = new Dictionary<UIPanelType, string>();

        TextAsset ta = Resources.Load<TextAsset>("UIPanelType");
        UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);
        foreach (UIPanelInfo info in jsonObject.infoList)
        {
            UIPanelPathDict.Add(info.panelType, info.path);
        }
    }
}
