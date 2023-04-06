using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
public int speed;
    // Start is called before the first frame update
   // boolean shift = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   // if (Input.GetKeyDown("Shift")){
    //    shift = true;
   // }
    //if (Input.GetKeyUp("Shift")){
      //  shift = false;

   // }
    if (Input.GetKey("="))
        {
            speed++;
        }

    if (Input.GetKey("-"))
        {
            speed--;
        }

     var x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
    var y = Input.GetAxis("Vertical") * Time.deltaTime * speed;   
   transform.Translate (x, y, 0);
    }
}
