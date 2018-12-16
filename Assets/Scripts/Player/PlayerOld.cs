using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOld: MonoBehaviour {

    public float MoveSpeed = 10f;
    public float JumpForce = 40000f;

    private Rigidbody2D body;

    private readonly List<IInteractable> overlappingInteractables = new List<IInteractable>();

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        var horizVelocity = 0f;

		if (Input.GetKey(KeyCode.A)) {
            horizVelocity -= MoveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            horizVelocity = MoveSpeed;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        var velocity = body.velocity;
        velocity.x = horizVelocity;
        body.velocity = velocity;
	}

    private void Jump()
    {
        body.AddForce(Vector2.up * JumpForce);
    }

    private void Shoot()
    {
        var bulletPrefab = Resources.Load<GameObject>("bullet");
        var bullet = Instantiate(bulletPrefab);
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Vector2 origin = (Vector2)transform.position + direction.normalized * 0.1f;
        bullet.GetComponent<Bullet>().Fire(origin, direction, 30f);
    }

    private void Interact()
    {
        if (overlappingInteractables.Count > 0)
        {
            overlappingInteractables[0].Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null)
        {
            overlappingInteractables.Add(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null)
        {
            overlappingInteractables.Remove(interactable);
        }
    }
}
