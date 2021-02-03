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

    private void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        targetTransform = BuildingManager.Instance.GetHeadquartersBuilding().transform;
    }

    private void Update() {
        Vector3 moveDirection = (targetTransform.position - transform.position).normalized;
        float moveSpeed = 6f;
        rigidbody2d.velocity = moveDirection * moveSpeed;
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
}