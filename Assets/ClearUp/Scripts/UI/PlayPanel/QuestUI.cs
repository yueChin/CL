using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QuestUI : MonoBehaviour {
	// Use this for initialization
	void Start ()
    {
        EventCenter.AddListener<Vector3,Quest>(EventType.AddQuest,AddQuest);
		//事件
	}  

    private void AddQuest(Vector3 pos,Quest quest)
    {
        GameObject gameObject = FactoryManager.AssetFactory.LoadGameObject("UI/Bars/QuestBar");//实例化，按标准需要从工厂里生成，或者应该由uimgr管理生成
        gameObject.GetComponent<QuestBarUI>().SetQuest(quest);
        //坐标转换
        gameObject.transform.position = Camera.main.ViewportToScreenPoint(Camera.main.WorldToViewportPoint(pos));
        gameObject.transform.localScale = Vector3.zero;
        gameObject.transform.DOMove(this.transform.position,0.3f).SetEase(Ease.InSine);
        gameObject.transform.DOScale(1, 0.3f).SetEase(Ease.InSine).OnComplete(() => gameObject.transform.SetParent(this.transform));        
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<Vector3, Quest>(EventType.AddQuest, AddQuest);
    }
}
