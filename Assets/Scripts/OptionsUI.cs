using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour {

    [SerializeField] private SoundManager soundManager;
    [SerializeField] private MusicManager musicManager;
    private TextMeshProUGUI soundVolumeText;
    private TextMeshProUGUI musicVolumeText;


    private void Awake() {

        soundVolumeText = transform.Find("soundLevelText").GetComponent<TextMeshProUGUI>();
        musicVolumeText = transform.Find("musicLevelText").GetComponent<TextMeshProUGUI>();

        transform.Find("soundIncButton").GetComponent<Button>().onClick.AddListener(() => {
            soundManager.IncreaseVolume();
            UpdateSoundText();
        });
        transform.Find("soundDecButton").GetComponent<Button>().onClick.AddListener(() => {
            soundManager.decreaseVolume();
            UpdateSoundText();
        });
        transform.Find("musicIncButton").GetComponent<Button>().onClick.AddListener(() => {
            musicManager.IncreaseVolume();
            UpdateMusicText();
        });
        transform.Find("musicDecButton").GetComponent<Button>().onClick.AddListener(() => {
            musicManager.decreaseVolume();
            UpdateMusicText();
        });
        transform.Find("mainMenuButton").GetComponent<Button>().onClick.AddListener(() => {
            Time.timeScale = 1f;
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });
    }

    private void Start() {
        UpdateSoundText();
        gameObject.SetActive(false);
    }

    private void UpdateSoundText() {
        soundVolumeText.SetText(Mathf.RoundToInt(soundManager.GetVolume() * 10).ToString());
    }

    private void UpdateMusicText() {
        musicVolumeText.SetText(Mathf.RoundToInt(musicManager.GetVolume() * 10).ToString());
    }

    public void ToggleVisible() {
        gameObject.SetActive(!gameObject.activeSelf);

        if (gameObject.activeSelf) {
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
        }
    }
}

