using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButton : MonoBehaviour {

    [SerializeField] private HealthSystem healthSystem;

    private void Awake() {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() => {
            healthSystem.HealFull();
        });
    }
}
