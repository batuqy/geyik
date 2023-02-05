using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace ResourceNameSpace {
    public enum ResourceType { 
    WORSHIPPER,
    WATER,
    LIGHT,
    MAGIC
    }
}

public class ResourceManagement : MonoBehaviour
{

    public static ResourceManagement Instance { get; private set; }
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

    public GameObject resourceText;

    public Resource magicResource;
    public Resource worshipperResource;
    public Resource lightResource;
    public Resource waterResource;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddResource(ResourceNameSpace.ResourceType resourceType) {

        if (resourceType == ResourceNameSpace.ResourceType.MAGIC) {
            magicResource.resourceAmount += (1 * magicResource.resourceIncreaseMultiplier);

        }
        else if (resourceType == ResourceNameSpace.ResourceType.WORSHIPPER)
        {
            worshipperResource.resourceAmount += (1 * worshipperResource.resourceIncreaseMultiplier);
        }
        else if (resourceType == ResourceNameSpace.ResourceType.WATER)
        {
            waterResource.resourceAmount += (1 * waterResource.resourceIncreaseMultiplier);
        }
        else if (resourceType == ResourceNameSpace.ResourceType.LIGHT)
        {
            lightResource.resourceAmount += (1 * lightResource.resourceIncreaseMultiplier);
        }

        UpdateResources();
    } 


    public void DecreaseResource(ResourceNameSpace.ResourceType resourceType)
    {

        if (resourceType == ResourceNameSpace.ResourceType.MAGIC)
        {
            magicResource.resourceAmount -= (1 * magicResource.resourceDecreaseMultiplier);

        }
        else if (resourceType == ResourceNameSpace.ResourceType.WORSHIPPER)
        {
            worshipperResource.resourceAmount -= (1 * worshipperResource.resourceDecreaseMultiplier);
        }
        else if (resourceType == ResourceNameSpace.ResourceType.WATER)
        {
            waterResource.resourceAmount -= (1 * waterResource.resourceDecreaseMultiplier);
        }
        else if (resourceType == ResourceNameSpace.ResourceType.LIGHT)
        {
            lightResource.resourceAmount -= (1 * lightResource.resourceDecreaseMultiplier);
        }

        UpdateResources();
    }

    void UpdateResources() {
        magicResource.textMesh.SetText(magicResource.resourceAmount.ToString());
        lightResource.textMesh.SetText(lightResource.resourceAmount.ToString());
        waterResource.textMesh.SetText(waterResource.resourceAmount.ToString());
        worshipperResource.textMesh.SetText(worshipperResource.resourceAmount.ToString());
    }

    public int GetIncreaseMultiplier(ResourceNameSpace.ResourceType resourceType) {

        if (resourceType == ResourceNameSpace.ResourceType.MAGIC)
        {
            return magicResource.resourceIncreaseMultiplier;
        }
        else if (resourceType == ResourceNameSpace.ResourceType.WORSHIPPER)
        {
            return worshipperResource.resourceIncreaseMultiplier;
        }
        else if (resourceType == ResourceNameSpace.ResourceType.WATER)
        {
            return waterResource.resourceIncreaseMultiplier;
        }
        else if (resourceType == ResourceNameSpace.ResourceType.LIGHT)
        {
            return lightResource.resourceIncreaseMultiplier;
        }

        else
            return 0;
    }

    public int GetResourceAmount(ResourceNameSpace.ResourceType resourceType)
    {

        if (resourceType == ResourceNameSpace.ResourceType.MAGIC)
        {
            return magicResource.resourceAmount;
        }
        else if (resourceType == ResourceNameSpace.ResourceType.WORSHIPPER)
        {
            return worshipperResource.resourceAmount;
        }
        else if (resourceType == ResourceNameSpace.ResourceType.WATER)
        {
            return waterResource.resourceAmount;
        }
        else if (resourceType == ResourceNameSpace.ResourceType.LIGHT)
        {
            return lightResource.resourceAmount;
        }

        else
            return 0;
    }


    public void IncreaseResourceMultiplier(ResourceNameSpace.ResourceType resourceType,int amount)
    {

        if (resourceType == ResourceNameSpace.ResourceType.MAGIC)
        {
            magicResource.resourceIncreaseMultiplier+=amount;
        }
        else if (resourceType == ResourceNameSpace.ResourceType.WORSHIPPER)
        {
            magicResource.resourceIncreaseMultiplier+=amount;
        }
        else if (resourceType == ResourceNameSpace.ResourceType.WATER)
        {
            waterResource.resourceIncreaseMultiplier+=amount;
        }
        else if (resourceType == ResourceNameSpace.ResourceType.LIGHT)
        {
            lightResource.resourceIncreaseMultiplier +=amount;
        }

    }

    public void AddResourceTotal(ResourceNameSpace.ResourceType resourceType,int amount)
    {

        if (resourceType == ResourceNameSpace.ResourceType.MAGIC)
        {
            magicResource.resourceAmount += amount;  
        }
        else if (resourceType == ResourceNameSpace.ResourceType.WORSHIPPER)
        {
            worshipperResource.resourceAmount += amount;
        }
        else if (resourceType == ResourceNameSpace.ResourceType.WATER)
        {
            waterResource.resourceAmount += amount;
        }
        else if (resourceType == ResourceNameSpace.ResourceType.LIGHT)
        {
            lightResource.resourceAmount += amount;
        }
        FadingText.Create(transform.position,amount);

        UpdateResources();
    }


    public void DecreaseResourceTotal(ResourceNameSpace.ResourceType resourceType, int amount)
    {

        if (resourceType == ResourceNameSpace.ResourceType.MAGIC)
        {
            magicResource.resourceAmount -= amount;

        }
        else if (resourceType == ResourceNameSpace.ResourceType.WORSHIPPER)
        {
            worshipperResource.resourceAmount -= amount;
        }
        else if (resourceType == ResourceNameSpace.ResourceType.WATER)
        {
            waterResource.resourceAmount -= amount;
        }
        else if (resourceType == ResourceNameSpace.ResourceType.LIGHT)
        {
            lightResource.resourceAmount -= amount;
        }

        UpdateResources();
    }

}
