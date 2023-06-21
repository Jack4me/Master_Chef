using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter {
    private float timer = 0;
    private float timerPlateCreated = 3;
    private int plateCounter;
    private int plateAmount = 3;

    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemove;
    
    
    [SerializeField] private KitchenObjectsSO plateKitchenObjSO; 
    private void Update(){
        timer += Time.deltaTime;
        if (timer > timerPlateCreated){
            timer = 0f;
            if (plateCounter < plateAmount){
                plateCounter++;
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void InteractCounter(Player _player){
        if (!_player.AlreadyHasKitchenObject()){
            if (plateCounter > 0){
                plateCounter--;
                KitchenObject.CreateKitchenObj(plateKitchenObjSO, _player);
                OnPlateRemove?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}