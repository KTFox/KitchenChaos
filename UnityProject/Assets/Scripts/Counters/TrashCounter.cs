using System;
using UnityEngine;

public class TrashCounter : BaseCounter {

    public static event EventHandler OnAnyObjectTrashed;

    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            // Player is carrying a kitchen object

            player.GetKitchenObject().DestroySelf();

            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);  
        }
    }

}

