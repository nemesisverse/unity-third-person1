using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private Attack attack;  //attack = stateMachine.Attacks[attackIndex];
    private float previousFrameTime;

    private bool alreadyappliedforce;
    
    public PlayerAttackingState(PlayerStateMachine stateMachine , int attackIndex) : base(stateMachine) //constructor and abstract class
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        Debug.Log($"Switching to combo state with index: {attack.ComboStateIndex}");
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationrName, attack.TransitionDuration);//ye current animation se target animation me  transition me le  jaa rha hai
        
    }
    public override void Exit()
    {
       
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTaret();

        float normalizeTime = GetNormalizeTime();

        if(normalizeTime >= previousFrameTime && normalizeTime < 1)

        {

            if(normalizeTime >= attack.ForceTime )
            {
                TryApplyForce();
            }
            if (stateMachine.InputReader.isAttacking)
            {
                TryComboAttack(normalizeTime);
            }
        }
        else 
        {
            if(stateMachine.targetter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargettingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlyaerFreeLookState(stateMachine));
            }
        }
        previousFrameTime = normalizeTime;
    }

    private void TryApplyForce()
    {
        if (alreadyappliedforce) { return; }
        stateMachine.forcereciever.AddForce(stateMachine.transform.forward * attack.Force);
        alreadyappliedforce = true;
    }
 
    private float GetNormalizeTime()
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if(stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0;

        }

        
    }

    void TryComboAttack(float normalizeTime)
    {
        
        if (attack.ComboStateIndex == -1) { return; }
        if (normalizeTime < attack.ComboAttackTime) { return; }

       

        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attack.ComboStateIndex));

    }

}


