using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconSingleUI : MonoBehaviour {
    [SerializeField] private Image icon;
    public void SetKitchenIconSO(KitchenObjectsSO kitchenObjectsSo){
        icon.sprite = kitchenObjectsSo.visual;
        
    }
}
