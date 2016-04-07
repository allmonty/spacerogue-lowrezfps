using UnityEngine;
using System.Collections;

public class PlayerMovementControl : MovementController {

    [Header("Jumping")]
    public float jumpInitialSpeed = 10.0f;
    public float distToGround = 1.1f;

    Vector3 horizontalVelocity = Vector3.zero;

    override public void jump()
    {
        RaycastHit hitInfo;
        if(Physics.Raycast(transform.position, -Vector3.up, out hitInfo, distToGround))
        {
            if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                rigid.velocity += new Vector3(0f, jumpInitialSpeed, 0f);
            }
        }
    }

    override protected void accelerate()
    {
        rigid.velocity += moveDir.normalized * acceleration * Time.fixedDeltaTime;

        horizontalVelocity.Set(rigid.velocity.x, 0f, rigid.velocity.z);
        
        if (horizontalVelocity.magnitude > maxHorizontalSpeed)
        {
            horizontalVelocity = horizontalVelocity.normalized * maxHorizontalSpeed;
            rigid.velocity = horizontalVelocity + (Vector3.up * rigid.velocity.y);
        }
    }

    override protected void decelerate()
    {
        horizontalVelocity.Set(rigid.velocity.x, 0f, rigid.velocity.z);

        if(horizontalVelocity.x != 0f && moveDir.x == 0f)
        {
            float decelerationFactor = deceleration * Time.fixedDeltaTime;
            decelerationFactor *= horizontalVelocity.x > 0f ? -1f : 1f;

            if (Mathf.Abs(horizontalVelocity.x + decelerationFactor) >= decelerationFactor)
            {
                rigid.velocity += new Vector3(decelerationFactor, 0f, 0f);
            }
            else
            {
                rigid.velocity = new Vector3(0f, rigid.velocity.y, rigid.velocity.z);
            }
        }

        if (horizontalVelocity.z != 0f && moveDir.z == 0f)
        {
            float decelerationFactor = deceleration * Time.fixedDeltaTime;
            decelerationFactor *= horizontalVelocity.z > 0f ? -1f : 1f;

            if (Mathf.Abs(horizontalVelocity.z + decelerationFactor) >= decelerationFactor)
            {
                rigid.velocity += new Vector3(0f, 0f, decelerationFactor);
            }
            else
            {
                rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y, 0f);
            }
        }
    }
}