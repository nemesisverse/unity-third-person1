using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTargettingState : PlayerBaseState
{
    private readonly int TargettingBlendTree = Animator.StringToHash("TargettingBlendTree");

    private readonly int TargettingForwardHash = Animator.StringToHash("TargettingForwardSpeed");

    private readonly int TargettingRightHash = Animator.StringToHash("TargettingRightSpeed");
    public PlayerTargettingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.InputReader.CancelEvent += OnCancel;
        stateMachine.Animator.Play(TargettingBlendTree);


    }

    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= OnCancel;
        
    }

    void OnCancel()
    {
        stateMachine.targetter.Cancel();
        stateMachine.SwitchState(new PlyaerFreeLookState(stateMachine));
    }

    public override void Tick(float deltaTime)
    {
        

        if(stateMachine.InputReader.isAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        if (stateMachine.targetter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlyaerFreeLookState(stateMachine));
            return;
        }
       
        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.TargettingMovementSpeed, deltaTime);

        UpdateAnimator(deltaTime);


        FaceTaret();
        
    }

    Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        return movement;
    }

    /*** private void UpdateAnimatorTarget(float deltaTime)
     {
         if(stateMachine.InputReader.MovementValue.y == 0)
         {
             stateMachine.Animator.SetFloat(TargettingForwardHash,0, 0.1f, deltaTime);
         }
         else
         {
             float value = stateMachine.InputReader.MovementValue.y > 0 ? 1f:-1f;
             stateMachine.Animator.SetFloat(TargettingForwardHash , value, 0.1f, deltaTime) ;
         }

         if (stateMachine.InputReader.MovementValue.x == 0)
         {
             stateMachine.Animator.SetFloat(TargettingForwardHash, 0, 0.1f, deltaTime);
         }
         else
         {
             float value = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
             stateMachine.Animator.SetFloat(TargettingRightHash, value, 0.1f, deltaTime);

         }
     }
    ***/



    private void UpdateAnimator(float deltaTime)
    {
        stateMachine.Animator.SetFloat(TargettingForwardHash, Normalize(stateMachine.InputReader.MovementValue.x), 0.1f, deltaTime);
        stateMachine.Animator.SetFloat(TargettingRightHash, Normalize(stateMachine.InputReader.MovementValue.y), 0.1f, deltaTime);
    }

    private float Normalize(float value)
    {
        return value == 0f ? 0f : (value < 0f ? -1f : 1f);
    }
}
