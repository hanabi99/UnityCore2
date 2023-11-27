using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public int num;
    public Item()
    {

    }
    public Item(int id, int num)
    {
        this.id = id;
        this.num = num;
    }
}
public class BagMgr : BaseManager<BagMgr>
{
    [HideInInspector]
    public List<Item> items = new List<Item>(100000);



    public void Init()
    {
        for (int i = 0; i < 100000; i++)
        {
            Item item = new Item();
            item.id = i;
            item.num = i;

            items.Add(item);

        }
    }
}
