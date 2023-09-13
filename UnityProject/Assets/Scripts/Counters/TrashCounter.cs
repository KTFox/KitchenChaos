using UnityEngine;

public class TrashCounter : BaseCounter {

    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            // Player is carrying a kitchen object

            player.GetKitchenObject().DestroySelf();
        }
    }

}

