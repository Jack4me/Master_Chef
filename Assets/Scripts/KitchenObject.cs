using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    private IKitchenObjectInterface kitchenObjectInterface;
    private Transform instantiatePoint;

    public KitchenObjectsSO GetKitchenObjectsSo(){
        return kitchenObjectsSO;
    }

    public bool TryGetPlate(out PlateKitchenObj plateObject){
        if (this is PlateKitchenObj){
            plateObject = this as PlateKitchenObj;
            return true;
        }
        else{
            plateObject = null;
            return false;
        }
    }

    public void SetKitchenObject(IKitchenObjectInterface _kitchenTakerInterface){
        if (this.kitchenObjectInterface != null){
            this.kitchenObjectInterface.ClearKitchenObject();
        }
        this.kitchenObjectInterface = _kitchenTakerInterface;
        if (kitchenObjectInterface.AlreadyHasKitchenObject()){
            Debug.LogError("Already has kitchen object");
        }
        this.kitchenObjectInterface.SetKitchenObject(this);
        transform.parent = this.kitchenObjectInterface.KitchenObjectInstantiatePoint();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectInterface GetIKitchenObjectInterface(){
        return kitchenObjectInterface;
    }

    public void DestroySelfObj(){
        kitchenObjectInterface.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject CreateKitchenObj(KitchenObjectsSO kitchenObjectsSo, IKitchenObjectInterface kitchenObjectInterface){
        
        Transform newFoodObjTransform = Instantiate(kitchenObjectsSo.prefab);
        
        KitchenObject kitchenObject = newFoodObjTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObject(kitchenObjectInterface);
        return kitchenObject;
    }
}