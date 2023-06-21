using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVIsual : MonoBehaviour {
    [SerializeField] private GameObject fryingParticleVisual;
    [SerializeField] private GameObject fryingStoveVisual;
    [SerializeField] StoveCounter stoveCounter;

    private void Start(){
        stoveCounter.OntoveVisualEvent += StoveCounterOnOntoveVisualEvent;
    }

    private void StoveCounterOnOntoveVisualEvent(object sender, StoveCounter.OnStateChangedEventArgs e){
        bool ShowFried = e.stateCookingArgs == StoveCounter.State.Frying ||
                         e.stateCookingArgs == StoveCounter.State.Fried;
        
            fryingParticleVisual.SetActive(ShowFried);
            fryingStoveVisual.SetActive(ShowFried);
            
        
    }
}