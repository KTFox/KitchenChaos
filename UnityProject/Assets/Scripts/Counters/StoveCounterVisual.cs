using UnityEngine;

public class StoveCounterVisual : MonoBehaviour {

    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;
    [SerializeField] private StoveCounter stoveCounter;

    private void Start() {
        stoveCounter.OnChangedState += StoveCounter_OnChangedState;
    }

    private void StoveCounter_OnChangedState(object sender, StoveCounter.OnChangedStateArgs e) {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        stoveOnGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }
}

