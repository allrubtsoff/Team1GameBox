using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMarkersController : MonoBehaviour
{
    [SerializeField] private AttackMarkersConfigs markersConfigs;
    [Header("Marker prefabs")]
    [SerializeField] private GameObject pondMarkerPrefab;
    [SerializeField] private GameObject rayMarkerPrefab;
    [SerializeField] private GameObject coneMarkerPrefab;

    private const float posCorrection = 0.1f;
    private const int multyConesCount = 2;
    private const float k_Angle = 120f;

    private void Awake()
    {
        LikhoChargingState.CreateMarker += CreateEnemyPondMarker;
        GiantChargeState.CreateMarker += CreateEnemyRayMarker;
        AirAtack.CreateMarker += CreateEnemyPondMarker;
    }

    private void CreateEnemyPondMarker(Vector3 pos, float timeToDel) 
        => StartCoroutine(PondCorutine(pos, timeToDel));
    private void CreateEnemyRayMarker(Vector3 pos, Vector3 target, float timeToDel) 
        => StartCoroutine(RayCorutine(pos, target, timeToDel));
    private void CreateSingeConeMarker(Vector3 pos, Vector3 target, float timeToDel)
        => StartCoroutine(ConeCorutine(pos, target, timeToDel));
    private void CreateMultyCones(Vector3 pos, float timeToDel) 
        => StartCoroutine(MultyConeCorutine(pos, timeToDel)); 


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
        pos.y = posCorrection;
        target.y = pos.y;
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

    private IEnumerator ConeCorutine(Vector3 pos, Vector3 target, float timeToDel)
    {
        pos.y = posCorrection;
        target.y = pos.y;
        GameObject cone = Instantiate(coneMarkerPrefab, pos, Quaternion.identity);
        cone.transform.LookAt(target);
        var coneScript = cone.GetComponent<MarkerDamageScript>();
        var coneMaterial = cone.GetComponent<Renderer>().material;
        coneScript.RayResize(markersConfigs.coneMarkerWidth, markersConfigs.coneMarkerLength);
        float delay = timeToDel / 3;
        coneMaterial.color = Color.green;
        yield return new WaitForSeconds(delay);
        coneMaterial.color = Color.yellow;
        yield return new WaitForSeconds(delay);
        coneMaterial.color = Color.red;
        yield return new WaitForSeconds(delay);
        coneScript.TryToHit(markersConfigs.coneMarkerDamage);
        Destroy(cone);
    }

    private IEnumerator MultyConeCorutine(Vector3 pos, float timeToDel)
    {
        pos.y -= posCorrection;
        float r = Random.Range(0, k_Angle);
        GameObject[] cones = new GameObject[multyConesCount];
        MarkerDamageScript[] conesScript = new MarkerDamageScript[multyConesCount];
        Material[] conesMaterial = new Material[multyConesCount];
        for (int i = 0; i < cones.Length; i++)
        {
            cones[i] = Instantiate(coneMarkerPrefab, pos, Quaternion.Euler(0, r, 0));
            conesScript[i] = cones[i].GetComponent<MarkerDamageScript>();
            conesScript[i].ConeResize(markersConfigs.bossConeMarkerWidth, markersConfigs.bossConeMarkerLength);
            conesMaterial[i] = cones[i].GetComponent<Renderer>().material;
            conesMaterial[i].color = Color.green;
            r += k_Angle;
        }
        float delay = timeToDel / 3;
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < cones.Length; i++)
        {
            conesMaterial[i].color = Color.yellow;
        }
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < cones.Length; i++)
        {
            conesMaterial[i].color = Color.red;
        }
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < cones.Length; i++)
        {
            conesScript[i].TryToHit(markersConfigs.bossConeMarkerDamage);
            Destroy(cones[i]);
        }
    }

    private void OnDestroy()
    {
        LikhoChargingState.CreateMarker -= CreateEnemyPondMarker;
        GiantChargeState.CreateMarker -= CreateEnemyRayMarker;
        AirAtack.CreateMarker -= CreateEnemyPondMarker;
    }
}
