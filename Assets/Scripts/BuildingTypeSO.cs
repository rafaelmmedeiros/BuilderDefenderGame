using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject {

    public string nameString;
    public Transform prefab;
    public Sprite sprite;
    public ResourceGeneratorData resourceGeneratorData;
    public float minConstructionRadius;
    public ResourceAmount[] constructResourceCostArray;
    public int healthAmountMax;

    public string GetConstructionResourceCostString() {

        string str = "";
        foreach (ResourceAmount resourceAmount in constructResourceCostArray) {
            str += "<color=#" + resourceAmount.resourceType.colorHex + ">" + resourceAmount.resourceType.nameShort + ": " + resourceAmount.amount + "</color> ";
        }
        return str;
    }

}
