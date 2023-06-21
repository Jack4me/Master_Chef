using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ContainerCounterAnimation : MonoBehaviour {
   private const string OPEN_CLOSE_TRIGGER = "OpenClose";
     private Animator containerCounterAnimation;
     [SerializeField]private ContainerCounter conteinerCounter;

    private void Awake(){
      containerCounterAnimation =  GetComponent<Animator>();
    }

    private void Start(){
        conteinerCounter.conteinerOpenCloseAnimation += ConteinerCounterOncounteinerOpenCloseAnimation;
    }

    private void ConteinerCounterOncounteinerOpenCloseAnimation(object sender, EventArgs e){
        containerCounterAnimation.SetTrigger(OPEN_CLOSE_TRIGGER);
    }
}
