using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simController : MonoBehaviour
{
bool peramones;
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

    public void peramonesOn(){
        peramones = true;
        Debug.Log("peramones on");
    }

    public void peramonesOff(){
        peramones = false;
        Debug.Log("peramonse off");
    }
    
    public void zoomIn(){
    Debug.Log("zoom In");
    }
    
    public void zoomOut(){
    Debug.Log("zoom Out");    
    }
}
