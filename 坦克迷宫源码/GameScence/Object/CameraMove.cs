using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // ����� ����Ŀ��
    public Transform targetPlayer;
    public float H = 11;

    private Vector3 pos;
    void LateUpdate()
    {
        if(targetPlayer == null) return;
        // x �� z�����һ��
        pos.x = targetPlayer.position.x;
        pos.z = targetPlayer.position.z;
        // ͨ���ⲿ��������� �߶�
        pos.y = H;
        this.transform.position = pos;
    }
}
