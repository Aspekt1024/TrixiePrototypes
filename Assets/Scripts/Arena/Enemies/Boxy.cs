using Aspekt.PlayerController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxy : EnemyBase {

    public float MoveSpeed = 2f;

    private enum Animations
    {
        Normal, Exploding
    }

    private enum States
    {
        Normal, Defeated
    }
    private States state;

    private Player player;
    
    private void Update()
    {
        if (state == States.Defeated) return;

        if (TiltIsGreater(45f))
        {
            body.angularVelocity = 0f;
            StartCoroutine(DefeatedRoutine());
            state = States.Defeated;
        }
        else
        {
            if (player.transform.position.x > transform.position.x)
            {
                body.velocity = Vector3.right * MoveSpeed;
            }
            else
            {
                body.velocity = Vector3.left * MoveSpeed;
            }
        }
    }

    private bool TiltIsGreater(float tiltAngle)
    {
        float zRot = transform.eulerAngles.z;
        while (zRot > 180f)
        {
            zRot = zRot - 360f;
        }
        return Mathf.Abs(zRot) > tiltAngle;
    }

    protected override void OnSpawn()
    {
        player = FindObjectOfType<Player>();
        state = States.Normal;
        //anim.Play(Animations.Normal.ToString(), 0, 0f);
    }

    protected override void OnHit(Bullet bullet)
    {
        float dir = bullet.transform.position.x > transform.position.x ? 1f : -1f;
        float force = 100f;
        body.angularVelocity = dir * force;
        transform.position += 0.1f * Vector3.up;
    }

    private IEnumerator DefeatedRoutine()
    {
        body.velocity = Vector2.zero;

        while (!TiltIsGreater(89f))
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        body.constraints = RigidbodyConstraints2D.FreezeAll;
        anim.Play(Animations.Exploding.ToString(), 0, 0f);
        Camera.main.GetComponent<CameraShake>().Shake(0.2f, 1f);

        yield return new WaitForSeconds(2f);

        DeathAnimationComplete();
    }
    
}
