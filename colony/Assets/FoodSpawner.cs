using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject food;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int num = Random.Range(0, 1000);
        if (num == 1)
        {
            spawn();
        }
    }

    void spawn()
    {
        Instantiate(food, new Vector3(Random.Range(-50, 50), Random.Range(-50, 50),0), Quaternion.identity);
    }
}
