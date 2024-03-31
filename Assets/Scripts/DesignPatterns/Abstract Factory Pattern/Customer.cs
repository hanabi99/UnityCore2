using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{

    void Start()
    {
        //生产宝马320系列配件
        FactoryBMW320 factoryBMW320 = new FactoryBMW320();
        Debug.Log(factoryBMW320.createEngine().name);
        Debug.Log(factoryBMW320.createAircondition().name);

        //生产宝马523系列配件  
        FactoryBMW523 factoryBMW523 = new FactoryBMW523();
        Debug.Log(factoryBMW523.createEngine().name);
        Debug.Log(factoryBMW523.createAircondition().name);
    }
}


