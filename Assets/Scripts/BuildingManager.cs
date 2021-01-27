using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    private void Update() {
        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
