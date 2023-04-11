using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simController : MonoBehaviour
{
private bool peramones;
public GameObject mainCamera;

    // Start is called before the first frame update
   
    void Start()
    {

    mainCamera.GetComponent<Camera>().enabled = true;
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
        mainCamera.GetComponent<Camera>().cullingMask = -1;
        
    }

    public void peramonesOff(){
        peramones = false;
        mainCamera.GetComponent<Camera>().cullingMask = 1;
        Debug.Log("peramonse off");
    }
    
    public void zoomIn(){
    mainCamera.GetComponent<Camera>().orthographicSize = mainCamera.GetComponent<Camera>().orthographicSize--;
    Debug.Log("zoom In");
    }
    
    public void zoomOut(){
    mainCamera.GetComponent<Camera>().orthographicSize = mainCamera.GetComponent<Camera>().orthographicSize++;
    Debug.Log(mainCamera.GetComponent<Camera>().orthographicSize);    
    }
}
