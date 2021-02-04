using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour {

    public static ArrowProjectile Create(Vector3 position, Enemy enemy) {
        Transform pfArrowProjectile = Resources.Load<Transform>("pfArrowProjectile");
        Transform arrowTrasnform = Instantiate(pfArrowProjectile, position, Quaternion.identity);

        ArrowProjectile arrowProjectile = arrowTrasnform.GetComponent<ArrowProjectile>();
        arrowProjectile.SetTarget(enemy);

        return arrowProjectile;
    }

    private Enemy _targetEnemy;
    private Vector3 _lastMoveDirection;
    private float _timeToExpire = 2f;
    private int _damageAmount = 10;

    private void Update() {
        Vector3 moveDirection;

        if (_targetEnemy != null) {
            moveDirection = (_targetEnemy.transform.position - transform.position).normalized;
            _lastMoveDirection = moveDirection;
        } else {
            moveDirection = _lastMoveDirection;
        }

        float moveSpeed = 20f;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDirection));

        _timeToExpire -= Time.deltaTime;
        if (_timeToExpire < 0f) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null) {
            // Hit an enemy
            enemy.GetComponent<HealthSystem>().Damage(_damageAmount);
            Destroy(gameObject);
        }

    }

    private void SetTarget(Enemy targetEnemy) {
        _targetEnemy = targetEnemy;
    }
}