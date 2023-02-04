using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class TimelineManager : MonoBehaviour
{
    public Light2D globalLight;


    public float dayInMinute;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeLine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TimeLine() {

        float elapsedTime = 0;

        while (elapsedTime <= 1f) {

            elapsedTime += (Time.deltaTime / dayInMinute);


            float dayLightIntensity = Mathf.Lerp(0.15f, 1f, elapsedTime);

            globalLight.intensity = dayLightIntensity;

            if (elapsedTime >= 1f) {
                yield break;
            }
            yield return null;
        }



    }
       
    
    



}
