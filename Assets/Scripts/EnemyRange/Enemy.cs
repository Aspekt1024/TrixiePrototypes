using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Enemy : MonoBehaviour, IShootable
{
    public float DeathCooldown = 5f;

    private float destroyTime = 0f;
    private EnemyHandler handler;

    private enum states
    {
        Alive, Dead
    }
    private states state;

    private enum alignments
    {
        Evil, Friendly
    }
    private alignments alignment;

    public void Destroy()
    {
        handler.EnemyDestroyed(this);
        destroyTime = Time.time;
    }

    public void Revive()
    {
        gameObject.SetActive(true);
    }

    public bool IsEvil { get { return alignment == alignments.Evil; } }

    private void Start()
    {
        handler = FindObjectOfType<EnemyHandler>();
        state = states.Alive;
    }
    private void Update()
    {
        if (state == states.Dead)
        {
            if (Time.time > destroyTime + DeathCooldown)
            {
                Revive();
            }
        }
    }

    public void SetEvil()
    {
        alignment = alignments.Evil;
        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
    }

    public void SetFriendly()
    {
        alignment = alignments.Friendly;
        GetComponent<SpriteRenderer>().color = new Color(0, 1, 1, 1);
    }

    public void HandleShot(Bullet bullet)
    {
        Destroy();
    }
}
