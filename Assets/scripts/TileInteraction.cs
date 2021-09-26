using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = System.Diagnostics.Debug;

public class TileInteraction : MonoBehaviour
{
    private PlayerInput _inputActions;

    private void Start()
    {
        _inputActions = new PlayerInput();
        _inputActions.TileInteraction.Enable();

        _inputActions.TileInteraction.OpenTile.performed += OpenTile_performed;

    }

    private void OpenTile_performed(InputAction.CallbackContext obj)
    {
        var ray = Camera.main.ScreenPointToRay(_inputActions.TileInteraction.MousePosition.ReadValue<Vector2>());
        if (!Physics.Raycast(ray, out var hit, 100)) return;
        if (hit.collider.gameObject.tag.ToLower().Contains("livercell"))
        {
            hit.collider.gameObject.transform.parent.gameObject.GetComponent<LiverCell>().ToggleCell();
        }
    }

}
