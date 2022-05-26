using System.Collections;
using UnityEngine;

public class AxeReturn : MonoBehaviour
{
    [SerializeField] private PlayerAbilitiesConfigs configs;
    [SerializeField] private float timeToRelocateAfterCollision;
    [SerializeField] private Transform playersHand;
    [SerializeField] private LayerMask playersLayer;
    [SerializeField] private LayerMask enemyLayer;

    private int counter = 0;
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != playersLayer)
        {
            rigidBody.isKinematic = true;
            StartCoroutine(ReturnAxe());
        }
        if (collision.transform.TryGetComponent(out IDamageable damageable) && counter ==0)
        {
            counter++;
            damageable.TakeDamage(configs.axeThrowDmg, enemyLayer);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator ReturnAxe()
    {
        yield return new WaitForSeconds(timeToRelocateAfterCollision);
        counter = 0;
        gameObject.SetActive(false);
        gameObject.transform.parent = playersHand;
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
