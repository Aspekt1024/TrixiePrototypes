using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class SwitchableMonoBehaviour : MonoBehaviour, ISwitchable
{
    public abstract void SwitchOn();
    public abstract void SwitchOff();
}
