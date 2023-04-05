using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("t")){
            Time.timeScale = Time.timeScale++;
            Debug.Log(Time.timeScale);
        }
        if (Input.GetKey("g")){
            Time.timeScale = Time.timeScale--;
        }
    }
}
