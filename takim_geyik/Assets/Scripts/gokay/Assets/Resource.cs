using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Resource
{
    public string resourceName;
    public int resourceAmount;
    public int resourceIncreaseMultiplier;
    public int resourceDecreaseMultiplier;
    public TextMeshProUGUI textMesh;
    public Image spriteImage;
    public Sprite resourceSprite;

}
