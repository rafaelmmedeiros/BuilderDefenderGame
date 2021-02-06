using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairButton : MonoBehaviour {

    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private ResourceTypeSO goldResourceType;

    private void Awake() {
        transform.Find("button").GetComponent<Button>().onClick.AddListener(() => {
            int missingHealth = healthSystem.GetHealthAmountMax() - healthSystem.GetHealthAmount();
            int repairCost = missingHealth / 4;

            ResourceAmount[] resourceAmountCost = new ResourceAmount[] {
                new ResourceAmount {resourceType = goldResourceType, amount = repairCost}
            };

            if (ResourceManager.Instance.CanAfford(resourceAmountCost)) {
                // Congrats for the Repair
                ResourceManager.Instance.SpendResouces(resourceAmountCost);
                healthSystem.HealFull();
            } else {
                // You are fucked dude...
                TooltipUI.Insntace.Show("You are short on cash... Too Bad!", new TooltipUI.TooltipTimer { timer = 3f });
            }
        });
    }
}
