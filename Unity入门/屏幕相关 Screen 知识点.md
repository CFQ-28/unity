# 屏幕相关 Screen 知识点

参考网站：[深入了解Unity的Screen类：一份详细的技术指南(四)_unity 获取屏幕分辨率-CSDN博客](https://blog.csdn.net/qq_33795300/article/details/131700773)

在Unity中，`Screen`类提供了访问和控制屏幕相关信息和属性的方法。这些信息包括屏幕分辨率、刷新率、全屏模式等。以下是一些常用的`Screen`类的知识点和使用方法：

### 1. 屏幕分辨率和刷新率

- **获取屏幕分辨率**: 使用`Screen.width`和`Screen.height`来获取屏幕的宽度和高度，以像素为单位。

  ```csharp
  int screenWidth = Screen.width;
  int screenHeight = Screen.height;
  ```

- **获取屏幕刷新率**: 使用`Screen.currentResolution.refreshRate`来获取当前显示模式的刷新率。

  ```csharp
  int refreshRate = Screen.currentResolution.refreshRate;
  ```

### 2. 全屏模式和窗口模式

- **设置全屏模式**: 使用`Screen.SetResolution`方法设置游戏窗口的分辨率和是否全屏。

  ```csharp
  // 设置为全屏模式
  Screen.SetResolution(screenWidth, screenHeight, true);
  ```

- **退出全屏模式**: Unity中没有直接的API来退出全屏模式，但可以通过设置分辨率来切换窗口模式。

  ```csharp
  // 设置为窗口模式
  Screen.SetResolution(screenWidth, screenHeight, false);
  ```

### 3. 其他屏幕相关信息

- **获取DPI**: Unity中没有直接的API来获取设备的DPI（每英寸像素密度），可以根据屏幕分辨率和屏幕尺寸来估算。

- **获取当前显示模式**: 使用`Screen.currentResolution`来获取当前使用的分辨率和刷新率信息。

  ```csharp
  Resolution currentResolution = Screen.currentResolution;
  Debug.Log("Current Resolution: " + currentResolution.width + "x" + currentResolution.height);
  Debug.Log("Refresh Rate: " + currentResolution.refreshRate);
  ```

### 示例代码

以下是一个示例，展示了如何使用`Screen`类获取和设置屏幕分辨率，并在窗口模式和全屏模式之间切换：

```csharp
using UnityEngine;

public class ScreenExample : MonoBehaviour
{
    void Start()
    {
        // 获取屏幕分辨率和刷新率
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        int refreshRate = Screen.currentResolution.refreshRate;
        Debug.Log("Screen Resolution: " + screenWidth + "x" + screenHeight);
        Debug.Log("Refresh Rate: " + refreshRate);

        // 设置为全屏模式
        Screen.SetResolution(screenWidth, screenHeight, true);
    }

    void Update()
    {
        // 示例：按下 Esc 键切换为窗口模式
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            int screenWidth = Screen.width;
            int screenHeight = Screen.height;

            // 设置为窗口模式
            Screen.SetResolution(screenWidth, screenHeight, false);
        }
    }
}
```

通过`Screen`类，你可以轻松地获取和管理Unity游戏的屏幕相关信息，并根据需要设置全屏或窗口模式。这些功能非常有用，特别是在需要适应不同分辨率和显示模式的情况下。