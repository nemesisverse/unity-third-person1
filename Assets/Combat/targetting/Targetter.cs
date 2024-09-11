using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetter : MonoBehaviour
{
    private Camera MaineCamera;

    [SerializeField] private CinemachineTargetGroup cinetargetGroup;
    public List<Target> targets = new List<Target>();

    
    public Target CurrentTarget { get; private set; }

    private void Start()
    {
        MaineCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        Target target = other.GetComponent<Target>();

        if (target == null) { return; }

        targets.Add(target);
        target.OnDestroyed += RemoveTarget;

    }


    private void OnTriggerExit(Collider other)
    {
        Target target = other.GetComponent<Target>();

        if (target == null) { return; }

        targets.Remove(target);
        
    }

    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }

        Target cloasesttarget = null;
        float closesttargetdistance = Mathf.Infinity;

        foreach(Target target in targets)
        {
            Vector2 ViewPos = MaineCamera.WorldToViewportPoint(target.transform.position); //screen par object   kidhar hai?

            if(ViewPos.x < 0 || ViewPos.x > 1  || ViewPos.y < 0 || ViewPos.y > 1)
            {
                continue;
            }

            Vector2 tocenter = ViewPos - new Vector2(0.5f,0.5f);
            if(tocenter.sqrMagnitude < closesttargetdistance)
            {
                cloasesttarget = target;
                closesttargetdistance = tocenter.sqrMagnitude;
            }
            
        }

        if (cloasesttarget == null) { return false; }

        CurrentTarget = cloasesttarget;
        cinetargetGroup.AddMember(CurrentTarget.transform,1f,2f);

        return true;

    }

    public void Cancel()
    {
        if (CurrentTarget == null) { return; }
        cinetargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }


   private void RemoveTarget(Target target)
    {
        if(CurrentTarget == target)
        {
            cinetargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }

        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);

        

    }
}
