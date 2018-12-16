using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GravityBox : MonoBehaviour
{
    public GravField.FieldTypes[] FieldPriorities = new GravField.FieldTypes[1]
    {
        GravField.FieldTypes.AffectAll,
    };

    private GravField affectingField;
    private readonly List<GravField> fields = new List<GravField>();
    private Rigidbody2D body;

    private const float lerpTime = 0.7f;
    private float timeSwitched = 0f;
    private Vector2 originalVelocity;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (affectingField == null) return;
        if (!affectingField.IsActive)
        {
            affectingField = null;
            return;
        }
        float ratio = (Time.time - timeSwitched) / lerpTime;
        body.velocity = Vector2.Lerp(originalVelocity, affectingField.GetFieldVelocity(), ratio);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var field = collision.GetComponentInParent<GravField>();
        if (field != null)
        {
            fields.Add(field);
            DetermineAffectingField();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var field = collision.GetComponentInParent<GravField>();
        if (field != null)
        {
            fields.Remove(field);
            DetermineAffectingField();
        }
    }

    private void DetermineAffectingField()
    {
        if (!fields.Any())
        {
            affectingField = null;
            return;
        }

        foreach (var field in fields)
        {
            int index = GetFieldIndex(field.FieldType);
            if (index == -1) continue;

            if (affectingField == null)
            {
                affectingField = field;
                timeSwitched = Time.time;
                originalVelocity = body.velocity;
            }
            else if (index <= GetFieldIndex(affectingField.FieldType))
            {
                affectingField = field;
                timeSwitched = Time.time;
                originalVelocity = body.velocity;
            }
        }
    }
    
    private int GetFieldIndex(GravField.FieldTypes type)
    {
        for (int i = 0; i < FieldPriorities.Length; i++)
        {
            if (FieldPriorities[i] == type) return i;
        }
        return -1;
    }
}
