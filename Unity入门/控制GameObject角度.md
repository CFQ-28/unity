# 控制GameObject角度

在Unity中，改变GameObject的欧拉角（Euler angles）通常通过其Transform组件来实现。以下是几种常见的方法及其详细说明：

### 1. 直接设置Transform.eulerAngles

你可以直接设置GameObject的`Transform.eulerAngles`来改变其旋转。

```csharp
using UnityEngine;

public class SetEulerAngles : MonoBehaviour
{
    public Vector3 targetEulerAngles;

    void Update()
    {
        transform.eulerAngles = targetEulerAngles;
    }
}
```

### 2. 使用Transform.Rotate方法

`Transform.Rotate`方法根据给定的轴和角度增量旋转GameObject。

```csharp
using UnityEngine;

public class RotateWithEulerAngles : MonoBehaviour
{
    public float rotationSpeed = 100.0f;

    void Update()
    {
        float rotateHorizontal = Input.GetAxis("Horizontal");
        float rotateVertical = Input.GetAxis("Vertical");

        Vector3 rotation = new Vector3(rotateVertical, rotateHorizontal, 0);
        
        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
    }
}
```

### 3. 使用Quaternion.Euler方法

`Quaternion.Euler`方法将欧拉角转换为四元数，然后设置`Transform.rotation`。

```csharp
using UnityEngine;

public class SetRotationWithEuler : MonoBehaviour
{
    public Vector3 targetEulerAngles;

    void Update()
    {
        Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);
        transform.rotation = targetRotation;
    }
}
```

### 4. 使用Quaternion.Lerp进行插值旋转

`Quaternion.Lerp`方法在两个旋转之间进行线性插值。

```csharp
using UnityEngine;

public class RotateWithLerpEuler : MonoBehaviour
{
    public Vector3 targetEulerAngles;
    public float rotationSpeed = 1.0f;

    void Update()
    {
        Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
```

### 5. 使用Quaternion.Slerp进行球形插值旋转

`Quaternion.Slerp`方法在两个旋转之间进行平滑的球形插值。

```csharp
using UnityEngine;

public class RotateWithSlerpEuler : MonoBehaviour
{
    public Vector3 targetEulerAngles;
    public float rotationSpeed = 1.0f;

    void Update()
    {
        Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
```

### 6. 使用Rigidbody.MoveRotation（物理驱动）

当你的GameObject具有Rigidbody组件时，使用`Rigidbody.MoveRotation`方法可以实现平滑的物理旋转。

```csharp
using UnityEngine;

public class RotateWithRigidbodyEuler : MonoBehaviour
{
    public float rotationSpeed = 100.0f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float rotateHorizontal = Input.GetAxis("Horizontal");
        float rotateVertical = Input.GetAxis("Vertical");

        Vector3 rotation = new Vector3(rotateVertical, rotateHorizontal, 0) * rotationSpeed * Time.fixedDeltaTime;

        Quaternion deltaRotation = Quaternion.Euler(rotation);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
```

### 示例代码总结

以下是一个综合示例，展示了如何使用不同方法改变GameObject的欧拉角：

```csharp
using UnityEngine;

public class RotationExamplesEuler : MonoBehaviour
{
    public float rotationSpeed = 100.0f;
    public Vector3 targetEulerAngles;
    public Transform target;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 1. 直接设置Transform.eulerAngles
        transform.eulerAngles = targetEulerAngles;

        // 2. 使用Transform.Rotate方法
        float rotateHorizontal = Input.GetAxis("Horizontal");
        float rotateVertical = Input.GetAxis("Vertical");

        Vector3 rotation = new Vector3(rotateVertical, rotateHorizontal, 0);
        transform.Rotate(rotation * rotationSpeed * Time.deltaTime);

        // 3. 使用Quaternion.Euler方法
        Quaternion targetRotation = Quaternion.Euler(targetEulerAngles);
        transform.rotation = targetRotation;

        // 4. 使用Quaternion.Lerp进行插值旋转
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // 5. 使用Quaternion.Slerp进行球形插值旋转
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        // 6. 使用Rigidbody.MoveRotation
        if (rb != null)
        {
            float rotateHorizontal = Input.GetAxis("Horizontal");
            float rotateVertical = Input.GetAxis("Vertical");

            Vector3 rotation = new Vector3(rotateVertical, rotateHorizontal, 0) * rotationSpeed * Time.fixedDeltaTime;

            Quaternion deltaRotation = Quaternion.Euler(rotation);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }
}
```

通过这些方法，你可以根据具体的需求选择合适的方式来控制GameObject的旋转和欧拉角变化，达到你想要的效果。