# Lua笔记

Lua是一种轻量级的脚本语言，由标准C语言编写并以源代码形式开放。它的设计目的是为了嵌入应用程序中，从而为应用程序提供灵活的扩展和定制功能。

Lua具有以下特性：

轻量级：它用标准C语言编写并以源代码形式开放，编译后仅仅一百余K，可以很方便地嵌入别的程序里。

可扩展：Lua提供了非常易于使用的扩展接口和机制：由宿主语言 (通常是C或C++)提供这些功能，Lua可以使用它们，就像是本来就内置的功能一样。

其他特性：支持面向过程 (procedure-oriented)编程和函数式编程 (functional programming)；自动内存管理；只提供了一种通用类型的表（table），用它可以实现数组，哈希表，集合，对象； 语言内置模式匹配；闭包 (closure)；函数也可以看做一个值；提供多线程（协同进程，并非操作系统所支持的线程）支持； 通过闭包和table可以很方便地支持面向对象编程所需要的一些关键机制，比如数据抽象，虚函数，继承和重载等。

在Unity开发中，Lua一般会被使用于热更新和热重载。 

## 1 基本语法

;分号是可以省略的，建议不写 

```lua
print("Hello World！");

print("www.runoob.com")
```

## 2 注释

### 2.1 单行注释  两个减号是单行注释:

```lua
--单行注释 
```

### 2.2 多行注释 

```lua
--[[
第一种
多行注释
]]--

--[[
第二种
多行注释
]]

--[[
第三种
多行注释
--]] 
```

## 3 全局变量

**在默认情况下，变量总是认为是全局的**。

全局变量不需要声明，给一个变量赋值后即创建了这个全局变量，**访问一个没有初始化的全局变量也不会出错，只不过得到的结果是：nil。** 

```lua
> print(b)
nil

> b=10
> print(b)
10 
```

**如果你想删除一个全局变量，只需要将变量赋值为nil。**

```lua
b = nil
print(b)      --> nil 
```

## 4 数据类型

Lua 是动态类型语言，**变量不要类型定义**,只需要为变量赋值。 值可以存储在变量中，作为参数传递或结果返回。

Lua 中有 8 个基本类型分别为：

4个基本类型：**nil、boolean、number、string**

4个复杂类型：**userdata、function、thread 和 table**

数据类型描述nil这个最简单，只有值nil属于该类，表示一个无效值（在条件表达式中相当于false）有点类似C#中的null。

boolean包含两个值：false和true。

number表示双精度类型的实浮点数

string字符串由一对双引号或单引号来表示

function由 C 或 Lua 编写的函数

userdata表示任意存储在变量中的C数据结构

thread表示执行的独立线路，用于执行协同程序

table：Lua 中的表（table）其实是一个"关联数组"（associative arrays），数组的索引可以是数字、字符串或表类型。在 Lua 里，table 的创建是通过"构造表达式"来完成，最简单构造表达式是{}，用来创建一个空表。 

### 4.1 简单变量类型

**nil, number, string, boolean**

### 4.2 复杂数据类型
函数 function

表 table

数据结构 userdata：

userdata 是一种用户自定义数据，用于表示一种由应用程序或 C/C++ 语言库所创建的类型，可以将任意 C/C++ 的任意数据类型的数据（通常是 struct 和 指针）存储到 Lua 变量中调用。


协同程序 thread：

在 Lua 里，最主要的线程是协同程序（coroutine）。它跟线程（thread）差不多，拥有自己独立的栈、局部变量和指令指针，可以跟其他协同程序共享全局变量和其他大部分东西。

线程跟协程的区别：线程可以同时多个运行，而协程任意时刻只能运行一个，并且处于运行状态的协程只有被挂起（suspend）时才会暂停。

### 4.3 变量声明
**lua中所有的变量声明 都不需要申明变量类型，它会自动判断变量类型，类似C#中的 var**

**lua中的一个变量 可以随便赋值，自动识别类型**

**我们可以使用 type 函数测试给定变量或者值的类型：** 

```lua
print(type("Hello world"))    --> string
print(type(10.4*3))       --> number
print(type(print))        --> function
print(type(type))        --> function
print(type(true))        --> boolean
print(type(nil))         --> nil
print(type(type(X)))       --> string，因此type函数返回值是string 
```

nil 类型表示一种没有任何有效值，它只有一个值 -- nil，例如打印一个没有赋值的变量，**便会输出一个 nil 值**：

```lua
> print(type(a))
nil
--lua中使用没有声明过的变量，不会报错 因为默认值都是nil 
```

对于全局变量和 table，nil 还有一个"删除"作用，给全局变量或者 table 表里的变量赋一个 nil 值，等同于把它们删掉，执行下面代码就知 

```lua
tab1 = { key1 = "val1", key2 = "val2", "val3" }
for k, v in pairs(tab1) do
 print(k .. " - " .. v)
end

tab1.key1 = nil
for k, v in pairs(tab1) do
 print(k .. " - " .. v)
end 
```

nil 作比较时应该加上双引号 "：

```lua
>type(X)
nil

>type(X)==nil
false

>type(X)=="nil"
true
```

**type(X)==nil 结果为 false 的原因是 type(X) 实质是返回的 "nil" 字符串，是一个 string 类型**：

```lua
type(type(X))==string
```

#### 4.3.1 字符串
**字符串的声明**，使用单引号或者双引号包裹，lua里没有char

```lua
a = "12312"
print(a)
a = '123'
print(a)
```

**获取字符串的长度**

```lua
s = "aBcdE字符串"
--一个汉字占3个长度
--一个英文字符占1个长度
print(#s)
--在字符串变量前加上#就代表长度
```

**字符串多行打印**

```lua
--第一种
--lua中支持转义字符
print("123\n123")
--第二种
s = [[我是
你好
kkk
]]
print(s)
--第三种
print([[我是
   你是
   ]])
```

**字符串拼接**

```lua
--第一种 字符串拼接 使用..
print("123".."456")
s1 = "123"
s2 = 111
print(s1..s2)
--无论是什么类型 只要使用..就是字符串拼接
--第二种
print(string.format("你好，啦啦啦啦%d次"， 10))
--%d 与数字配对拼接
--%a 与任何字符配对拼接
--%s 与字符配对
--......
```

**别的类型转成字符串**

```lua
a = true
print(tostring(a))
```

**字符串提供的公共方法，大部分方法不会对原字符串造成影响**

```lua
str = "abCdefg"
--小写转大写
print(string.upper(str))
--大写转小写
print(string.lower(str))
--翻转字符串
print(string.reverse(str))
--字符串查找索引，lua中字符串索引是从1开始的，返回开始索引和结束索引
print(string.find(str, "Cde"))
--截取字符串
print(string.sub(str, 3, 4))
--字符串重复，重复拼接n次
print(string.rep(sre, 2))
--字符串修改，会返回一个数字表示修改的次数
print(string.gsub(str, "Cd", "**"))

--字符转ASCⅡ码
a = string.byte("Lua", 1)
print(a)
--ASCⅡ码转字符
print(string.char(a))
```

## 5 运算符
### 5.1 算数运算符
+-*/%  在lua中没有自增自减 ++ --；没有复合运算符 += -= /= *= %=

```lua
print("加法运算符" .. 1 + 2)
a = 1
b = 2
print(a + b)
--字符串可以进行算术运算符操作，会自动转换成number
print("123.4" + ) -->124.4

print("减法运算" .. 1 - 2)
print("123.4" - 1) -->122.4

print("乘法运算" .. 1 * 2)
print("123.4" * 2)

print("除法运算" .. 1 / 2)
print("123.4" / 2)

print("取余运算" .. 1 % 2)
print("123.4" % 2)

--^ lua中 该符号是 幂运算
print("幂运算" .. 2 ^ 2)
print("123.4" ^ 2)
```

### 5.2 条件运算符

```lua
-- > < >= <= == ~=
print(3>1)
print(3<1)
print(3>=1)
print(3<=1)
print(3==1)
--不等于是 ~=
print(3~=1)
```

### 5.3 逻辑运算符

```lua
-- C#: &&  ||  !  "短路"
-- lua:and or not  lua中也遵循逻辑运算的“短路”规则
print(true and false)
print(true and true)
print(true or false)
print(false or false)

print(not true)
print(true and print("123"))
```

### 5.4位运算符
-- & | 不支持位运算符 需要我们自己实现

### 5.5 三目运算符
-- ? : lua中 也不支持三目运算

## 6 条件分支语句
```lua
--if 条件 then ..... end
--单分支
if a > 5 then
   print("123")
end

--双分支
if a < 5 then
   print("123")
else
   print("321")
end

--多分支
if a < 5 then
   print("123")
--注意 lua中 elseif 一定是连接着的
elseif a == 9 then
   print("6")
elseif a == 8 then
   print("5")
else
   print("321")
end

--lua中没有 switch语句 需要自己实现
```

## 7 循环语句

```lua
--while语句
num = 0
--while 条件 do ..... end
while num < 5 do
   print(num)
   num = num + 1
end

--do while语句
--repeat ..... until 结束条件
repeat
   print(num)
   num = num + 1
until num > 5 --满足条件跳出 结束

--for语句
for i = 2,5 do --默认递增 i会默认+1
   print(i)
end

for i = 1,5,2 do --如要自定义增量 直接逗号后面写
   print(i)
end
```

## 8 函数

```lua
function 函数名()
end

a = function()
end
```

### 8.1 无参数无返回值

```lua
function F1()
   print("F1")
end
F1()
--有点类似 C# 中的 委托和事件
F2 = function()
   print("F2")
end
F2()
```

### 8.2 有参数

```lua
function F3(a)
	print(a)
end
F3(1)
F3("123")
F3(true)
--如果你传入的参数 和函数参数个数不匹配
--不会报错 只会补空nil 或者 丢弃多余参数
F3()
F3(1,2,3)
```

### 8.3 有返回值

```lua
function F4(a)
   return a, "123", true
end
--多返回值时，在前面申明多个变量来获取即可
--如果变量不够 不影响 会根据顺序来逐个获取
--如果变量多了 不影响 会直接赋值nil
temp, temp2, temp3, temp4 = F4("1")
print(temp)
print(temp2)
print(temp3)
print(temp4)
```

### 8.4 函数的类型

```lua
--函数类型 就是 function
F5 = function()
   print("123")
end
print(type(F5))
```

### 8.5 函数的重载

```lua
--函数名相同 参数类型不同 或者参数个数不同
--lua中 函数不支持重载
--默认会调用最后一个声明的函数
function F6()
   print("123")
end
function F6(str)
   print(str)
end

F6()
```

### 8.6 变长参数

```lua
function F7( ... )
   --变长参数使用 先用一个表存起来 再使用
arg = { ... }
   for i = 1, #arg do
       print(arg[i])
   end
end
F7(1, "123", true,4,5,6)
```

### 8.7 函数嵌套

```lua
function F8()
F9 = function()
       print(123)
   end
   return F9
   --或者
   return function()
       print(123)
   end
end

f9 = F8()
f9()
```



### 8.8 闭包

```lua
function F9(x)
   --改变传入参数的生命周期
   return function(y)
       return x + y
   end
end

f10 = F9(10)
print(f10(5))
```

## 9 复杂数据类型——表

### 9.1 所有的复杂类型都是table（表）

```lua
a = { 1,2,3,4,"123",true,nil }
--lua中索引从1开始
print(a[1])
print(a[5])
print(a[6])
--#是通用的获取长度的关键字
--在打印长度的时候 空nil会被忽略并截断
--如果表中（数组中）某一位变成nil 会影响#获取的长度
print(#a)
```

### 9.2 数组的遍历

```lua
--通过表长的遍历不是非常可靠的遍历方法
for i = 1,#a do
   print(a[i])
end
```

### 9.3 二维数组

```lua
a = {{1,2,3},{4,5,6}}
print(a[1][1])
print(a[1][2])
print(a[1][3])
```

### 9.4 二维数组的遍历

```lua
for i=1,#a do
   b = a[i]
   for j=1,#b do
       print(b[j])
   end
end
```

### 9.5 自定义索引

```lua
aa = {[0]=1,2,3,[-1]=4,5}
print(aa[0])
print(aa[-1])
print(#aa)
--自定义索引跳跃性设置，如果只挑一格 长度受自定义索引最大值影响
```

### 9.6 迭代器遍历

**迭代器遍历，主要是用来遍历表的**

**#得到的长度并不准确，所以一般不要用#来遍历表**

```lua
a = {[0] = 1,2,[-1] = 3,4,5}

--ipairs遍历，还是从1开始往后遍历的，小于等于0的索引得不到
--只能找到连续索引的 键值 如果中间断序了 它也无法遍历出后面的内容
for i,k in ipairs(a) do
   print("ipairs遍历键值"..i.."_"..k)
end

--pairs遍历
--建议使用它遍历各种不规则的表
--它可以得到所有信息
for i,v in pairs(a) do
   print("pairs遍历键值"..i.."_"..v)
end

for i in pairs(a) do
   print("pairs遍历键"..i)
end
```

## 10 复杂数据类型——表2

### 10.1 字典

**字典的本质还是表**

#### 10.1.1 字典的声明

**字典是由键值对构成的**

```lua
a = {["name"] = "123", ["age"] = 14, ["1"] = 5}
--访问单个变量 用中括号填键 来访问
print(a["name"])
print(a["age"])
print(a["1"])
--还可以类似 .成员变量的形式得到值
print(a.name)
print(a.age)
print(a.1) --但是不能使用数字
--修改
a["name"] = "TLS"
print(a.name)
print(a["name"])
--新增
a["sex"] = false
print(a.sex)
print(a["sex"])
--删除
a["sex"] = nil
print(a.sex)
print(a["sex"])
```

#### 10.1.2 字典的遍历

```lua
--如果要模拟字典，遍历一定要用pairs
for k,v in pairs(a) do
   --可以传多个参数 一样可以打印出来
   print(k,v)
end

for k in pairs(a) do
   print(k)
   print(a[k])
end
--没办法只用一个变量去遍历值
for _,v in pairs(a) do
   print(v)
end
```

### 10.2 类和结构体

**Lua中是默认没有面向对象的，需要我们自己来实现**

```lua
--成员变量 成员函数.....
Student =
{
   --年龄
   age = 1,
   --性别
   sex = true,
   --成长函数
   Up = function()
       print(age) --这样写 这个age和表中age没有任何关系
       print(Student.age) --想要在内部函数中 调用表本身的属性或方法 一定要指定是谁的 所以要使用表名.
       print("我成长了")
   end,
   --学习函数
   Learn = function(t)
       --第二种 能够在函数内部调用自己属性或者方法的 方法
       --把自己作为一个参数传进来 在内部访问
       print(t.sex)
       print("好好学习")
   end
}
--Lua中 .和:的区别
Student.Learn(Student)
--:调用方法 会默认把调用者 作为第一个参数传入方法中
Student:Learn()

--申明表过后，在表外去申明表有的变量和方法
Student.name = "hhc"
Student.Speak = function()
   print("说话")
end
--C#中要使用类 需要实例化new对象 静态直接使用
--Lua中类的表现 更像是一个类中有很多 静态变量和函数
print(Student.age)
Student.Up()
Student.Speak()
--声明函数的第三个方法
function Student:Speak2()
   --lua中 有一个关键字 self 表示 默认传入的第一个参数，self和this完全不一样
   print(self.name .. "说话2")
end
```

### 10.3 表的公共操作

```lua
t1 = { { age = 1, name = "123" }, { age = 2, name = "345" } }
t2 = { name = "hhc", sex = true }

--插入
print(#t1)
table.insert(t1, t2)
print(#t1)
print(t1[1])
print(t1[2])
print(t1[3])
print(t1[3].sex)

--删除指定元素
--remove方法 传入表 会移除表的最后一个索引内容
table.remove(t1)
print(#t1)
print(t1[1].name)
print(t1[2].name)
print(t1[3])
--remove方法 传两个参数 第一个参数 是要移除内容的表 第二个参数是要移除内容的索引
table.remove(t1, 1)
print(t1[1].name)

--排序
t2 = {5,2,7,9,5}
--传入要排序的表 默认是降序排序
table.sort(t2)
for _,v in pairs(t2) do
   print(v)
end
--传入两个参数 第一个是用于排序的表 第二个是排序规则函数
table.sort(t2, function(a, b)
    if a> b then
           return true
       end
end )
for _,v in pairs(t2) do
   print(v)
end

--拼接
tb = { "123", "456", "789", "10101"}
--连接函数 用于拼接表中的元素 返回值 是一个字符串
str = table.contact(tb, ";")
print(str)
```

## 11 多Lua脚本执行

### 11.1 全局变量与本地变量

```lua
--全局变量
a = 1
b = "123"

for i = 1,2 do
   c = "hhc" --也是全局变量
end
print(c)

--本地（局部）变量 的关键字 local
for i = 1,2 do
   local d = "hhc"
   print("循环中" .. d)
end
print(d)

fun = function()
   local tt = "123123123"
end
fun()
print(tt)

local tt2 = "555"
print(tt2)
```

### 11.2 多脚本执行

```lua
print("Test测试")
testA = "123"
local testLocalA = "456"
--可以通过 return 将本地变量传递出去
return texstLocalA

--另一个脚本中
--关键字 require("脚本名") require('脚本名')
require("Test")
print(testA)
print(testLocalA) --不是全局变量 无法使用 访问为空

--卸载后，再执行接收返回值
local testLA = require("Test")
print(testLA)
```

### 11.3 脚本卸载

```lua
--如果是require加载执行的脚本 加载过一次过后不会再被执行
require('Test')
--package.loaded["脚本名"]
--返回值是boolean 意思是 该脚本是否被执行，true为已加载，nil为未加载
print(package.loaded["Test"])
--卸载已经执行过的脚本
package.loaded["Test"] = nil
print(package.loaded["Test"])
require("Test")
```

### 11.4 大G表

```lua
--_G表是一个总表（table）他将我们申明的所有全局的变量都存储在其中
for k,v in pairs(_G) do
   print(k,v)
end
--本地变量 加了local的变量是不会存到大_G表中的
```

## 12 特殊用法

### 12.1 多变量赋值

```lua
a, b, c = 1, 2, "123"
print(a)
print(b)
print(c)
--多变量赋值，如果后面的值不够 会自动补空
a,b,c = 1, 2
print(a)
print(b)
print(c) --nil
--多变量赋值，如果后面的值多了，会自动省略
a, b, c = 1, 2, 3, 4, 5
print(a)
print(b)
print(c)
```

### 12.2 多返回值

```lua
function Test()
   return 10, 20, 30, 40
end
--多返回值时 你用几个变量接收 就有几个值
--如果少了 就少接几个 如果多了 就自动补空
a, b, c = Test()
print(a)
print(b)
print(c)

a, b, c, d, e, f = Test()
print(a)
print(b)
print(c)
print(d)
print(e)
print(f)
```

### 12.3 and or

```lua
--逻辑与 逻辑或
-- and or 他们不仅可以连接 boolean 任何东西都可以用来连接
-- 在lua中 只有 nil 和 false 才认为是假
-- "短路"——对于and来说 有假则假 对于or来说 有真则真
-- 所以 他们只需判断 第一个是否满足 满足就会停止计算了，一直到最后的话 会将最后一个值返回出去
print( 1 and 2 )
print( 0 and 1 )
print( nil and 1 )
print( false and 2 )
print( true and 3 )
print( true or 1 )
print( false or 1 )
print( nil or 2 )

--lua不支持三目运算符
x = 3
y = 2
-- ? :
local res = (x>y) and x or y
print(res)
--(x>y) and x ——> x
-- x or y ——> x

--(x>y) and x ——> (x>y)
--(x>y) or y ——> y
```

## 13 协同程序

### 13.1 协程的创建

```lua
--常用方式
--coroutine.create()
fun = function()
   print(123)
end
co = coroutine.create(fun)
--协程本质是一个线程对象
print(co)
print(type(co))

--coroutine.wrap() 创建协程的第二种方式
co2 = coroutine.wrap(fun)
--这种方法创建出来的是一种函数
print(co2)
print(type(co2))
```

### 13.2 协程的运行

```lua
--第一种方式 对应的是 通过create创建的协程
coroutine.resume(co)
--第二种方式
co2()
```

### 13.3 协程的挂起

```lua
fun2 = function()
   local i = 1
   while true do
       print(i)
       i = i + 1
       --协程的挂起函数
       coroutine.yield(i)
    end
end

co3 = coroutine.create(fun2)
--默认第一个返回值 是 协程是否启动成功
--第二个是yield里面的返回值
isOk, temp1 = coroutine.resume(co3)
print(isOk, temp1)
isOk, temp1 = coroutine.resume(co3)
print(isOk, temp1)

co4 = coroutine.wrap(fun2)
--这种方式的协程调用 也可以有返回值 只是没有默认第一个返回值了
print("返回值"..co4())
print("返回值"..co4())
print("返回值"..co4())
```

### 13.4 协程的状态

```lua
--coroutine.status(协程对象)
--dead 结束
--suspended 暂停
--running 进行中
print(coroutine.status(co))
print(coroutine.status(co3))

fun2 = function()
   local i = 1
   while true do
       print(i)
       i = i + 1
       print(coroutine.status(co3)) --进行中
       coroutine.yield(i)
    end
end

--这个函数可以得到当前正在 运行中的线程编号
print(coroutine.running())
```

## 14 元表

### 14.1 元表概念

任何表变量都可以作为另一个表变量的元表

任何表变量都可以有自己的元表（爸爸）

当我们子表中进行一些特定操作时 会执行元表中的内容

### 14.2 设置元表

```lua
meta = {}
myTable = {}
--设置元表函数
--第一个参数 子表 第二个参数 元表（爸爸）
setmetatable(myTable, meta)
```

### 14.3 特定操作

#### 14.3.1 特定操作 - __tostring

```lua
meta2 =
{
   --当子表要被当做字符串使用时 会默认调用这个元表中的tostring方法
   __tostring = function(t)
       return t.name
   end
}
myTable2 =
{
   name = "hhc2"
}
setmetatable(myTable2, meta2)
print(myTable2)
```

#### 14.3.2 特定操作 - __call

```lua
meta3 =
{
--当子表被当作一个函数来使用时 会默认调用这个__call中的方法
   --当希望传参数时 一定要记住 默认第一个参数 是调用者本身
   __call = function(a, b)
       print(a)
       print(b)
       print("i love u")
   end
}
myTable3 =
{
   name = "hhc3"
}
setmetatable(myTable3, meta3)
--把子表当作函数使用 就会调用元表的__call方法
myTable3(1)
```

#### 14.3.3 特定操作 - 运算符重载

```lua
meta4 =
{
   --相当于运算符重载 当子表使用+运算符时 会调用该方法
   --运算符+
   __add = function(t1, t2)
       return 5
   end

   --运算符 -
   __sub = function(t1, t2)
       return 0
   end

   --运算符 *
   __mul =
   --运算符 /
   __div =
   --运算符 %
__mod =
   --运算符 ^
   __pow =
   --运算符 ==  如果要用条件运算符 来比较两个对象 这两个对象的元表一定要一致 才能准确调用方法
   __eq =
   --运算符 <
   __lt =
   --运算符 <=
   __le =
   --运算符..
   __concat =
}
myTable4 = {}
setmetatable(myTable4, meta4)
myTable5 = {}

print(myTable4 + myTable5)
```

#### 14.3.4 特定操作 - __ index和 __ newIndex

```lua
meta6Father =
{
   age = 1
}
meta6Father.__index = meta6Father
meta6 =
{
   age = 1
   --__index = { age = 2}
}
--__index的赋值 尽量写在表外面来初始化 避免出错
meta6.__index = meta6
--meta6.__index = { age = 2}
myTable6 = {}
setmetatable(meta6, meta6Father)
setmetatable(myTable6, meta6)
--得到元表的方法
print(getmetatable(myTable6))
--rawget 当我们使用它时 会去找自己身上有没有这个变量 不会去元表找
--myTable6.age = 1
print(rawget(myTable6, "age"))

--__index 当子表中 找不到某一属性时 会到元表中 __index指定的表去找索引
print(myTable6.age)--newIndex 当赋值时，如果赋值一个不存在的索引
--那么会把这个值赋值到newindex所指的表中 不会修改自己
meta7 = {}
meta7.__newindex = {}
myTable7 = {}
setmetatable(myTable7, meta7)
myTable7.age = 1
print(myTable7.age)
print(meta7.__newindex.age)
--rawset 该方法会忽略newindex的设置 只会该自己的变量 不会修改元表
rawset(myTable, "age", 2)
```

## 15 面向对象

### 15.1 封装

```lua
--面向对象 类 其实都是基于 table来实现
--元表相关知识点
Object = {}
Object.id = 1

function Object:Test()
   print(self.id)
end

--冒号 是会自动将调用这个函数的对象 作为第一个参数传入的写法
function Object:new()
   --self 代表的是 我们默认传入的第一个参数
   --对象就是变量 返回一个新的变量
   --返回出去的内容 本质上就是表对象
   local obj = {}
   --元表知识__index 当找自己的变量 找不到时 就去找元表当中__index指向的内容
   self.__index = self
   setmetatable(obj, self)
   return obj
end

local myObj = Object:new()
print(myObj)
print(myObj.id)
myObj:Test()
--对空表中 申明一个新的属性 叫做id
myObj.id = 2
print(Object.id)
```

### 15.2 继承

```lua
--C# class 类型 : 继承类
--写一个用于继承的方法
function Object:subClass(className)
   -- _G知识点 是总表 所有声明的全局变量 都以键值对的形式存在其中
   _G[className] = {}
   --写相关继承的规则
   --用到元表
   local obj = _G[className]
   self.__index = self
   --子类 定义一个base属性 base属性代表父类
   obj.base = self
   setmetatable(obj, self)
end
--print(_G)
--_G["a"] = 1
--_G.b = "123"
--print(a, b)

Object:subClass("Person")
print(Person)
print(Person.id)

local p1 = Person:new()
print(p1.id)
p1.id = 100
print(p1.id)
p1:Test()

Object:subClass("Monster")
local m1 = Monster:new()
print(m1.id)
m1.id = 200
print(m1.id)
m1:Test()
```

### 15.3 多态

```lua
--相同行为 不同表象 就是多态
--相同方法 不同执行逻辑 就是多态
Object:subClass("GameObject")
GameObject.posX = 0
GameObject.posY = 0
function GameObject:Move()
   self.posX = self.posX + 1
   self.posY = self.posY + 1
   print(self.posX, self.posY)
end

GameObject:subClass("Player")
function Player:Move()
   --base 指的是 GameObject 表（类）
   --这种方式调用 相当于是把基类表 作为第一个参数传入了方法中
   --避免把基类表 传入到方法中 这样相当于就是公用一张表的属性了
   --我们如果要执行父类逻辑 我们不要直接使用冒号调用
   --要通过.调用 然后把自己传入第一个参数
   self.base.Move(self)
end

local p1 = Player:new()
p1:Move()
local p2 = Player:new()
p2:Move()
p1:Move()
```

## 16 Lua自带库

### 16.1 时间

```lua
--系统时间
print(os.time())
--传入参数 得到时间
print(os.time({ year = 2023, month = 9, day = 1}))
--os.date("*t")
local nowTime = os.date("*t")
for k,v in pairs(nowTime) do
   print(k, v)
end
print(nowTime.hour)
```

### 16.2 数学运算

```lua
--math
--绝对值
print(math.abs(-11))
--弧度转角度
print(math.deg(math.pi))
--三角函数 传弧度
print(math.cos(math.pi))
--向下 向上取整
print(math.floor(2.6))
print(math.ceil(5.2))
--最大最小值
print(math.max(1,2))
print(math.min(4,5))
--小数分离 分成整数部分和小数部分
print(math.modf(1.2))
--幂运算
print(math.pow(2,5))
--随机数 先设置随机数种子
math.randomseed(os.time())
print(math.random(100))
print(math.random(100))
--开方
print(math.sqrt(4))
```

### 16.3 路径

```lua
--lua脚本加载路径
print(package.path)
package.path = package.path .. ";C:\\"
print(package.path)
```

### 16.4 Lua垃圾回收

```lua
--垃圾回收
--collectgarbage
--获取当前lua占用内存数 K字节 用返回值*1024 就可以得到具体的内存占用字节数
test = { id = 1, name = "123123"}
print(collectgarbage("count"))
--lua中的机制和C#中垃圾回收机制很类似 解除羁绊 就是变垃圾
test = nil
--进行垃圾回收 理解有点像C#的 GC
collectgarbage("collect")
print(collectgarbage("count"))

--lua中 有自动定时进行GC的方法
--Unity中热更新开发 尽量不要去自动垃圾回收
```



## 17 如何在游戏中执行用户输入的代码

在游戏中执行用户输入的代码需要谨慎，因为这可能会带来安全风险。如果您确实想要实现这个功能，您可以考虑在游戏中嵌入一个脚本引擎，例如Lua或Python。这样，您可以在运行时执行用户输入的脚本代码。

首先，您需要在游戏中创建一个文本输入框，让用户可以输入代码。然后，您可以使用脚本引擎提供的API来执行用户输入的代码。例如，在Unity中使用xLua插件，您可以使用DoString方法来执行用户输入的Lua代码。

## 18 什么是XLua

xLua是一个在Unity中使用Lua语言的插件。它提供了一种简单的方法来在Unity项目中嵌入Lua脚本，并在运行时执行这些脚本。

xLua具有许多优点，包括简单易用、性能高效和跨平台支持。它还支持热更新，可以让您在不重新编译和发布游戏的情况下更新游戏逻辑。

使用xLua，您可以将游戏逻辑和界面逻辑分离，使用Lua脚本来实现游戏逻辑。这样，您可以更快地迭代游戏逻辑，并且可以在运行时动态更新游戏逻辑。

xLua提供了丰富的文档和示例，可以帮助您快速入门。如果您想要在Unity项目中使用Lua语言，那么xLua是一个不错的选择。

要使用xLua编写游戏逻辑，您需要先在Unity项目中安装xLua插件。安装完成后，您可以在Unity项目中创建Lua脚本文件，并使用Lua语言编写游戏逻辑。

xLua提供了一组API，可以让您在Lua脚本中访问Unity的功能。例如，您可以使用

```lua
CS.UnityEngine
```

命名空间来访问Unity的类和方法。您还可以使用

```lua
xlua.hotfix
```

方法来热更新C#代码。

下面是一个简单的示例，演示如何使用xLua编写游戏逻辑：

```lua
local Vector3 = CS.UnityEngine.Vector3
local transform = self.transform

-- 移动物体
local function move()
   transform.position = transform.position + Vector3(1, 0, 0)
end

-- 每帧调用
function update()
   move()
end
```


在这个示例中，我们定义了一个move函数，用于移动游戏物体。然后，在update函数中调用move函数，使物体每帧都移动一定距离。

## 19 如何安装XLua
要在Unity中安装和使用xLua，您可以按照以下步骤操作：

1.访问xLua的GitHub页面（https://github.com/Tencent/xLua），并下载最新版本的xLua插件。

2.将下载的xLua插件解压缩到您的Unity项目的Assets文件夹中。

3.在Unity编辑器中，打开您的项目，并确保xLua插件已经导入到项目中。

4.在Unity项目中创建一个C#脚本，用于初始化xLua环境。例如：

```c#
using UnityEngine;
using XLua;

public class XLuaManager : MonoBehaviour
{
   private LuaEnv luaEnv;

   void Start()
   {
       luaEnv = new LuaEnv();
       luaEnv.DoString("require 'main'");
   }

   void Update()
   {
       if (luaEnv != null)
       {
           luaEnv.Tick();
       }
   }

   void OnDestroy()
   {
       if (luaEnv != null)
       {
           luaEnv.Dispose();
           luaEnv = null;
       }
   }
}
```

在这个示例中，我们创建了一个XLuaManager类，用于管理xLua环境。在Start方法中，我们创建了一个LuaEnv实例，并执行了一个名为main的Lua脚本。在Update方法中，我们调用了luaEnv.Tick方法来更新xLua环境。最后，在OnDestroy方法中，我们释放了xLua环境。

1.在Unity项目中创建一个名为main.lua的文件，并使用Lua语言编写游戏逻辑。例如：

```c#
print("Hello, xLua!")
```

在这个示例中，我们创建了一个简单的Lua脚本，用于输出一条消息。

1.运行您的Unity项目，并检查控制台输出。如果一切正常，您应该能看到来自Lua脚本的输出。

## 20 如何在Lua中访问Unity的功能
在使用xLua的Unity项目中，您可以在Lua脚本中访问Unity的类和方法。xLua提供了一个全局的CS表，用于访问C#的命名空间和类型。

例如，要在Lua脚本中访问Unity的Vector3类，您可以使用以下代码：

```c#
local Vector3 = CS.UnityEngine.Vector3
```

在这个示例中，我们使用CS.UnityEngine.Vector3来访问Unity的Vector3类，并将其赋值给一个局部变量。然后，我们就可以在Lua脚本中使用这个局部变量来创建和操作Vector3对象。

除了访问Unity的类和方法外，您还可以在Lua脚本中访问Unity的组件和游戏对象。例如，要在Lua脚本中访问当前游戏对象的Transform组件，您可以使用以下代码：

```c#
local transform = self.transform
```

在这个示例中，我们使用self.transform来访问当前游戏对象的Transform组件，并将其赋值给一个局部变量。然后，我们就可以在Lua脚本中使用这个局部变量来操作Transform组件。

## 21 如何在Lua中处理输入事件
在使用xLua的Unity项目中，您可以在Lua脚本中处理输入事件。要做到这一点，您需要在C#脚本中监听输入事件，并将它们传递给Lua脚本。

例如，要在Lua脚本中处理鼠标点击事件，您可以在C#脚本中监听鼠标点击事件，并调用Lua脚本中定义的函数来处理这些事件。下面是一个简单的示例：

```c#
// C#脚本
using UnityEngine;
using XLua;

public class InputManager : MonoBehaviour
{
   public LuaFunction onMouseDown;

   void Update()
   {
       if (Input.GetMouseButtonDown(0))
       {
           onMouseDown?.Call();
       }
   }
}
```

在这个示例中，我们定义了一个InputManager类，用于管理输入事件。在Update方法中，我们监听鼠标点击事件，并调用Lua脚本中定义的onMouseDown函数来处理这些事件。

```lua
-- Lua脚本
local inputManager = CS.InputManager.Instance
inputManager.onMouseDown = function()
   print("Mouse button down!")
end
```

在这个示例中，我们获取了InputManager类的实例，并为其onMouseDown属性赋值了一个函数。当鼠标点击事件发生时，这个函数将被调用，并输出一条消息。

这就是在Lua脚本中处理输入事件的基本方法。您可以根据需要扩展这个方法，以支持更多类型的输入事件。