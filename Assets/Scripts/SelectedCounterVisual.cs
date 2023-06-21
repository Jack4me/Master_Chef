using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {
    [SerializeField] private BaseCounter selectedBaseCounter;
    [SerializeField] private GameObject[] visualSelectedArray;

    private void Start(){
        Player.Instance_Player.OnSelectedCounterChange += Instance_PlayerOnSelectedCounterChange;
    }

    private void Instance_PlayerOnSelectedCounterChange(object sender, Player.SelectedCounterEventArgs e){
        if (e.selectedCounterArgs == selectedBaseCounter){
            Show();
        }
        else{
            Hide();
        }
    }

    private void Show(){
        foreach (GameObject gameFoodVisual in visualSelectedArray){
            gameFoodVisual.SetActive(true);
        }
    }

    private void Hide(){
        foreach (GameObject gameFoodVisual in visualSelectedArray){
            gameFoodVisual.SetActive(false);
        }
    }
}