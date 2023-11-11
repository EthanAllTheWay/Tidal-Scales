using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private Fish currentFish = null;
    private float score;

    //A dictionary that I use to find the name of the action specified by the index
    Dictionary<int, string> inputActionDictionary = new Dictionary<int, string>()
    {
        {0, "Left trigger"},
        {1, "Left middle trigger"},
        {2 ,"Right middle trigger" },
        {3, "Right trigger"}
    };

    private void Awake()
    {
        // We read information from the inputs mapping
        gameplayActionMap = primaryInputs.FindActionMap("Gameplay");
        triggerAction = gameplayActionMap.FindAction(inputActionDictionary[(int)actionIndex]);

        // We specify what method will be invoked at what time (performed in this case)
        triggerAction.performed += ctx => {
            // Return if the game is paused. The prevents players from pausing the game to get points.
            if (GameUIController.gamePaused)
            {
                return;
            }
            Capture();
            };
    }

    // Input system needs enable and disable the action in order to work.
    private void OnEnable()
    {
        triggerAction.Enable();
    }

    private void OnDisable()
    {
        triggerAction.Disable();
    }

    // Destroys the fish that is inside the indicator
    private void Capture()
    {
        if (currentFish != null)
        {
            Destroy(currentFish.gameObject);
            currentFish = null;
            score = PlayerPrefs.GetFloat("Score");
            score += 1;
            PlayerPrefs.SetFloat("Score", score);
            Debug.Log("Score: " + score);

        }
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
        }
    }
}
