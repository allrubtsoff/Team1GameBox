using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IPickable
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<StarterAssets.StarterAssetsInputs>() != null)
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        // типа добавили в интвентарь в свободный слот если есть, если нет то так и лежим
    }
}
