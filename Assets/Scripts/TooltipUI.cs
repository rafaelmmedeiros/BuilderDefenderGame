using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour {

    [SerializeField] private RectTransform canvasRectTransform;

    private RectTransform rectTransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform backgroundRectTransform;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();

        SetText("Congrats, you are not blind");
    }

    private void Update() {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        // Tooltip goes outside screen
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width) {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height) {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void SetText(string tooltipText) {
        // Apply the text
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();

        // Modify the Background to adjust to text size
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textSize + padding;
    }
}
