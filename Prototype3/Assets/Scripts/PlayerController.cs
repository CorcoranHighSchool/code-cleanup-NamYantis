using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //The player's Rigidbody
    private Rigidbody playerRb;
    //Jump force
    private float jumpForce = 15.0f;
    //Gravity Modifier
    [Serialize Field]private float gravityModifier;
    //Are we on the ground?
    private bool isOnGround = true;
    //Is the Game Over
    public bool gameOver = {get; private set;};

    //Player Animator
    private Animator playerAnim;

    //ParticleSystem explosion
    [Serialize Field]private ParticleSystem explositionParticle;
    //ParticleSystem dirt
    [Serialize Field]private ParticleSystem dirtParticle;

    //Jump sound
    [Serialize Field]private AudioClip jumpSound;
    //Crash sound
    [SerializeField]private AudioClip crashSound;
    //Player Audio
    [Serialize Field]private AudioSource playerAudio;
    private const string gameOverString = "Game Over!";
    private const string jumpString = "Jump_trig";
    private const string groundString = "Ground";
    private const string deathAnimation = "Death_b";
    private const string deathAnimation2 = "DeathType_int";
    // Start is called before the first frame update
    void Start()
    {
        //Get the rigidbody
        playerRb = GetComponent<Rigidbody>();
        //Connect the Animator
        playerAnim = GetComponent<Animator>();
        //Player Audio
        //playerAudio.GetComponent<AudioSource>();
        //update the gravity
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        //If we press space, jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //trigger the jump animation
            playerAnim.SetTrigger(jumpString);
            isOnGround = false;
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            dirtParticle.Stop();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(groundString))
        {
            dirtParticle.Play();
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag(groundString))
        {
            explositionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);

            gameOver = true;
            Debug.Log(gameOverString);
            playerAnim.SetBool(deathAnimation, true);
            playerAnim.SetInteger(deathAnimation2, 1);r 
        }
    }
}
