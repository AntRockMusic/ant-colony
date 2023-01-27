using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensor : MonoBehaviour
{
    private int bobs = 0;
    private int inside = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "bit")
        {
            inside++;
        }
        if (col.gameObject.tag == "bob")
        {
            bobs++;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "bit")
        {
            inside--;
        }
        if (col.gameObject.tag == "bob")
        {
            bobs--;
        }
    }

    public int getBobs()
    {
        return bobs;
    }

    public int getInside()
    {
        return inside;
    }
}
