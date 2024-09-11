using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReciever : MonoBehaviour
{
    [SerializeField] CharacterController Controller;
    public Vector3 impact;
    public Vector3 Movement  => impact + Vector3.up*Verticalvelocity;

    private float Verticalvelocity;

    private Vector3 dampingvelocity;

    [SerializeField] private float drag = 0.3f;

    

    private void Update()
    {
        if(Verticalvelocity <0 && Controller.isGrounded)
        {
            Verticalvelocity = Physics.gravity.y * Time.deltaTime;
        }

        else
        {
            Verticalvelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact , Vector3.zero ,ref dampingvelocity , drag);
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }


}
