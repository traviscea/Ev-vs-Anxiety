using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.ComponentModel;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float jump;

    public float move;
    public Rigidbody2D rb;

    public float groundCheckRadius;
    public Transform groundCheck;
    
    public LayerMask groundLayer;
    private bool isOnGround;

    public SBKManager sb;

    [SerializeField] private Animator animator;

    [SerializeField] private GameObject sbkHelpImage;
    [SerializeField] private GameObject completionImage;
    public float helpDisplayDuration = 7f;

    public float completionDisplayDuration = 20f;

    public int totalSBKCount = 85;

    private SpriteRenderer spriteRenderer;

    private AudioSource audioSource;

    public AudioClip collectSBKSound;
    public AudioClip sbkInfoSound;
    public AudioClip completeLevelSound;
    public AudioClip jumpingSound;

    public bool isAttacking = false;
    public float attackDelay = 0.2f;

    public GameObject attackHitBox;
    public ProjectileScript projectilePreFab;
    public Transform launchOffset;

    public SBKManager sBKManager;
    public int facingDirection;
    public Vector3 lastDirection;

    public bool previouslyShownSBKHelp;
    public bool isLevelOne;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Scene scene = SceneManager.GetActiveScene();
        isLevelOne = scene.name == ("Level 1");
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        if (sbkHelpImage != null)
        {
            sbkHelpImage.SetActive(false); 
        }
        if (completionImage != null)
        {
            completionImage.SetActive(false);
        }
        attackHitBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        isOnGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        move = Input.GetAxis("Horizontal");
        if (move > 0)
        {
            // Moving right, ensure sprite is not flipped
            //spriteRenderer.flipX = false;
            transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            facingDirection = -1;
            
        }
        else if (move < 0)
        {
            // Moving left, flip the sprite
            //spriteRenderer.flipX = true;
            transform.localScale = new Vector3(-0.5f,0.5f,0.5f);
            facingDirection = 1;
        }

        rb.linearVelocity = new Vector2(speed * move, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            rb.AddForce(new Vector2(rb.linearVelocity.x, jump));
            PlaySound(jumpingSound, 2f);
            
        }

        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            Attack("isAttack");
        }
        if (Input.GetButtonDown("Fire2") && !isAttacking)
        {
            Attack("isSwordAttack");
        }
        if (Input.GetButtonDown("Fire3") && !isAttacking && sBKManager.sbkNote_Count > 0)
        {
            animator.SetTrigger("isThrowing");
            Instantiate(projectilePreFab, launchOffset.position, transform.rotation);
            --sBKManager.sbkNote_Count;
        }


        if(move != 0){
            animator.SetBool("isRunning", true);
        }
        else
        {
           animator.SetBool("isRunning", false); 
        }

        if(isOnGround == false ){
            animator.SetBool("isJumping", true);
        }
        else
        {
           animator.SetBool("isJumping", false); 
        }
    }

    private void PlaySound(AudioClip audioClip, float volume)
    {
        if (audioSource != null && audioClip != null) {
            audioSource.PlayOneShot(audioClip, volume);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SBKNote"))
        {
            Destroy(other.gameObject);
            sb.sbkNote_Count++;
            if(sb.sbkNote_Count == 1 && sbkHelpImage != null && !previouslyShownSBKHelp && isLevelOne) {
                StartCoroutine(DisplayImageForSeconds(sbkHelpImage, helpDisplayDuration));
                PlaySound(sbkInfoSound, 5f);
                previouslyShownSBKHelp = true;
            }
            // else if(sb.sbkNote_Count == totalSBKCount && completionImage != null) {
            //     StartCoroutine(DisplayImageForSeconds(completionImage, completionDisplayDuration));
            //     PlaySound(completeLevelSound, 5f);
            // }
            else{
                PlaySound(collectSBKSound, 2f);
            }
        }
    }

    IEnumerator DisplayImageForSeconds(GameObject gameObject, float seconds)
    {
        // Show the image
        gameObject.SetActive(true);
    

        // Wait for the specified duration
        yield return new WaitForSeconds(seconds);

        // Hide the image again
        if (gameObject != null)
        {
            gameObject.SetActive(false);
        }
    }

    void Attack(string trigger)
    {

        animator.SetTrigger(trigger);
        isAttacking = true;
        attackHitBox.SetActive(true);
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttacking = false;
        attackHitBox.SetActive(false);
    }



}
