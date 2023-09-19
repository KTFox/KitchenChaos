using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveSound : MonoBehaviour {

    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnChangedState += StoveCounter_OnChangedState;
    }

    private void StoveCounter_OnChangedState(object sender, StoveCounter.OnChangedStateArgs e) {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        if (playSound) {
            audioSource.Play();
        } else {
            audioSource.Pause();
        }
    }
}

