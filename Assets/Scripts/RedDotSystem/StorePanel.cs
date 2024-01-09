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

    public GameObject storeWindow;
    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        shopBtn.onClick.AddListener(() => { OnClick("shopBtn"); });
        coinBtn.onClick.AddListener(() => { OnClick("coinBtn"); });
        diamandBtn.onClick.AddListener(() => { OnClick("diamandBtn"); });
        closeBtn.onClick.AddListener(() => { OnClick("closeBtn"); });

        // 自定义红点树
        RedDotTreeNode storeMainRoot = new RedDotTreeNode { node = RedDotDefine.StoreRoot, logicHander = OnStoreRedDotLogicHandler };
        RedDotTreeNode store_Coin_Node = new RedDotTreeNode { node = RedDotDefine.Store_Coin,parentNode = RedDotDefine.StoreRoot, logicHander = OnStoreCoinRedDotLogicHandler };
        RedDotTreeNode store_Diamand_Node = new RedDotTreeNode { node = RedDotDefine.Store_Diamand, parentNode = RedDotDefine.StoreRoot, logicHander = OnStoreDiamandRedDotLogicHandler };
        RedDotSystem.GetInstance().InitRedDotTree(new List<RedDotTreeNode> { storeMainRoot, store_Coin_Node, store_Diamand_Node });
    }

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "shopBtn":
                storeWindow.SetActive(true);
                break;
            case "coinBtn":
                RedDotData.Store_Gold_isRead = true;
                RedDotSystem.GetInstance().UpdateRedDotState(RedDotDefine.Store_Coin);
                break;
            case "diamandBtn":
                RedDotData.Store_Diamand_isRead = true;
                RedDotSystem.GetInstance().UpdateRedDotState(RedDotDefine.Store_Diamand);
                break;
            case "closeBtn":
                storeWindow.SetActive(false);
                break;
        }
    }

    public void OnStoreRedDotLogicHandler(RedDotTreeNode redDotNode)
    {
        if (RedDotData.Store_Gold_isRead && RedDotData.Store_Diamand_isRead)
        {
            redDotNode.redDotActive = false;
        }
        else
        {
            redDotNode.redDotActive = true;
        }
        Debug.Log("OnStoreRedDotLogicHandler:" + redDotNode.redDotActive);
    }
    public void OnStoreCoinRedDotLogicHandler(RedDotTreeNode redDotNode)
    {
        redDotNode.redDotActive = RedDotData.Store_Gold_isRead == false;
        Debug.Log("OnStoreCoinRedDotLogicHandler:" + redDotNode.redDotActive);

    }
    public void OnStoreDiamandRedDotLogicHandler(RedDotTreeNode redDotNode)
    {
        redDotNode.redDotActive = RedDotData.Store_Diamand_isRead == false;
        Debug.Log("OnStoreDiamondRedDotLogicHandler:" + redDotNode.redDotActive);
    }
}
