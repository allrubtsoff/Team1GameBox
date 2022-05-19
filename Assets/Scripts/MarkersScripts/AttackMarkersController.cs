using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMarkersController : MonoBehaviour
{
    [SerializeField] private AttackMarkersConfigs markersConfigs;
    [Header("Marker prefabs")]
    [SerializeField] private GameObject pondMarkerPrefab;
    [SerializeField] private GameObject rayMarkerPrefab;

    private const float posCorrection = 1f;

    private void Awake()
    {
        LikhoChargingState.CreateMarker += CreateEnemyPondMarker;
        GiantChargeState.CreateMarker += CreateEnemyRayMarker;
        AirAtack.CreateMarker += CreateEnemyPondMarker;
    }

    private void CreateEnemyPondMarker(Vector3 pos, float timeToDel) => StartCoroutine(PondCorutine(pos, timeToDel));
    private void CreateEnemyRayMarker(Vector3 pos, Vector3 target, float timeToDel) 
        => StartCoroutine(RayCorutine(pos, target, timeToDel));


    private IEnumerator PondCorutine(Vector3 pos, float timeToDel)
    {
        GameObject pond = Instantiate(pondMarkerPrefab, pos, Quaternion.identity);
        float delay = timeToDel / 3;
        var pondScript = pond.GetComponent<MarkerDamageScript>();
        var pondMaterial = pond.GetComponent<Renderer>().material;
        pondScript.PondResize(markersConfigs.jumpMarkerSize);
        pondMaterial.color = Color.green;
        yield return new WaitForSeconds(delay);
        pondMaterial.color = Color.yellow;
        yield return new WaitForSeconds(delay);
        pondMaterial.color = Color.red;
        yield return new WaitForSeconds(delay);
        pondScript.TryToHit(markersConfigs.jumpMarkerDamage);
        Destroy(pond);
    }

    private IEnumerator RayCorutine(Vector3 pos, Vector3 target, float timeToDel)
    {
        target.y -= posCorrection;
        pos.y -= posCorrection;
        GameObject rayMark = Instantiate(rayMarkerPrefab, pos, Quaternion.identity);
        rayMark.transform.LookAt(target);
        rayMark.transform.Rotate(90, rayMark.transform.rotation.y, rayMark.transform.rotation.z);
        float delay = timeToDel / 3;
        var rayMarkScript = rayMark.GetComponent<MarkerDamageScript>();
        var rayMaterial = rayMark.transform.GetChild(0).GetComponent<Renderer>().material;
        rayMarkScript.RayResize(markersConfigs.rayMarkerWidth, markersConfigs.rayMarkerLength);
        rayMaterial.color = Color.green;
        yield return new WaitForSeconds(delay);
        rayMaterial.color = Color.yellow;
        yield return new WaitForSeconds(delay);
        rayMaterial.color = Color.red;
        yield return new WaitForSeconds(delay);
        rayMarkScript.TryToHit(markersConfigs.rayMarkerDamage);
        Destroy(rayMark);
    }

    private void OnDestroy()
    {
        LikhoChargingState.CreateMarker -= CreateEnemyPondMarker;
        GiantChargeState.CreateMarker -= CreateEnemyRayMarker;
        AirAtack.CreateMarker -= CreateEnemyPondMarker;
    }
}
