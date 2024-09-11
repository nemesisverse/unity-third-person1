using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field:SerializeField] public  InputReader InputReader { get;private set; }
    [field:SerializeField] public  CharacterController controller { get;private set; }

    [field:SerializeField] public  Animator Animator { get;private set; }
    [field:SerializeField] public  float FreeMovementSpeed { get;private set; }
    [field:SerializeField] public  float TargettingMovementSpeed { get;private set; }

    [field: SerializeField] public ForceReciever forcereciever { get; private set; }
    [field:SerializeField] public float RotationDamping { get;private set; }

    [field:SerializeField] public Targetter targetter { get;private set; }
    [field:SerializeField] public Attack[] Attacks { get;private set; }


    

    


    public Transform MainCameraTransform { get; private set; }

    internal void Move(Vector3 vector3)
    {
        throw new NotImplementedException();
    }

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlyaerFreeLookState(this));
    }
}
