using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] private float shootTimerMax;
    private float shootTimer;
    private Enemy targetEnemy;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax;
    private Vector3 projectileSpawnPosition;

    private void Awake() {
        projectileSpawnPosition = transform.Find("ProjectileSpawnPosition").position;
    }

    private void Update() {
        HandleTargetering();
        HandleShooting();
    }

    private void HandleShooting() {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f) {
            shootTimer += shootTimerMax;

            if (targetEnemy != null) {
                ArrowProjectile.Create(projectileSpawnPosition, targetEnemy);
            }
        }
    }

    private void HandleTargetering() {
        lookForTargetTimer -= Time.deltaTime;

        // To not overloud the application.
        if (lookForTargetTimer < 0f) {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTargets();
        }
    }

    private void LookForTargets() {
        float targetMaxRadius = 40f;
        Collider2D[] targetsInRangeArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D target in targetsInRangeArray) {
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null) {
                // It´s a ENEMY!! It will become the new Target!! :)
                if (targetEnemy == null) {
                    targetEnemy = enemy;
                } else {
                    // Compare distance and pick the closest.
                    if (Vector3.Distance(transform.position, enemy.transform.position)
                    < Vector3.Distance(transform.position, targetEnemy.transform.position)) {
                        // This is the closers enemy! Time to Attack!
                        targetEnemy = enemy;
                    }
                }
            }
        }
    }
}
