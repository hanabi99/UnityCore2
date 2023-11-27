using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IItemBase<T>
{
    public void InitInfo(T info);
}
/// <summary>
/// 
/// </summary>
/// <typeparam name="T">数据来源类</typeparam>
/// <typeparam name="K">数据UI类</typeparam>
public class CustomSV<T, K> where K : IItemBase<T>
{
    private int ViewHigh;

    private RectTransform Content;

    public Dictionary<int, GameObject> nowShowItems = new Dictionary<int, GameObject>();

    private int oldMinIndex = -1;
    private int oldMaxIndex = -1;

    //数据列表
    public List<T> items;

    //格子间隔宽高 : 注意是间隔宽高
    private int itemW;
    private int itemH;

    //格子的列数
    private int col;

    //动态加载的资源路径
    private string resName;


    public bool isExpand;


    public void InitItemInfo(int itemW, int itemH, int col, string resName)
    {
        this.itemW = itemW;
        this.itemH = itemH;
        this.col = col;
        this.resName = resName;
    }

    /// <summary>
    /// 初始化 Content的位置 和 ViewPort的高，和Content的位置的宽
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="ViewH"></param>
    /// <param name="ContentW"></param>
    public void InitContentAndSVH(RectTransform trans, int ViewH, int ContentW)
    {
        if (isExpand)
        {
            this.Content = trans;
            Content.sizeDelta = new Vector2(ContentW, ViewH);
            this.ViewHigh = ViewH;
        }
        else
        {
            this.Content = trans;
            Content.sizeDelta = new Vector2(ContentW, Mathf.CeilToInt((items.Count / col)) * itemH);
            this.ViewHigh = ViewH;
        }
    }

    /// <summary>
    /// 更新Content大小
    /// </summary>
    /// <param name="ContentW"></param>
    public void UpdateContentSize(int ContentW)
    {
        Content.sizeDelta = new Vector2(ContentW,Mathf.CeilToInt((items.Count / col) * itemH));
    }


    /// <summary>
    /// 初始化高 如果是实时扩展高度就不用
    /// </summary>
    /// <param name="items"></param>
    public void InitData(List<T> items)
    {
        this.items = items;
    }

    public void CheckShowOrHide()
    {

        //显示第一个格子和最后一个格子的索引
        int minIndex = (int)(Content.anchoredPosition.y / itemH) * col;
        int maxIndex = (int)((Content.anchoredPosition.y + ViewHigh) / itemH) * col + col - 1;

        if(Content.anchoredPosition.y < 0)
        {
            return;
        }

        if (minIndex < 0)
        {
            minIndex = 0;
        }

        if (isExpand == true)
        {
            if (maxIndex >= items.Count)
            {
                maxIndex = items.Count - 1;
            }
        }
        else
        {
            if (maxIndex > items.Count)
            {
                maxIndex = items.Count - 1;
            }
        }

        if (minIndex != oldMinIndex || maxIndex != oldMaxIndex)
        {
            //向上拖动
            for (int i = oldMinIndex; i < minIndex; ++i)
            {
                if (nowShowItems.ContainsKey(i))
                {
                    if (nowShowItems[i] != null)
                    {
                        PoolMgr.GetInstance().PushObj(resName, nowShowItems[i]);
                    }
                    nowShowItems.Remove(i);
                }
            }
            //向下拖动
            for (int i = maxIndex + 1; i <= oldMaxIndex; ++i)
            {
                if (nowShowItems.ContainsKey(i))
                {
                    if (nowShowItems[i] != null)
                    {
                        PoolMgr.GetInstance().PushObj(resName, nowShowItems[i]);
                    }
                    nowShowItems.Remove(i);
                }
            }
        }


        oldMinIndex = minIndex;
        oldMaxIndex = maxIndex;

        //Debug.Log("min" + minIndex);

        //Debug.Log("max" + maxIndex);

        for (int i = minIndex; i <= maxIndex; ++i)
        {
            if (nowShowItems.ContainsKey(i))
            {
                continue;
            }
            else
            {
                int index = i;

                nowShowItems.Add(index, null);
                PoolMgr.GetInstance().GetObj(resName, (obj) =>
                {
                    obj.transform.SetParent(Content);

                    obj.transform.localScale = Vector3.one;
                    obj.transform.localPosition = new Vector3((index % col) * itemW, -index / col * itemH, 0);

                    obj.GetComponent<K>().InitInfo(items[index]);

                    if (nowShowItems.ContainsKey(index))//由于滑的太快  因为是异步操作 所以可能已经被移除字典了 
                    {
                        nowShowItems[index] = obj;
                    }

                    else
                    {
                        PoolMgr.GetInstance().PushObj(resName, obj);
                    }
                }

                );
            }

        }
    }
}


