using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antSpawner : MonoBehaviour
{
    public GameObject ant;
    int count = 1000;
    // Start is called before the first frame update
    void Start()
    {
       
        Instantiate(ant, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        count--;
        if (count == 0)
        {
            count = 1000;
            Instantiate(ant, transform.position, Quaternion.identity);
        }
    }
}
