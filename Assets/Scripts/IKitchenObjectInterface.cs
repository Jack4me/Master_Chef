using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectInterface {
    public Transform KitchenObjectInstantiatePoint();


public void ClearKitchenObject();


public void SetKitchenObject(KitchenObject kitchenObject);

public KitchenObject GetKitchenObjects();

public bool AlreadyHasKitchenObject();
}
