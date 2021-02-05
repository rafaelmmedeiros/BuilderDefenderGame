using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    private BuildingTypeSO buildingType;
    private HealthSystem healthSystem;
    private Transform buildingDemolishButton;

    private void Awake() {
        buildingDemolishButton = transform.Find("pfBuildingDemolishButton");
        if (buildingDemolishButton != null) {
            HideBuildingDemolishButton();
        }
    }

    private void Start() {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetHealthAmountMax(buildingType.healthAmountMax, true);
        healthSystem.OnDie += HealthSystem_OnDied;
    }

    private void HealthSystem_OnDied(object sender, EventArgs e) {
        Destroy(gameObject);
    }

    private void OnMouseEnter() {
        // Only works if the GameObject has a collider!
        ShowBuildingDemolishButton();
    }

    private void OnMouseExit() {
        HideBuildingDemolishButton();
    }

    private void ShowBuildingDemolishButton() {
        if (buildingDemolishButton != null) {
            buildingDemolishButton.gameObject.SetActive(true);
        }
    }

    private void HideBuildingDemolishButton() {
        if (buildingDemolishButton != null) {
            buildingDemolishButton.gameObject.SetActive(false);
        }
    }
}
