using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Lesson3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是文件流
        //在C#中提供了一个文件流类 FileStream类
        //它主要作用是用于读写文件的细节
        //我们之前学过的File只能整体读写文件
        //而FileStream可以以读写字节的形式处理文件

        //说人话：
        //文件里面存储的数据就像是一条数据流（数组或者列表）
        //我们可以通过FileStream一部分一部分的读写数据流
        //比如我可以先存一个int（4个字节）再存一个bool（1个字节）再存一个string（n个字节）
        //利用FileStream可以以流式逐个读写
        #endregion

        #region 知识点二 FileStream文件流类常用方法
        //类名：FileStream
        //需要引用命名空间：System.IO

        #region 1.打开或创建指定文件
        //方法一：new FileStream
        //参数一：路径
        //参数二：打开模式
        //  CreateNew:创建新文件 如果文件存在 则报错
        //  Create:创建文件，如果文件存在 则覆盖
        //  Open:打开文件，如果文件不存在 报错
        //  OpenOrCreate:打开或者创建文件根据实际情况操作
        //  Append:若存在文件，则打开并查找文件尾，或者创建一个新文件
        //  Truncate:打开并清空文件内容
        //参数三：访问模式
        //参数四：共享权限
        //  None 谢绝共享
        //  Read 允许别的程序读取当前文件
        //  Write 允许别的程序写入该文件
        //  ReadWrite 允许别的程序读写该文件
        //FileStream fs = new FileStream(Application.dataPath + "/Lesson3.tang", FileMode.Create, FileAccess.ReadWrite);

        //方法二：File.Create
        //参数一：路径
        //参数二：缓存大小
        //参数三：描述如何创建或覆盖该文件（不常用）
        //  Asynchronous 可用于异步读写
        //  DeleteOnClose 不在使用时，自动删除
        //  Encrypted 加密
        //  None 不应用其它选项
        //  RandomAccess 随机访问文件
        //  SequentialScan 从头到尾顺序访问文件
        //  WriteThrough 通过中间缓存直接写入磁盘
        //FileStream fs2 = File.Create(Application.dataPath + "/Lesson3.tang");

        //方法三：File.Open
        //参数一：路径
        //参数二：打开模式
        //FileStream fs3 = File.Open(Application.dataPath + "/Lesson3.tang", FileMode.Open);
        #endregion

        #region 2.重要属性和方法
        //FileStream fs = File.Open(Application.dataPath + "Lesson3.tang", FileMode.OpenOrCreate);
        //文本字节长度
        //print(fs.Length);

        //是否可写
        //if( fs.CanRead )
        //{

        //}

        //是否可读
        //if( fs.CanWrite )
        //{

        //}

        //将字节写入文件 当写入后 一定执行一次
        //fs.Flush();

        //关闭流 当文件读写完毕后 一定执行
        //fs.Close();

        //缓存资源销毁回收
        //fs.Dispose();

        #endregion

        #region 3.写入字节
        print(Application.persistentDataPath);
        using (FileStream fs = new FileStream(Application.persistentDataPath + "/Lesson3.tang", FileMode.OpenOrCreate, FileAccess.Write))
        {

            byte[] bytes = BitConverter.GetBytes(999);
            //方法：Write
            //参数一：写入的字节数组
            //参数二：数组中的开始索引
            //参数三：写入多少个字节
            fs.Write(bytes, 0, bytes.Length);

            //写入字符串时
            bytes = Encoding.UTF8.GetBytes("唐老狮哈哈哈哈");
            //先写入长度
            //int length = bytes.Length;
            fs.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
            //再写入字符串具体内容
            fs.Write(bytes, 0, bytes.Length);

            //避免数据丢失 一定写入后要执行的方法
            fs.Flush();
            //销毁缓存 释放资源
            fs.Dispose();
        }

        #endregion

        #region 4.读取字节

        #region 方法一：挨个读取字节数组

        using (FileStream fs2 = File.Open(Application.persistentDataPath + "/Lesson3.tang", FileMode.Open, FileAccess.Read))
        {
            //读取第一个整形
            byte[] bytes2 = new byte[4];
            //参数一：用于存储读取的字节数组的容器
            //参数二：容器中开始的位置
            //参数三：读取多少个字节装入容器
            //返回值：当前流索引前进了几个位置
            int index = fs2.Read(bytes2, 0, 4);
            int i = BitConverter.ToInt32(bytes2, 0);
            print("取出来的第一个整数" + i);//999
            print("索引向前移动" + index + "个位置");

            //读取第二个字符串
            //读取字符串字节数组长度
            index = fs2.Read(bytes2, 0, 4);
            print("索引向前移动" + index + "个位置");
            int length = BitConverter.ToInt32(bytes2, 0);
            //要根据我们存储的字符串字节数组的长度 来声明一个新的字节数组 用来装载读取出来的数据
            bytes2 = new byte[length];
            index = fs2.Read(bytes2, 0, length);
            print("索引向前移动" + index + "个位置");
            //得到最终的字符串 打印出来
            print(Encoding.UTF8.GetString(bytes2));
            fs2.Dispose();
        }

        #endregion

        #region 方法二：一次性读取再挨个读取
        print("***************************");
        using (FileStream fs3 = File.Open(Application.persistentDataPath + "/Lesson3.tang", FileMode.Open, FileAccess.Read))
        {
            //一开始就申明一个 和文件字节数组长度一样的容器
            byte[] bytes3 = new byte[fs3.Length];
            fs3.Read(bytes3, 0, (int)fs3.Length);
            fs3.Dispose();
            //读取整数
            print(BitConverter.ToInt32(bytes3, 0));
            //得去字符串字节数组的长度
            int length2 = BitConverter.ToInt32(bytes3, 4);
            //得到字符串
            print(Encoding.UTF8.GetString(bytes3, 8, length2));
        }
        
        #endregion

        #endregion

        #endregion

        #region 知识点三 更加安全的使用文件流对象
        //using关键字重要用法
        //using (申明一个引用对象)
        //{
        //使用对象
        //}
        //无论发生什么情况 当using语句块结束后 
        //会自动调用该对象的销毁方法 避免忘记销毁或关闭流
        //using是一种更安全的使用方法

        //强调：
        //目前我们对文件流进行操作 为了文件操作安全 都用using来进行处理最好

        #endregion

        #region 总结
        //通过FIleStream读写时一定要注意
        //读的规则一定是要和写是一致的
        //我们存储数据的先后顺序是我们制定的规则
        //只要按照规则读写就能保证数据的正确性
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
