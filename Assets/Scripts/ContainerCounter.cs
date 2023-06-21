using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter {
    [SerializeField] private KitchenObjectsSO kitchenObjectsSo;
    public event EventHandler conteinerOpenCloseAnimation;

    public override void InteractCounter(Player _player){
        if (!_player.AlreadyHasKitchenObject()){
            
            KitchenObject newObj = GetKitchenObjects();
            KitchenObject.CreateKitchenObj(kitchenObjectsSo, _player);
            conteinerOpenCloseAnimation?.Invoke(this, EventArgs.Empty);
        }
        else{
            conteinerOpenCloseAnimation?.Invoke(this, EventArgs.Empty);
        }
    }
}