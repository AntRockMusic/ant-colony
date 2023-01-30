using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brain : MonoBehaviour
{
    private sensor left;
    private sensor right;
    private int baseMoveSpeed = 20;
    private float moveSpeed;                                                                        //This is a fluid variable that will contain the movement speed of the ant based on enviromental factors
    private float turnSpeed;                                                                          //This is a fluid variable that will contain the turning speed of the ant based on enviromental factors
    private int baseTurnSpeed = 200;                                                               //This is the base turn speed of the ant
    private float cert;                                                                               //This is to represent the ants certanty on where its going
    private enum State {Forage, FoundFood, Circle, GoTo}

    void Start()
    {
        left = transform.Find("leftSensor").gameObject.GetComponent<sensor>();
        right = transform.Find("rightSensor").gameObject.GetComponent<sensor>();
        cert = 50;
    }


    void Update()
    {
        
        circle();
        //forage();
    }


    /*
     ############################################################### F o r a g e ###############################################################
     if the ant is in a foraging state then they will enact this algorithm
         */

    void forage()
    {
        int l = left.getInside();
        int r = right.getInside();
        float dir = leftOrRight(l,r);
        cert = l + r;
        if (cert == 0)
        {
            dir = (leftOrRight(left.getBobs(), right.getBobs())*-1);
        }
        moveSpeed = baseMoveSpeed / (cert * 2 + 1);
        turnSpeed = baseTurnSpeed / (cert + 1);
        turnAnt(dir);
        moveAnt();
    }

    /*
     ############################################################### M o v e  A n t ###############################################################
     this function will move the ant forward
         */
    void moveAnt()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    /*
     ############################################################### T u r n  A n t ###############################################################
     this function will turn the ant clock wise if dir is positive and counter clockwise if dir is negative
         */
    void turnAnt(float dir)
    {
        transform.Rotate(Vector3.forward * turnSpeed * dir * Time.deltaTime);
    }

    /*
     ############################################################### L e f t  O r  R i g h t ###############################################################
     this function will return the a positive number if the ant is to turn left or a negative number if the ant is to turn right. 
     This is based on what is inside there sensors
         */
    float leftOrRight(int l, int r)
    {
        float x;
        
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

    /*
     ############################################################### C i r c l e ###############################################################
     this function will make an ant create a spiral around a point
     */
    void circle()
    {
        if (cert > 1)
        {
            moveAnt();
            turnAnt((float)0.5);
            cert = cert * (float)0.9991;
            moveSpeed = baseMoveSpeed;
            turnSpeed = baseTurnSpeed * (cert / 6 + 1);
        }//else go to x
        
    }

    /*
     ############################################################### G o  T o  X ############################################################### TO DO
     this will send an ant to a set of coordinates
     */
}
