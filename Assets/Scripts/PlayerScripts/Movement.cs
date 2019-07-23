using System;
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
    public bool Block;
    public bool HeavyPunch;
    public bool HasKeyCard;

    public int Credit;

    public GameObject[] Hairs;
    public List<String> AnimationNames;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Block = false;
        HasKeyCard = false;
        Credit = 0;
        animator.SetBool("usingFist", false);
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            AnimationNames.Add(clip.name);
        }
    }

    void Update()
    {
        SetHair();

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
            if (Input.GetKeyDown(KeyCode.Mouse0) && gameObject.GetComponent<PlayerHealt>().currentHealth > 0 && punchTimer <= 0 && !Block)
            {
                Attack(attackHitboxes[0]);
                animator.SetTrigger("punchTrigger");
                idleTimer = 0;
                punchTimer = punchTimerFixed;
                walkTimer = walkTimerFixed;
            }

            // Heavy Attack
            if (Input.GetKeyDown(KeyCode.Mouse1) && gameObject.GetComponent<PlayerHealt>().currentHealth > 0 && punchTimer <= 0 && !Block && HeavyPunch)
            {
                Attack(attackHitboxes[0]);
                animator.SetTrigger("punchTrigger");
                idleTimer = 0;
                punchTimer = punchTimerFixed;
                walkTimer = walkTimerFixed;
            }

            //Block
            if (Input.GetKeyDown(KeyCode.Space) && gameObject.GetComponent<PlayerHealt>().currentHealth > 0 && punchTimer <= 0)
            {
                Block = true;
                animator.SetBool("isBlocking", true);
                idleTimer = 0;
            } else if(Input.GetKeyUp(KeyCode.Space))
            {
                Block = false;
                animator.SetBool("isBlocking", false);
            }

            if (!Block) {

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

                    // Switch sprites between side, front and back
                    // SIDE
                    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                    {
                        animator.SetBool("lookingSide", true);
                        animator.SetBool("lookingUp", false);
                        animator.SetBool("lookingDown", false);
                        if(Input.GetKey(KeyCode.A))
                        {
                            attackHitboxes[0].transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
                        } else if (Input.GetKey(KeyCode.D))
                        {
                            attackHitboxes[0].transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                        }
                        attackHitboxes[0].transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                    }
                    // UP
                    else if (Input.GetKey(KeyCode.W))
                    {
                        animator.SetBool("lookingSide", false);
                        animator.SetBool("lookingUp", true);
                        animator.SetBool("lookingDown", false);
                        attackHitboxes[0].transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
                        attackHitboxes[0].transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 90, 0);
                    }
                    // DOWN
                    else if (Input.GetKey(KeyCode.S))
                    {
                        animator.SetBool("lookingSide", false);
                        animator.SetBool("lookingUp", false);
                        animator.SetBool("lookingDown", true);
                        attackHitboxes[0].transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);
                        attackHitboxes[0].transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 90, 0);
                    }
                }
                else
                {
                    animator.SetBool("isWalking", false);
                }

                // Trigger idle2 animation
                if (idleTimer >= 5)
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
    }

    private void SetHair()
    {
        int currentHair = 0;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle_Back"))
        {
            currentHair = 0;
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle_Side"))
        {
            currentHair = 1;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle_Front"))
        {
            currentHair = 2;
        }

        for (int i = Hairs.Length-1; i >= 0; i--)
        {
            Debug.Log(i);
            if(currentHair == i)
            {
                Hairs[i].SetActive(true);
            } else
            {
                Hairs[i].SetActive(false);
            }
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

    public void UpgradePunch(bool status)
    {
        HeavyPunch = status;

        // Changes Player sprite to have BFF
        animator.SetTrigger("fistSpriteTrigger");
        animator.SetBool("usingFist", true);
    }

    public void PickUp(string str)
    {
        if(str.Equals("KeyCard"))
        {
            HasKeyCard = true;
        } else if(str.Equals("Credit"))
        {
            Credit++;
        } else
        {
            Debug.Log(str);
        }

    }

    public int GetCredit()
    {
        return Credit;
    }
}
