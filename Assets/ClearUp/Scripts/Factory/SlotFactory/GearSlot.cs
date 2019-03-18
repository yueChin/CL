using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GearSlot : BaseSlot
{
    protected PartOfBodyType _PartOfBodyType;

    protected override void Awake()
    {
        base.Awake();
        GameControl.GetGameControl.DoReturnPackageUIEventHandle += new GameControl.DoReturnPackageUI(CheckGear);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Destroy(this.GetComponent<PackageSlot>());
        Destroy(this.GetComponent<BattleSkillSlot>());
    }

    public void SetSlotGood(BaseGears baseGears)
    {
        _BaseGood = baseGears;
        _Image.sprite = baseGears.GetSprite;
        _PartOfBodyType = baseGears.PartOfBodyType;
    }

    public BaseGears GetGear()
    {
        return _BaseGood as BaseGears;
    }

    protected override void OnSingleClick()
    {
        UIManager.GetUIManager.ShowGearSlotInfo(this.transform.position, _BaseGood.Description);
    }

    protected override void OnDoubleClick()
    {
        base.OnDoubleClick();
        //通知GearInventory卸下装备
        if(GameControl.GetGameControl.DisChargePlayerGear(_BaseGood as BaseGears))
        {
            //UI动画
            UIManager.GetUIManager.SlotMove(OnSlotMoveAnimation);
        }      
    }

    private void CheckGear(BaseGears baseGears)
    {
        if (baseGears.Equals(_BaseGood))
        {
            UIManager.GetUIManager.SlotMove(OnSlotMoveAnimation);
        }
    }

    public override void OnSlotMoveAnimation(PlayerPanel playerPanel)
    {
        base.OnSlotMoveAnimation(playerPanel);
        this.transform.DOMove(playerPanel.PlayerItemPos, 0.5f).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            playerPanel.PushItemSlotIn(FactoryManager.SlotFactory.GetSlot<PackageSlot>(_BaseGood));
            ObjectsPoolManager.DestroyActiveObject("UI/Slots/Slot", this.gameObject);
        });
    }

    protected override void Destroy()
    {
        ObjectsPoolManager.DestroyActiveObject("UI/Slots/Slot", this.gameObject);
    }
}
