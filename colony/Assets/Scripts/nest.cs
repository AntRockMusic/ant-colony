using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nest : MonoBehaviour
{
    public GameObject ant;
    public int food;
    // Start is called before the first frame update
    void Start()
    {
        food = 0;    
    }

    // Update is called once per frame
    void Update()
    {
        if (food  > 4)
        {
            food -= 4;
            makeAnt();
        }
    }

    public void takeFood(int foodToGet)
    {
        Debug.Log("giving food");
        food += foodToGet;
    }

    private void makeAnt()
    {
        Debug.Log("Make ant");
        Instantiate(ant, transform.position, transform.rotation);
        //make an ant
    }

}
