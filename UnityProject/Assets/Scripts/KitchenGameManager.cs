using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour {

    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    private float timeWaitingToStart = 1f;
    private float timeCountDownToStart = 3f;
    private float timeGamePlaying;
    private float timeGamePlayingMax = 20f;
    private bool isGamePaused;

    private void Awake() {
        Instance = this;

        state = State.WaitingToStart;
    }

    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e) {
        TogglePauseGame();
    }

    private void Update() {
        timeWaitingToStart -= Time.deltaTime;

        switch (state) {
            case State.WaitingToStart:
                if (timeWaitingToStart <= 0f) {
                    state = State.CountDownToStart;

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountDownToStart:
                timeCountDownToStart -= Time.deltaTime;
                if (timeCountDownToStart <= 0f) {
                    state = State.GamePlaying;

                    OnStateChanged?.Invoke(this, EventArgs.Empty);

                    timeGamePlaying = timeGamePlayingMax;
                }
                break;
            case State.GamePlaying:
                timeGamePlaying -= Time.deltaTime;
                if (timeGamePlaying <= 0f) {
                    state = State.GameOver;

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    public bool IsGameStartCountdownActive() {
        return state == State.CountDownToStart;
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public float GetTimeCountDownToStart() {
        return timeCountDownToStart;
    }

    public float GetTimeGamePlayingNormalized() {
        return 1 - timeGamePlaying / timeGamePlayingMax;
    }

    public void TogglePauseGame() {
        if (!isGamePaused) {
            Time.timeScale = 0f;

            isGamePaused = true;

            OnGamePaused?.Invoke(this, EventArgs.Empty);
        } else {
            Time.timeScale = 1f;

            isGamePaused = false;

            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

}
