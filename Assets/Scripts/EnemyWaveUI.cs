using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EnemyWaveUI : MonoBehaviour {

    [SerializeField] private EnemyWaveManager enemyWaveManager;

    private Camera mainCamera;
    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;
    private RectTransform enemyWaveSpawnPositionIndicator;
    private RectTransform enemyClosestPositionIndicator;


    private void Awake() {
        waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
        enemyWaveSpawnPositionIndicator = transform.Find("enemyWaveSpawnPositionIndicator").GetComponent<RectTransform>();
        enemyClosestPositionIndicator = transform.Find("enemyClosestPositionIndicator").GetComponent<RectTransform>();
    }

    private void Start() {
        mainCamera = Camera.main;
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
    }

    private void EnemyWaveManager_OnWaveNumberChanged(object sender, EventArgs e) {
        SetWaveNumberText("Wave " + enemyWaveManager.GetWaveNumber());
    }

    private void Update() {
        HandleNextWaveMessage();
        HandleEnemyWaveSpawnPositionIndicator();
        HandleEnemyClosestPositionIndicator();
    }

    private void HandleEnemyWaveSpawnPositionIndicator() {
        Vector3 directionToNextSpawnPosition = (enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;

        enemyWaveSpawnPositionIndicator.anchoredPosition = directionToNextSpawnPosition * 300f;
        enemyWaveSpawnPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(directionToNextSpawnPosition));

        float distanceToNextSpawPosition = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
        enemyWaveSpawnPositionIndicator.gameObject.SetActive(distanceToNextSpawPosition > mainCamera.orthographicSize * 1.5f);

    }

    private void HandleNextWaveMessage() {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();

        if (nextWaveSpawnTimer <= 0f) {
            SetMessageText("");
        } else {
            SetMessageText($"Next Wave in {nextWaveSpawnTimer:F1}s");
        }
    }

    private void HandleEnemyClosestPositionIndicator() {
        float targetMaxRadius = 9999f;
        Collider2D[] targetsInRangeArray = Physics2D.OverlapCircleAll(mainCamera.transform.position, targetMaxRadius);

        Enemy targetEnemy = null;

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

        if (targetEnemy != null) {
            Vector3 directionToClosestEnemy = (targetEnemy.transform.position - mainCamera.transform.position).normalized;

            enemyClosestPositionIndicator.anchoredPosition = directionToClosestEnemy * 250f;
            enemyClosestPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(directionToClosestEnemy));

            float distanceToClosestEnemy = Vector3.Distance(targetEnemy.transform.position, mainCamera.transform.position);
            enemyClosestPositionIndicator.gameObject.SetActive(distanceToClosestEnemy > mainCamera.orthographicSize * 1.5f);
        } else {
            // There is no enemies alive.
            enemyClosestPositionIndicator.gameObject.SetActive(false);
        }
    }

    private void SetWaveNumberText(string waveNUmber) {
        waveNumberText.SetText(waveNUmber);
    }

    private void SetMessageText(string message) {
        waveMessageText.SetText(message);
    }


}
