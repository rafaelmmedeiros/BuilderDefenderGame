using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    private AudioSource audioSource;

    private float volume = .5f;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();

        volume = LoadPrefs();

        audioSource.volume = volume;
    }

    public void IncreaseVolume() {
        volume += .1f;
        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
        SavePrefs(volume);
    }

    public void decreaseVolume() {
        volume -= .1f;
        volume = Mathf.Clamp01(volume);
        audioSource.volume = volume;
        SavePrefs(volume);
    }

    public void SavePrefs(float volume) {
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public float LoadPrefs() {
        return PlayerPrefs.GetFloat("musicVolume");
    }

    public float GetVolume() {
        return volume;
    }
}
