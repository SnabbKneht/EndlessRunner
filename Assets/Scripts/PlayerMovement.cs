using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // config parameters
    
    public float MovementSpeed => movementSpeed;
    [SerializeField] private float movementSpeed;

    public float HorizontalMovementSpeed => horizontalMovementSpeed;
    [SerializeField] private float horizontalMovementSpeed;

    public float JumpHeight => jumpHeight;
    [SerializeField] private float jumpHeight;

    public float GroundCheckDistance => groundCheckDistance;
    [SerializeField] private float groundCheckDistance;
    
    public Vector3 MovementDirection => movementDirection;
    [SerializeField] private Vector3 movementDirection;

    public float GravityValue => gravityValue;
    [SerializeField] private float gravityValue = -9.81f;

    private AudioClip JumpSound => jumpSound;
    [SerializeField] private AudioClip jumpSound;

    private float JumpSoundVolume => jumpSoundVolume;
    [SerializeField] private float jumpSoundVolume;
    
    // cached references

    private CharacterController Controller { get; set; }

    private AudioSource AudioSourceReference { get; set; }

    private Animator AnimatorReference { get; set; }
    
    // properties

    public Lane CurrentLane { get; private set; }
    
    private float CurrentVerticalVelocity { get; set; }
    
    public bool IsGrounded { get; private set; }
    
    private float TargetHorizontalCoord { get; set; }
    
    // ==========

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        AudioSourceReference = GetComponent<AudioSource>();
        AnimatorReference = GetComponentInChildren<Animator>();
        CurrentLane = Lane.Middle;
        TargetHorizontalCoord = 0f;
    }
    
    private void Update()
    {
        UpdateIsGrounded();
        HandleMovement();
        
        AnimatorReference.SetBool("IsGrounded", IsGrounded);
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
        
        HandleHorizontalPosition();
    }

    private void HandleHorizontalPosition()
    {
        if(Mathf.Approximately(transform.position.x, TargetHorizontalCoord)) return;
        float moveDirection = Mathf.Sign(TargetHorizontalCoord - transform.position.x);
        float moveX = moveDirection * HorizontalMovementSpeed * Time.deltaTime;
        
        var moveVector = new Vector3(moveX, 0f, 0f);
        Controller.Move(moveVector);
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
        AudioSourceReference.PlayOneShot(JumpSound, JumpSoundVolume);
        AnimatorReference.SetTrigger("Jump");
    }

    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        if(CurrentLane == Lane.Left) return;
        TargetHorizontalCoord -= Config.Instance.LaneWidth;
        CurrentLane--;
    }

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if(!context.performed) return;
        if(CurrentLane == Lane.Right) return;
        TargetHorizontalCoord += Config.Instance.LaneWidth;
        CurrentLane++;
    }
}
