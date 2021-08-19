// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerInput.inputactions'

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
            ""name"": ""Player"",
            ""id"": ""b948c5ce-dc8b-4400-a7cc-b7900bf854fa"",
            ""actions"": [
                {
                    ""name"": ""CurlingRotate"",
                    ""type"": ""Button"",
                    ""id"": ""53602a9d-c7f7-41e9-9e83-b406181f5eb6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BrushAddSpeed"",
                    ""type"": ""Button"",
                    ""id"": ""c59d3e08-8176-4530-b3c8-d80b7d46b0c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cc5d9425-9217-4e62-a279-396f30ad0cfc"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keybiard"",
                    ""action"": ""CurlingRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ccffb5a7-f66b-479b-b636-9b2f5b47bb6a"",
                    ""path"": ""<Keyboard>/slash"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keybiard"",
                    ""action"": ""BrushAddSpeed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keybiard"",
            ""bindingGroup"": ""Keybiard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_CurlingRotate = m_Player.FindAction("CurlingRotate", throwIfNotFound: true);
        m_Player_BrushAddSpeed = m_Player.FindAction("BrushAddSpeed", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_CurlingRotate;
    private readonly InputAction m_Player_BrushAddSpeed;
    public struct PlayerActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @CurlingRotate => m_Wrapper.m_Player_CurlingRotate;
        public InputAction @BrushAddSpeed => m_Wrapper.m_Player_BrushAddSpeed;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @CurlingRotate.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCurlingRotate;
                @CurlingRotate.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCurlingRotate;
                @CurlingRotate.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCurlingRotate;
                @BrushAddSpeed.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBrushAddSpeed;
                @BrushAddSpeed.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBrushAddSpeed;
                @BrushAddSpeed.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBrushAddSpeed;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CurlingRotate.started += instance.OnCurlingRotate;
                @CurlingRotate.performed += instance.OnCurlingRotate;
                @CurlingRotate.canceled += instance.OnCurlingRotate;
                @BrushAddSpeed.started += instance.OnBrushAddSpeed;
                @BrushAddSpeed.performed += instance.OnBrushAddSpeed;
                @BrushAddSpeed.canceled += instance.OnBrushAddSpeed;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeybiardSchemeIndex = -1;
    public InputControlScheme KeybiardScheme
    {
        get
        {
            if (m_KeybiardSchemeIndex == -1) m_KeybiardSchemeIndex = asset.FindControlSchemeIndex("Keybiard");
            return asset.controlSchemes[m_KeybiardSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnCurlingRotate(InputAction.CallbackContext context);
        void OnBrushAddSpeed(InputAction.CallbackContext context);
    }
}
