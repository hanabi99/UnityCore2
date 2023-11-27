using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTest : MonoBehaviour
{
    
    IEnumerator Start()
    {
        ClassWithCounter classwith = new ClassWithCounter();
        classwith.Operator();
        yield return new WaitForSeconds(3);
        ClassWithCounter classwith2 = new ClassWithCounter();
        yield return new WaitForSeconds(2);
        classwith2.Operator();
    }


}

public class ClassWithCounter
{
    protected static int m_Objcounter = 0;
    protected bool m_bEnable = false;

    public ClassWithCounter() {
        m_Objcounter++;
        m_bEnable = m_Objcounter ==1  ? true : false;
        if(m_bEnable == false)
        {
            Debug.LogError("��ǰ������" + m_Objcounter + "������һ��");
        }
    }

    public void Operator()
    {
        if(m_bEnable == false)
        {
            Debug.LogError("��ǰ������������һ���޷�ִ��");
            return;
        }
        Debug.Log("����ִ��" + m_Objcounter);
    }
}
