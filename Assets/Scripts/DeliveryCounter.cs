using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void InteractCounter(Player _player){
        if (_player.AlreadyHasKitchenObject()){
            if (_player.GetKitchenObjects().TryGetPlate(out PlateKitchenObj plateKitchenObj)){
                DeliveryManager.Instance.DevilerRecipe(plateKitchenObj);
                _player.GetKitchenObjects().DestroySelfObj();
            }
        }
    }
}
