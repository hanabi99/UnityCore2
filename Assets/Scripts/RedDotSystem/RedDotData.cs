using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BagData
{
    public int id;
    public int count;
    public bool isRead;
}
public class RedDotData {
    //商城
    public static bool Store_Gold_isRead = false;

    public static bool Store_Diamand_isRead = false;


    //任务

    public static bool Task_Box1_isRead = false;

    public static bool Task_Box2_isRead = false;

    public static bool Task_Box3_isRead = false;
    //背包

    public static List<BagData> bagRedItems = new List<BagData>();

}
