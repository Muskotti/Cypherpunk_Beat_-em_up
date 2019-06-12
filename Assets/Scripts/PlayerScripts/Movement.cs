using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    CharacterController characterController;
    public float jumpSpeed = 8.0F;
    public float movementSpeed = 5.0f;
    private Vector3 moveDirection = Vector3.zero;

    public Collider[] attackHitboxes;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack(attackHitboxes[0]);
            }

            if(Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = moveDirection * movementSpeed;
        }

        //Gravity
        moveDirection.y -= 10f * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
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
    }
}
