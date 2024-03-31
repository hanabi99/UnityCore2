using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AbstractFactory
{
    public Engine createEngine();

    public Aircondition createAircondition();
}
//Ϊ����320ϵ���������  
public class FactoryBMW320 : AbstractFactory
{
    public Engine createEngine()
    {
        return new EngineA();
    }

    public Aircondition createAircondition()
    {
        return new AirconditionA();
    }
}
//����523ϵ��
public class FactoryBMW523 : AbstractFactory
{

    public Engine createEngine()
    {
        return new EngineB();
    }

    public Aircondition createAircondition()
    {
        return new AirconditionB();
    }
}


