using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectInterface {
  
    [SerializeField] private Transform instantiatePoint;

    private KitchenObject _kitchenObject;
    public virtual void InteractCounter(Player _player){
    }
    public virtual void InteractAlternat(Player _player){
    }
    public Transform KitchenObjectInstantiatePoint(){
        return instantiatePoint;
    }

    public void ClearKitchenObject(){
        _kitchenObject = null;
    }

    public void SetKitchenObject(KitchenObject kitchenObject){
        _kitchenObject = kitchenObject;
    }
    public bool AlreadyHasKitchenObject(){

        return _kitchenObject != null;
    }

    public KitchenObject GetKitchenObjects(){
        return _kitchenObject;
    }
}