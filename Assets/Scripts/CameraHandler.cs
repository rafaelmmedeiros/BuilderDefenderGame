﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour {

    public static CameraHandler Instance { get; private set; }

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private float orthographicSize;
    private float targetOrthographicSize;
    private bool edgeScrolling;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        edgeScrolling = false;
        orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }

    private void Update() {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement() {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (edgeScrolling) {

            float edgeScrollSize = 30;

            if (Input.mousePosition.x > Screen.width - edgeScrollSize) {
                x = +1f;
            }
            if (Input.mousePosition.x < edgeScrollSize) {
                x = -1f;
            }
            if (Input.mousePosition.y > Screen.height - edgeScrollSize) {
                y = +1f;
            }
            if (Input.mousePosition.y < edgeScrollSize) {
                y = -1f;
            }
        }

        Vector3 moveDirection = new Vector3(x, y).normalized;

        float moveSpeed = 30f;
        if (Input.GetKey(KeyCode.LeftShift)) {
            moveSpeed = 60f;
        }

        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void HandleZoom() {

        float zoomAmount = 2f;
        if (Input.GetKey(KeyCode.LeftShift)) {
            zoomAmount = 4f;
        }

        targetOrthographicSize += -Input.mouseScrollDelta.y * zoomAmount;

        float minOrthographicSize = 10;
        float maxOrthographicSize = 30;

        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        float zoomSpeed = 5f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);

        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }

    public void SetEdgeScrolling(bool edgeScrolling) {
        this.edgeScrolling = edgeScrolling;
    }

    public bool GetEdgeScrolling() {
        return edgeScrolling;
    }
}
