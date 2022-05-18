using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lol : MonoBehaviour
{
    [SerializeField] MousePositionManager mousepos;
    [SerializeField] GameObject player;

    // Update is called once per frame
    void Update()
    {
        //transform.position = mousepos.MousePosition;
        transform.position = player.transform.position +  player.transform.forward;
        //Debug.Log(Vector3.Angle(transform.position, mousepos.MousePosition));   
    }
}
