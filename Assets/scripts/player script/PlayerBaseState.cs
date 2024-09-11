using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public void Move(float deltaTime)
    {
        Move(Vector3.zero , deltaTime);
    }
    public void Move(Vector3 motion , float deltaTime)
    {
        stateMachine.controller.Move((motion + stateMachine.forcereciever.Movement) * deltaTime);
    }

    protected void FaceTaret()
    {
        if(stateMachine.targetter.CurrentTarget == null)
        {
            return;
        }

        Vector3 lookpos = stateMachine.targetter.CurrentTarget.transform.position - stateMachine.transform.position;
        lookpos.y = 0f;
        stateMachine.transform.rotation= Quaternion.LookRotation(lookpos);
    }
}
