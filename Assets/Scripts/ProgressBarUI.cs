using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject interfaceGameObj;
    private IHasProgress IHasProgress;

    private void Start(){
        IHasProgress = interfaceGameObj.GetComponent<IHasProgress>();
        IHasProgress.OnProgressBarEvent += OnHasProgressBarEvent;
        progressBar.fillAmount = 0f;
        Hide();
    }

    private void OnHasProgressBarEvent(object sender, IHasProgress.OnProgressEventArgs e){
        progressBar.fillAmount = e.progressNormalize;
        if (e.progressNormalize == 0 || e.progressNormalize == 1){
            Hide();
        }
        else{
            Show();
        }
    }

    private void Hide(){
        gameObject.SetActive(false);
    }

    private void Show(){
        gameObject.SetActive(true);
    }
}