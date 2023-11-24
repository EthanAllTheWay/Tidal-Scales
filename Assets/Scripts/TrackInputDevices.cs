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
        }
            
    }

}
