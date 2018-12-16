using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, ISwitchable, IInteractable {

    public SwitchIndicator Indicator;
    public SwitchableMonoBehaviour ControlledObject;
    public States InitialState = States.Off;
    
    public enum States
    {
        On, Off
    }
    private States state;

    public bool IsOn { get { return state == States.On; } }

    private void Start()
    {
        if (Indicator == null)
        {
            Debug.LogError("Switch missing Indicator Object in Inspector");
        }

        if (InitialState == States.Off)
        {
            SwitchOff();
        }
        else
        {
            SwitchOn();
        }
    }

    public void Interact()
    {
        Toggle();
    }

    public void SwitchOn()
    {
        state = States.On;
        Indicator.SwitchOn();
        if (ControlledObject != null)
        {
            ControlledObject.SwitchOn();
        }
    }

    public void SwitchOff()
    {
        state = States.Off;
        Indicator.SwitchOff();
        if (ControlledObject != null)
        {
            ControlledObject.SwitchOff();
        }
    }

    private void Toggle()
    {
        if (state == States.Off)
        {
            SwitchOn();
        }
        else
        {
            SwitchOff();
        }
    }


}
