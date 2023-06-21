using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {
    public event EventHandler<IHasProgress.OnProgressEventArgs> OnProgressBarEvent;

    [SerializeField] private RecipeFryingMeatSO[] cookedPattySOArray;
    [SerializeField] private RecipeBurningMeatSO[] burningPattySOArray;
    private RecipeFryingMeatSO fryingRecipeSo;
    private RecipeBurningMeatSO burningRecipeMeatSo;
    private float fryingTimer;
    private float burningTimer;

    public event EventHandler<OnStateChangedEventArgs> OntoveVisualEvent;
    public class OnStateChangedEventArgs : EventArgs {
        public State stateCookingArgs;
    }

    public enum State {
        Idle,
        Frying,
        Fried,
        Burned
    }

    public State stateCookingMeat;

    private void Start(){
        stateCookingMeat = State.Idle;
        
    }

    private void Update(){
        if (AlreadyHasKitchenObject()){
            switch (stateCookingMeat){
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressBarEvent?.Invoke(this, new IHasProgress.OnProgressEventArgs{
                        progressNormalize =  fryingTimer/ fryingRecipeSo.fryingTimerProgress
                    });
                    if (fryingTimer > fryingRecipeSo.fryingTimerProgress){
                        GetKitchenObjects().DestroySelfObj();
                        KitchenObject.CreateKitchenObj(fryingRecipeSo.outputKitchenObjectSO, this);
                        stateCookingMeat = State.Fried;
                        burningTimer = 0f;
                         burningRecipeMeatSo = GetBurnedRecipeSO(GetKitchenObjects().GetKitchenObjectsSo());
                         OntoveVisualEvent?.Invoke(this, new OnStateChangedEventArgs{
                             stateCookingArgs = stateCookingMeat
                         });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressBarEvent?.Invoke(this, new IHasProgress.OnProgressEventArgs{
                        progressNormalize =  burningTimer/ burningRecipeMeatSo.burningTimerMax
                    });
                    if (burningTimer > burningRecipeMeatSo.burningTimerMax){
                        Debug.Log("Burned");
                        GetKitchenObjects().DestroySelfObj();
                        

                        KitchenObject.CreateKitchenObj(burningRecipeMeatSo.outputKitchenObjectSO, this);
                        stateCookingMeat = State.Burned;
                        OntoveVisualEvent?.Invoke(this, new OnStateChangedEventArgs{
                            stateCookingArgs = stateCookingMeat
                        });
                    }
                    
                    break;
                case State.Burned:
                    OnProgressBarEvent?.Invoke(this, new IHasProgress.OnProgressEventArgs{
                        progressNormalize =  0f
                    });
                    break;
            }
        }
    }

    public override void InteractCounter(Player _player){
        if (!AlreadyHasKitchenObject()){
            if (_player.AlreadyHasKitchenObject()){
                if (HasRecipeWithInput(_player.GetKitchenObjects().GetKitchenObjectsSo())){
                    KitchenObject fromPlayer = _player.GetKitchenObjects();
                    fromPlayer.SetKitchenObject(this);
                    
                    fryingRecipeSo = GetFryingRecipeSO(GetKitchenObjects().GetKitchenObjectsSo());
                    fryingTimer = 0f;
                    stateCookingMeat = State.Frying;
                    OntoveVisualEvent?.Invoke(this, new OnStateChangedEventArgs{
                        stateCookingArgs = stateCookingMeat
                    });
                    
                    OnProgressBarEvent?.Invoke(this, new IHasProgress.OnProgressEventArgs{
                        progressNormalize =  fryingTimer/ fryingRecipeSo.fryingTimerProgress
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
                stateCookingMeat = State.Idle;
                OntoveVisualEvent?.Invoke(this, new OnStateChangedEventArgs{
                    stateCookingArgs = stateCookingMeat
                });
            }
        }
    }

    public bool HasRecipeWithInput(KitchenObjectsSO kitchenObjectsSo){
        RecipeFryingMeatSO outputSo = GetFryingRecipeSO(kitchenObjectsSo);
        return outputSo != null;
    }

    public KitchenObjectsSO GetOutputFromInput(KitchenObjectsSO inputKitchenObjectSO){
        RecipeFryingMeatSO fryingOutputSo = GetFryingRecipeSO(inputKitchenObjectSO);
        if (fryingOutputSo != null){
            return fryingOutputSo.outputKitchenObjectSO;
        }
        else{
            return null;
        }
    }

    private RecipeFryingMeatSO GetFryingRecipeSO(KitchenObjectsSO inputKitchenObjectSO){
        foreach (RecipeFryingMeatSO friyingRicipe in cookedPattySOArray){
            if (friyingRicipe.inputKitchenObjectSO == inputKitchenObjectSO){
                return friyingRicipe;
            }
        }
        return null;
    }

    private RecipeBurningMeatSO GetBurnedRecipeSO(KitchenObjectsSO inputKitchenObjectSO){
        foreach (RecipeBurningMeatSO BurnedPatty in burningPattySOArray){
            if (BurnedPatty.inputKitchenObjectSO == inputKitchenObjectSO){
                return BurnedPatty;
            }
        }
        return null;
    }
}