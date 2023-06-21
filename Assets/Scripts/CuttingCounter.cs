using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress {
    [SerializeField] private KitchenRecipeInputOutputSO[] kitchenObjectInputOutputSO;
    private int cuttingProgress;
    public event EventHandler OnCuttingEvent;
    public event EventHandler<IHasProgress.OnProgressEventArgs> OnProgressBarEvent;
    
    public override void InteractCounter(Player _player){
        if (!AlreadyHasKitchenObject()){
            if (_player.AlreadyHasKitchenObject()){
                if (ValidateKitchenObjectCounter(_player.GetKitchenObjects().GetKitchenObjectsSo())){
                    KitchenObject @new = _player.GetKitchenObjects();
                    @new.SetKitchenObject(this);
                    cuttingProgress = 0;
                    KitchenRecipeInputOutputSO outputSo = HasRecipeWithInput(GetKitchenObjects().GetKitchenObjectsSo());
                    OnProgressBarEvent?.Invoke(this, new IHasProgress.OnProgressEventArgs{
                        progressNormalize = (float)cuttingProgress/outputSo.cuttingMaximumProgress
                    });
                    
                }
            }
            else{
                //player does not carrying something
            }
        }
        else{
            if (_player.AlreadyHasKitchenObject()){
                if (_player.GetKitchenObjects().TryGetPlate(out PlateKitchenObj plateObject )){
                    
                    if (plateObject.TryAddIngridientToList(GetKitchenObjects().GetKitchenObjectsSo())){
                        GetKitchenObjects().DestroySelfObj();
                    }
                }
            }
            else{
                GetKitchenObjects().SetKitchenObject(_player);
            }
        }
    }


    public override void InteractAlternat(Player _player){
        if (AlreadyHasKitchenObject() && ValidateKitchenObjectCounter(GetKitchenObjects().GetKitchenObjectsSo())){
            cuttingProgress++;
            OnCuttingEvent?.Invoke(this, EventArgs.Empty);
            KitchenRecipeInputOutputSO outputSo = HasRecipeWithInput(GetKitchenObjects().GetKitchenObjectsSo());
            OnProgressBarEvent?.Invoke(this, new IHasProgress.OnProgressEventArgs{
                progressNormalize = (float)cuttingProgress/outputSo.cuttingMaximumProgress
            });
            if (cuttingProgress>= outputSo.cuttingMaximumProgress){
                
            KitchenObjectsSO outputKitchenObj =
                GetOutputForInput(GetKitchenObjects().GetKitchenObjectsSo());
            GetKitchenObjects().DestroySelfObj();
            KitchenObject.CreateKitchenObj(outputKitchenObj, this);
            }
        }
    }
    public bool ValidateKitchenObjectCounter(KitchenObjectsSO kitchenObjectsSo){
        KitchenRecipeInputOutputSO outputSo = HasRecipeWithInput(kitchenObjectsSo);
        return outputSo != null;
    }

    public KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputKitchenObjectSO){

          KitchenRecipeInputOutputSO outputSo = HasRecipeWithInput(inputKitchenObjectSO);
          return outputSo.outputKitchenObjectSO;
    }

    private KitchenRecipeInputOutputSO HasRecipeWithInput(KitchenObjectsSO inputKitchenObjectSO){
        foreach (KitchenRecipeInputOutputSO cuttingRecipeSO in kitchenObjectInputOutputSO){
            if (cuttingRecipeSO.inputKitchenObjectSO == inputKitchenObjectSO){
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}