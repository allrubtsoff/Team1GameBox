using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class AttackMarkersController : MonoBehaviour
{
    [SerializeField] private AttackMarkersConfigs markersConfigs;
    [Header("Marker prefabs")]
    [SerializeField] private GameObject pondMarkerPrefab;
    [SerializeField] private GameObject rayMarkerPrefab;
    [SerializeField] private GameObject coneMarkerPrefab;

    private const float yPosCorrection = 0.02f;
    private const int multyConesCount = 3;
    private const float k_Angle = 120f;
    private const float cloudCorDelay = 0.5f;

    private void Awake()
    {
        LikhoChargingState.CreateMarker += CreateEnemyPondMarker;
        GiantChargeState.CreateMarker += CreateEnemyRayMarker;
        AirAtack.CreateMarker += CreateEnemyPondMarker;
        YagaController.CreateConeMarker += CreateSingeConeMarker;
    }

    public void CreateEnemyPondMarker(Vector3 pos, float timeToDel) 
        => StartCoroutine(PondCorutine(pos, timeToDel));
    public void CreateEnemyRayMarker(Vector3 pos, Vector3 target, float timeToDel) 
        => StartCoroutine(RayCorutine(pos, target, timeToDel));
    public void CreateSingeConeMarker(Vector3 pos, Vector3 target, float timeToDel)
        => StartCoroutine(ConeCorutine(pos, target, timeToDel));
    public void CreateMultyCones(Vector3 pos, float timeToDel) 
        => StartCoroutine(MultyConeCorutine(pos, timeToDel));
    public void CreateClowdSpell(ThirdPersonController controller, Vector3 pos)
        => StartCoroutine(ClowdSpellCorutine(controller, pos));
    public void CreateBossPondSpell(Vector3 pos, float timeToDel)
        => StartCoroutine(BossPondCorutine(pos, timeToDel));

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
        pos.y = yPosCorrection;
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
        pos.y = yPosCorrection;
        target.y = pos.y;
        GameObject cone = Instantiate(coneMarkerPrefab, pos, Quaternion.identity);
        cone.transform.LookAt(target);
        var coneScript = cone.GetComponent<MarkerDamageScript>();
        var coneMaterial = cone.GetComponent<Renderer>().material;
        coneScript.ConeResize(markersConfigs.bossSingleConeMarkerWidth, markersConfigs.bossSingleConeMarkerLength);
        float delay = timeToDel / 3;
        coneMaterial.color = Color.green;
        yield return new WaitForSeconds(delay);
        coneMaterial.color = Color.yellow;
        yield return new WaitForSeconds(delay);
        coneMaterial.color = Color.red;
        yield return new WaitForSeconds(delay);
        coneScript.TryToHit(markersConfigs.bossConeMarkerDamage);
        Destroy(cone);
    }

    private IEnumerator MultyConeCorutine(Vector3 pos, float timeToDel)
    {
        pos.y -= yPosCorrection;
        float r = Random.Range(0, k_Angle);
        GameObject[] cones = new GameObject[multyConesCount];
        MarkerDamageScript[] conesScript = new MarkerDamageScript[multyConesCount];
        Material[] conesMaterial = new Material[multyConesCount];
        for (int i = 0; i < cones.Length; i++)
        {
            cones[i] = Instantiate(coneMarkerPrefab, pos, Quaternion.Euler(0, r, 0));
            conesScript[i] = cones[i].GetComponent<MarkerDamageScript>();
            conesScript[i].ConeResize(markersConfigs.bossMultyConeMarkerWidth, markersConfigs.bossMultyConeMarkerLength);
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

    private IEnumerator ClowdSpellCorutine(ThirdPersonController controller, Vector3 pos)
    {
        GameObject clowd = Instantiate(pondMarkerPrefab, pos, Quaternion.identity);
        var clowdMarkerScript = clowd.GetComponent<MarkerDamageScript>();
        var clowdMaterial = clowd.GetComponent<Renderer>().material;
        clowdMarkerScript.PondResize(markersConfigs.bossPondMarkerSize);
        clowdMaterial.color = Color.magenta;
        clowd.AddComponent<ClowdScript>();
        var clowdFollow = clowd.GetComponent<ClowdScript>();
        clowdFollow.Position = pos;
        clowdFollow.Controller = controller;
        clowdFollow.Speed = markersConfigs.bossClowdMarkerSpeed;
        Destroy(clowd, markersConfigs.bossClowdMarkerLifetime);
        yield break;
    }

    private IEnumerator BossPondCorutine(Vector3 pos, float timeToDel)
    {
        GameObject pond = Instantiate(pondMarkerPrefab, pos, Quaternion.identity);
        float delay = timeToDel / 3;
        var pondScript = pond.GetComponent<MarkerDamageScript>();
        var pondMaterial = pond.GetComponent<Renderer>().material;
        pondScript.PondResize(markersConfigs.bossPondMarkerSize);
        pondMaterial.color = Color.green;
        yield return new WaitForSeconds(delay);
        pondMaterial.color = Color.yellow;
        yield return new WaitForSeconds(delay);
        pondMaterial.color = Color.red;
        yield return new WaitForSeconds(delay);
        pondScript.TryToHit(markersConfigs.bossPondMarkerDamage);
        Destroy(pond);
    }

    private void OnDestroy()
    {
        LikhoChargingState.CreateMarker -= CreateEnemyPondMarker;
        GiantChargeState.CreateMarker -= CreateEnemyRayMarker;
        AirAtack.CreateMarker -= CreateEnemyPondMarker;
        YagaController.CreateConeMarker -= CreateSingeConeMarker;
    }
}
