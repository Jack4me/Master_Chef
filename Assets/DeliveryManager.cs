using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour {
    public static DeliveryManager Instance{ get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeOrderSO> waitingRecipeSOList;
    private float spawnRecipeTimer = 4f;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;
    public event EventHandler OnRecipeWainting;
    public event EventHandler OnRecipeEnd;

    private void Awake(){
        waitingRecipeSOList = new List<RecipeOrderSO>();
        Instance = this;
        if (Instance == null){
            Debug.LogError("We are have more than one INSTANCE");
            Instance = this;
        }
    }

    private void Start(){
    }

    public List<RecipeOrderSO> GetRecipeSOList(){
        return waitingRecipeSOList;
    }

    private void Update(){
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f){
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (waitingRecipeSOList.Count < waitingRecipeMax){
                RecipeOrderSO waitingRecipeSO =
                    recipeListSO.recipeListSO[Random.Range(0, recipeListSO.recipeListSO.Count)];
                Debug.Log(waitingRecipeSO);
                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeWainting?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DevilerRecipe(PlateKitchenObj plateKitchenObj){
        for (int i = 0; i < waitingRecipeSOList.Count; i++){
            RecipeOrderSO recipeOrderSO = waitingRecipeSOList[i];
            if (recipeOrderSO.kitchenObjectsSOList.Count == plateKitchenObj.GetKitchenObjSOList().Count){
                bool plateContentsCountMatches = true;
                foreach (KitchenObjectsSO kitchenObjectsSO in recipeOrderSO.kitchenObjectsSOList){
                    bool ingridientFound = false;
                    foreach (KitchenObjectsSO plateObjectsSO in plateKitchenObj.GetKitchenObjSOList()){
                        if (kitchenObjectsSO == plateObjectsSO){
                            ingridientFound = true;
                            break;
                        }
                    }
                    if (!ingridientFound){
                        plateContentsCountMatches = false;
                    }
                }
                if (plateContentsCountMatches){
                    Debug.Log("Recive correct Recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeEnd?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
    }
}