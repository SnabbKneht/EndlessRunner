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

    private AudioClip JumpSound => jumpSound;
    [SerializeField] private AudioClip jumpSound;

    private float JumpSoundVolume => jumpSoundVolume;
    [SerializeField] private float jumpSoundVolume;
    
    // cached references

    private CharacterController Controller { get; set; }

    private AudioSource AudioSourceReference { get; set; }
    
    // properties

    public Lane CurrentLane { get; private set; }
    
    private float CurrentVerticalVelocity { get; set; }
    
    public bool IsGrounded { get; private set; }
    
    // ==========

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        AudioSourceReference = GetComponent<AudioSource>();
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
        AudioSourceReference.PlayOneShot(JumpSound, JumpSoundVolume);
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