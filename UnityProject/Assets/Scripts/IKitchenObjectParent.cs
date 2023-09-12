using UnityEngine;

public interface IKitchenObjectParent {

    public Transform GetTheKitchenObjectFollowTransform();
    public void SetKitchenObjet(KitchenObject kitchenObject);

    public KitchenObject GetKitchenObject();

    public void ClearKitchenObject();

    public bool HasKitchenObject();

}
