using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagRedDotPanel : MyGameFrameWork.BasePanel
{
    public Button bagBtn;

    public Transform[]  itemGridTrans;

    public Button closeBtn;

    public GameObject BagWindow;

    public override void Init()
    {
      
    }

    private void Awake()
    {
        for (int i = 0; i < 15; i++)
        {
            RedDotData.bagRedItems.Add(new BagData { id = i, count = i + 1 }); ;
        }
        bagBtn.onClick.AddListener(() => { OnClick("bagBtn"); });
        closeBtn.onClick.AddListener(() => { OnClick("closeBtn"); });
    }

    protected override void OnClick(string btnName)
    {
        
    }



}
