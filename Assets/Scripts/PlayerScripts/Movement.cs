using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    CharacterController characterController;
    public float jumpSpeed = 8.0F;
    public float movementSpeed = 5.0f;
    private Vector3 moveDirection = Vector3.zero;

    public Animator animator;

    public Collider[] attackHitboxes;
    public bool IsDead;
    public float idleTimer = 0f;
    public float punchTimer;
    public float punchTimerFixed;
    public float walkTimer;
    public float walkTimerFixed;

    public bool interacting;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        idleTimer += Time.deltaTime;
        if(punchTimer > 0)
        {
            punchTimer -= Time.deltaTime;
        }

        if(walkTimer > 0)
        {
            walkTimer -= Time.deltaTime;
        }

        if (!IsDead && !interacting)
        {
            // Attack
            if (Input.GetKeyDown(KeyCode.Mouse0) && gameObject.GetComponent<PlayerHealt>().currentHealth > 0 && punchTimer <= 0)
            {
                Attack(attackHitboxes[0]);
                animator.SetTrigger("punchTrigger");
                idleTimer = 0;
                punchTimer = punchTimerFixed;
                walkTimer = walkTimerFixed;
            }
            
            if (walkTimer <= 0)
            {
                characterController.enabled = true;
            } else
            {
                characterController.enabled = false;
            }

            if (characterController.isGrounded)
            {

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }

                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                moveDirection = moveDirection * movementSpeed;
            }

            // Sprite flip
            if (moveDirection.x > 0)
            {
                if (transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
            }
            else if (moveDirection.x < 0)
            {
                if (transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
            }

            // Walking animation
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                idleTimer = 0;
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

            // Trigger idle2 animation
            if (idleTimer >= 2)
            {
                animator.SetBool("idle2", true);
            }
            else
            {
                animator.SetBool("idle2", false);
            }

            //Gravity
            moveDirection.y -= 10f * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    public void Attack(Collider collider)
    {
        Collider[] cols = Physics.OverlapBox(collider.bounds.center, collider.bounds.extents, collider.transform.rotation,LayerMask.GetMask("Hitboxes"));
        foreach(Collider c in cols)
        {
            if (c.transform.root == transform)
            {
                continue;
            }
            
            switch(c.name)
            {
                case "EnemyHitbox":
                    c.SendMessageUpwards("TakeDamage", 1);
                    break;
                default:
                    Debug.Log(c.name);
                    break;
            }
        }
    }

    public void SetMoveStatus (bool status)
    {
        characterController.enabled = status;
        interacting = !status;
    }

    public void SetDeadStatus(bool status)
    {
        IsDead = status;
    }
}
