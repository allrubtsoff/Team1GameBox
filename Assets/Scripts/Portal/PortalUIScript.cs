using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalUIScript : MonoBehaviour
{
    private MeshRenderer m_MeshRenderer;

    private void OnEnable() => PortalScript.PlayerInTrigger += ChangeUIState;
    private void OnDisable() => PortalScript.PlayerInTrigger -= ChangeUIState;


    private void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
    }

    private void ChangeUIState(bool newState) => m_MeshRenderer.enabled = newState;
}
