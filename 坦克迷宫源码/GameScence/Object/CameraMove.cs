using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // 摄像机 看向目标
    public Transform targetPlayer;
    public float H = 11;

    private Vector3 pos;
    void LateUpdate()
    {
        if(targetPlayer == null) return;
        // x 和 z和玩家一样
        pos.x = targetPlayer.position.x;
        pos.z = targetPlayer.position.z;
        // 通过外部调整摄像机 高度
        pos.y = H;
        this.transform.position = pos;
    }
}
