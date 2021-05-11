// GENERATED AUTOMATICALLY FROM 'Assets/Player Controls.inputactions'

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
    ""name"": ""Player Controls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""9fb8c2e1-f959-4462-8679-596c8678d845"",
            ""actions"": [
                {
                    ""name"": ""Gas"",
                    ""type"": ""Button"",
                    ""id"": ""f3bf0945-f4bf-43fe-9bc2-edeabfec96e2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reverse"",
                    ""type"": ""Button"",
                    ""id"": ""baf038b3-ffaf-4d11-b1c9-8866d024577c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""85131782-df9d-44bd-9e3e-d18f48328954"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""View"",
                    ""type"": ""PassThrough"",
                    ""id"": ""eaf15b21-7e1c-4767-99f9-64c7d6af133b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ResetCamera"",
                    ""type"": ""Button"",
                    ""id"": ""d128d0f4-45c4-49cc-a1a4-88b72e553a2a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ReverseCamera"",
                    ""type"": ""Button"",
                    ""id"": ""41fcee48-6a60-4d5a-a396-ee895d617d88"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.1)""
                },
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""fd78a256-263b-4e26-ba1a-35a26b8a01b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.1)""
                },
                {
                    ""name"": ""NextChannel"",
                    ""type"": ""Button"",
                    ""id"": ""e2a52324-1703-43e6-8ab7-d95087237abb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PreviousChannel"",
                    ""type"": ""Button"",
                    ""id"": ""fcaf3065-5fea-4f15-8816-6c05fbaaa0ae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""VolUp"",
                    ""type"": ""Button"",
                    ""id"": ""5e1b02a0-50fc-45dc-a852-8fee764bd0c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""VolDown"",
                    ""type"": ""Button"",
                    ""id"": ""0898368a-936c-4999-b9c9-5d428e5e8fc9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""29eb365e-5932-4842-8a2f-0a73c0e044be"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Gas"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a921edb2-927e-4b72-b413-5f6cb83f880a"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reverse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b239516-e6d6-4c42-aa5c-ffb4e11a2ec9"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb1a464d-c47b-4f53-a146-6a234441241b"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""View"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""269e204c-6e19-4590-9ebf-5e7065dbf092"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5349f52b-4d16-4029-a94e-853752edfdd6"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReverseCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84196395-d577-4c0f-9901-b27bebb6844f"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66e01ac7-f0e3-4af4-a48f-3a619b83dccf"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextChannel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eebddb7f-0382-4f25-a5ed-45b949dc47fe"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PreviousChannel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""530de455-7465-4b75-ab4a-7de5533ed2a4"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VolUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e07d83ca-5d20-48af-8042-7008f2e845cb"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VolDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Gas = m_Gameplay.FindAction("Gas", throwIfNotFound: true);
        m_Gameplay_Reverse = m_Gameplay.FindAction("Reverse", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_View = m_Gameplay.FindAction("View", throwIfNotFound: true);
        m_Gameplay_ResetCamera = m_Gameplay.FindAction("ResetCamera", throwIfNotFound: true);
        m_Gameplay_ReverseCamera = m_Gameplay.FindAction("ReverseCamera", throwIfNotFound: true);
        m_Gameplay_Quit = m_Gameplay.FindAction("Quit", throwIfNotFound: true);
        m_Gameplay_NextChannel = m_Gameplay.FindAction("NextChannel", throwIfNotFound: true);
        m_Gameplay_PreviousChannel = m_Gameplay.FindAction("PreviousChannel", throwIfNotFound: true);
        m_Gameplay_VolUp = m_Gameplay.FindAction("VolUp", throwIfNotFound: true);
        m_Gameplay_VolDown = m_Gameplay.FindAction("VolDown", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Gas;
    private readonly InputAction m_Gameplay_Reverse;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_View;
    private readonly InputAction m_Gameplay_ResetCamera;
    private readonly InputAction m_Gameplay_ReverseCamera;
    private readonly InputAction m_Gameplay_Quit;
    private readonly InputAction m_Gameplay_NextChannel;
    private readonly InputAction m_Gameplay_PreviousChannel;
    private readonly InputAction m_Gameplay_VolUp;
    private readonly InputAction m_Gameplay_VolDown;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Gas => m_Wrapper.m_Gameplay_Gas;
        public InputAction @Reverse => m_Wrapper.m_Gameplay_Reverse;
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @View => m_Wrapper.m_Gameplay_View;
        public InputAction @ResetCamera => m_Wrapper.m_Gameplay_ResetCamera;
        public InputAction @ReverseCamera => m_Wrapper.m_Gameplay_ReverseCamera;
        public InputAction @Quit => m_Wrapper.m_Gameplay_Quit;
        public InputAction @NextChannel => m_Wrapper.m_Gameplay_NextChannel;
        public InputAction @PreviousChannel => m_Wrapper.m_Gameplay_PreviousChannel;
        public InputAction @VolUp => m_Wrapper.m_Gameplay_VolUp;
        public InputAction @VolDown => m_Wrapper.m_Gameplay_VolDown;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Gas.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGas;
                @Gas.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGas;
                @Gas.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGas;
                @Reverse.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReverse;
                @Reverse.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReverse;
                @Reverse.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReverse;
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @View.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnView;
                @View.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnView;
                @View.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnView;
                @ResetCamera.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnResetCamera;
                @ResetCamera.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnResetCamera;
                @ResetCamera.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnResetCamera;
                @ReverseCamera.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReverseCamera;
                @ReverseCamera.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReverseCamera;
                @ReverseCamera.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReverseCamera;
                @Quit.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnQuit;
                @Quit.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnQuit;
                @Quit.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnQuit;
                @NextChannel.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNextChannel;
                @NextChannel.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNextChannel;
                @NextChannel.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNextChannel;
                @PreviousChannel.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPreviousChannel;
                @PreviousChannel.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPreviousChannel;
                @PreviousChannel.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPreviousChannel;
                @VolUp.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnVolUp;
                @VolUp.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnVolUp;
                @VolUp.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnVolUp;
                @VolDown.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnVolDown;
                @VolDown.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnVolDown;
                @VolDown.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnVolDown;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Gas.started += instance.OnGas;
                @Gas.performed += instance.OnGas;
                @Gas.canceled += instance.OnGas;
                @Reverse.started += instance.OnReverse;
                @Reverse.performed += instance.OnReverse;
                @Reverse.canceled += instance.OnReverse;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @View.started += instance.OnView;
                @View.performed += instance.OnView;
                @View.canceled += instance.OnView;
                @ResetCamera.started += instance.OnResetCamera;
                @ResetCamera.performed += instance.OnResetCamera;
                @ResetCamera.canceled += instance.OnResetCamera;
                @ReverseCamera.started += instance.OnReverseCamera;
                @ReverseCamera.performed += instance.OnReverseCamera;
                @ReverseCamera.canceled += instance.OnReverseCamera;
                @Quit.started += instance.OnQuit;
                @Quit.performed += instance.OnQuit;
                @Quit.canceled += instance.OnQuit;
                @NextChannel.started += instance.OnNextChannel;
                @NextChannel.performed += instance.OnNextChannel;
                @NextChannel.canceled += instance.OnNextChannel;
                @PreviousChannel.started += instance.OnPreviousChannel;
                @PreviousChannel.performed += instance.OnPreviousChannel;
                @PreviousChannel.canceled += instance.OnPreviousChannel;
                @VolUp.started += instance.OnVolUp;
                @VolUp.performed += instance.OnVolUp;
                @VolUp.canceled += instance.OnVolUp;
                @VolDown.started += instance.OnVolDown;
                @VolDown.performed += instance.OnVolDown;
                @VolDown.canceled += instance.OnVolDown;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnGas(InputAction.CallbackContext context);
        void OnReverse(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnView(InputAction.CallbackContext context);
        void OnResetCamera(InputAction.CallbackContext context);
        void OnReverseCamera(InputAction.CallbackContext context);
        void OnQuit(InputAction.CallbackContext context);
        void OnNextChannel(InputAction.CallbackContext context);
        void OnPreviousChannel(InputAction.CallbackContext context);
        void OnVolUp(InputAction.CallbackContext context);
        void OnVolDown(InputAction.CallbackContext context);
    }
}
