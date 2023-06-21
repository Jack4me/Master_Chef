using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform icon;

    private void Start(){
        icon.gameObject.SetActive(false);
    }

    public void SetText(RecipeOrderSO recipeOrderSo){
        text.text = recipeOrderSo.recipeName;

        foreach (Transform child in iconContainer){
            if(child == icon)continue;
             Destroy(child.gameObject);
        }

        foreach (KitchenObjectsSO kitchenObjSO in recipeOrderSo.kitchenObjectsSOList){
            Transform newKitchenObj = Instantiate(icon, iconContainer);
            newKitchenObj.gameObject.SetActive(true);
            newKitchenObj.GetComponent<Image>().sprite = kitchenObjSO.visual;
        }
    }

    
}
