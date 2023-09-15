using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress {

    public event EventHandler<IHasProgress.OnPregressChangedArgs> OnProgressChanged;
    public event EventHandler<OnChangedStateArgs> OnChangedState;
    public class OnChangedStateArgs : EventArgs {
        public State state;
    }

    public enum State {
        Idle, Frying, Fried, Burned
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;
    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;

    private void Start() {
        state = State.Idle;
    }

    private void Update() {
        if (HasKitchenObject()) {
            switch (state) {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnPregressChangedArgs {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax) {
                        // Frying is complete

                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObjectSO(fryingRecipeSO.output, this);

                        state = State.Fried;

                        burningTimer = 0f;

                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnChangedState?.Invoke(this, new OnChangedStateArgs { state = state });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnPregressChangedArgs {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });

                    if (burningTimer > burningRecipeSO.burningTimerMax) {
                        // Object burned

                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObjectSO(burningRecipeSO.output, this);

                        state = State.Burned;

                        OnChangedState?.Invoke(this, new OnChangedStateArgs { state = state });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnPregressChangedArgs {
                            progressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // The counter doesn't have a kitchen object on it

            if (player.HasKitchenObject()) {
                // Player is carrying kitchen object

                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // Player is carrying something that can be fried

                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;

                    fryingTimer = 0f;

                    OnChangedState?.Invoke(this, new OnChangedStateArgs { state = state });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnPregressChangedArgs { 
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax });
                }
            } else {
                // Player isn't carrying a kitchen object
            }
        } else {
            // The counter has a kitchen object on it

            if (player.HasKitchenObject()) {
                // Player is carrying a kitchen object

                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // Player is carrying a plate

                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();

                        state = State.Idle;

                        OnChangedState?.Invoke(this, new OnChangedStateArgs { state = state });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnPregressChangedArgs {
                            progressNormalized = 0f
                        });
                    }
                }
            } else {
                // Player isn't carrying a kitchen object

                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;

                OnChangedState?.Invoke(this, new OnChangedStateArgs { state = state });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnPregressChangedArgs {
                    progressNormalized = 0f
                });
            }
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        if (fryingRecipeSO != null) {
            return fryingRecipeSO.output;
        } else {
            return null;
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        return fryingRecipeSO != null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
            if (fryingRecipeSO.input == inputKitchenObjectSO) {
                return fryingRecipeSO;
            }
        }

        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray) {
            if (burningRecipeSO.input == inputKitchenObjectSO) {
                return burningRecipeSO;
            }
        }

        return null;
    }

}

