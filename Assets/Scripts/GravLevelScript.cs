using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravLevelScript : MonoBehaviour {

    public PressureSwitch RedPressureSwitch;
    public PressureSwitch BluePressureSwitch;

    public GameObject WinGameObject;

    private enum States
    {
        None, Won
    }
    private States state;

    private void Start()
    {
        WinGameObject.SetActive(false);
        state = States.None;
    }

    private void Update()
    {
        if (state == States.Won) return;

        if (RedPressureSwitch.IsOn && BluePressureSwitch.IsOn)
        {
            state = States.Won;
            WinGame();
        }
    }

    private void WinGame()
    {
        WinGameObject.SetActive(true);
    }
}
