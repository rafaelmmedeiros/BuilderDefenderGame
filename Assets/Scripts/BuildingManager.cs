using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour {

    public static BuildingManager Instance { get; private set; }

    public event EventHandler<OnActiveBuildingTypeChangeEventArgs> OnActiveBuildingTypeChange;

    public class OnActiveBuildingTypeChangeEventArgs : EventArgs {
        public BuildingTypeSO activeBuildingType;
    }

    [SerializeField] private Building headquartersBuilding;

    private Camera mainCamera;
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO activeBuildingType;

    private void Awake() {
        Instance = this;
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }

    private void Start() {
        mainCamera = Camera.main;

        headquartersBuilding.GetComponent<HealthSystem>().OnDie += Headquarters_OnDie;
    }

    private void Headquarters_OnDie(object sender, EventArgs e) {
        GameOverUI.Instance.Show();
        SoundManager.Instance.PlaySound(SoundManager.Sound.GameOver);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) {
            // Condition to be able to select a construciton to build
            if (activeBuildingType != null) {
                if (CanSpawnBuilding(activeBuildingType, UtilsClass.GetMouseWorldPosition(), out string errorMessage)) {
                    // Do you can afford the building?
                    if (ResourceManager.Instance.CanAfford(activeBuildingType.constructResourceCostArray)) {
                        // Time to pay...
                        ResourceManager.Instance.SpendResouces(activeBuildingType.constructResourceCostArray);
                        // Congratulations!! Time to Instantiate you new BUILDING!!
                        //Instantiate(activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
                        BuildingConstruction.Create(UtilsClass.GetMouseWorldPosition(), activeBuildingType);
                    } else {
                        // Trying to be smart?
                        TooltipUI.Insntace.Show("Cannot afford " + activeBuildingType.GetConstructionResourceCostString(), new TooltipUI.TooltipTimer { timer = 2f });
                    }
                } else {
                    TooltipUI.Insntace.Show(errorMessage, new TooltipUI.TooltipTimer { timer = 2f });
                }
            }
        }
    }

    public void SetActiveBuildingType(BuildingTypeSO buildingType) {
        activeBuildingType = buildingType;
        OnActiveBuildingTypeChange?.Invoke(this, new OnActiveBuildingTypeChangeEventArgs {
            activeBuildingType = activeBuildingType
        });
    }

    public BuildingTypeSO GetActiveBuildingType() {
        return activeBuildingType;
    }

    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorMessage) {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        // Search for things with collider in the placement area.
        Collider2D[] collider2DArray = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);
        bool isAreaClear = collider2DArray.Length == 0;
        if (!isAreaClear) {
            errorMessage = "Area is not clear!";
            return false;
        }

        // Search for building of the same type in a minConstructionRadius
        collider2DArray = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach (Collider2D collider2D in collider2DArray) {
            // Collider inside contruction Radius
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null) {
                // Has a BuildingTypeHolder
                if (buildingTypeHolder.buildingType == buildingType) {
                    // There is a building of this type in radius
                    errorMessage = "To close of a building of the same type!";
                    return false;
                }
            }
        }

        // Max Contruction Radius Rule
        float maxConstructionRadius = 25;
        collider2DArray = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        foreach (Collider2D collider2D in collider2DArray) {
            // Collider inside contruction Radius
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null) {
                // It´s a Bulding!
                errorMessage = "";
                return true;
            }
        }
        errorMessage = "To far of your base!";
        return false;

    }

    public Building GetHeadquartersBuilding() {
        return headquartersBuilding;
    }
}