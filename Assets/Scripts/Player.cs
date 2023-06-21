using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectInterface {
    public static Player Instance_Player{ get; set; }

    public int Speed;
    private bool isWalking;
    [SerializeField] private LayerMask counterMask;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private Transform instantiatePoint;


    private KitchenObject _kitchenObject;
    private Vector3 lastInteractDir;
    public event EventHandler<SelectedCounterEventArgs> OnSelectedCounterChange;

    public class SelectedCounterEventArgs : EventArgs {
        public BaseCounter selectedCounterArgs;
    }

    private void Awake(){
        if (Instance_Player != null){
            Debug.LogError("We are have more than one INSTANCE");
        }
        Instance_Player = this;
    }

    private void Start(){
        gameInput.OnInteractEventHandler += OnInteractEventHandler;
        gameInput.OnInteractAlternat += OnInputInteractAlternat;
    }

    private void OnInputInteractAlternat(object sender, EventArgs e){
        if (baseCounter != null){
            baseCounter.InteractAlternat(this);
        }
    }

    private void OnInteractEventHandler(object sender, EventArgs e){
        if (baseCounter != null){
            baseCounter.InteractCounter(this);
        }
    }

    void Update(){
        HandleMovement();
        HadnleInteractions();
    }

    private void HadnleInteractions(){
        Vector3 inputVector = gameInput.Vector2InputNormalazed();
        Vector3 direction = new Vector3(inputVector.x, 0, inputVector.y);
        if (direction != Vector3.zero){
            lastInteractDir = direction;
        }
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance,
                counterMask)){
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)){
                if (baseCounter != this.baseCounter){
                    ClearCounterSelected(baseCounter);
                }
            }
            else{
                ClearCounterSelected(null);
            }
        }
        else{
            ClearCounterSelected(null);
        }
    }

    private void HandleMovement(){
        Vector3 inputVector = gameInput.Vector2InputNormalazed();
        Vector3 direction = new Vector3(inputVector.x, 0, inputVector.y);
        float distance = Speed * Time.deltaTime;
        float playerHeight = 1f;
        float playerRadius = .7f;
        bool canMove =  !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
            playerRadius, direction, distance);
        if (!canMove){
            Vector3 moveX = new Vector3(direction.x, 0, 0).normalized;
            canMove = direction.x != 0 &&!Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                playerRadius, moveX, distance);
            if (canMove){
                direction = moveX;
            }
            else{
                Vector3 moveZ = new Vector3(0, 0, direction.z).normalized;
                canMove = direction.z != 0 &&!Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight,
                    playerRadius, moveZ, distance);
                if (canMove){
                    direction = moveZ;
                }
            }
        }
        if (canMove){
            transform.position += direction * distance;
        }
        isWalking = direction != Vector3.zero;
        float rotateSpeed = 12;
        transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking(){
        return isWalking;
    }

    private void ClearCounterSelected(BaseCounter baseCounter){
        this.baseCounter = baseCounter;
        OnSelectedCounterChange?.Invoke(this, new SelectedCounterEventArgs{
            selectedCounterArgs = this.baseCounter
        });
    }
    public Transform KitchenObjectInstantiatePoint(){
        return instantiatePoint;
    }


    public void SetKitchenObject(KitchenObject kitchenObject){
        _kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObjects(){
        return _kitchenObject;
    }

    public void ClearKitchenObject(){
        _kitchenObject = null;
    }

    public bool AlreadyHasKitchenObject(){
        return _kitchenObject != null;
    }
}