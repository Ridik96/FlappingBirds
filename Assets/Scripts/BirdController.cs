using UnityEngine.SceneManagement;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    private AudioSource m_audioSource;

    public Rigidbody2D m_rigidbodyBird;
    public float force;
    public float velocity;
    public float rotationSpeed;
    public LayerMask mask;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (mask == (mask | (1 << collision.collider.gameObject.layer)))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public float BirdSpeed => m_rigidbodyBird.velocity.magnitude;
    private void FixedUpdate()
    {
        m_rigidbodyBird.velocity = new Vector2(velocity, m_rigidbodyBird.velocity.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        GameplayManager.OnGamePaused += doPause;
        GameplayManager.OnGamePlaying += doPlay;
    }
    
    
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
        {
            m_rigidbodyBird.AddForce(Vector2.up * force);
            m_audioSource.PlayOneShot(GameplayManager.Instance.GameDatabase.Jump);
        }

          if(m_rigidbodyBird.velocity.y < 0 )
         {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.rotation = Quaternion.Euler(0, 0, -30.0f), Time.deltaTime * rotationSpeed);
         }
         else
         {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.rotation = Quaternion.Euler(0, 0, 30.0f), Time.deltaTime * rotationSpeed );
        }
    }

    private void doPlay()
    {
        m_rigidbodyBird.simulated = true;
    }

    private void doPause()
    {
        m_rigidbodyBird.simulated = false;
    }

    private void OnDestroy()
    {
        GameplayManager.OnGamePaused -= doPause;
        GameplayManager.OnGamePlaying -= doPlay;
    }
}
