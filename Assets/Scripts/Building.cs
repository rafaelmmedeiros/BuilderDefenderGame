﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    private BuildingTypeSO buildingType;
    private HealthSystem healthSystem;
    private Transform buildingDemolishButton;
    private Transform buildingRepairButton;

    private void Awake() {
        buildingDemolishButton = transform.Find("pfBuildingDemolishButton");
        buildingRepairButton = transform.Find("pfBuildingRepairButton");

        HideBuildingDemolishButton();
        HideBuildingRepairButton();

    }

    private void Start() {
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetHealthAmountMax(buildingType.healthAmountMax, true);
        healthSystem.OnDie += HealthSystem_OnDied;
        healthSystem.OnDamage += HealthSystem_OnDamage;
        healthSystem.OnHealed += HealthSystem_OnHealed;
    }

    private void HealthSystem_OnHealed(object sender, EventArgs e) {
        if (healthSystem.IsFullHealth()) {
            HideBuildingRepairButton();
        }
    }

    private void HealthSystem_OnDamage(object sender, EventArgs e) {
        ShowBuildingRepairButton();
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
        CineMachineShake.Instance.ShakeCamera(7f, .15f);
        ChromaticAberrationEffect.Instance.SetWeight(1f);
    }

    private void HealthSystem_OnDied(object sender, EventArgs e) {
        Instantiate(Resources.Load<Transform>("pfBuildingDestroyedParticles"), transform.position, Quaternion.identity);
        Destroy(gameObject);
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
        CineMachineShake.Instance.ShakeCamera(10f, .2f);
        ChromaticAberrationEffect.Instance.SetWeight(1f);
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

    private void ShowBuildingRepairButton() {
        if (buildingRepairButton != null) {
            buildingRepairButton.gameObject.SetActive(true);
        }
    }

    private void HideBuildingRepairButton() {
        if (buildingRepairButton != null) {
            buildingRepairButton.gameObject.SetActive(false);
        }
    }
}
