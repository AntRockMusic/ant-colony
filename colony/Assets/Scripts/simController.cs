using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class simController : MonoBehaviour
{
private bool peramones;
public GameObject timeSlider;
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
        if (Input.GetKeyDown("t")){
            setTimeScale((Time.timeScale + 0.5f));
            Debug.Log(Time.timeScale);
        }
        if (Input.GetKeyDown("g")){
            setTimeScale((Time.timeScale - 0.5f));
        }
    }
    public void setTimeScale(float newTime){
        Time.timeScale = newTime;
    }

    public void slideTime(){
    float sliderTime = timeSlider.GetComponent<Slider>().value;
    setTimeScale(sliderTime);
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
    mainCamera.GetComponent<Camera>().orthographicSize--;
    }
    
    public void zoomOut(){

    mainCamera.GetComponent<Camera>().orthographicSize++;
    }
}
