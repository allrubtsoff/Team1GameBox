using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePositionManager : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs input;
    [SerializeField] private LayerMask aimColliderLayerMask;
    
    private RaycastHit raycastHit;
    private GameObject player;

    public Vector3 MousePosition { get; set; }
    public float AngleBetweenMouseAndPlayer { get; set; }

    private void Start()
    {
        player = input.gameObject;
    }

    void Update()
    {
        OnRaycastSystem();
    }

    //TODO
    private void CheckAngleBetweenMouseAndPlayer(Quaternion a, Quaternion b)
    {
        AngleBetweenMouseAndPlayer = Quaternion.Angle(a, b);
    }

    private Vector3 CalculateMousePosition()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = player.transform.position.z;
        return mousePos;
    }

    private void OnRaycastSystem()
    {
        Ray ray = Camera.main.ScreenPointToRay(CalculateMousePosition());
        if (Physics.Raycast(ray, out raycastHit, 999f, aimColliderLayerMask))
        {
            MousePosition = raycastHit.point;
        }
    }

    public void LookAtMouseDirection() 
    {
        player.GetComponent<ThirdPersonController>().isAtacking = true;
        var a = player.transform.rotation;
        Vector3 direction = new Vector3(MousePosition.x, player.transform.position.y, MousePosition.z);
        player.transform.LookAt(Vector3.Lerp(player.transform.position, direction, 1f));
        var b = player.transform.rotation;
        CheckAngleBetweenMouseAndPlayer(a,b);
    }

    public void StopLookingAtMouseDirection()
    {
        player.GetComponent<ThirdPersonController>().isAtacking = false;
    }
}
