using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem;

public class TrackInputDevices : MonoBehaviour
{
    [SerializeField]
    private Indicator[] indicatorList;

    void OnEnable()
    {
        InputUser.onChange += OnInputDeviceChange;
    }

    void OnDisable()
    {
        InputUser.onChange -= OnInputDeviceChange;
    }

    void OnInputDeviceChange(InputUser user, InputUserChange change, InputDevice device)
    {
        if (change == InputUserChange.ControlSchemeChanged)
        {
            string schemeName = user.controlScheme.Value.name;
            foreach (Indicator indicator in indicatorList) 
            {
                indicator.SwitchInputLabels(schemeName);
            }

/*            switch (schemeName)
            {
                case "Keyboard":
                    Debug.Log("Keyboard being used.");
                    break;
                case "Gamepad":
                    Debug.Log("Gamepad being used.");
                    break;
            }*/
/*            Debug.Log("user.ToString(): " + user.ToString());
            Debug.Log("user.GetType(): " + user.GetType());
            Debug.Log("user.controlScheme: " + user.controlScheme);
            Debug.Log("user.controlScheme.Value.name: " + user.controlScheme.Value.name);
            Debug.Log("user.controlScheme.Value.bindingGroup: " + user.controlScheme.Value.bindingGroup);
            Debug.Log("user.controlScheme.Value.deviceRequirements: " + user.controlScheme.Value.deviceRequirements);
            Debug.Log("user.controlSchemeMatch: " + user.controlSchemeMatch);
            Debug.Log("change.ToString(): " + change);
            if (device == null)
            {
                Debug.Log("device is null.");
            }
            else
            {
                Debug.Log("device is not null.");
            }*/
        }
            
    }

}
