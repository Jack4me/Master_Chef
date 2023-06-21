using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter {
    public override void InteractCounter(Player _player){
        if (_player.AlreadyHasKitchenObject()){
            _player.GetKitchenObjects().DestroySelfObj();
        }
    }
}