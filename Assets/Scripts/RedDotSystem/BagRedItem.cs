using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagRedItem : MonoBehaviour
{
    public GameObject redDotObj;

    public Text countText;

    private BagData bagData;

    public void SetData(BagData bagData)
    {
        this.bagData = bagData;

        countText.text = "x" + bagData.count.ToString();

        redDotObj.SetActive(bagData.isRead == false);
    }

    public void OnButtonClick()
    {
        if (this.bagData.isRead == false)
        {
            this.bagData.isRead = true;
            redDotObj.SetActive(false);
            RedDotSystem.GetInstance().UpdateRedDotState(RedDotDefine.BagRoot);
        }
    }
}
