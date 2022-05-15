using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePositionManager : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs input;
    [SerializeField] private LayerMask aimColliderLayerMask;
   
    private RaycastHit raycastHit;

    public Vector3 MousePosition { get; set; }

    void Update()
    {
        OnRaycastSystem();
    }

    private Vector3 CalculateMousePosition()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = input.gameObject.transform.position.z;
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
}
