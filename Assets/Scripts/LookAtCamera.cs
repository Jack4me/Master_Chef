using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {
    public enum ModeCutting {
        Normal,
        Invert,
        LookAtCamera,
        InvertedLookAtCamera
    }

    public ModeCutting modeCutting;

    private void LateUpdate(){
        switch (modeCutting){
            case ModeCutting.Normal:
                transform.LookAt(Camera.main.transform);
                break;
            case ModeCutting.Invert:
                Vector3 direction = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + direction);
                break;
            case ModeCutting.LookAtCamera:
                transform.forward = Camera.main.transform.forward;
                break;
            case ModeCutting.InvertedLookAtCamera:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}