using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerAbilitiesConfigs", menuName = "ScriptableObject/PlayerAbilitiesConfigs", order = 3)]

public class PlayerAbilitiesConfigs : ScriptableObject
{
    [Header("Magic Kick")]
    public float magicKickDamage;
    public float magicKickCost;
    public float magicKickCooldown;
    public float magicKickRange;

    [Header("Mighty Punch")]
    public float mightyPunchDamage;
    public float mightyPunchCost;
    public float mightyPunchCooldown;
    public float mightyPunchRange;
}
