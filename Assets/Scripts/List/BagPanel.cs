using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPanel : BasePanel
{
    public int ViewHigh;

    public RectTransform Content;

    public Button AddBtn;

    CustomSV<Item, BagItem> sV;

    public Dictionary<int, GameObject> nowShowItems = new Dictionary<int, GameObject>();




    private void Start()
    {
        sV = new CustomSV<Item, BagItem>();
        sV.isExpand = true;
       // BagMgr.GetInstance().Init();
        sV.InitData(BagMgr.GetInstance().items);
        sV.InitItemInfo(300, 200, 2,"UI/Item");
        sV.InitContentAndSVH(Content, ViewHigh, 600);
        AddBtn.onClick.AddListener(AddItem);
    }

    public void AddItem()
    {
        sV.items.Add(new Item(sV.items.Count, sV.items.Count));
        sV.UpdateContentSize(600);
        Debug.Log("列表长度" + sV.items.Count);
    }
  
    private void Update()
    {
        sV.CheckShowOrHide();
    }
   

}
