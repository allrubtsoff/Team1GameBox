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
        Vector2 vector = player.GetComponent<StarterAssets.StarterAssetsInputs>().move;
        transform.position = new Vector3( player.transform.position.x,0,player.transform.position.z)+ new Vector3(vector.x, player.transform.position.y, vector.y);
        //transform.position = player.transform.position +  player.transform.forward;
    }
}
