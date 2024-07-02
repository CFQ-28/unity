using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //�����Ҫ�����Ŀ�����
    public Transform target;
    //��������Ŀ����� ��xyz�ϵ�ƫ��λ��
    public Vector3 offsetPos;
    //����λ�õ�yƫ��ֵ
    public float bodyHeight;

    //�ƶ�����ת�ٶ�
    public float moveSpeed;
    public float rotationSpeed;

    private Vector3 targetPos;
    private Quaternion targetRotation;
    private Vector3 Rotation = new Vector3();

    //�����ת�ٶ�
    public float mouseSensitivity = 200f;
    private float yRotation = 0f;

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null)
            return;
        //����Ŀ����� ������ �������ǰ��λ�úͽǶ�
        //λ�õļ���
        //���ƫ��z����
        targetPos = target.position + target.forward * offsetPos.z;
        //����ƫ��y����
        targetPos += Vector3.up * offsetPos.y;
        //����ƫ��x����
        targetPos += target.right * offsetPos.x;
        //��ֵ���� ������� ��ͣ��Ŀ��㿿£
        this.transform.position = Vector3.Lerp(this.transform.position,targetPos,moveSpeed*Time.deltaTime);

        // ���������Y���ϵ��ƶ���
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        yRotation -= mouseY; // ��ת���Y���������������ӽ�
        yRotation = Mathf.Clamp(yRotation, -10f, 30f); // ���������ӽǷ�Χ

        //��ת�ļ���
        targetRotation = Quaternion.LookRotation(target.position + Vector3.up * bodyHeight
            - this.transform.position);

        targetRotation = Quaternion.Euler(yRotation, targetRotation.eulerAngles.y, 0f);

        this.transform.rotation = targetRotation;

        //this.transform.rotation = Quaternion.Slerp(this.transform.rotation,targetRotation,
        //    rotationSpeed*Time.deltaTime);
    }

    /// <summary>
    /// �������������Ŀ�����
    /// </summary>
    /// <param name="player"></param>
    public void SetTarget(Transform player)
    {
        target = player;
    }
}
