using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "OrderRecipe")]
public class RecipeOrderSO : ScriptableObject {
    public List<KitchenObjectsSO> kitchenObjectsSOList;
    public string recipeName;
}
