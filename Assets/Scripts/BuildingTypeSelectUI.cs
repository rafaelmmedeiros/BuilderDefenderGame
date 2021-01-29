using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour {

    [SerializeField] private Sprite arrowSprite;
    private Dictionary<BuildingTypeSO, Transform> buttonTransformDictionary;

    private void Awake() {
        Transform buttonTemplate = transform.Find("buttonTemplate");
        buttonTemplate.gameObject.SetActive(false);

        BuildingTypeListSO buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);

        buttonTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        int index = 0;
        foreach (BuildingTypeSO buildingType in buildingTypeList.list) {
            Transform buttonTransform = Instantiate(buttonTemplate, transform);
            buttonTransform.gameObject.SetActive(true);

            float offsetAmount = +110f;
            buttonTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);

            buttonTransform.Find("image").GetComponent<Image>().sprite = buildingType.sprite;

            buttonTransform.GetComponent<Button>().onClick.AddListener(() => {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            buttonTransformDictionary[buildingType] = buttonTransform;
            index++;
        }
    }

    private void Update() {
        UpdateActiveBuildingTypeButton();
    }

    private void UpdateActiveBuildingTypeButton() {
        foreach (BuildingTypeSO buildingType in buttonTransformDictionary.Keys) {
            Transform buttonTransform = buttonTransformDictionary[buildingType];

            buttonTransform.Find("selected").gameObject.SetActive(false);
        }

        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        buttonTransformDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
    }
}
