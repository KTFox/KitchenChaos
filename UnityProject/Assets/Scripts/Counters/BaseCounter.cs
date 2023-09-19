using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {

    public static event EventHandler OnAnyObjectPlaceHere;

    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public virtual void Interact(Player player) {
        Debug.LogError("BaseCounter.Interact().");
    }

    public virtual void InteractAlternate(Player player) {

    }

    public Transform GetTheKitchenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void SetKitchenObjet(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) {
            OnAnyObjectPlaceHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

}

