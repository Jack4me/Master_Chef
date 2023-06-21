using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CuttingCounterAnimation : MonoBehaviour {
   private const string CUT = "Cut";
     private Animator cuttingCounterAnimation;
     [SerializeField]private CuttingCounter cuttingCounter;

    private void Awake(){
        cuttingCounterAnimation =  GetComponent<Animator>();
    }

    private void Start(){
        cuttingCounter.OnCuttingEvent += CuttingCounterOnOnCuttingEvent;
        
    }

    private void CuttingCounterOnOnCuttingEvent(object sender, EventArgs e){
        cuttingCounterAnimation.SetTrigger(CUT);
    }

    
}
