using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour {

    private float timer;
    private float timerMax;

    private void Awake() {
        timerMax = 1f;
    }

    private void Update() {

        timer -= Time.deltaTime;

        if (timer <= 0f) {
            timer += timerMax;
            Debug.Log("DING!");
        }
    }
}
