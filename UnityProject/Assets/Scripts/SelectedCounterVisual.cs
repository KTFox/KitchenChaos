using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {

    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualCounter;


    private void Start() {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedArgs e) {
        if (e.selectedCounter == clearCounter) {
            Show();
        } else { Hide(); }
    }

    private void Show() {
        visualCounter.SetActive(true);
    }

    private void Hide() {
        visualCounter?.SetActive(false);
    }

}

