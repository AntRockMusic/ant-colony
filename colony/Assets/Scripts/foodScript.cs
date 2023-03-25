using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodScript : MonoBehaviour
{
    int neutrition = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int eat()
    {
        Destroy(transform.gameObject);
        return neutrition;
    }
}
