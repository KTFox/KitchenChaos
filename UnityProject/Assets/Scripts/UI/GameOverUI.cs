using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI scoresNumberText;
    [SerializeField] private TextMeshProUGUI highestScoresNumberText;

    private void Start() {
        KitchenGameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (KitchenGameManager.Instance.IsGameOver()) {
            Show();

            scoresNumberText.text = DeliveryManager.Instance.GetScores().ToString();
            highestScoresNumberText.text = PlayerPrefs.GetInt(DeliveryManager.HIGHEST_SCORES).ToString();
        } else {
            Hide();
        }
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

}

