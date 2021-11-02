using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float gravityForce;
    public float force;
    public float velocity;
    public float rotationSpeed;
    public LayerMask mask;

    [Header("Component")]
    public AudioSource m_audioSource;
    public Rigidbody2D m_rigidbodyBird;
    public Animator m_animator;
    public SpriteRenderer m_spriterenderer;

    [HideInInspector] public bool death = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (mask == (mask | (1 << collision.collider.gameObject.layer)))
        {
            m_audioSource.PlayOneShot(GameplayManager.Instance.GameDatabase.Inpact);
            death = true;
            m_animator.enabled = false;
            GameplayManager.Instance.m_HUD.SetEnd = death;
            Time.timeScale = 0;
           
        }
    }

    public float BirdSpeed => m_rigidbodyBird.velocity.magnitude;
    private void FixedUpdate()
    {
        m_rigidbodyBird.velocity = new Vector2(velocity, m_rigidbodyBird.velocity.y);
    }

    void Start()
    {
        GameplayManager.OnGamePaused += doPause;
        GameplayManager.OnGamePlaying += doPlay;
        Time.timeScale = 1
;
    }
    
    
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space)&&(!GameplayManager.Instance.isBeginning))
        { 
            m_rigidbodyBird.AddForce(Vector2.up * force,ForceMode2D.Impulse);
            m_audioSource.PlayOneShot(GameplayManager.Instance.GameDatabase.Jump);
        }

        //Physics2D.gravity = new Vector3(0, -gravityForce, 0);
        
       if(m_rigidbodyBird.velocity.y < 0 )
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.rotation = Quaternion.Euler(0, 0, -30.0f), Time.deltaTime * rotationSpeed);
        }

       else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.rotation = Quaternion.Euler(0, 0, 30.0f), Time.deltaTime * rotationSpeed);
        }
    }

    private void doPlay()
    {
        m_rigidbodyBird.simulated = true;
        m_animator.enabled = true;
    }

    private void doPause()
    {
        m_rigidbodyBird.simulated = false;
        m_animator.enabled = false;
    }

    private void OnDestroy()
    {
        GameplayManager.OnGamePaused -= doPause;
        GameplayManager.OnGamePlaying -= doPlay;
    }
}
