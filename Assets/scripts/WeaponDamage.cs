using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider mycollider;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
       // if (other == mycollider) { return; }

        //if (alreadyCollidedWith.Contains(other)) { return; }

        alreadyCollidedWith.Add(other);
        if(other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(10);
        }
    }
}
