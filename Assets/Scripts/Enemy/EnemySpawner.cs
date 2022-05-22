using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ThirdPersonController _controller;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private List<GameObject> enemiesOnScene;
    [SerializeField] private float _spawnMaxDistance;
    [SerializeField] private float _spawnMinDistance;
    [SerializeField] private float _spawnDelay;


    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    private void Awake()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            for (int j = 0; j < enemies[i].count; j++)
            {
                GameObject enemyObject = Instantiate(enemies[i].prefab);
                enemiesOnScene.Add(enemyObject);
                enemyObject.SetActive(false);
            }
        }       
    }

    private void Start()
    {
        Shuffle(enemiesOnScene);
    }

    public void EnemySummon(int enemiesCount) => StartCoroutine(SummonCorutine(enemiesCount));

    private void Shuffle(List<GameObject> list)
    {
        System.Random r = new System.Random();

        for (int i = list.Count - 1; i >= 1; i--)
        {
            int j = r.Next(i + 1);

            GameObject tmp = list[j];
            list[j] = list[i];
            enemiesOnScene[i] = tmp;
        }
    }

    private IEnumerator SummonCorutine(int enemiesCount)
    {
        for (int i = 0; i < enemiesOnScene.Count; i++)
        {
            yield return new WaitForSeconds(_spawnDelay);
            Vector3 point;

            while (!(RandomPoint(transform.position, _spawnMaxDistance, out point) &&
                                Vector3.Distance(transform.position, point) > _spawnMinDistance))
            {
#if(UNITY_EDITOR)
                Debug.Log("Looking for a new point to spawn");
#endif
            }

            if (i > enemiesCount)
            {
                StopCoroutine(SummonCorutine(enemiesCount));
                Shuffle(enemiesOnScene);
                break;
            }
            else
            {
                enemiesOnScene[i].GetComponent<EnemyController>().Target = _controller.transform;
                enemiesOnScene[i].transform.position = point;
                enemiesOnScene[i].SetActive(true);
            }
        }
    }
}

[System.Serializable]
public class Enemy
{
    public int count;
    public GameObject prefab;
}
