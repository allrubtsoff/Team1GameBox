using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class PunchDamage : MonoBehaviour
{
    private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private LayerMask _mask;
    

    public void Kick(int damage, float pushForce)
    {
        if (_enemies != null)
        {
            foreach (var enemy in _enemies)
            {

            }
        }

    }
}
