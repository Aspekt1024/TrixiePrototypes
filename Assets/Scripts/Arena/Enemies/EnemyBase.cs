using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IShootable {

    public enum EnemyTypes
    {
        Undefined, Ground, Air
    }
    public EnemyTypes Type;

    public event System.Action<EnemyBase> OnDeath = delegate { };

    /// <summary>
    /// Animations and additional effects to go here
    /// </summary>
    protected abstract void OnHit(Bullet bullet);
    protected abstract void OnSpawn();

    protected Animator anim;
    protected Rigidbody2D body;

    public void Spawn(Vector2 location)
    {
        OnSpawn();
        transform.position = location;
    }

    // For IShootable
    public void HandleShot(Bullet bullet)
    {
        OnHit(bullet);
    }

    protected void DeathAnimationComplete()
    {
        OnDeath?.Invoke(this);
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        Spawn(transform.position);
    }
}
