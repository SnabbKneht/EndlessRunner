using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    // config parameters
    
    public float MovementSpeed => movementSpeed;
    [SerializeField] private float movementSpeed;

    public float JumpHeight => jumpHeight;
    [SerializeField] private float jumpHeight;

    public float GroundCheckDistance => groundCheckDistance;
    [SerializeField] private float groundCheckDistance;
    
    public Vector3 MovementDirection => movementDirection;
    [SerializeField] private Vector3 movementDirection;

    public float GravityValue => gravityValue;
    [SerializeField] private float gravityValue = -9.81f;
    
    // cached references

    public CharacterController Controller { get; private set; }
    
    // properties

    public Lane CurrentLane { get; private set; }
    
    private float CurrentVerticalVelocity { get; set; }
    
    private bool IsGrounded { get; set; }
    
    // ==========

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        CurrentLane = Lane.Middle;
    }
    
    private void Update()
    {
        UpdateIsGrounded();
        HandleMovement();
    }

    private void HandleMovement()
    {
        Controller.Move(Time.deltaTime * MovementSpeed * movementDirection);

        if(IsGrounded && CurrentVerticalVelocity < 0)
        {
            CurrentVerticalVelocity = 0f;
        }

        CurrentVerticalVelocity += gravityValue * Time.deltaTime;
        Controller.Move(Time.deltaTime * CurrentVerticalVelocity * Vector3.up);
    }

    private void UpdateIsGrounded()
    {
        IsGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            GroundCheckDistance,
            LayerMask.GetMask("Ground"));
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        if(!IsGrounded) return;
        CurrentVerticalVelocity = Mathf.Sqrt(JumpHeight * -2f * gravityValue);
    }

    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        if(CurrentLane == Lane.Left) return;
        Controller.Move(Vector3.left * Config.Instance.LaneWidth);
        CurrentLane--;
    }

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        if(CurrentLane == Lane.Right) return;
        Controller.Move(Vector3.right * Config.Instance.LaneWidth);
        CurrentLane++;
    }
}
