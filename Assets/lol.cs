using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lol : MonoBehaviour
{
    [SerializeField] MousePositionManager mousepos;

    // Update is called once per frame
    void Update()
    {
        transform.position = mousepos.MousePosition;
    }
}
