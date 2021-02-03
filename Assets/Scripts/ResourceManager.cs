using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    public static ResourceManager Instance { get; private set; }
    public event EventHandler OnResourceAmountChanged;
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;

    private void Awake() {
        Instance = this;
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        foreach (ResourceTypeSO resourceType in resourceTypeList.list) {
            resourceAmountDictionary[resourceType] = 0;
        }
    }

    private void TestLogResourceAmountDictionary() {
        foreach (ResourceTypeSO resourceType in resourceAmountDictionary.Keys) {
            Debug.Log(resourceType.nameString + ": " + resourceAmountDictionary[resourceType]);
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount) {
        resourceAmountDictionary[resourceType] += amount;

        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceTypeSO resourceType) {
        return resourceAmountDictionary[resourceType];
    }

    public bool CanAfford(ResourceAmount[] resourceAmountsArray) {
        // Run all array and verify cost and amount
        foreach (ResourceAmount resourceAmount in resourceAmountsArray) {
            // Verify if has resource amount in a interecation
            if (GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount) {
                // Can afford
            } else {
                // No money... sorry.
                return false;
            }
        }
        // Congratulations for a new BUILDING!
        return true;
    }

    public void SpendResouces(ResourceAmount[] resourceAmountsArray) {
        // Run all array and spend each resource.
        foreach (ResourceAmount resourceAmount in resourceAmountsArray) {
            // Time to pay...
            resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;

        }
    }
}