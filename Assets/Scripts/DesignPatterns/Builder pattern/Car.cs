using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car { 

    private Window window;
    private Engine engine;
    private Aircondition aircondition; 

    public Window GetWindow()
    {
        Debug.Log(window.name);
        return window;
    }

    public void SetWindow(Window window)
    {
        this.window = window;
    }

    public Engine GetEngine()
    {
        Debug.Log(engine.name);
        return engine;
    }

    public void SetEngine(Engine engine)
    {
        this.engine = engine;
    }

    public Aircondition GetAircondition()
    {
        Debug.Log(aircondition.name);
        return aircondition;
    }

    public void SetAircondition(Aircondition aircondition)
    {
        this.aircondition = aircondition;
    }

    public void CarToString()
    {
        Debug.Log("本车由：" + window.name + "+" + engine.name + "+" + aircondition.name + "组成");
    }
}

public class CarAbuilder : Carbuilder
{
    public override void buildAircondition()
    {
        car.SetAircondition(new AirconditionA());
    }

    public override void buildEngine()
    {
        car.SetEngine(new EngineB());
    }

    public override void buildWindow()
    {
        car.SetWindow(new WindowB());
    }
}
public class CarBbuilder : Carbuilder
{
    public override void buildAircondition()
    {
        car.SetAircondition(new AirconditionB());
    }

    public override void buildEngine()
    {
        car.SetEngine(new EngineB());
    }

    public override void buildWindow()
    {
        car.SetWindow(new WindowB());
    }
}


public class Engine { public string name; }

public class EngineA : Engine
{

    public EngineA()
    {
        name = "EngineA";
    }
}
public class EngineB : Engine
{
    public EngineB()
    {
        name = "EngineB";
    }
}

public class Aircondition { public string name; }

public class AirconditionA : Aircondition
{
    public AirconditionA()
    {
        name = "AirconditionA";
    }
}
public class AirconditionB : Aircondition
{
    public AirconditionB()
    {
        name = "AirconditionB";
    }
}
public class Window
{
    public string name;
}
public class WindowA : Window
{
    public WindowA()
    {
        name = "WindowA";
    }
}
public class WindowB : Window
{
    public WindowB()
    {
        name = "WindowB";
    }
}


