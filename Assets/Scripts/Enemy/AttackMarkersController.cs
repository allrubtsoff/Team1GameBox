using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMarkersController : MonoBehaviour
{
    [SerializeField] private GameObject pondMarker;

    private IEnumerator PondCorutine(Vector3 pos, float timeToDel)
    {
        GameObject pond = Instantiate(pondMarker, pos, Quaternion.identity);
        var pondMaterial = pond.GetComponent<Material>();
        pondMaterial.color = Color.green;
        yield return new WaitForSeconds(timeToDel / 3);
        pondMaterial.color = Color.yellow;
        yield return new WaitForSeconds(timeToDel / 3);
        pondMaterial.color = Color.red;
        yield return new WaitForSeconds(timeToDel / 3);

    }
}
