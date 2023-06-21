using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "My Assets/MeatPattyToBurnedPatty")]
public class RecipeBurningMeatSO : ScriptableObject {
    public KitchenObjectsSO inputKitchenObjectSO;
    public KitchenObjectsSO outputKitchenObjectSO;
    public float burningTimerMax;
    
}
