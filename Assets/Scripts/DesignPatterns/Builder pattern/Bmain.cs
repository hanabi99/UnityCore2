using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bmain : MonoBehaviour
{
    void Start()
    {
        Engineer engineer = new Engineer();
        engineer.SetCarBuilder(new CarAbuilder());
        Car car = engineer.Construct();
        car.CarToString();
        engineer.SetCarBuilder(new CarBbuilder());
        Car car2 = engineer.Construct();
        car2.CarToString();
    }

}
