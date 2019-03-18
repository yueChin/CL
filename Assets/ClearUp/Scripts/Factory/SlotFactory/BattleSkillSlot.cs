using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSkillSlot : BaseSlot {

    protected override void OnEnable()
    {
        base.OnEnable();
        Destroy(this.GetComponent<GearSlot>());
        Destroy(this.GetComponent<PackageSlot>());
        _AudioSource.clip = FactoryManager.AssetFactory.LoadAudioClip("Audios/BattleSkillAdd");
        _AudioSource.Play();
        StartCoroutine(AudioComplete());
    }

    protected override void Start()
    {
        base.Start();                
        EventCenter.AddListener<BattleSkill>(EventType.DoneSkillUI,DoneSkill);
        EventCenter.AddListener(EventType.LevelBattle,RemoveSkill);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventCenter.RemoveListener<BattleSkill>(EventType.DoneSkillUI, DoneSkill);
        EventCenter.RemoveListener(EventType.LevelBattle, RemoveSkill);
    }

    protected virtual void RemoveSkill()
    {
        //Debug.Log("移除战斗slot");
        ObjectsPoolManager.DestroyActiveObject("BattleSlot", this.gameObject);
    }

    private IEnumerator AudioComplete()
    {
        yield return _AudioSource.clip.length;
        _AudioSource.clip = FactoryManager.AssetFactory.LoadAudioClip("Audios/BattleSkillRelease");
    }

    protected virtual void DoneSkill(BattleSkill battleSkill)
    {
        if (_BaseGood.Equals(battleSkill))
        {
            this.transform.SetParent(UIManager.GetUIManager.GetCanvasTransform,true);
            this.transform.localScale = Vector3.one;
            this.transform.DOLocalMoveY(this.transform.localPosition.y + 50f, 1f).OnComplete(()=> {
                ObjectsPoolManager.DestroyActiveObject("BattleSlot", this.gameObject); 
            });
        }
    }

    protected override void OnSingleClick()
    {
        UIManager.GetUIManager.ShowSkillSlotInfo(Camera.main.WorldToScreenPoint(this.transform.position), _BaseGood.Description);
    }

    protected override void Destroy()
    {
        ObjectsPoolManager.DestroyActiveObject("BattleSlot", this.gameObject);
    }
}
