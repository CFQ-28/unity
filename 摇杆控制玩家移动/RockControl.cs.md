# RockControl.cs

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RockControl : MonoBehaviour
{
    public RectTransform rock;

    public EventTrigger et;

    public PlayerMove player;

    // Start is called before the first frame update
    void Start()
    {
        //拖动中
        EventTrigger.Entry en = new EventTrigger.Entry();
        en.eventID = EventTriggerType.Drag;
        en.callback.AddListener(RockDrag);
        et.triggers.Add(en);
        //结束拖动
        en = new EventTrigger.Entry();
        en.eventID = EventTriggerType.EndDrag;
        en.callback.AddListener(EndRockDrag);
        et.triggers.Add(en);
    }

    private void RockDrag(BaseEventData data)
    {
        PointerEventData eventData = data as PointerEventData;
        rock.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
        //锚点大于圆圈的范围
        if(rock.anchoredPosition.magnitude > 150)
        {
            //单位向量 乘以 长度 
            rock.anchoredPosition = rock.anchoredPosition.normalized * 150;
        }
        //玩家移动
        player.Move(rock.anchoredPosition);
    }

    private void EndRockDrag(BaseEventData data)
    {
        //回到中心点
        rock.anchoredPosition = Vector2.zero;
        //停止移动 
        player.Move(Vector2.zero);
    }
}

```

