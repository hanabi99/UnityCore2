using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanel : MyGameFrameWork.BasePanel
{
    public Button taskBtn;

    public Button box1Btn;

    public Button box2Btn;

    public Button box3Btn;

    public Button closeBtn;

    public GameObject TaskWindow;


    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        taskBtn.onClick.AddListener(() => { OnClick("taskBtn"); });
        box1Btn.onClick.AddListener(() => { OnClick("box1Btn"); });
        box2Btn.onClick.AddListener(() => { OnClick("box2Btn"); });
        box3Btn.onClick.AddListener(() => { OnClick("box3Btn"); });
        closeBtn.onClick.AddListener(() => { OnClick("closeBtn"); });

        // 自定义红点树
        RedDotTreeNode Task_MainRoot = new RedDotTreeNode { node = RedDotDefine.TaskRoot, nodeType = RedDotTreeNodeType.RedDotNodeNum,logicHander = OnTaskRedDotLogicHandler };
        RedDotTreeNode Task_Box1_Node = new RedDotTreeNode { node = RedDotDefine.Task_Box1, parentNode = RedDotDefine.TaskRoot, logicHander = OnTaskBox1RedDotLogicHandler };
        RedDotTreeNode Task_Box2_Node = new RedDotTreeNode { node = RedDotDefine.Task_Box2, parentNode = RedDotDefine.TaskRoot, logicHander = OnTaskBox2RedDotLogicHandler };
        RedDotTreeNode Task_Box3_Node = new RedDotTreeNode { node = RedDotDefine.Task_Box3, parentNode = RedDotDefine.TaskRoot, logicHander = OnTaskBox3RedDotLogicHandler };
        RedDotSystem.GetInstance().InitRedDotTree(new List<RedDotTreeNode> { Task_MainRoot, Task_Box1_Node, Task_Box2_Node, Task_Box3_Node });
    }

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "taskBtn":
                TaskWindow.SetActive(true);
                break;
            case "box1Btn":
               
                break;
            case "box2Btn":
                break;
            case "box3Btn":

                break;
            case "closeBtn":
                TaskWindow.SetActive(false);
                break;
        }
    }

    public void OnTaskRedDotLogicHandler(RedDotTreeNode redDotNode)
    {
        if (RedDotData.Task_Box1_isRead && RedDotData.Task_Box2_isRead && RedDotData.Task_Box3_isRead)
        {
            redDotNode.redDotActive = false;
        }
        else
        {
            redDotNode.redDotActive = true;
        }
        Debug.Log("OnTaskRedDotLogicHandler:" + redDotNode.redDotActive);
    }
    public void OnTaskBox1RedDotLogicHandler(RedDotTreeNode redDotNode)
    {
        redDotNode.redDotActive = RedDotData.Task_Box1_isRead == false;
        Debug.Log("OnTaskBox1RedDotLogicHandler:" + redDotNode.redDotActive);

    }
    public void OnTaskBox2RedDotLogicHandler(RedDotTreeNode redDotNode)
    {
        redDotNode.redDotActive = RedDotData.Task_Box2_isRead == false;
        Debug.Log("OnTaskBox2RedDotLogicHandler:" + redDotNode.redDotActive);
    }
    public void OnTaskBox3RedDotLogicHandler(RedDotTreeNode redDotNode)
    {
        redDotNode.redDotActive = RedDotData.Task_Box3_isRead == false;
        Debug.Log("OnTaskBox3RedDotLogicHandler:" + redDotNode.redDotActive);
    }


}
