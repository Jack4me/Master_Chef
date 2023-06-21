using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateVisual : MonoBehaviour {
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform topPoint;
    [SerializeField] private Transform prefabPlate;
    private List<GameObject> plateVisualGameObjList;

    private void Awake(){
        plateVisualGameObjList = new List<GameObject>();
    }

    private void Start(){
        plateCounter.OnPlateSpawn += PlateCounterOnOnPlateSpawn;
        plateCounter.OnPlateRemove += PlateCounterOnOnPlateRemove;
    }

    private void PlateCounterOnOnPlateRemove(object sender, EventArgs e){
        GameObject lastPlateInList = plateVisualGameObjList[plateVisualGameObjList.Count-1];
        plateVisualGameObjList.Remove(lastPlateInList);
        Destroy(lastPlateInList);
    }

    private void PlateCounterOnOnPlateSpawn(object sender, EventArgs e){
        Transform plateTransform = Instantiate(prefabPlate, topPoint);
        plateTransform.localPosition = new Vector3(0, 0.1f * plateVisualGameObjList.Count, 0);
        plateVisualGameObjList.Add(plateTransform.gameObject);
    }
}