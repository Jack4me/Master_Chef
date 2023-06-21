using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour {
    [Serializable]
    public struct KitchenSO_GameObj {
        public KitchenObjectsSO kitchenObjectsSO;
        public GameObject kitchenGameObject;

    }

    [SerializeField] private List<KitchenSO_GameObj> kitchenGameObjList;
    [SerializeField] private PlateKitchenObj plateKitchenObj;

    private void Start(){
        plateKitchenObj.OnAddIngredients += PlateKitchenObjOnOnAddIngredients;
        foreach (KitchenSO_GameObj linkKitchenObj in kitchenGameObjList){
            linkKitchenObj.kitchenGameObject.SetActive(false);
            
        }
    }

    private void PlateKitchenObjOnOnAddIngredients(object sender, PlateKitchenObj.OnIngredientAdd e){
        foreach (KitchenSO_GameObj linkKitchenObj in kitchenGameObjList){
            if (linkKitchenObj.kitchenObjectsSO == e.kitchenObjectsSo){
                linkKitchenObj.kitchenGameObject.SetActive(true);
            }
        }
    }
}
