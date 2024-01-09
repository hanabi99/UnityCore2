using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGameFrameWork;
using UnityEngine.UI;

public class StorePanel : MyGameFrameWork.BasePanel
{
    public Button shopBtn;

    public Button coinBtn;

    public Button diamandBtn;

    public Button closeBtn;
    void Start()
    {

    }
    public override void Init()
    {
        shopBtn.onClick.AddListener(() => { OnClick("shopBtn"); });
        coinBtn.onClick.AddListener(() => { OnClick("coinBtn"); });
        diamandBtn.onClick.AddListener(() => { OnClick("diamandBtn"); });
        closeBtn.onClick.AddListener(() => { OnClick("closeBtn"); });
    }

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "shopBtn":
                break;
            case "coinBtn":
                break;
            case "diamandBtn":
                break;
            case "closeBtn":
                break;
        }
    }
}
