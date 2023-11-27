using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public GameObject attackTarget;
    void Update()
    {
       transform.position =  Vector3.MoveTowards(transform.position, attackTarget.transform.position, 10 * Time.deltaTime);
    }
}
