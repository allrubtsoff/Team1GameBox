using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesConfig", menuName = "ScriptableObject/EnemiesConfigs", order = 1)]
public class EnemiesConfigs : ScriptableObject
{
    public float reactDistance;
    public float stoppingDistance;
    public float speed;
    public float specialAttackChance;
    public int damage;
    public int health;
}
