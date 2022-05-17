using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMarkersController : MonoBehaviour
{
    [SerializeField] private AttackMarkersConfigs markersConfigs;
    [Header("Marker prefabs")]
    [SerializeField] private GameObject pondMarker;

    private void OnEnable()
    {
        ChargingState.CreateMarker += CreateEnemyPondTarget;
    }

    private void OnDisable()
    {
        ChargingState.CreateMarker -= CreateEnemyPondTarget;
    }

    public void CreateEnemyPondTarget(Vector3 pos, float timeToDel) => StartCoroutine(PondCorutine(pos, timeToDel));

    private IEnumerator PondCorutine(Vector3 pos, float timeToDel)
    {
        GameObject pond = Instantiate(pondMarker, pos, Quaternion.identity);
        float delay = timeToDel / 3;
        var pondScript = pond.GetComponent<PondMarkerScript>();
        var pondMaterial = pond.GetComponent<Renderer>().material;
        pondScript.Resize(markersConfigs.jumpMarkerSize, markersConfigs.jumpMarkerSize);
        pondMaterial.color = Color.green;
        yield return new WaitForSeconds(delay);
        pondMaterial.color = Color.yellow;
        yield return new WaitForSeconds(delay);
        pondMaterial.color = Color.red;
        yield return new WaitForSeconds(delay);
        pondScript.TryToHit(markersConfigs.jumpMarkerDamage);
        Destroy(pond);
    }
}
