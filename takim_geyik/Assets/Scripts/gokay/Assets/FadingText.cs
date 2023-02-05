using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadingText : MonoBehaviour
{
    
    private float disappearTimer;
    private TextMeshPro textMesh;
    private Color textColor;


    public static FadingText Create(Vector3 position, int amount)
    {
        GameObject fadingTextTransform = Instantiate(ResourceManagement.Instance.resourceText, position, Quaternion.identity);

        FadingText fadingText = fadingTextTransform.transform.GetComponent<FadingText>();
        fadingText.Setup(amount);

        return fadingText;
    }

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }


   

    public void Setup(int amount) {
        textMesh.SetText("+" + amount.ToString());
        textColor = textMesh.color;
        disappearTimer = 1f;
    }

    private void Update()
    {
        float moveYSpeed = 5f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;

        if (disappearTimer < 0) {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <= 0) {
                Destroy(gameObject);
            }

        }
        
    }


}
