using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "My KitchenObjects/SolidKitchenObjects")]
public class KitchenObjectsSO : ScriptableObject {
    public Transform prefab;
    public string nameFood;
    public Sprite visual;
}
