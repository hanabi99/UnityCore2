using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagRedDotPanel : MyGameFrameWork.BasePanel
{
    public Button bagBtn;

    public Transform[] itemGridTrans;

    public Button closeBtn;

    public GameObject BagWindow;

    public GameObject bagItem;

    public bool isCreate;
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
        RedDotTreeNode BagNode = new RedDotTreeNode { nodeType = RedDotTreeNodeType.ReDotNodeDataNum, node = RedDotDefine.BagRoot, logicHander = OnBagRedDotLogicHandler };
        RedDotSystem.GetInstance().InitRedDotTree(new List<RedDotTreeNode>() { BagNode });
    }

    public void OnBagRedDotLogicHandler(RedDotTreeNode redDotNode)
    {
        redDotNode.redDotCount = 0;
        foreach (var item in RedDotData.bagRedItems)
        {
            if(item.isRead == false)
            {
                redDotNode.redDotCount++;
            }
        }
        Debug.Log("redNode.redDotCount :" + redDotNode.redDotCount);
    }

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "bagBtn":
                BagWindow.SetActive(true);
                if (isCreate == false)
                {
                    for (int i = 0; i < RedDotData.bagRedItems.Count; i++)
                    {
                        GameObject obj = Instantiate(bagItem, itemGridTrans[i]);
                        obj.SetActive(true);
                        obj.transform.localPosition = Vector3.zero;
                        obj.transform.localRotation = Quaternion.identity;
                        obj.transform.localScale = Vector3.one;
                        BagRedItem BagRedItem = obj.GetComponent<BagRedItem>();
                        BagRedItem.SetData(RedDotData.bagRedItems[i]);
                    }
                    isCreate = true;
                }
                break;
            case "closeBtn":
                BagWindow.SetActive(false);
                break;
        }
    }



}
