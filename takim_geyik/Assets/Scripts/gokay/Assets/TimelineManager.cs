using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;


public class TimelineManager : MonoBehaviour
{
    public Light2D globalLight;

    public int elapsedTurn;

    public int day;

    public int currentDayInterval;

    public float intensityChangeRate;

    public int dayInTurn;

    public bool isNight = false;

    public bool isTimeProgressing;

    public TextMeshProUGUI dayTextMesh;
    public TextMeshProUGUI turnTextMesh;



    public static TimelineManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProgressTime() {

        float previousDayIntensity = 0f;
        float currentDayIntensity = 0f;

        currentDayInterval++;

        elapsedTurn++;
        day = elapsedTurn / (2 * dayInTurn);

        SetTimelineText();

        if (currentDayInterval == 1)
        {
            if (isNight == false)
            {
                previousDayIntensity = 0.30f;
                currentDayIntensity = dayIntensitybyTurn(currentDayInterval);
            }
            else if (isNight == true)
            {
                previousDayIntensity = 1f;
                currentDayIntensity = dayIntensitybyTurn(dayInTurn - currentDayInterval);
            }
        }

        else if (currentDayInterval >= 2 && currentDayInterval < dayInTurn)
        {
            if (isNight == false)
            {
                previousDayIntensity = dayIntensitybyTurn(currentDayInterval - 1);
                currentDayIntensity = dayIntensitybyTurn(currentDayInterval);
            }

            if (isNight == true)
            {
                previousDayIntensity = dayIntensitybyTurn(dayInTurn - (currentDayInterval - 1));
                currentDayIntensity = dayIntensitybyTurn(dayInTurn - currentDayInterval);
            }
        }

        else if (currentDayInterval == dayInTurn)
        {


            if (isNight == false)
            {
                previousDayIntensity = dayIntensitybyTurn(currentDayInterval - 1);
                currentDayIntensity = 1;
            }
            if (isNight == true)
            {

                previousDayIntensity = dayIntensitybyTurn(dayInTurn - (currentDayInterval - 1));
                currentDayIntensity = 0.15f;
            }

            isNight = !isNight;
            currentDayInterval = 1;

           
        }


        Debug.Log("Previous Day Intensity" + previousDayIntensity);

        Debug.Log("Current Day Intensity" + currentDayIntensity);

        if (isTimeProgressing == false)
        {
            StartCoroutine(ProgressTimeLerp(previousDayIntensity, currentDayIntensity));
        }
     


    }

    IEnumerator ProgressTimeLerp(float from,float to) {

        isTimeProgressing = true;
        float elapsedTime = 0;

        while (elapsedTime <= 1f) {

            elapsedTime += (Time.deltaTime / intensityChangeRate);

            float dayLightIntensity = Mathf.Lerp(from,to, elapsedTime);

            globalLight.intensity = dayLightIntensity;

            if (elapsedTime >= 1f) {
                isTimeProgressing = false;
                yield break;
            }
            yield return null;
        }
    }



    float dayIntensitybyTurn(int dayInterval) {


     
       
         return dayInterval * (1f / dayInTurn);
        
    }


    public void SetTimelineText() {
        print("CHANGED");
        dayTextMesh.SetText("Day: " + day.ToString());
        turnTextMesh.SetText("Turn: " + elapsedTurn.ToString());
    }
    



}
