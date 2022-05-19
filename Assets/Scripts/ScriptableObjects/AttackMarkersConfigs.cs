using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackMarkersConfigs", menuName = "ScriptableObject/AttackMarkersConfigs", order = 2)]
public class AttackMarkersConfigs : ScriptableObject
{
    [Header("Enemy")]
    public float jumpMarkerSize = 3f;
    public int jumpMarkerDamage = 10;
    public float rayMarkerLength = 6f;
    public float rayMarkerWidth = 2f;
    public int rayMarkerDamage = 10;
    public float coneMarkerLength = 3f;
    public float coneMarkerWidth = 4f;
    public int coneMarkerDamage = 10;

    [Header("Boss")]
    public float bossJumpMarkerSize = 3f;
    public int bossJumpMarkerDamage = 10;
    public float bossRayMarkerLength = 6f;
    public float bossRayMarkerWidth = 2f;
    public int bossRayMarkerDamage = 10;
    public float bossConeMarkerLength = 3f;
    public float bossConeMarkerWidth = 4f;
    public int bossConeMarkerDamage = 10;
}
