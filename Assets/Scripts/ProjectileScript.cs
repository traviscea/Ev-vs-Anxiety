using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 15f;
    public bool thrown;
    public Rigidbody2D rb;

    public PlayerMovement player;

    private bool hit = false;

    void Start()
    {
        GameObject playerGameObject = GameObject.FindWithTag("Player");
        if (playerGameObject != null)
        {
            PlayerMovement playerMovementScript = playerGameObject.GetComponent<PlayerMovement>();

            if (playerMovementScript != null)
            {
                Debug.Log("Found PlayerController script on the Player object.");
                player = playerMovementScript;
            }
            else
            {
                Debug.LogWarning("PlayerController script not found on the Player object.");
            }
        }
        else
        {
            Debug.LogWarning("GameObject with tag 'Player' not found in the scene.");
        }

        if(player.facingDirection == 1)
        {
        transform.localScale = new Vector3(-2f,2f,2f);
        //transform.position += -transform.right * speed * Time.deltaTime;
        rb.linearVelocity = -transform.right * speed;
    
        } else
        {
         transform.localScale = new Vector3(2f,2f,2f);
        //transform.position += transform.right * speed * Time.deltaTime;
        rb.linearVelocity = transform.right * speed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the projectile hasn't stuck to anything yet
        if (!hit)
        {
            Stick(collision);
            hit = true;
        }
    }

    void Stick(Collision2D col)
    {
        // Optional: adjust the projectile's position slightly into the surface to avoid z-fighting or floating
        // Note: Use transform.position modification if you're not using Rigidbody2D.MovePosition exclusively.

        // Make the projectile a child of the object it hit
        transform.parent = col.transform;

        // Disable physics components to stop all motion and allow it to move with the parent
        // For 2D, use the 2D versions of the components
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            Destroy(rb);
        }
        // if (TryGetComponent<Collider2D>(out Collider2D col2D))
        // {
        //     Destroy(col2D);
        // }

        // Optional: Destroy the script itself if no other logic is needed after sticking
        // Destroy(this);

        // Optional: Destroy the projectile after a few seconds even if it's stuck (e.g., for cleanup)
        // Destroy(gameObject, 5f);
    }

}
