using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kek : MonoBehaviour
{
    [SerializeField] MousePositionManager mousepos;
    [SerializeField] GameObject player;
    void Update()
    {
        transform.position = player.transform.position + player.transform.forward;
    }
}
