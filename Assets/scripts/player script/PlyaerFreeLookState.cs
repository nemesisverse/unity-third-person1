using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlyaerFreeLookState : PlayerBaseState
{
    [SerializeField] float speed = 2f;

    private readonly int FreeLookBlendTree = Animator.StringToHash("FreeLookBlendTree");

    public PlyaerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        
        stateMachine.InputReader.TargetEvent += OnTarget;

        
        stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTree,0.2f);
    }

    public override void Tick(float deltaTime)
    {

        if (stateMachine.InputReader.isAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine,0));
            return;
        }


        Vector3 movement = CalculateMovement();


        Move(movement * stateMachine.FreeMovementSpeed , deltaTime);
        
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat("FreeLookSpeed", 0, 0.1f, deltaTime);
            return;
        }
        stateMachine.Animator.SetFloat("FreeLookSpeed", 1, 0.1f, deltaTime);
        FreeLookRotation(movement ,deltaTime);

    }

   

    private void FreeLookRotation(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation , 
            Quaternion.LookRotation(movement) , deltaTime*stateMachine.RotationDamping);
    }

    void OnTarget()
    {
        if (!stateMachine.targetter.SelectTarget()) { return; }
        stateMachine.SwitchState(new PlayerTargettingState(stateMachine));
    }

    public override void Exit()
    {
       stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x;



    }



    

    
    


  

 

 
}
