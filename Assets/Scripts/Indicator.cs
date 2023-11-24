using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

[Serializable]
public enum trigger
{
    leftTrigger,
    middleLeftTrigger,
    middleRightTrigger,
    rightTrigger
}

public class Indicator : MonoBehaviour
{
    // Inputs mapping
    public InputActionAsset primaryInputs;
    InputActionMap gameplayActionMap;
    InputAction triggerAction;
    //This specify what action is gonna be used by the indicator
    public trigger actionIndex;
    public float pressValue = 0.15f;
    private float originalValue;
    private Fish currentFish = null;
    //This is to call score system
    private Score score;

    // Sound variables
    private SoundEffects soundEffectsInstance;
    private AudioSource audioSource = null;
    private AudioClip[] missClipArray = null;
    private AudioClip[] catchClipArray = null;

    // Splash effect variables
    [SerializeField]
    private ParticleSystem splashEffect;

    // Input variables
    public static bool controllerInput;
    public static bool keyboardInput;
    public static InputControl currentControlInput;

    [SerializeField]
    private GameObject indicatorText;

    [SerializeField]
    private GameObject arrowImage;


    //A dictionary that I use to find the name of the action specified by the index
    Dictionary<int, string> inputActionDictionary = new Dictionary<int, string>()
    {
        {0, "Left trigger"},
        {1, "Left middle trigger"},
        {2 ,"Right middle trigger" },
        {3, "Right trigger"}
    };

    Dictionary<int, string> keyboardControlsDictionary = new Dictionary<int, string>()
    {
        {0, "A"},
        {1, "S"},
        {2 ,"D" },
        {3, "F"}
    };

    Dictionary<int, string> gamepadControlsDictionary = new Dictionary<int, string>()
    {
        {0, "<-"},
        {1, "?"},
        {2 ,"^" },
        {3, "->"}
    };


    private void Awake()
    {
        originalValue = transform.position.y;
        // We read information from the inputs mapping
        gameplayActionMap = primaryInputs.FindActionMap("Gameplay");
        triggerAction = gameplayActionMap.FindAction(inputActionDictionary[(int)actionIndex]);

        // We specify what method will be invoked at what time (performed in this case)
        triggerAction.performed += Capture;
        triggerAction.canceled += RestorePos; 
    }

    private void Start()
    {
        //We search score system
        score = GameObject.FindGameObjectWithTag("Fisherman").GetComponent<Score>();

        // Assign audio clips for the SoundEffects class if instance is not null.
        soundEffectsInstance = SoundEffects.instance;
        if (soundEffectsInstance == null)
        {
            Debug.LogWarning("SoundEffects instance is null.");
        }
        else
        {
            audioSource = SoundEffects.instance.audioSource;
            if (audioSource == null)
            {
                Debug.LogWarning("audio source is null. Unable to play audio clips.");
            }
            missClipArray = SoundEffects.instance.missClipArray;
            if (missClipArray == null)
            {
                Debug.LogWarning("note miss audio clip array is null. Will not be able to play these audio clips.");
            }
            catchClipArray = SoundEffects.instance.catchClipArray;
            if (catchClipArray == null)
            {
                Debug.LogWarning("note catch audio clip array is null. Will not be able to play these audio clips.");
            }
        }
    }

    // Input system needs enable and disable the action in order to work.
    private void OnEnable()
    {
        triggerAction.Enable();
        //InputUser.onChange += SwitchLabels;
    }

    private void OnDisable()
    {
        triggerAction.performed -= Capture;
        triggerAction.canceled -= RestorePos;
        triggerAction.Disable();
        //InputUser.onChange -= SwitchLabels;

    }

/*    private void SwitchLabels(InputUser inputUser, InputUserChange inputUserChange, InputDevice inputDevice)
    {
        if (inputUserChange == InputUserChange.ControlSchemeChanged)
        {
            Debug.Log("Input device changed.");
        }

    }*/

    // Destroys the fish that is inside the indicator
    private void Capture(CallbackContext ctx)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y-pressValue, transform.position.z);
        // Return if the game is paused. The prevents players from pausing the game to get points.
        if (GameUIController.gamePaused)
        {
            return;
        }

        InputControl currentInput = ctx.action.activeControl;

/*        foreach (InputControlScheme controlScheme in primaryInputs.controlSchemes)
        {
            Debug.Log("controlScheme: " + controlScheme);

            Debug.Log("controlScheme.name: " + controlScheme.name);
            Debug.Log("controlScheme.GetType(): " + controlScheme.GetType());
           
        }*/
        

 /*       if (currentControlInput == null)
        {
            currentControlInput = currentInput;
        }*/
        
/*        foreach (InputDevice inputDevice in InputSystem.devices)
        {
            Debug.Log("inputDevice" + inputDevice);
            Debug.Log("inputDevice enabled: " + inputDevice.enabled);
        }*/

/*        if (currentControlInput.GetType() != currentInput.GetType())
        {
            // Switch input labels to the indicator.
            SwitchInputLabels(currentInput);
        }
        else
        {
            Debug.Log("controls have not changed.");
        }*/

        if (currentFish != null)
        {
            // Hit
            splashEffect.Play();
            SoundEffects.PlayAudioClip(audioSource, catchClipArray, (int)actionIndex);
            score.addScore(currentFish.beatOfThisNote); // Calls score system to work
            score.ShowFloatingScore(this.transform.position);
            Destroy(currentFish.gameObject);
            currentFish = null;
        }
        else
        {
            // Miss
            score.ShowMissMessage(this.transform.position);
            SoundEffects.PlayAudioClipAtRandom(audioSource, missClipArray);
            score.multiplier = 1; //If you press a button when there isn't any fish, multiplier resets
        }
    }

    void RestorePos(CallbackContext ctx)
    {
        transform.position = new Vector3(transform.position.x, originalValue, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            currentFish = other.GetComponent<Fish>();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            currentFish = null;
            score.multiplier = 1;
        }
    }

    public void SwitchInputLabels(String schemeType)
    {
        switch (schemeType)
        {
            case "Keyboard":
                indicatorText.SetActive(true);
                arrowImage.SetActive(false);
                //indicatorText.text = keyboardControlsDictionary[(int)actionIndex].ToString();
                Debug.Log("Keyboard being used.");
                break;
            case "Gamepad":
                indicatorText.SetActive(false);
                arrowImage.SetActive(true);
                //indicatorText.text = gamepadControlsDictionary[(int)actionIndex].ToString();
                Debug.Log("Gamepad being used.");
                break;
        }
    }

    /// <summary>
    /// Switch the input labels on the Indicator GameObject.
    /// </summary>
    /// <param name="inputType"></param>
    public void SwitchInputLabels(InputControl inputType)
    { 
        switch (inputType)
        {
            case KeyControl:
                currentControlInput = inputType;

                Debug.Log("Switching labels to keyboard inputs.");
                break;
            case ButtonControl:
                currentControlInput = inputType;
                Debug.Log("Switching labels to Gamepad inputs.");
                break;
        }
    }

}