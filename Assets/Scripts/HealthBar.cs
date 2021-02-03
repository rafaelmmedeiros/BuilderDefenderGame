using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

    [SerializeField] private HealthSystem healthSystem;
    private Transform barTransform;

    private void Awake() {
        barTransform = transform.Find("bar");
    }

    private void Start() {
        // Subscribe on the event.
        healthSystem.OnDamage += HealtSystem_OnDamaged;
        UpdateBar();
        UpdateHealthBarVisible();
    }
    private void HealtSystem_OnDamaged(object sender, EventArgs e) {
        UpdateBar();
        UpdateHealthBarVisible();
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