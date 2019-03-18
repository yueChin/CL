using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem {

    private BaseGoodCreature mCurrentCreature;
    private GameControl mGameControl;
    private int mTalkCount;
    public DialogSystem(GameControl gameControl)
    {
        mGameControl = gameControl;
    }

    /// <summary>
    /// 显示商店的UI界面，控制交互
    /// </summary>
    /// <param name="characterInventory"></param>
    /// <param name="bussinessInventory"></param>
    public void ShowShopDialog(BaseBussinessGoodCreature baseBussinessGoodCreature, BaseCreature player)
    {
        //Debug.Log("进入对话");
        mGameControl.GameFrezze();
        mCurrentCreature = baseBussinessGoodCreature;
        mTalkCount = 0;
        UIManager.GetUIManager.PushPanel(UIPanelType.Dialog);
        mGameControl.ShowShopDialog(player.CreatureIcon,baseBussinessGoodCreature.CreatureIcon);
        baseBussinessGoodCreature.ShowStore();
    }

    /// <summary>
    /// 购买物品，从slot 到 gamecontrol 到dialogsystem
    /// </summary>
    /// <param name="baseGoods"></param>
    /// <returns></returns>
    public bool BuyGood(BaseCreature baseCreature, BaseGoods baseGoods)
    {
        if (!baseCreature.IsEnoughCoin(baseGoods.Price)) return false;
        PlayerSaySomething("介个我要了");
        InventoryManager.GetInventoryManager.BuyGood(baseGoods, mCurrentCreature, baseCreature);
        mCurrentCreature.SellGoodGainCoin(baseGoods.Price);
        baseCreature.BuyGoodLoseCoin(baseGoods.Price);
        return true;
    }

    /// <summary>
    /// 出售物品
    /// </summary>
    /// <param name="baseGoods"></param>
    /// <returns></returns>
    public bool SellGood(BaseCreature baseCreature, BaseGoods baseGoods)
    {
        if (!mCurrentCreature.IsEnoughCoin(baseGoods.Price)) return false;
        PlayerSaySomething("介个给你");
        InventoryManager.GetInventoryManager.SellGood(baseGoods, mCurrentCreature, baseCreature);
        baseCreature.SellGoodGainCoin(baseGoods.Price);
        mCurrentCreature.BuyGoodLoseCoin(baseGoods.Price);
        return true;
    }

    public void PlayerSaySomething(string something)
    {
        mGameControl.PlayerTalkUI(something);
    }

    public void NPCSaySomething()
    {
        //for (int i = 0; i < mCurrentCreature.Dialogs.Length; i++)
        //{
        //    Debug.Log(mCurrentCreature.Dialogs[i]);
        //}       
        if (mTalkCount < mCurrentCreature.Dialogs.Length)
            mGameControl.NPCTalkUI(mCurrentCreature.Dialogs[mTalkCount]);
        else
        {
            mGameControl.NPCTalkUI("...");
            ExitDialog();
        }
        mTalkCount++;
    }

    public void PlayerExit()
    {
        PlayerSaySomething("俺老孙去也");
        ExitDialog();
    }

    public void ExitDialog()
    {
        UIManager.GetUIManager.PopPanel();
        mGameControl.ExitDialogUI();
        mGameControl.GameResume();
    }
}
