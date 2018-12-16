using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravField : SwitchableMonoBehaviour {

    public FieldTypes FieldType = FieldTypes.AffectAll;
    public States DefaultState = States.Inactive;
    public float GravSpeed = 5f;
    public GameObject Field;

    public enum FieldTypes
    {
        AffectAll, Green, Blue, Red,
    }

    public enum States
    {
        Active, Inactive
    }
    private States state;

    public bool IsActive { get { return state == States.Active; } }

    private void Start()
    {
        if (Field == null)
        {
            Debug.LogError("Grav Field missing Field Object in Inspector");
        }
        if (DefaultState == States.Inactive)
        {
            SwitchOff();
        }
        else
        {
            SwitchOn();
        }
    }

    public override void SwitchOff()
    {
        state = States.Inactive;
        Field.SetActive(false);
    }

    public override void SwitchOn()
    {
        state = States.Active;
        Field.SetActive(true);
    }

    public Vector2 GetFieldVelocity()
    {
        return transform.up * GravSpeed;
    }
}
