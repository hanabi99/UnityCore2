using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagItem : BasePanel,IItemBase<Item>
{
    public void InitInfo(Item item)
    {
        GetControl<Text>("num").text = item.num.ToString();
    }
}
