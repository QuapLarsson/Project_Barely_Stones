// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Free Movement"",
            ""id"": ""e58bb49a-8d04-4d55-b418-c739e3137071"",
            ""actions"": [
                {
                    ""name"": ""Move Left"",
                    ""type"": ""Button"",
                    ""id"": ""73406914-3bfa-4935-80d0-d86c4d35cfee"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move Right"",
                    ""type"": ""Button"",
                    ""id"": ""a603c8d5-0881-4fb7-bad7-e47ca910291e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move Up"",
                    ""type"": ""Button"",
                    ""id"": ""b1e539c4-8760-45cc-b58e-183a46a1498c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move Down"",
                    ""type"": ""Button"",
                    ""id"": ""26bb9757-5070-488e-adce-31a79f550350"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5ca49071-3122-4722-ab3b-0bbca1aad362"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dec2f945-d96f-4149-bc4d-3d8d4b4c48d7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8cdf472b-58b4-4b54-835e-599fe0832561"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""10945a04-7320-48b9-b789-255c841a47c1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Free Movement
        m_FreeMovement = asset.FindActionMap("Free Movement", throwIfNotFound: true);
        m_FreeMovement_MoveLeft = m_FreeMovement.FindAction("Move Left", throwIfNotFound: true);
        m_FreeMovement_MoveRight = m_FreeMovement.FindAction("Move Right", throwIfNotFound: true);
        m_FreeMovement_MoveUp = m_FreeMovement.FindAction("Move Up", throwIfNotFound: true);
        m_FreeMovement_MoveDown = m_FreeMovement.FindAction("Move Down", throwIfNotFound: true);
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

    // Free Movement
    private readonly InputActionMap m_FreeMovement;
    private IFreeMovementActions m_FreeMovementActionsCallbackInterface;
    private readonly InputAction m_FreeMovement_MoveLeft;
    private readonly InputAction m_FreeMovement_MoveRight;
    private readonly InputAction m_FreeMovement_MoveUp;
    private readonly InputAction m_FreeMovement_MoveDown;
    public struct FreeMovementActions
    {
        private @PlayerControls m_Wrapper;
        public FreeMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveLeft => m_Wrapper.m_FreeMovement_MoveLeft;
        public InputAction @MoveRight => m_Wrapper.m_FreeMovement_MoveRight;
        public InputAction @MoveUp => m_Wrapper.m_FreeMovement_MoveUp;
        public InputAction @MoveDown => m_Wrapper.m_FreeMovement_MoveDown;
        public InputActionMap Get() { return m_Wrapper.m_FreeMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FreeMovementActions set) { return set.Get(); }
        public void SetCallbacks(IFreeMovementActions instance)
        {
            if (m_Wrapper.m_FreeMovementActionsCallbackInterface != null)
            {
                @MoveLeft.started -= m_Wrapper.m_FreeMovementActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.performed -= m_Wrapper.m_FreeMovementActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.canceled -= m_Wrapper.m_FreeMovementActionsCallbackInterface.OnMoveLeft;
                @MoveRight.started -= m_Wrapper.m_FreeMovementActionsCallbackInterface.OnMoveRight;
                @MoveRight.performed -= m_Wrapper.m_FreeMovementActionsCallbackInterface.OnMoveRight;
                @MoveRight.canceled -= m_Wrapper.m_FreeMovementActionsCallbackInterface.OnMoveRight;
                @MoveUp.started -= m_Wrapper.m_FreeMovementActionsCallbackInterface.OnMoveUp;
                @MoveUp.performed -= m_Wrapper.m_FreeMovementActionsCallbackInterface.OnMoveUp;
                @MoveUp.canceled -= m_Wrapper.m_FreeMovementActionsCallbackInterface.OnMoveUp;
                @MoveDown.started -= m_Wrapper.m_FreeMovementActionsCallbackInterface.OnMoveDown;
                @MoveDown.performed -= m_Wrapper.m_FreeMovementActionsCallbackInterface.OnMoveDown;
                @MoveDown.canceled -= m_Wrapper.m_FreeMovementActionsCallbackInterface.OnMoveDown;
            }
            m_Wrapper.m_FreeMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveLeft.started += instance.OnMoveLeft;
                @MoveLeft.performed += instance.OnMoveLeft;
                @MoveLeft.canceled += instance.OnMoveLeft;
                @MoveRight.started += instance.OnMoveRight;
                @MoveRight.performed += instance.OnMoveRight;
                @MoveRight.canceled += instance.OnMoveRight;
                @MoveUp.started += instance.OnMoveUp;
                @MoveUp.performed += instance.OnMoveUp;
                @MoveUp.canceled += instance.OnMoveUp;
                @MoveDown.started += instance.OnMoveDown;
                @MoveDown.performed += instance.OnMoveDown;
                @MoveDown.canceled += instance.OnMoveDown;
            }
        }
    }
    public FreeMovementActions @FreeMovement => new FreeMovementActions(this);
    public interface IFreeMovementActions
    {
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
        void OnMoveUp(InputAction.CallbackContext context);
        void OnMoveDown(InputAction.CallbackContext context);
    }
}
