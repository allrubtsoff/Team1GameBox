using StarterAssets;
using UnityEngine;

public class ClowdScript : MonoBehaviour
{
    public Vector3 Position { get; set; }
    public ThirdPersonController Controller { get; set; }
    public float Speed { get; set; }

    private void Update()
    {
        Vector3 target = new Vector3(Controller.transform.position.x, Position.y, Controller.transform.position.z);
        float deltaSpeed = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, deltaSpeed);
    }
}
