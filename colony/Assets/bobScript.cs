using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bobScript : MonoBehaviour
{
    int timeToLive = 10000;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToLive > 0) {
            timeToLive--;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
