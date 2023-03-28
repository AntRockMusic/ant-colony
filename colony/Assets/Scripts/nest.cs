using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nest : MonoBehaviour
{
    private int food;
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
        food += foodToGet;
    }

    private void makeAnt()
    {
        Debug.Log("Make ant");
        //make an ant
    }

}
