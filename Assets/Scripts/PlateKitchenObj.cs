using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObj : KitchenObject {
     private List<KitchenObjectsSO> kitchenObjSOList;
     [SerializeField]private List<KitchenObjectsSO> validRecipeKitchenObjSO;
     
     public event EventHandler<OnIngredientAdd> OnAddIngredients;

     public class OnIngredientAdd : EventArgs {
        public KitchenObjectsSO kitchenObjectsSo;
     }

    private void Awake(){
        kitchenObjSOList = new List<KitchenObjectsSO>();
    }

    public bool TryAddIngridientToList(KitchenObjectsSO kitchenObjectsSo){
        if (!validRecipeKitchenObjSO.Contains(kitchenObjectsSo)){
            return false;
        }
        if (kitchenObjSOList.Contains(kitchenObjectsSo)){
            return false;
        }
        else{
            kitchenObjSOList.Add(kitchenObjectsSo);
            OnAddIngredients?.Invoke(this, new OnIngredientAdd{
                kitchenObjectsSo = kitchenObjectsSo
            });
            return true;
        }
    }
    
    public List<KitchenObjectsSO> GetKitchenObjSOList(){
        return kitchenObjSOList;
    }
}