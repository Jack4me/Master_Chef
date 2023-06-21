using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "My Assets/CuttingObjects")]
public class KitchenRecipeInputOutputSO : ScriptableObject {
    public KitchenObjectsSO inputKitchenObjectSO;
    public KitchenObjectsSO outputKitchenObjectSO;
    public int cuttingMaximumProgress;
}
