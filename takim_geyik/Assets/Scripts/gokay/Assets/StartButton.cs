using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnHover()
    {
       transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.yellow;
    }

    public void OnHoverUp()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;


    }

}
