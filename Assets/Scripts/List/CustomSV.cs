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
/// <typeparam name="T">������Դ��</typeparam>
/// <typeparam name="K">����UI��</typeparam>
public class CustomSV<T, K> where K : IItemBase<T>
{
    private int ViewHigh;

    private RectTransform Content;

    public Dictionary<int, GameObject> nowShowItems = new Dictionary<int, GameObject>();

    private int oldMinIndex = -1;
    private int oldMaxIndex = -1;

    //�����б�
    public List<T> items;

    //���Ӽ����� : ע���Ǽ�����
    private int itemW;
    private int itemH;

    //���ӵ�����
    private int col;

    //��̬���ص���Դ·��
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
    /// ��ʼ�� Content��λ�� �� ViewPort�ĸߣ���Content��λ�õĿ�
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
    /// ����Content��С
    /// </summary>
    /// <param name="ContentW"></param>
    public void UpdateContentSize(int ContentW)
    {
        Content.sizeDelta = new Vector2(ContentW,Mathf.CeilToInt((items.Count / col) * itemH));
    }


    /// <summary>
    /// ��ʼ���� �����ʵʱ��չ�߶ȾͲ���
    /// </summary>
    /// <param name="items"></param>
    public void InitData(List<T> items)
    {
        this.items = items;
    }

    public void CheckShowOrHide()
    {

        //��ʾ��һ�����Ӻ����һ�����ӵ�����
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
            //�����϶�
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
            //�����϶�
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

                    if (nowShowItems.ContainsKey(index))//���ڻ���̫��  ��Ϊ���첽���� ���Կ����Ѿ����Ƴ��ֵ��� 
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


