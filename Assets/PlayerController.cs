using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : MonoBehaviour
{
  public static PlayerController instance;
  private Rigidbody2D rb;
  private Animator animator;
    private BoxCollider2D coll;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 15f;
    [SerializeField] private float velPower = 1f;
    [SerializeField] private float frictionAmount = 5f;
    [SerializeField] private GameObject SCRAPSprite;
    
    
    public float dirX = 0f;


    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public void Update()
    {
     if (dirX !=0f)
        {
            animator.SetBool("Running",true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }
    public void FixedUpdate()
    {
        GetInputs();
        Move();
        Flip();
    }
    private void GetInputs()
    {
        dirX = Input.GetAxisRaw("Horizontal");
    }
    public void Move()
    {

        float targetSpeed = dirX * moveSpeed;

        // Calculate the difference between the current velocity and the target speed
        float speedDiff = targetSpeed - rb.linearVelocityX;

        // Adjust acceleration rate based on whether accelerating or decelerating
        float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : deceleration;

        // Compute the movement force with velocity scaling
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velPower) * Mathf.Sign(speedDiff);

        // Apply force only on the X axis
        rb.AddForce(movement * Vector2.right);
    }

    private void Flip()
    {
        if (dirX > 0f)
        {
            Vector3 newScale = SCRAPSprite.transform.localScale;
            newScale.x = Mathf.Abs(newScale.x); // Ensure it's positive first
            SCRAPSprite.transform.localScale = new Vector3(-newScale.x, newScale.y, newScale.z); // Flip correctly
        }
        else if (dirX < 0f)
        {
            Vector3 newScale = SCRAPSprite.transform.localScale;
            newScale.x = -Mathf.Abs(newScale.x); // Ensure it's negative first
            SCRAPSprite.transform.localScale = new Vector3(-newScale.x, newScale.y, newScale.z); // Flip correctly
        }
    }

}
