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

    private void Update() {
        Vector3 moveDirection = (_targetEnemy.transform.position - transform.position).normalized;
        transform.eulerAngles = new Vector3(0,0,UtilsClass.GetAngleFromVector(moveDirection));

        float moveSpeed = 20f;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null) {
            // Hit an enemy
            Destroy(gameObject);
        }

    }

    private void SetTarget(Enemy targetEnemy) {
        _targetEnemy = targetEnemy;
    }


}
