using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    public event EventHandler<OnIngredientAddedArgs> OnIngredientAdded;
    public class OnIngredientAddedArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenSOList;

    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if (validKitchenSOList.Contains(kitchenObjectSO)) {
            // Is a valid ingredient

            if (kitchenObjectSOList.Contains(kitchenObjectSO)) {
                return false;
            } else {
                kitchenObjectSOList.Add(kitchenObjectSO);

                OnIngredientAdded?.Invoke(this, new OnIngredientAddedArgs { kitchenObjectSO = kitchenObjectSO });

                return true;
            }
        } else {
            return false;
        }
    }

}

