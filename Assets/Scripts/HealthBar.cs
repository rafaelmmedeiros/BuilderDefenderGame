﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    [SerializeField] private HealthSystem healthSystem;
    private Transform barTransform;
    private Transform separatorContainer;

    private void Awake() {
        barTransform = transform.Find("bar");
    }

    private void Start() {
        separatorContainer = transform.Find("separatorContainer");
        ConstructHealthBarSeparators();

        healthSystem.OnDamage += HealthSystem_OnDamaged;
        healthSystem.OnHealed += HealthSystem_OnHealed;
        healthSystem.OnHealthAmountMaxChanged += HealthSystem_OnHealthAmountMaxChanged;

        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnHealthAmountMaxChanged(object sender, EventArgs e) {
        ConstructHealthBarSeparators();
    }

    private void HealthSystem_OnHealed(object sender, EventArgs e) {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnDamaged(object sender, EventArgs e) {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void ConstructHealthBarSeparators() {
        Transform separatorTemplate = separatorContainer.Find("separatorTemplate");
        separatorTemplate.gameObject.SetActive(false);

        foreach (Transform separatorTransform in separatorContainer) {
            if (separatorTransform == separatorTemplate) continue;
            Destroy(separatorTransform.gameObject);
        }

        // This is a BAD implementation, too bad... if some assface change the health bar layout?
        // Better to calculate this.
        int healthAmountPerSeparator = 10;
        float barSize = 3f;
        float barOneHealthAmountSize = barSize / healthSystem.GetHealthAmountMax();
        int healthSeparatorCount = Mathf.FloorToInt(healthSystem.GetHealthAmountMax() / healthAmountPerSeparator);

        for (int i = 1; i < healthSeparatorCount; i++) {
            Transform separatorTransform = Instantiate(separatorTemplate, separatorContainer);
            separatorTransform.gameObject.SetActive(true);
            separatorTransform.localPosition = new Vector3(barOneHealthAmountSize * i * healthAmountPerSeparator, 0, 0);
        }
    }

    private void UpdateBar() {
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1, 1);
    }

    private void UpdateHealthBarVisible() {
        if (!healthSystem.IsFullHealth()) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }
}