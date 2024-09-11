using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class StateMachine : MonoBehaviour
{
    private State CurrentState;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SwitchState(State newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState?.Tick(Time.deltaTime);
    }

   
}
