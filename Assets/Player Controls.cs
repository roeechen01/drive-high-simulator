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
                    ""name"": ""NextStation"",
                    ""type"": ""Button"",
                    ""id"": ""e2a52324-1703-43e6-8ab7-d95087237abb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PreviousStation"",
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
                    ""interactions"": ""Press(pressPoint=0.1)""
                },
                {
                    ""name"": ""VolDown"",
                    ""type"": ""Button"",
                    ""id"": ""0898368a-936c-4999-b9c9-5d428e5e8fc9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.1)""
                },
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""d0b9af9d-346d-4b35-be55-ef68f59fd9aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleHUD"",
                    ""type"": ""Button"",
                    ""id"": ""fb0e41ac-0c14-4823-b8d9-842004f933c0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleCinematicMode"",
                    ""type"": ""Button"",
                    ""id"": ""10003404-2abc-43e7-940f-895649f1ddea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleJoint"",
                    ""type"": ""Button"",
                    ""id"": ""eb0ef7bb-f0ff-47f5-9332-713c01f62ebf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LightClipper"",
                    ""type"": ""Button"",
                    ""id"": ""efead080-3383-4731-aca6-24c05fb77386"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.1)""
                },
                {
                    ""name"": ""Hit"",
                    ""type"": ""Button"",
                    ""id"": ""8f2a7578-ed69-4569-ba05-4af865720cb2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.1)""
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
                    ""action"": ""NextStation"",
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
                    ""action"": ""PreviousStation"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""7c88afd3-ca49-46fc-bcc1-066626b109e8"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""811d7ff5-903e-45c3-a551-fffa33ba79f3"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleHUD"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9265aacb-67b7-4ada-a76c-d179179b2429"",
                    ""path"": ""<DualShockGamepad>/touchpadButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleCinematicMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b62e8a5-5955-42c3-9e73-bf1992d9a523"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleJoint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""901eae8d-96c0-4874-a44b-b45e5451e61e"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LightClipper"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7a9d3b3-313f-4296-98c0-b7dc5c660f04"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hit"",
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
        m_Gameplay_NextStation = m_Gameplay.FindAction("NextStation", throwIfNotFound: true);
        m_Gameplay_PreviousStation = m_Gameplay.FindAction("PreviousStation", throwIfNotFound: true);
        m_Gameplay_VolUp = m_Gameplay.FindAction("VolUp", throwIfNotFound: true);
        m_Gameplay_VolDown = m_Gameplay.FindAction("VolDown", throwIfNotFound: true);
        m_Gameplay_Restart = m_Gameplay.FindAction("Restart", throwIfNotFound: true);
        m_Gameplay_ToggleHUD = m_Gameplay.FindAction("ToggleHUD", throwIfNotFound: true);
        m_Gameplay_ToggleCinematicMode = m_Gameplay.FindAction("ToggleCinematicMode", throwIfNotFound: true);
        m_Gameplay_ToggleJoint = m_Gameplay.FindAction("ToggleJoint", throwIfNotFound: true);
        m_Gameplay_LightClipper = m_Gameplay.FindAction("LightClipper", throwIfNotFound: true);
        m_Gameplay_Hit = m_Gameplay.FindAction("Hit", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_NextStation;
    private readonly InputAction m_Gameplay_PreviousStation;
    private readonly InputAction m_Gameplay_VolUp;
    private readonly InputAction m_Gameplay_VolDown;
    private readonly InputAction m_Gameplay_Restart;
    private readonly InputAction m_Gameplay_ToggleHUD;
    private readonly InputAction m_Gameplay_ToggleCinematicMode;
    private readonly InputAction m_Gameplay_ToggleJoint;
    private readonly InputAction m_Gameplay_LightClipper;
    private readonly InputAction m_Gameplay_Hit;
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
        public InputAction @NextStation => m_Wrapper.m_Gameplay_NextStation;
        public InputAction @PreviousStation => m_Wrapper.m_Gameplay_PreviousStation;
        public InputAction @VolUp => m_Wrapper.m_Gameplay_VolUp;
        public InputAction @VolDown => m_Wrapper.m_Gameplay_VolDown;
        public InputAction @Restart => m_Wrapper.m_Gameplay_Restart;
        public InputAction @ToggleHUD => m_Wrapper.m_Gameplay_ToggleHUD;
        public InputAction @ToggleCinematicMode => m_Wrapper.m_Gameplay_ToggleCinematicMode;
        public InputAction @ToggleJoint => m_Wrapper.m_Gameplay_ToggleJoint;
        public InputAction @LightClipper => m_Wrapper.m_Gameplay_LightClipper;
        public InputAction @Hit => m_Wrapper.m_Gameplay_Hit;
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
                @NextStation.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNextStation;
                @NextStation.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNextStation;
                @NextStation.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnNextStation;
                @PreviousStation.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPreviousStation;
                @PreviousStation.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPreviousStation;
                @PreviousStation.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPreviousStation;
                @VolUp.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnVolUp;
                @VolUp.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnVolUp;
                @VolUp.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnVolUp;
                @VolDown.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnVolDown;
                @VolDown.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnVolDown;
                @VolDown.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnVolDown;
                @Restart.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRestart;
                @Restart.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRestart;
                @Restart.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRestart;
                @ToggleHUD.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToggleHUD;
                @ToggleHUD.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToggleHUD;
                @ToggleHUD.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToggleHUD;
                @ToggleCinematicMode.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToggleCinematicMode;
                @ToggleCinematicMode.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToggleCinematicMode;
                @ToggleCinematicMode.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToggleCinematicMode;
                @ToggleJoint.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToggleJoint;
                @ToggleJoint.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToggleJoint;
                @ToggleJoint.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnToggleJoint;
                @LightClipper.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLightClipper;
                @LightClipper.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLightClipper;
                @LightClipper.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLightClipper;
                @Hit.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHit;
                @Hit.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHit;
                @Hit.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHit;
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
                @NextStation.started += instance.OnNextStation;
                @NextStation.performed += instance.OnNextStation;
                @NextStation.canceled += instance.OnNextStation;
                @PreviousStation.started += instance.OnPreviousStation;
                @PreviousStation.performed += instance.OnPreviousStation;
                @PreviousStation.canceled += instance.OnPreviousStation;
                @VolUp.started += instance.OnVolUp;
                @VolUp.performed += instance.OnVolUp;
                @VolUp.canceled += instance.OnVolUp;
                @VolDown.started += instance.OnVolDown;
                @VolDown.performed += instance.OnVolDown;
                @VolDown.canceled += instance.OnVolDown;
                @Restart.started += instance.OnRestart;
                @Restart.performed += instance.OnRestart;
                @Restart.canceled += instance.OnRestart;
                @ToggleHUD.started += instance.OnToggleHUD;
                @ToggleHUD.performed += instance.OnToggleHUD;
                @ToggleHUD.canceled += instance.OnToggleHUD;
                @ToggleCinematicMode.started += instance.OnToggleCinematicMode;
                @ToggleCinematicMode.performed += instance.OnToggleCinematicMode;
                @ToggleCinematicMode.canceled += instance.OnToggleCinematicMode;
                @ToggleJoint.started += instance.OnToggleJoint;
                @ToggleJoint.performed += instance.OnToggleJoint;
                @ToggleJoint.canceled += instance.OnToggleJoint;
                @LightClipper.started += instance.OnLightClipper;
                @LightClipper.performed += instance.OnLightClipper;
                @LightClipper.canceled += instance.OnLightClipper;
                @Hit.started += instance.OnHit;
                @Hit.performed += instance.OnHit;
                @Hit.canceled += instance.OnHit;
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
        void OnNextStation(InputAction.CallbackContext context);
        void OnPreviousStation(InputAction.CallbackContext context);
        void OnVolUp(InputAction.CallbackContext context);
        void OnVolDown(InputAction.CallbackContext context);
        void OnRestart(InputAction.CallbackContext context);
        void OnToggleHUD(InputAction.CallbackContext context);
        void OnToggleCinematicMode(InputAction.CallbackContext context);
        void OnToggleJoint(InputAction.CallbackContext context);
        void OnLightClipper(InputAction.CallbackContext context);
        void OnHit(InputAction.CallbackContext context);
    }
}
