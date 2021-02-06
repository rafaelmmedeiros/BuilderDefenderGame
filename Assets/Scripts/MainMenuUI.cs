using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

    private void Awake() {
        transform.Find("playButton").GetComponent<Button>().onClick.AddListener(() => {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });
        transform.Find("exitButton").GetComponent<Button>().onClick.AddListener(() => {
            Application.Quit();
        });
    }

}
