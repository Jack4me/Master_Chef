using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter {
    [SerializeField] private KitchenObjectsSO kitchenObjectsSo;

    public override void InteractCounter(Player _player){
        if (!AlreadyHasKitchenObject()){
            if (_player.AlreadyHasKitchenObject()){
                //_player.GetKitchenObjects().SetKitchenObject(this); same as two filds below 
                KitchenObject kitchenObjects = _player.GetKitchenObjects();
                kitchenObjects.SetKitchenObject(this);
            }
            else{
                //player does not carrying something
            }
        }
        else{
            if (_player.AlreadyHasKitchenObject()){
                if (_player.GetKitchenObjects().TryGetPlate(out PlateKitchenObj plateObject)){
                    if (plateObject.TryAddIngridientToList(GetKitchenObjects().GetKitchenObjectsSo())){
                        GetKitchenObjects().DestroySelfObj();
                    }
                }
                else{
                    if (GetKitchenObjects().TryGetPlate(out plateObject)){
                        if (plateObject.TryAddIngridientToList(_player.GetKitchenObjects().GetKitchenObjectsSo())){
                            _player.GetKitchenObjects().DestroySelfObj();
                        }
                    }
                }
            }
            else{
                GetKitchenObjects().SetKitchenObject(_player);
            }
        }
    }
}