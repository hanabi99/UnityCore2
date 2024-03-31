using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Carbuilder
{

    protected Car car = new Car();

    public abstract void buildWindow();

    public abstract void buildEngine();

    public abstract void buildAircondition();



    public Car GetCar()
    {
        return car;
    }

}





