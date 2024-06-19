# Input鼠标键盘输入

在Unity中，`Input`类用于处理来自鼠标、键盘、触摸屏和其他输入设备的输入。以下是一些常用的鼠标和键盘输入处理方法和示例代码：

### 1. 鼠标输入

#### 1.1 获取鼠标位置

鼠标位置可以通过`Input.mousePosition`来获取，它返回一个`Vector3`，表示鼠标在屏幕上的位置。

```csharp
using UnityEngine;

public class MousePositionExample : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Debug.Log("Mouse Position: " + mousePosition);
    }
}
```

#### 1.2 检测鼠标按钮

`Input.GetMouseButtonDown(int button)`、`Input.GetMouseButton(int button)`和`Input.GetMouseButtonUp(int button)`分别用于检测鼠标按钮的按下、保持和释放状态。`button`参数表示鼠标按钮，0为左键，1为右键，2为中键。

```csharp
using UnityEngine;

public class MouseButtonExample : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left mouse button pressed");
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("Left mouse button held down");
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Left mouse button released");
        }
    }
}
```

#### 1.3 鼠标滚轮输入

鼠标滚轮输入可以通过`Input.GetAxis("Mouse ScrollWheel")`来获取，返回一个浮点数，表示滚轮滚动的距离。

```csharp
using UnityEngine;

public class MouseScrollExample : MonoBehaviour
{
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            Debug.Log("Mouse ScrollWheel: " + scroll);
        }
    }
}
```

### 2. 键盘输入

#### 2.1 检测键盘按键

`Input.GetKeyDown(KeyCode key)`、`Input.GetKey(KeyCode key)`和`Input.GetKeyUp(KeyCode key)`分别用于检测键盘按键的按下、保持和释放状态。

```csharp
using UnityEngine;

public class KeyboardInputExample : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key pressed");
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Space key held down");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Space key released");
        }
    }
}
```

#### 2.2 使用轴进行输入

Unity提供了一些默认的输入轴，例如水平轴和垂直轴，通过`Input.GetAxis`或`Input.GetAxisRaw`获取。

```csharp
using UnityEngine;

public class AxisInputExample : MonoBehaviour
{
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0.0f)
        {
            Debug.Log("Horizontal axis: " + horizontal);
        }

        if (vertical != 0.0f)
        {
            Debug.Log("Vertical axis: " + vertical);
        }
    }
}
```

### 3. 综合示例

以下是一个综合示例，展示了如何处理鼠标和键盘输入，并应用于对象移动和旋转。

```csharp
using UnityEngine;

public class InputExample : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 100f;

    void Update()
    {
        // 键盘输入
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);

        // 鼠标输入
        if (Input.GetMouseButton(1)) // 右键按住旋转
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector3 rotation = new Vector3(-mouseY, mouseX, 0) * rotateSpeed * Time.deltaTime;
            transform.Rotate(rotation, Space.Self);
        }

        // 鼠标滚轮缩放
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            Vector3 scaleChange = Vector3.one * scroll;
            transform.localScale += scaleChange;
        }

        // 鼠标位置检测
        Vector3 mousePosition = Input.mousePosition;
        Debug.Log("Mouse Position: " + mousePosition);
    }
}
```

通过这些示例，你可以处理各种鼠标和键盘输入，以控制游戏对象的行为。根据需要，可以扩展这些示例以适应不同的游戏逻辑和交互需求。