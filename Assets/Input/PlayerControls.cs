// GENERATED AUTOMATICALLY FROM 'Assets/Input/PlayerControls.inputactions'

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
            ""name"": ""Station"",
            ""id"": ""8d9898a0-f9ea-4743-a859-f6c89bd57735"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""5d7c7500-1c79-496d-b1d1-c315963bb8ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""4d089e72-f6f5-4ed1-958c-fb3daa51b938"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""InteractPad"",
                    ""type"": ""Button"",
                    ""id"": ""3044db3c-8192-4a93-8feb-8e6ac0dd2229"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""99250dd7-7ca1-4ea8-a3c4-a997c815f2ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""d4642898-7a25-4b36-8d00-ae517c702b9a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""4e1343db-56fe-4526-a69a-9116f2f88f15"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""5238319d-9628-4ce7-98d2-8fc7650e9dff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeCamera"",
                    ""type"": ""Button"",
                    ""id"": ""600cc469-32e5-45ec-ad94-ea77a75f06a3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""280432ba-9e63-4dc5-81c8-774cbec07ead"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""fb329a86-0ae7-442c-abcd-38761fa3c6fe"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0a689614-1695-4869-9d8d-d09fed6df2f1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7e9a4dd0-6845-4fd3-a1d5-e8eda697dc74"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a399c780-d5d5-465c-8b7e-fce4e37350a1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""75a3fc68-6cec-469b-801d-94fbd4a82b96"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d7ab2b48-b648-463c-8ee5-129ee28dbd93"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2fd7701f-a551-4999-946a-a05ee713ff0c"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""55ed17f4-c444-4d54-bc3f-7656d48dc873"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1cc7ca73-e5fc-4b31-b700-f499d57d826c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""6eacabda-65a3-443a-99d1-fe19e1c79087"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8ec6a349-abfb-4fb9-917f-871ad885321d"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5dc4c784-c030-4cd7-851a-7d931484ce9e"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""88a5d09b-9604-4c82-8b56-118f438402a6"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5aaa4206-4e99-47b7-8a59-8d3c33396720"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bef5ba91-4e4c-488d-bed9-2d6123cd882a"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a13090de-d416-417a-9f5a-ee3b577094fc"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc779388-fa4d-431f-b8ec-032bcc188ac1"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17494de0-7e74-4532-9f47-650a3b05e0a2"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""728eed8d-6221-46cd-b801-349979c2f624"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""31b0bb8b-b06d-4307-bf7a-d2b5d679a889"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""732659f8-7610-43bd-84ad-d47e917fd615"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""555bac08-476f-4392-a06d-cc07c66f4f51"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86be9bf6-9215-489a-898f-ef3c0a2c4382"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""548374e9-ec47-47f0-8865-22d5856933db"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef784e03-6f88-4fe4-83b2-c7de3b6dea41"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0015a627-b241-4503-8ff2-6c8830816a55"",
                    ""path"": ""<Keyboard>/rightShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7649e26e-5c56-40ea-a168-b7d7dddc61ef"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InteractPad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90775ea4-2c6f-4e1d-ba23-667e9495d2ac"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InteractPad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Space"",
            ""id"": ""93b71626-1539-4509-8dfd-31ccb02962b2"",
            ""actions"": [
                {
                    ""name"": ""SpaceMove"",
                    ""type"": ""Button"",
                    ""id"": ""5f954f2b-bab1-4ac4-85fb-8296c770040a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""InteractPad"",
                    ""type"": ""Button"",
                    ""id"": ""7e4df1d9-7dc1-4cfe-9ed5-554a0da651c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""c381f4fc-c209-4d49-a391-dae272b0abc0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""9f762a3f-b4b6-4a89-8de4-75100a86ef82"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""226934e1-efe9-4944-8c87-3a490dc6f2ec"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpaceMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b36894fd-9b98-450b-bc65-500712f83b37"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpaceMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1d68c180-23ed-43df-a47f-b229d8faad9f"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpaceMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""39fa3b53-19ea-4bb9-8186-58ae74da29ae"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpaceMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3a849157-b177-4697-aff1-4a03bc2bb8f7"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpaceMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""044e27b1-5fd6-498c-902c-d875962ae4e8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpaceMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1f5ec32c-55d2-453d-8f8f-4e8ce8d65bda"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpaceMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""294c81a3-28d2-4dbe-9002-306194e0248f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpaceMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4492a559-b9c3-40e3-b2fe-f51b99f74145"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpaceMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""58d89d9e-3387-43d7-990f-a8a557133443"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpaceMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""18c57db6-809e-428e-ac79-4f78cebc29a5"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8605588-ccfb-404c-8971-34d5956635b4"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0bffc3e4-4d5e-43cf-9e7f-9f5c75e01f8e"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c30ff88b-ef61-4662-88a0-f7ac6c34f7f3"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09fd1b1c-6c2b-4a9b-aab6-33eb61589974"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InteractPad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""87cfe268-30b4-420e-9d7c-43a04f28fa91"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InteractPad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Kite"",
            ""id"": ""543ed3ac-38d9-4f0f-abf7-e8ed85533e93"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""f7b8bb2b-23e3-4eb1-acb2-f3a37d68122b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""7694b13d-99a0-479d-b0e4-54ed3d37109d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3582e890-d6ed-4dc2-a6bc-2f3662d856fd"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ff02255b-a5e9-49c0-942f-782e3f9f6de3"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6a001d4a-3ef7-4720-be57-5e95d26c4476"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a8cda4a1-06b2-4b23-a12d-476221f89958"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""34c2864d-b4c4-4520-bdea-8bae602052d9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""fc7e7fa1-e5cc-4d80-b705-b91eaacd51d4"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cafe542d-cebe-4c1c-a4f1-b461aabc3692"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""22164a3f-4488-47c8-8e0d-5ee1a3589f05"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9f6d355c-2783-4d48-8bb1-aa810b445507"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""fcef74d8-54da-41bb-9301-bf1d53924695"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1add4f20-8da2-418d-9d47-3d114b69b682"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""67122ad1-663c-4f57-83e9-69924afe8665"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f2141ce7-cdf5-4b3a-bca7-7a1795d4dafe"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2c224ee1-1664-42d0-bfda-8ae99a2fc65b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Station
        m_Station = asset.FindActionMap("Station", throwIfNotFound: true);
        m_Station_Move = m_Station.FindAction("Move", throwIfNotFound: true);
        m_Station_Interact = m_Station.FindAction("Interact", throwIfNotFound: true);
        m_Station_InteractPad = m_Station.FindAction("InteractPad", throwIfNotFound: true);
        m_Station_Pause = m_Station.FindAction("Pause", throwIfNotFound: true);
        m_Station_MousePosition = m_Station.FindAction("MousePosition", throwIfNotFound: true);
        m_Station_MouseClick = m_Station.FindAction("MouseClick", throwIfNotFound: true);
        m_Station_RightClick = m_Station.FindAction("RightClick", throwIfNotFound: true);
        m_Station_ChangeCamera = m_Station.FindAction("ChangeCamera", throwIfNotFound: true);
        // Space
        m_Space = asset.FindActionMap("Space", throwIfNotFound: true);
        m_Space_SpaceMove = m_Space.FindAction("SpaceMove", throwIfNotFound: true);
        m_Space_InteractPad = m_Space.FindAction("InteractPad", throwIfNotFound: true);
        m_Space_Pause = m_Space.FindAction("Pause", throwIfNotFound: true);
        m_Space_Interact = m_Space.FindAction("Interact", throwIfNotFound: true);
        // Kite
        m_Kite = asset.FindActionMap("Kite", throwIfNotFound: true);
        m_Kite_Movement = m_Kite.FindAction("Movement", throwIfNotFound: true);
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

    // Station
    private readonly InputActionMap m_Station;
    private IStationActions m_StationActionsCallbackInterface;
    private readonly InputAction m_Station_Move;
    private readonly InputAction m_Station_Interact;
    private readonly InputAction m_Station_InteractPad;
    private readonly InputAction m_Station_Pause;
    private readonly InputAction m_Station_MousePosition;
    private readonly InputAction m_Station_MouseClick;
    private readonly InputAction m_Station_RightClick;
    private readonly InputAction m_Station_ChangeCamera;
    public struct StationActions
    {
        private @PlayerControls m_Wrapper;
        public StationActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Station_Move;
        public InputAction @Interact => m_Wrapper.m_Station_Interact;
        public InputAction @InteractPad => m_Wrapper.m_Station_InteractPad;
        public InputAction @Pause => m_Wrapper.m_Station_Pause;
        public InputAction @MousePosition => m_Wrapper.m_Station_MousePosition;
        public InputAction @MouseClick => m_Wrapper.m_Station_MouseClick;
        public InputAction @RightClick => m_Wrapper.m_Station_RightClick;
        public InputAction @ChangeCamera => m_Wrapper.m_Station_ChangeCamera;
        public InputActionMap Get() { return m_Wrapper.m_Station; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(StationActions set) { return set.Get(); }
        public void SetCallbacks(IStationActions instance)
        {
            if (m_Wrapper.m_StationActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_StationActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_StationActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_StationActionsCallbackInterface.OnMove;
                @Interact.started -= m_Wrapper.m_StationActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_StationActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_StationActionsCallbackInterface.OnInteract;
                @InteractPad.started -= m_Wrapper.m_StationActionsCallbackInterface.OnInteractPad;
                @InteractPad.performed -= m_Wrapper.m_StationActionsCallbackInterface.OnInteractPad;
                @InteractPad.canceled -= m_Wrapper.m_StationActionsCallbackInterface.OnInteractPad;
                @Pause.started -= m_Wrapper.m_StationActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_StationActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_StationActionsCallbackInterface.OnPause;
                @MousePosition.started -= m_Wrapper.m_StationActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_StationActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_StationActionsCallbackInterface.OnMousePosition;
                @MouseClick.started -= m_Wrapper.m_StationActionsCallbackInterface.OnMouseClick;
                @MouseClick.performed -= m_Wrapper.m_StationActionsCallbackInterface.OnMouseClick;
                @MouseClick.canceled -= m_Wrapper.m_StationActionsCallbackInterface.OnMouseClick;
                @RightClick.started -= m_Wrapper.m_StationActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_StationActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_StationActionsCallbackInterface.OnRightClick;
                @ChangeCamera.started -= m_Wrapper.m_StationActionsCallbackInterface.OnChangeCamera;
                @ChangeCamera.performed -= m_Wrapper.m_StationActionsCallbackInterface.OnChangeCamera;
                @ChangeCamera.canceled -= m_Wrapper.m_StationActionsCallbackInterface.OnChangeCamera;
            }
            m_Wrapper.m_StationActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @InteractPad.started += instance.OnInteractPad;
                @InteractPad.performed += instance.OnInteractPad;
                @InteractPad.canceled += instance.OnInteractPad;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @MouseClick.started += instance.OnMouseClick;
                @MouseClick.performed += instance.OnMouseClick;
                @MouseClick.canceled += instance.OnMouseClick;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
                @ChangeCamera.started += instance.OnChangeCamera;
                @ChangeCamera.performed += instance.OnChangeCamera;
                @ChangeCamera.canceled += instance.OnChangeCamera;
            }
        }
    }
    public StationActions @Station => new StationActions(this);

    // Space
    private readonly InputActionMap m_Space;
    private ISpaceActions m_SpaceActionsCallbackInterface;
    private readonly InputAction m_Space_SpaceMove;
    private readonly InputAction m_Space_InteractPad;
    private readonly InputAction m_Space_Pause;
    private readonly InputAction m_Space_Interact;
    public struct SpaceActions
    {
        private @PlayerControls m_Wrapper;
        public SpaceActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SpaceMove => m_Wrapper.m_Space_SpaceMove;
        public InputAction @InteractPad => m_Wrapper.m_Space_InteractPad;
        public InputAction @Pause => m_Wrapper.m_Space_Pause;
        public InputAction @Interact => m_Wrapper.m_Space_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Space; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SpaceActions set) { return set.Get(); }
        public void SetCallbacks(ISpaceActions instance)
        {
            if (m_Wrapper.m_SpaceActionsCallbackInterface != null)
            {
                @SpaceMove.started -= m_Wrapper.m_SpaceActionsCallbackInterface.OnSpaceMove;
                @SpaceMove.performed -= m_Wrapper.m_SpaceActionsCallbackInterface.OnSpaceMove;
                @SpaceMove.canceled -= m_Wrapper.m_SpaceActionsCallbackInterface.OnSpaceMove;
                @InteractPad.started -= m_Wrapper.m_SpaceActionsCallbackInterface.OnInteractPad;
                @InteractPad.performed -= m_Wrapper.m_SpaceActionsCallbackInterface.OnInteractPad;
                @InteractPad.canceled -= m_Wrapper.m_SpaceActionsCallbackInterface.OnInteractPad;
                @Pause.started -= m_Wrapper.m_SpaceActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_SpaceActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_SpaceActionsCallbackInterface.OnPause;
                @Interact.started -= m_Wrapper.m_SpaceActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_SpaceActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_SpaceActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_SpaceActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SpaceMove.started += instance.OnSpaceMove;
                @SpaceMove.performed += instance.OnSpaceMove;
                @SpaceMove.canceled += instance.OnSpaceMove;
                @InteractPad.started += instance.OnInteractPad;
                @InteractPad.performed += instance.OnInteractPad;
                @InteractPad.canceled += instance.OnInteractPad;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public SpaceActions @Space => new SpaceActions(this);

    // Kite
    private readonly InputActionMap m_Kite;
    private IKiteActions m_KiteActionsCallbackInterface;
    private readonly InputAction m_Kite_Movement;
    public struct KiteActions
    {
        private @PlayerControls m_Wrapper;
        public KiteActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Kite_Movement;
        public InputActionMap Get() { return m_Wrapper.m_Kite; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KiteActions set) { return set.Get(); }
        public void SetCallbacks(IKiteActions instance)
        {
            if (m_Wrapper.m_KiteActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_KiteActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_KiteActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_KiteActionsCallbackInterface.OnMovement;
            }
            m_Wrapper.m_KiteActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
            }
        }
    }
    public KiteActions @Kite => new KiteActions(this);
    public interface IStationActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnInteractPad(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnMouseClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnChangeCamera(InputAction.CallbackContext context);
    }
    public interface ISpaceActions
    {
        void OnSpaceMove(InputAction.CallbackContext context);
        void OnInteractPad(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IKiteActions
    {
        void OnMovement(InputAction.CallbackContext context);
    }
}
