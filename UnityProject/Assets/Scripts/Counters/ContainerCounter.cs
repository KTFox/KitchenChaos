using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {

    public event EventHandler OnPlayerGrappedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            //Player isn't carrying a kitchen object

            KitchenObject.SpawnKitchenObjectSO(kitchenObjectSO, player);

            OnPlayerGrappedObject?.Invoke(this, EventArgs.Empty);
        }
    }

}
