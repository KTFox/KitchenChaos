using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter {

    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            // Player is carrying something

            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                // Player is carrying a plate

                player.GetKitchenObject().DestroySelf();
            }
        }
    }

}

