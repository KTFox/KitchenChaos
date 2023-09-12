using UnityEngine;

public class KitchenObject : MonoBehaviour {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public ClearCounter GetClearCounter() {
        return clearCounter;
    }

    public void SetClearCounter(ClearCounter clearCounter) {
        if (this.clearCounter != null) {
            this.clearCounter.ClearKitchenObject();
        }

        this.clearCounter = clearCounter;

        if (this.clearCounter.HasKitchenObject()) {
            Debug.LogError("The counter already has a kitchen object!!!");
        }

        this.clearCounter.SetKitchenObjet(this);

        transform.parent = clearCounter.GetTheKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

}

