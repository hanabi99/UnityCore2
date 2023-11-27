using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
   

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(10,transform.position.y,10), 10 * Time.deltaTime);
    }
}
