using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour {

    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private List<BuildingTypeSO> ignoreBuildingTypeList;
    private Dictionary<BuildingTypeSO, Transform> buttonTransformDictionary;
    private Transform arrowButton;

    private void Awake() {
        Transform buttonTemplate = transform.Find("buttonTemplate");
        buttonTemplate.gameObject.SetActive(false);

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        buttonTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        int index = 0;

        arrowButton = Instantiate(buttonTemplate, transform);
        arrowButton.gameObject.SetActive(true);

        float offsetAmount = +110f;
        arrowButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

        arrowButton.Find("image").GetComponent<Image>().sprite = arrowSprite;
        arrowButton.Find("image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -40);

        arrowButton.GetComponent<Button>().onClick.AddListener(() => {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });

        index++;

        foreach (BuildingTypeSO buildingType in buildingTypeList.list) {

            if (ignoreBuildingTypeList.Contains(buildingType)) {
                continue; // Will Skip this current iteraction
            }

            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);

            offsetAmount = +110f;
            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            buttonTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            buttonTransform.GetComponent<Button>().onClick.AddListener(() => {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            // Using the class with Interfaces of mouse OnPinterEnter and exit.
            MouseEnterExitEvents mouseEnterExitEvents = buttonTransform.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs eventArgs) => {
                TooltipUI.Insntace.Show(buildingType.nameString);
            };

            mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs eventArgs) => {
                TooltipUI.Insntace.Hide();
            };

            buttonTransformDictionary[buildingType] = buttonTransform;
            index++;
        }
    }

    private void Start() {
        BuildingManager.Instance.OnActiveBuildingTypeChange += BuildingManager_OnActiveBuildingTypeChange;
        UpdateActiveBuildingTypeButton();
    }

    private void BuildingManager_OnActiveBuildingTypeChange(object sender, BuildingManager.OnActiveBuildingTypeChangeEventArgs e) {
        UpdateActiveBuildingTypeButton();
    }

    private void UpdateActiveBuildingTypeButton() {
        arrowButton.Find("selected").gameObject.SetActive(false);

        foreach (BuildingTypeSO buildingType in buttonTransformDictionary.Keys) {
            Transform buttonTransform = buttonTransformDictionary[buildingType];

            buttonTransform.Find("selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();

        if (activeBuildingType == null) {
            arrowButton.Find("selected").gameObject.SetActive(true);
        } else {
            buttonTransformDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
        }

    }
}
