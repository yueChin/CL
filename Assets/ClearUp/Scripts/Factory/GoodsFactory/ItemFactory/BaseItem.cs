using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : BaseGoods,IItem
{
    protected ItemType _ItemType;
    public BaseItem(int price) : base(price) { }

    public void SetItemType(ItemType itemType)
    {
        _ItemType = itemType;
    }

    public override void SetName()
    {
        _Name = _ItemType.ToString();
    }

    public override void SetDescription()
    {
        _Description = string.Format("描述：{0}\n 价格：{1}\n", PropertiesUtils.GetDescByProperties(_ItemType), _Price);
    }

    public override void SetSprite()
    {
        _Sprite = FactoryManager.AssetFactory.LoadSprite(GameControl.GetGameControl.SpritePathDict.TryGet(_Name));
    }

    public void TriggerItemEffect()
    {
        switch (_ItemType)
        {
            case ItemType.HeartStone:
                GameControl.GetGameControl.Addheart();
                break;
            case ItemType.TransferStone:
                //游戏状态切换到暂停？然后
                break; 
        }
    }
}
