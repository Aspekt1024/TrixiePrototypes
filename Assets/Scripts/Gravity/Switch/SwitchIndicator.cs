using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SwitchIndicator : MonoBehaviour, ISwitchable
{
    private SpriteRenderer sprite;

    private readonly Color onColor = new Color(0, 1, 0, 1);
    private readonly Color offColor = new Color(1, 0, 0, 1);

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void SwitchOn()
    {
        sprite.color = onColor;
    }

    public void SwitchOff()
    {
        sprite.color = offColor;
    }
}
