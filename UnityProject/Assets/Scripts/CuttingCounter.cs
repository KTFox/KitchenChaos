using UnityEngine;

public class CuttingCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // The counter doesn't have a kitchen object on it

            if (player.HasKitchenObject()) {
                // Player is carrying kitchen object

                player.GetKitchenObject().SetKitchenObjectParent(this);
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
        if (HasKitchenObject()) {
            // There is a kitchen object here

            // Cutting kitchen object
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObjectSO(cutKitchenObjectSO, this);
        }
    }

}

