using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TileInteraction : MonoBehaviour
{
    private PlayerInput inputActions;

    private void Start()
    {
        inputActions = new PlayerInput();
        inputActions.TileInteraction.Enable();

        inputActions.TileInteraction.OpenTile.performed += OpenTile_performed;

    }

    private void OpenTile_performed(InputAction.CallbackContext obj)
    {
        Ray ray = Camera.main.ScreenPointToRay(inputActions.TileInteraction.MousePosition.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.gameObject.tag.ToLower().Contains("livercell"))
            {
                hit.collider.gameObject.transform.parent.gameObject.GetComponent<LiverCell>().ToggleCell();
            }
        }
    }

}
