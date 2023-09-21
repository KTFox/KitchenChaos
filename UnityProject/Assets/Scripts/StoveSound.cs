using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveSound : MonoBehaviour {

    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;
    private float burnWarningSoundTimer;
    private bool playWarningSound;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnChangedState += StoveCounter_OnChangedState;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnPregressChangedArgs e) {
        float playBurningSoundProgressAmount = 0.5f;
        playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= playBurningSoundProgressAmount;
    }

    private void StoveCounter_OnChangedState(object sender, StoveCounter.OnChangedStateArgs e) {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        if (playSound) {
            audioSource.Play();
        } else {
            audioSource.Pause();
        }
    }

    private void Update() {
        if (playWarningSound) {
            burnWarningSoundTimer -= Time.deltaTime;
            if (burnWarningSoundTimer < 0) {
                float burnWarningSoundTimerMax = 0.2f;
                burnWarningSoundTimer = burnWarningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }

}

