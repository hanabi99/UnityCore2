using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace xml
{
    public class Test : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            PlayerInfo p = new PlayerInfo();
            p.LoadData("PlayerInfo");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
