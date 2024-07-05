# playerMove.cs

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 nowMoveDir = Vector3.zero;

    public float moveSpeed = 10;
    public float rotateSpeed = 40;
  
    // Update is called once per frame
    void Update()
    {
        if(nowMoveDir != Vector3.zero)
        {
            //朝面朝向移动
            this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            //不停的朝目标方向 转向
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(nowMoveDir), rotateSpeed * Time.deltaTime);
        }
    }

    public void Move(Vector2 dir)
    {
        nowMoveDir.x = dir.x;
        nowMoveDir.y = 0;
        nowMoveDir.z = dir.y;
    }
}

```

