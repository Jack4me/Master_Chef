using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour {
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;
    
    private void Awake(){
        recipeTemplate.gameObject.SetActive(false);
        
    }

    private void Start(){
        DeliveryManager.Instance.OnRecipeEnd += OnRecipeEnd;
        DeliveryManager.Instance.OnRecipeWainting += OnRecipeWainting;
        UpdateVisual();
    }

    private void OnRecipeWainting(object sender, EventArgs e){
        UpdateVisual();
    }

    private void OnRecipeEnd(object sender, EventArgs e){
        UpdateVisual();
    }

    public void UpdateVisual(){
        foreach (Transform child in container){
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (RecipeOrderSO recipeSO in DeliveryManager.Instance.GetRecipeSOList()){
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetText(recipeSO);
        }
    }
}