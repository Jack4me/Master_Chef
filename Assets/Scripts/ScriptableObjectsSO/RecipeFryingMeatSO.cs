using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "My Assets/MeatPattyToCookedPatty")]
public class RecipeFryingMeatSO : ScriptableObject {
    public KitchenObjectsSO inputKitchenObjectSO;
    public KitchenObjectsSO outputKitchenObjectSO;
    public float fryingTimerProgress;
    
}
