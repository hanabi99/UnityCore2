using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Lesson4 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 文件夹操作是指什么？
        //平时我们可以在操作系统的文件管理系统中
        //通过一些操作增删查改文件夹

        //我们目前要学习的就是通过代码的形式
        //来对文件夹进行增删查改的操作
        #endregion

        #region 知识点二 C#提供给我们的文件夹操作公共类
        //类名:Directory
        //命名空间：using System.IO
        //1.判断文件夹是否存在
        if( Directory.Exists(Application.dataPath + "/数据持久化四"))
        {
            print("存在文件夹");
        }
        else
        {
            print("文件夹不存在");
        }

        //2.创建文件夹
        DirectoryInfo info = Directory.CreateDirectory(Application.dataPath + "/数据持久化四");

        //3.删除文件夹
        //参数一：路径
        //参数二：是否删除非空目录，如果为true，将删除整个目录，如果是false，仅当该目录为空时才可删除
        //Directory.Delete(Application.dataPath + "/数据持久化四");

        //4.查找文件夹和文件
        //得到指定路径下所有文件夹名
        string[] strs = Directory.GetDirectories(Application.dataPath);
        for (int i = 0; i < strs.Length; i++)
        {
            print(strs[i]);
        }

        //得到指定路径下所有文件名
        strs = Directory.GetFiles(Application.dataPath);
        for (int i = 0; i < strs.Length; i++)
        {
            print(strs[i]);
        }

        //5.移动文件夹
        //如果第二个参数所在的路径 已经存在了一个文件夹 那么会报错
        //移动会把文件夹中的所有内容一起移到新的路径
        //Directory.Move(Application.dataPath + "/数据持久化四", Application.dataPath + "/123123123");
        #endregion

        print("**************************");
        #region 知识点三 DirectoryInfo和FileInfo
        //DirectoryInfo目录信息类
        //我们可以通过它获取文件夹的更多信息
        //它主要出现在两个地方
        //1.创建文件夹方法的返回值
        DirectoryInfo dInfo = Directory.CreateDirectory(Application.dataPath + "/数据持久化123");
        //全路径
        print(dInfo.FullName);
        //文件名
        print(dInfo.Name);

        //2.查找上级文件夹信息
        dInfo = Directory.GetParent(Application.dataPath + "/数据持久化123");
        //全路径
        print(dInfo.FullName);
        //文件名
        print(dInfo.Name);

        //重要方法
        //得到所有子文件夹的目录信息
        DirectoryInfo[] dInfos = dInfo.GetDirectories();

        //FileInfo文件信息类
        //我们可以通过DirectoryInfo得到该文件下的所有文件信息
        FileInfo[] fInfos = dInfo.GetFiles();
        for (int i = 0; i < fInfos.Length; i++)
        {
            print("**************");
            print(fInfos[i].Name);//文件名
            print(fInfos[i].FullName);//路径
            print(fInfos[i].Length);//字节长度
            print(fInfos[i].Extension);//后缀名
        }

        #endregion

        #region 总结
        //Directory提供给我们了常用的文件夹相关操作的API
        //只需要熟练使用它即可

        //DirectoryInfo和FileInfo 一般在多文件夹和多文件操作时会用到
        //了解即可
        //目前用的相对较少 他们的用法和Directory和File类的用法大同小异
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
