using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brain : MonoBehaviour
{
    private sensor left;
    private sensor right;
    private int baseMoveSpeed = 30;
    private float moveSpeed;
    private int turnSpeed;
    private int baseTurnSpeed = 5000;
    private int cert; 
    // Start is called before the first frame update
    void Start()
    {
        left = transform.Find("leftSensor").gameObject.GetComponent<sensor>();
        right = transform.Find("rightSensor").gameObject.GetComponent<sensor>();
    }

    // Update is called once per frame
    void Update()
    {
        float dir = leftOrRight();
        
        moveSpeed = baseMoveSpeed / (cert*2 + 1);
        turnSpeed = baseTurnSpeed / (cert * 2 + 1);
        transform.Translate(Vector3.up*moveSpeed*Time.deltaTime);
        transform.Rotate(Vector3.forward * turnSpeed * dir * Time.deltaTime);
        
    }
   
    float leftOrRight()
    {
        float x;
        int l = left.getInside();
        int r = right.getInside();
        cert = l + r;
        if (l < r)
        {
            x = -1;
        }
        else
        {
            x = 1;
        }
        if (l == r)
        {
            x = Random.Range(-1f, 1f);
            
        }
        
            return x;
    }
}
