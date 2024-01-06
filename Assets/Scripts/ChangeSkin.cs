using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.U2D.Animation;

public class ChangeSkin : MonoBehaviour
{
   // public SpriteResolver sr;

    private Dictionary<string, SpriteResolver> equipDic = new Dictionary<string, SpriteResolver>();

    // Start is called before the first frame update
    void Start()
    {
        #region ֪ʶ��һ �����ͬһ��psb�ļ���������װ��Դ
        //1.��ps������������Դʱ����һ����Ϸ��������л�װ��Դ���ڷź�λ��
        //2.�����ǵ������Դʱ��Ҫע���Ƿ������ص�ͼ��
        #endregion

        #region ֪ʶ��� �༭��װ��Դ�Ĺ�����Ϣ�Լ��������
        //ע�����
        //ÿ����λ �����Ĺ���Ҫ��ȷ����
        //Ϊͬһ����λ�Ĳ�ͬװ������
        #endregion

        #region ֪ʶ���� ��λ�װ
        //�����ؼ����
        //SpriteLibrary�����������Ͽ⣬ȷ����������Ϣ
        //SpriteResolver�������������������ȷ����λ����ʹ�õ�ͼƬ
        //һ�������ļ�
        //SpriteLibraryAsset�����������Ͽ���Դ�������¼��������Ϣ���ļ�
        #endregion

        #region ֪ʶ���� ���뻻װ
        //1.��ȡ����λ��SpriteResolver����Ҫ���������ռ䣩
        //2.ʹ��SpriteResolver��API����װ���л�
        //GetCategory() ��ȡ��ǰ��λĬ�ϵ������
        //SetCategoryAndLabel ���õ�ǰ��λ��Ҫ�л���ͼƬ��Ϣ
        //sr.SetCategoryAndLabel(sr.GetCategory(), "CASK 1");

        SpriteResolver[] srs = this.GetComponentsInChildren<SpriteResolver>();
        for (int i = 0; i < srs.Length; i++)
        {
            Debug.Log(srs[i].GetCategory());
            Debug.Log(srs[i]);
            equipDic.Add(srs[i].GetCategory(), srs[i]);
        }

        ChangeEquip("Weapon", "Super1");

        #endregion
    }

    public void ChangeEquip(string category, string equipName)
    {
        if (equipDic.ContainsKey(category))
        {
            equipDic[category].SetCategoryAndLabel(category, equipName);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
