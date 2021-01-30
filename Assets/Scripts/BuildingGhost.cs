using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour {

    private GameObject spriteGameObject;
    private void Awake() {
        spriteGameObject = transform.Find("sprite").gameObject;

        Hide();
    }
    private void Start() {
        BuildingManager.Instance.OnActiveBuildingTypeChange += BuildingManager_OnActiveBuildingTypeChange;
    }

    private void BuildingManager_OnActiveBuildingTypeChange(object sender, BuildingManager.OnActiveBuildingTypeChangeEventArgs eventArgs) {
        if (eventArgs.activeBuildingType == null) {
            Hide();
        } else {
            Show(eventArgs.activeBuildingType.sprite);
        };
    }

    private void Update() {
        transform.position = UtilsClass.GetMouseWorldPosition();
    }

    private void Show(Sprite ghostSprite) {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }

    private void Hide() {
        spriteGameObject.SetActive(false);
    }
}
