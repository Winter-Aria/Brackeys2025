using UnityEngine;
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
    [SerializeField] public float maxEnergy = 5f;
    [SerializeField] private float energyRegenRate = 1f;
    [SerializeField] private float sprintEnergyCost = 1f;
    [SerializeField] private GameObject SCRAPSprite;
    public ShakeData shakeData;
    public ParticleSystem particles;

    public float currentEnergy;
    private bool isSprinting;
    public float dirX = 0f;
    private bool menuActive = false;
    [SerializeField] private CanvasGroup menuCanvasGroup;

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
        HandleMenuToggle();
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

        // Allow sprinting to continue down to 0, but not start below 3
        if (Input.GetKey(KeyCode.LeftShift) && (isSprinting || currentEnergy > 3f))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
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

            if (currentEnergy <= 0f)
            {
                isSprinting = false; // Stop sprinting when energy hits 0
            }

            if (shakeData != null)
            {
                CameraShakerHandler.Shake(shakeData);
            }
            particles.Play();


            SoundManager.Instance.PlayLoopingBoostSound("Boost");
        }
        else
        {
            particles.Stop();


            SoundManager.Instance.StopLoopingBoostSound();
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
        if (dirX != 0f)
        {
            bool facingLeft = dirX > 0f; // Player starts facing left, so reverse logic

            // Flip player sprite
            Vector3 newScale = SCRAPSprite.transform.localScale;
            newScale.x = facingLeft ? -Mathf.Abs(newScale.x) : Mathf.Abs(newScale.x);
            SCRAPSprite.transform.localScale = newScale;

            // Flip Particles: Change localScale to properly flip the effect
            Vector3 particleScale = particles.transform.localScale;
            particleScale.x = facingLeft ? -Mathf.Abs(particleScale.x) : Mathf.Abs(particleScale.x);
            particles.transform.localScale = particleScale;
        }
    }



    private void HandleMenuToggle()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            menuActive = !menuActive;
            menuCanvasGroup.alpha = menuActive ? 1 : 0;
            menuCanvasGroup.interactable = menuActive;
            menuCanvasGroup.blocksRaycasts = menuActive;
        }

    }


    public void OnRunStart()
    {

        SoundManager.Instance.PlaySound2D("MovementStart");
    }


    public void OnRunStop()
    {

        SoundManager.Instance.PlaySound2D("MovementStop");
    }


    public void OnRunLoop()
    {

        SoundManager.Instance.PlayLoopingSound("MovementLoop");
    }


    public void OnStopRunLoop()
    {
        SoundManager.Instance.StopLoopingSound();
    }
}