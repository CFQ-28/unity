using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //摄像机要看向的目标对象
    public Transform target;
    //摄像机相对目标对象 在xyz上的偏移位置
    public Vector3 offsetPos;
    //看向位置的y偏移值
    public float bodyHeight;

    //移动和旋转速度
    public float moveSpeed;
    public float rotationSpeed;

    private Vector3 targetPos;
    private Quaternion targetRotation;
    private Vector3 Rotation = new Vector3();

    //鼠标旋转速度
    public float mouseSensitivity = 200f;
    private float yRotation = 0f;

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null)
            return;
        //根据目标对象 来计算 摄像机当前的位置和角度
        //位置的计算
        //向后偏移z坐标
        targetPos = target.position + target.forward * offsetPos.z;
        //向上偏移y坐标
        targetPos += Vector3.up * offsetPos.y;
        //左右偏移x坐标
        targetPos += target.right * offsetPos.x;
        //插值运算 让摄像机 不停向目标点靠拢
        this.transform.position = Vector3.Lerp(this.transform.position,targetPos,moveSpeed*Time.deltaTime);

        // 计算鼠标在Y轴上的移动量
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        yRotation -= mouseY; // 反转鼠标Y轴控制摄像机上下视角
        yRotation = Mathf.Clamp(yRotation, -10f, 30f); // 限制上下视角范围

        //旋转的计算
        targetRotation = Quaternion.LookRotation(target.position + Vector3.up * bodyHeight
            - this.transform.position);

        targetRotation = Quaternion.Euler(yRotation, targetRotation.eulerAngles.y, 0f);

        this.transform.rotation = targetRotation;

        //this.transform.rotation = Quaternion.Slerp(this.transform.rotation,targetRotation,
        //    rotationSpeed*Time.deltaTime);
    }

    /// <summary>
    /// 设置摄像机看向目标对象
    /// </summary>
    /// <param name="player"></param>
    public void SetTarget(Transform player)
    {
        target = player;
    }
}
