using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer : MonoBehaviour
{
    private Carbuilder carbuilder;

    public void SetCarBuilder(Carbuilder carbuilder)
    {
        this.carbuilder = carbuilder;
    }

    public Car Construct()
    {
        carbuilder.buildWindow();

        carbuilder.buildEngine();

        carbuilder.buildAircondition();

        return carbuilder.GetCar();
    }
}

