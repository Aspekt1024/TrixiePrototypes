using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PressureSwitch : Switch
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SwitchOn();
    }
}
