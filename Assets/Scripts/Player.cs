using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateAnimationSpeed = 10f;
    [SerializeField] private GameInput gameInput;

    private bool _isWalking;
    private Vector3 lastInteractDirection;

    public bool IsWalking => _isWalking;
    
    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDirection = moveDir;
        }

        float interactionDistance = 2f;
        bool didHit = Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit hitInfo, interactionDistance);

        if (didHit)
        {
            if (hitInfo.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                // Has ClearCounter
                clearCounter.Interact();
            }
        }

    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = (moveSpeed * Time.deltaTime);
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position,
            transform.position + Vector3.up * playerHeight,
            playerRadius,
            moveDir,
            moveDistance);
        if (canMove) {
            transform.position += moveDir * moveDistance;
        }
        
        _isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateAnimationSpeed * Time.deltaTime);
    }

}
