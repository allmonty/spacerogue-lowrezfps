using UnityEngine;
using System.Collections;

public abstract class MovementController : MonoBehaviour {

    public Vector3 moveDir;

    [Header("Horizontal Movement")]
    public float maxHorizontalSpeed = 5.0f;
    public float acceleration = 1.0f;
    public float deceleration = 1.0f;

    protected Rigidbody rigid = null;
    //protected Animator anim = null;

    virtual protected void Start()
    {
        rigid = GetComponent<Rigidbody>();
        //anim = GetComponentInChildren<Animator>();
    }

    virtual protected void FixedUpdate()
    {
        if (moveDir.magnitude > 0f)
        {
            accelerate();
        }

        if (moveDir.x == 0.0f || moveDir.z == 0.0f)
        {
            decelerate();
        }
    }

    abstract protected void accelerate();
    abstract protected void decelerate();

    virtual public void jump() {}
}
