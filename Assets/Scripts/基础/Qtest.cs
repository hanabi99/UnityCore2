using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Qtest : MonoBehaviour
{
    public Quaternion targetQ;

    public Quaternion startQ;

    public float rotatSpeed;

    public float roundTime;

    public GameObject Cube;


    void LateUpdate()
    {

        if (targetQ != Quaternion.LookRotation(Cube.transform.transform.position - transform.position))
        {
            targetQ = Quaternion.LookRotation(Cube.transform.transform.position - transform.position);
            startQ = transform.rotation;
            roundTime = 0;
        }
        roundTime += Time.deltaTime;
        transform.rotation =  Quaternion.Slerp(startQ, targetQ, roundTime);


    }
}
