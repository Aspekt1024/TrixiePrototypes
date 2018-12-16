using System.Collections;
using UnityEngine;

public class ExplodingEnemy : MonoBehaviour, IShootable {

    public int MaxHealth = 3;

    private int health;

    private enum States
    {
        Normal, Exploded
    }
    private States state;

    private enum Animations
    {
        Normal, Explosion, Hit
    }

    private Animator anim;

    public event System.Action<ExplodingEnemy> OnDeath = delegate { };

    public void HandleShot(Bullet bullet)
    {
        if (state == States.Exploded) return;

        if (health == 1)
        {
            health = 0;
            state = States.Exploded;
            anim.Play(Animations.Explosion.ToString(), 0, 0f);
            OnDeath?.Invoke(this);
        }
        else
        {
            health--;
            anim.Play(Animations.Hit.ToString(), 0, 0f);
        }
    }

    private void Start ()
    {
        anim = GetComponent<Animator>();
        Spawn(transform.position);
    }

    public void Spawn(Vector2 location)
    {
        state = States.Normal;
        health = MaxHealth;
        anim.Play(Animations.Normal.ToString(), 0, 0f);
        transform.position = location;
    }
}
