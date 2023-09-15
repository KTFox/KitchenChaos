using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    [SerializeField] private List<KitchenObjectSO> validKitchenSOList;

    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if (validKitchenSOList.Contains(kitchenObjectSO)) {
            // There is a kitchen object that is an ingredient on the clear counter

            if (kitchenObjectSOList.Contains(kitchenObjectSO)) {
                return false;
            } else {
                kitchenObjectSOList.Add(kitchenObjectSO);
                return true;
            }
        } else {
            return false;
        }
    }

}

