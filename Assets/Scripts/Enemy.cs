using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public static Enemy Create(Vector3 position) {
        Transform pfEnemy = Resources.Load<Transform>("pfEnemy");
        Transform enemyTrasnform = Instantiate(pfEnemy, position, Quaternion.identity);

        Enemy enemy = enemyTrasnform.GetComponent<Enemy>();
        return enemy;
    }

    private Rigidbody2D rigidbody2d;
    private Transform targetTransform;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .2f;

    private void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        targetTransform = BuildingManager.Instance.GetHeadquartersBuilding().transform;

        // With this, all spawned enemy will have a radom lookForTargetTimerMax, thats is beeter balance on update and some sensation to the player
        lookForTargetTimer = Random.Range(0f, lookForTargetTimerMax);
    }

    private void Update() {
        HandleMovement();
        HandleTargetering();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Building building = collision.gameObject.GetComponent<Building>();

        if (building != null) {
            // It´s a collision with a buildling
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            Destroy(gameObject);
        }
    }

    private void HandleMovement() {
        if (targetTransform != null) {
            Vector3 moveDirection = (targetTransform.position - transform.position).normalized;

            float moveSpeed = 6f;
            rigidbody2d.velocity = moveDirection * moveSpeed;

        } else {
            rigidbody2d.velocity = Vector3.zero;

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
        float targetMaxRadius = 10f;
        Collider2D[] targetsInRangeArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D target in targetsInRangeArray) {
            Building building = target.GetComponent<Building>();
            if (building != null) {
                // It´s a Building!! It will become the new Target!! :)
                if (targetTransform == null) {
                    targetTransform = building.transform;
                } else {
                    // Compare distance and pick the closest.
                    if (Vector3.Distance(transform.position, building.transform.position)
                    < Vector3.Distance(transform.position, targetTransform.position)) {
                        // This is the closers enemy! Time to Attack!
                        targetTransform = building.transform;
                    }
                }
            }
        }

        // If the target was destroyed before reach...
        if (targetTransform == null) {
            // TARGET THE HEADQAUTERS!!!
            targetTransform = BuildingManager.Instance.GetHeadquartersBuilding().transform;
        }
    }
}