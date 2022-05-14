using UnityEngine;
using StarterAssets;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(ActorView))]

public class PlayerInput2 : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private ActorView _actorView;
    private StarterAssetsInputs _inputs;
    private Vector3 _mousePos;
    private float _x = 0;
    private float _y = 0;

    private void Awake()
    {
        _inputs = GetComponent<StarterAssetsInputs>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        _x = _inputs.move.x;
        _y = _inputs.move.y;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(_inputs.look), out hit))
        {
            _mousePos = hit.point;
        }

        if (_x != 0 || _y != 0)
        {
            _playerMovement.Move(_x, _y);

        }
        _playerMovement.MouseLook(_mousePos);

        _actorView.AnimationState(_x, _y);
    }

}
