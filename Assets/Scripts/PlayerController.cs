using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using FirstGearGames.SmoothCameraShaker;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D coll;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 15f;
    [SerializeField] private float velPower = 1f;
    [SerializeField] private float frictionAmount = 5f;
    [SerializeField] private float maxEnergy = 5f;
    [SerializeField] private float energyRegenRate = 1f;
    [SerializeField] private float sprintEnergyCost = 1f;
    [SerializeField] private GameObject SCRAPSprite;
    public ShakeData shakeData;
    public ParticleSystem particles;

    public float currentEnergy;
    private bool isSprinting;
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
        currentEnergy = maxEnergy;
    }
    public void Update()
    {
        if (dirX != 0f)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        HandleSprint();
        RegenerateEnergy();
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
        isSprinting = Input.GetKey(KeyCode.LeftShift) && currentEnergy > 0f;
    }
    public void Move()
    {
        float speedModifier = isSprinting ? sprintMultiplier : 1f;
        float targetSpeed = dirX * moveSpeed * speedModifier;

        float speedDiff = targetSpeed - rb.linearVelocityX;
        float accelRate = Mathf.Abs(targetSpeed) > 0.01f ? acceleration : deceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, velPower) * Mathf.Sign(speedDiff);

        rb.AddForce(movement * Vector2.right);
    }

    private void HandleSprint()
    {
        if (isSprinting)
        {
            currentEnergy -= sprintEnergyCost * Time.deltaTime;
            currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);
            
            // Trigger screen shake when sprinting
            if (shakeData != null)
            {
                
                CameraShakerHandler.Shake(shakeData);
            }
            particles.Play();
        }
        else
        {
            particles.Stop();
        }
    }

    private void RegenerateEnergy()
    {
        if (!isSprinting)
        {
            currentEnergy += energyRegenRate * Time.deltaTime;
            currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);
        }
    }

    private void Flip()
    {
        if (dirX > 0f)
        {
            Vector3 newScale = SCRAPSprite.transform.localScale;
            newScale.x = Mathf.Abs(newScale.x);
            SCRAPSprite.transform.localScale = new Vector3(-newScale.x, newScale.y, newScale.z);

            // Flip particles
            Vector3 particleScale = particles.transform.localScale;
            particleScale.x = Mathf.Abs(particleScale.x);
            particles.transform.localScale = new Vector3(-particleScale.x, particleScale.y, particleScale.z);
        }
        else if (dirX < 0f)
        {
            Vector3 newScale = SCRAPSprite.transform.localScale;
            newScale.x = -Mathf.Abs(newScale.x);
            SCRAPSprite.transform.localScale = new Vector3(-newScale.x, newScale.y, newScale.z);

            // Flip particles
            Vector3 particleScale = particles.transform.localScale;
            particleScale.x = -Mathf.Abs(particleScale.x);
            particles.transform.localScale = new Vector3(-particleScale.x, particleScale.y, particleScale.z);
        }
    }

}
