using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealt : MonoBehaviour
{
    public int startHealt = 5;
    public int currentHealth;
    bool isStunned;
    bool knockedBack;

    public GameObject deathScreen;

    bool damage;
    public bool isDead;

    GameObject soundManager;

    public GameObject playerhit;
    private GameObject player;

    // Stun
    public float stunTimer;
    public float stunCountdown;

    public Animator animator;
    CharacterController cc;

    // Knockback
    float mass = 3.0F; // defines the character mass
    Vector3 impact = Vector3.zero;

    private void Awake()
    {
        currentHealth = startHealt;
        stunCountdown = stunTimer;
        isStunned = false;
        knockedBack = false;

        soundManager = GameObject.Find("SoundManager");
        deathScreen.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        cc = player.GetComponent<CharacterController>();
    }

    public void Update()
    {

        if (isStunned)
        {
            stunCountdown = stunCountdown -= Time.deltaTime;
        }

        if (stunCountdown <= 0)
        {
            if (!isDead)
            {
                isStunned = false;
                animator.SetBool("isStunned", false);

                // Enable scripts after stun ends
                GetComponent<Movement>().enabled = true;

                if (knockedBack)
                {
                    knockedBack = false;
                    playerStandUp();
                }
                // Enable movement when stun ends
                GetComponent<Movement>().enabled = true;
            }
            stunCountdown = stunTimer;
        }

        // Knockback
        if (impact.magnitude > 0.2F)
        {
            cc.Move(impact * Time.deltaTime);
            // consumes the impact energy each cycle:
            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount)
    {
        if (!gameObject.GetComponent<Movement>().Block) {
            Debug.Log(gameObject.GetComponent<Movement>().Block);
            soundManager.GetComponent<SoundManager>().takeDamagePlay();
            damage = true;
            currentHealth -= amount;
            GameObject hitmarker = Instantiate(playerhit, transform.position + new Vector3(0.0f, 0.1f, 0.0f), Quaternion.identity) as GameObject;
            Destroy(hitmarker, 0.2f);

            // Stun code
            animator.SetBool("isStunned", true);
            isStunned = true;
            stunCountdown = stunTimer;

            // Disable walking and punching when stunned
            GetComponent<Movement>().enabled = false;

            if (currentHealth <= 0 && !isDead)
            {
                Death();
            }
        }
    }

    public void AddImpact(Vector3 dir, float force)
    {
        knockedBack = true;

        PlayerLieDown();

        dir.Normalize();
        if (dir.y < 0)
        {
            dir.y = -dir.y; // reflect down force on the ground
        }
        impact += dir.normalized * force / mass;
    }

    void Death()
    {
        isDead = true;
        deathScreen.SetActive(true);
        player.SendMessage("SetMoveStatus",false);
        player.SendMessage("SetDeadStatus",true);

        animator.SetBool("idle2", false);
        animator.SetBool("isDead", true);

        PlayerLieDown();
        transform.localPosition = new Vector3(transform.localPosition.x - 1, transform.localPosition.y - 0.3f, transform.localPosition.z - 0.04f);
    }

    void PlayerLieDown()
    {
        if (player.transform.localScale.x > 0)
        {
            player.transform.Rotate(0, 0, 90);
        }
        else if (player.transform.localScale.x < 0)
        {
            player.transform.Rotate(0, 0, -90);
        }
    }

    void playerStandUp()
    {
        if (player.transform.localScale.x > 0)
        {
            player.transform.Rotate(0, 0, -90);
        }
        else if (player.transform.localScale.x < 0)
        {
            player.transform.Rotate(0, 0, 90);
        }
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 1, transform.localPosition.z + 1f);
    }
}
