using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTurret : EnemyBase {

    public int MaxHealth = 3;
    public Bullet Bullet;
    public float BulletSpeed = 5f;
    public float ShootCooldown = 5f;

    private int health;

    private enum States
    {
        Normal, Exploded
    }
    private States state;

    private enum Animations
    {
        Normal, Exploding, Hit
    }
    
    protected override void OnHit(Bullet bullet)
    {
        if (state == States.Exploded) return;

        if (health == 1)
        {
            health = 0;
            state = States.Exploded;
            anim.Play(Animations.Exploding.ToString(), 0, 0f);
            Invoke(nameof(DeathAnimationComplete), 2f);
        }
        else
        {
            health--;
            anim.Play(Animations.Hit.ToString(), 0, 0f);
        }
    }

    protected override void OnSpawn()
    {
        health = MaxHealth;
        anim.Play(Animations.Normal.ToString(), 0, 0f);
    }
}
