using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFactory  {
    private List<IEventBuilder> lEventBuilders;

    /// <summary>
    ///触碰到方块建筑，按建筑类型和方块类型随机触发事件
    /// </summary>
    /// <param name="buildtag">建筑的标签</param>
    /// <param name="cubetag">方块的标签</param>
    public void TriggerAdventureEvent(string buildtag, string cubetag, Vector3 vector3)
    {        
        if (lEventBuilders == null)
            lEventBuilders = new List<IEventBuilder>();
        //Debug.Log("触发事件" + lEventBuilders.Count);
        switch (buildtag)
        {
            case "Building":
                lEventBuilders.Add(new HouseEventBuilder(buildtag, cubetag, vector3));
                break;
            case "Mole":
                lEventBuilders.Add(new CreatureEventBuilder(buildtag, cubetag, vector3));
                break;
            case "Tree":
                lEventBuilders.Add(new TreeEventBuilder(buildtag, cubetag, vector3));
                break;
            case "Mountain":
                lEventBuilders.Add(new MountainEventBuilder(buildtag, cubetag, vector3));
                break;
            default:
                UnityEngine.Debug.Log("输入了错误的事件tag：" + buildtag);
                return;
        }
        if (lEventBuilders.Count <= 1)
        {
            lEventBuilders[0].RandomBuildingEvent();
        }
    }

    public void TriggerAdventureComplete()
    {
        if (lEventBuilders == null) { return; }
        //Debug.Log("事件完成之前的删除" + lEventBuilders.Count);
        if(lEventBuilders.Count > 0)
            lEventBuilders.RemoveAt(0);
       // Debug.Log("事件完成" +lEventBuilders.Count);
        if (lEventBuilders.Count > 0)
            lEventBuilders[0].RandomBuildingEvent();
    }
}
