using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    private BuildingTypeSO buildingType;
    private HealthSystem healthSystem;
    private void Start() {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetHealthAmountMax(buildingType.healthAmountMax, true);
        healthSystem.OnDie += HealthSystem_OnDied;
    }

    private void HealthSystem_OnDied(object sender, EventArgs e) {
        Destroy(gameObject);
    }
}
