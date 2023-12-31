using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {

    public const string HIGHEST_SCORES = "HighestScores";

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeSOTimer;
    private float spawnRecipeSOTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int scores;

    private void Awake() {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Start() {
        scores = 0;
    }

    private void Update() {
        SpawnRecipeSO();
    }

    private void SpawnRecipeSO() {
        spawnRecipeSOTimer -= Time.deltaTime;

        if (spawnRecipeSOTimer <= 0) {
            spawnRecipeSOTimer = spawnRecipeSOTimerMax;

            if (KitchenGameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipeMax) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                // Has the same number of ingredients

                bool plateContentMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    // Cycling through all ingredients in the Recipe

                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        // Cycling through all ingredients in the Plate

                        if (plateKitchenObjectSO == recipeKitchenObjectSO) {
                            // Ingredient matches!!!

                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound) {
                        // This Recipe ingredient was not found on the Plate

                        plateContentMatchesRecipe = false;
                    }
                }

                if (plateContentMatchesRecipe) {
                    // Player has delivered correct recipe!

                    scores += waitingRecipeSO.scores;
                    UpdateHighestScores();

                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);

                    return;
                }
            }
        }

        // No matches found
        // Player did not deliver correct recipe!!!
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    private void UpdateHighestScores() {
        if (scores > PlayerPrefs.GetInt(HIGHEST_SCORES)) {
            PlayerPrefs.SetInt(HIGHEST_SCORES, scores);
            PlayerPrefs.Save();
        }
    }

    public List<RecipeSO> GetRecipeSOWaitingList() {
        return waitingRecipeSOList;
    }

    public int GetScores() {
        return scores;
    }

}

