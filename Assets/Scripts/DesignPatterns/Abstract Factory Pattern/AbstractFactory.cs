using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AbstractFactory
{
    public Engine createEngine();

    public Aircondition createAircondition();
}
//为宝马320系列生产配件  
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
//宝马523系列
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


