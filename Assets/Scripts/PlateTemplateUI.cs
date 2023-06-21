using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateTemplateUI : MonoBehaviour {
    [SerializeField] private PlateKitchenObj plateKitchenObj;
    [SerializeField] private Transform iconTemplate;

    private void Awake(){
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start(){
        plateKitchenObj.OnAddIngredients += PlateKitchenObjOnOnAddIngredients;
        
    }

    private void PlateKitchenObjOnOnAddIngredients(object sender, PlateKitchenObj.OnIngredientAdd e){
        UpdateVisualIcon();
    }

    public void UpdateVisualIcon(){
        foreach (Transform children in transform){
             if (children == iconTemplate)continue;
                Destroy(children.gameObject);
            
        }
        foreach (KitchenObjectsSO kitchenObjectsSO in plateKitchenObj.GetKitchenObjSOList()){
            Transform transformIcon = Instantiate(iconTemplate, transform);
            transformIcon.gameObject.SetActive(true);
            transformIcon.GetComponent<IconSingleUI>().SetKitchenIconSO(kitchenObjectsSO);
        }
    }
}