using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {

    public event EventHandler OnDamage;
    public event EventHandler OnHealed;
    public event EventHandler OnDie;

    [SerializeField] private int healthAmountMax;
    private int healthAmount;

    private void Awake() {
        healthAmount = healthAmountMax;
    }

    public void Damage(int damageAmount) {
        healthAmount -= damageAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

        OnDamage?.Invoke(this, EventArgs.Empty);

        // OK, but still alive?
        if (IsDead()) {
            OnDie?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Heal(int healAmount) {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void HealFull() {
        healthAmount = healthAmountMax;

        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public bool IsFullHealth() {
        return healthAmount == healthAmountMax;
    }

    public bool IsDead() {
        return healthAmount == 0;
    }

    public int GetHealthAmount() {
        return healthAmount;
    }

    public float GetHealthAmountNormalized() {
        return (float)healthAmount / healthAmountMax;
    }

    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount) {
        this.healthAmountMax = healthAmountMax;

        if (updateHealthAmount) {
            healthAmount = healthAmountMax;
        }
    }
}
