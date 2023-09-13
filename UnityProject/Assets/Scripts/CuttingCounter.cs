using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // The counter doesn't have a kitchen object on it

            if (player.HasKitchenObject()) {
                // Player is carrying kitchen object

                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // Player is carrying something that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            } else {
                // Player isn't carrying a kitchen object
            }
        } else {
            // The counter has a kitchen object on it

            if (player.HasKitchenObject()) {
                // Player is carrying a kitchen object
            } else {
                // Player isn't carrying a kitchen object

                GetKitchenObject().SetKitchenObjectParent(player);

            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            // There is a kitchen object that can be cut here
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObjectSO(outputKitchenObjectSO, this);
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return cuttingRecipeSO.output;
            }
        }

        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return true;
            }
        }

        return false;
    }

}

