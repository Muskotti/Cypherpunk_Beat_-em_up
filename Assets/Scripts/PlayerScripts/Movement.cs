using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    CharacterController characterController;
    public float jumpSpeed = 8.0F;
    public float movementSpeed = 5.0f;
    public float hitForce;
    private Vector3 moveDirection = Vector3.zero;
    public String direction;
    GameObject soundManager;

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

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        soundManager = GameObject.Find("SoundManager");
        Block = false;
        HasKeyCard = false;
        Credit = 0;
        animator.SetBool("usingFist", false);
        direction = "right";
        Credit = SavedInfo.Credits;
        hitForce = 30;
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
                Attack(attackHitboxes[0], direction);
                animator.SetTrigger("punchTrigger");
                idleTimer = 0;
                punchTimer = punchTimerFixed;
                walkTimer = walkTimerFixed;
            }

            // Heavy Attack
            if (Input.GetKeyDown(KeyCode.Mouse1) && gameObject.GetComponent<PlayerHealt>().currentHealth > 0 && punchTimer <= 0 && !Block)
            {
                HeavyAttack(attackHitboxes[0], direction);
                animator.SetTrigger("heavyPunchTrigger");
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
                        if (Input.GetKey(KeyCode.A))
                        {
                            direction = "left";
                        }
                        else
                        {
                            direction = "right";
                        }

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
                    // UP and DOWN
                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                    {
                        if (transform.localScale.x < 0)
                        {
                            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                        }
                    }

                    // UP
                    if (Input.GetKey(KeyCode.W))
                    {
                        direction = "up";
                        animator.SetBool("lookingSide", false);
                        animator.SetBool("lookingUp", true);
                        animator.SetBool("lookingDown", false);
                        attackHitboxes[0].transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
                        attackHitboxes[0].transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 90, 0);
                    }
                    // DOWN
                    else if (Input.GetKey(KeyCode.S))
                    {
                        direction = "down";
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
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_walk_side"))
        {
            currentHair = 3;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_walk_back"))
        {
            currentHair = 4;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_walk_front"))
        {
            currentHair = 5;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_punch_front"))
        {
            currentHair = 6;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_punch_side"))
        {
            currentHair = 7;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_punch_back"))
        {
            currentHair = 8;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_block_side"))
        {
            currentHair = 9;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_block_front"))
        {
            currentHair = 10;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_block_back"))
        {
            currentHair = 11;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player2_idle"))
        {
            currentHair = 12;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_heavypunch_side"))
        {
            currentHair = 13;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_heavypunch_front"))
        {
            currentHair = 14;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_heavypunch_back"))
        {
            currentHair = 15;
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("First_idle_back"))
        {
            currentHair = 16;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fist_Idle_side"))
        {
            currentHair = 17;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("First_Idle_front"))
        {
            currentHair = 18;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fist_walk_side"))
        {
            currentHair = 19;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fist_walk_back"))
        {
            currentHair = 20;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fist_walk_front"))
        {
            currentHair = 21;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fist_punch_front"))
        {
            currentHair = 22;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fist_punch_side"))
        {
            currentHair = 23;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fist_punch_back"))
        {
            currentHair = 24;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fist_heavypunch_front"))
        {
            currentHair = 25;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fist_heavypunch_side"))
        {
            currentHair = 26;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fist_heavypunch_back"))
        {
            currentHair = 27;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fist_block_front"))
        {
            currentHair = 28;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fist_Block_side"))
        {
            currentHair = 29;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fist_block_back"))
        {
            currentHair = 30;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fist2_Idle"))
        {
            currentHair = 31;
        }

        for (int i = Hairs.Length-1; i >= 0; i--)
        {
            if(currentHair == i)
            {
                Hairs[i].SetActive(true);
            } else
            {
                Hairs[i].SetActive(false);
            }
        }
    }

    public void Attack(Collider collider, String direction)
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
                    c.SendMessageUpwards("TakeDamage", direction);
                    break;
                default:
                    Debug.Log(c.name);
                    break;
            }
        }
    }
    public void HeavyAttack(Collider collider, String direction)
    {
        Collider[] cols = Physics.OverlapBox(collider.bounds.center, collider.bounds.extents, collider.transform.rotation, LayerMask.GetMask("Hitboxes"));
        foreach (Collider c in cols)
        {
            if (c.transform.root == transform)
            {
                continue;
            }

            switch (c.name)
            {
                case "EnemyHitbox":
                    c.SendMessageUpwards("TakeHeavyDamage", direction);
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
        hitForce = 60;

        // Changes Player sprite to have BFF
        animator.SetTrigger("fistSpriteTrigger");
        animator.SetBool("usingFist", true);
    }

    public void PickUp(string str)
    {
        if(str.Equals("KeyCard"))
        {
            soundManager.GetComponent<SoundManager>().KeycardPickupPlay();
            HasKeyCard = true;
        } else if(str.Equals("Credit"))
        {
            soundManager.GetComponent<SoundManager>().CreditPickupPlay();
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

    public void SetCredits(int value)
    {
        Credit -= value;
    }
}
