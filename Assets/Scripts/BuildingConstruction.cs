using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour {

    public static BuildingConstruction Create(Vector3 position, BuildingTypeSO buildingType) {
        Transform pfBuildingConstruction = Resources.Load<Transform>("pfBuidlingConstruction");
        Transform buildingConstructionTrasnform = Instantiate(pfBuildingConstruction, position, Quaternion.identity);

        BuildingConstruction buildingConstruction = buildingConstructionTrasnform.GetComponent<BuildingConstruction>();
        buildingConstruction.SetBuildingType(buildingType);

        return buildingConstruction;
    }

    private BuildingTypeSO buildingType;
    private float constructionTimer;
    private float constructionTimerMax;

    private void Start() {

    }

    private void Update() {
        constructionTimer -= Time.deltaTime;

        if (constructionTimer <= 0f) {
            Instantiate(buildingType.prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void SetBuildingType(BuildingTypeSO buildingType) {
        this.buildingType = buildingType;

        constructionTimerMax = buildingType.constructionTimerMax;
        constructionTimer = constructionTimerMax;
    }
}
