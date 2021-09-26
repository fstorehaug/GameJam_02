// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""TileInteraction"",
            ""id"": ""8a52daf0-4401-4314-b3b2-7c2aa74c72e7"",
            ""actions"": [
                {
                    ""name"": ""OpenTile"",
                    ""type"": ""Button"",
                    ""id"": ""3859e904-9f87-4d24-81e6-78b6f2eb7926"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""f8c6649f-4cbf-45ed-bdfb-e3133fdf05ff"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f91d08f2-dec7-4cd9-9f2b-367512482517"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenTile"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f5d02e6-b41d-47f9-9046-492e8ef5bf09"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // TileInteraction
        m_TileInteraction = asset.FindActionMap("TileInteraction", throwIfNotFound: true);
        m_TileInteraction_OpenTile = m_TileInteraction.FindAction("OpenTile", throwIfNotFound: true);
        m_TileInteraction_MousePosition = m_TileInteraction.FindAction("MousePosition", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // TileInteraction
    private readonly InputActionMap m_TileInteraction;
    private ITileInteractionActions m_TileInteractionActionsCallbackInterface;
    private readonly InputAction m_TileInteraction_OpenTile;
    private readonly InputAction m_TileInteraction_MousePosition;
    public struct TileInteractionActions
    {
        private @PlayerInput m_Wrapper;
        public TileInteractionActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @OpenTile => m_Wrapper.m_TileInteraction_OpenTile;
        public InputAction @MousePosition => m_Wrapper.m_TileInteraction_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_TileInteraction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TileInteractionActions set) { return set.Get(); }
        public void SetCallbacks(ITileInteractionActions instance)
        {
            if (m_Wrapper.m_TileInteractionActionsCallbackInterface != null)
            {
                @OpenTile.started -= m_Wrapper.m_TileInteractionActionsCallbackInterface.OnOpenTile;
                @OpenTile.performed -= m_Wrapper.m_TileInteractionActionsCallbackInterface.OnOpenTile;
                @OpenTile.canceled -= m_Wrapper.m_TileInteractionActionsCallbackInterface.OnOpenTile;
                @MousePosition.started -= m_Wrapper.m_TileInteractionActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_TileInteractionActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_TileInteractionActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_TileInteractionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @OpenTile.started += instance.OnOpenTile;
                @OpenTile.performed += instance.OnOpenTile;
                @OpenTile.canceled += instance.OnOpenTile;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public TileInteractionActions @TileInteraction => new TileInteractionActions(this);
    public interface ITileInteractionActions
    {
        void OnOpenTile(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
}
