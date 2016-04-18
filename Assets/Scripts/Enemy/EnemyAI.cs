using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    public EnemyShoot shootControl;

    public float visionDistance = 10f;
    public float shootDistance = 5f;
    public float visionAngle = 30f;

    NavMeshAgent navAgent;
    Rigidbody rigid;
    Transform player;

    Vector3 startPosition;

    Quaternion initialRotation;
    bool shouldCorrectRotation = false;

    bool isPlayerInSight = false;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        rigid = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        startPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void OnDrawGizmos()
    {
        //Draws the vision sphere
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, visionDistance);

        //Draws the vision angle
        UnityEditor.Handles.color = new Color(0.0f, 0.0f, 1.0f, 0.5f);
        Vector3 centeredAngle = Quaternion.Euler(0f, -visionAngle/2f, 0f) * transform.forward;
        UnityEditor.Handles.DrawSolidArc(transform.position, transform.up, centeredAngle, visionAngle, visionDistance);

        UnityEditor.Handles.color = new Color(0.0f, 1.0f, 0.0f, 0.5f);
        UnityEditor.Handles.DrawSolidArc(transform.position, transform.up, centeredAngle, visionAngle, shootDistance);
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) <= visionDistance)
        {
            if(isInsideVisionCone(player.position))
            {
                if(isNotOccludedFromVision(player.position, "Player"))
                {
                    isPlayerInSight = true;
                }
                else
                {
                    isPlayerInSight = false;
                }
            }
            else
            {
                isPlayerInSight = false;
            }
        }
        else
        {
            isPlayerInSight = false;
        }

        if(isPlayerInSight)
        {
            attackPlayer();
        }
        else
        {
            backToStartPosition();
        }
    }

    bool isInsideVisionCone(Vector3 obj)
    {
        Vector3 objDirection = (obj - transform.position).normalized;
        float objAngleInVision = Vector3.Angle(transform.forward, objDirection);

        if(objAngleInVision <= (visionAngle/2f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool isNotOccludedFromVision(Vector3 obj, string layer)
    {
        Vector3 objDirection = (obj - transform.position).normalized;
        RaycastHit rayHit;
        if(Physics.Raycast(transform.position, objDirection, out rayHit, visionDistance))
        {
            if(rayHit.transform.gameObject.layer == LayerMask.NameToLayer(layer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void backToStartPosition()
    {
        rigid.velocity = Vector3.zero;
        navAgent.SetDestination(startPosition);
        if (Vector3.Distance(transform.position, startPosition) <= 1f)
        {
            if (Vector3.Angle(transform.forward, initialRotation.eulerAngles) >= 0.1f)
            {
                shouldCorrectRotation = true;
            }
            else
            {
                shouldCorrectRotation = false;
            }
        }

        if(shouldCorrectRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, Time.deltaTime);
        }
    }

    void attackPlayer()
    {
        rigid.velocity = Vector3.zero;
        float distanceFromPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceFromPlayer <= shootDistance)
        {
            shootControl.shoot(player.position);
        }

        if(distanceFromPlayer >= shootDistance/2f)
        {
            Vector3 destination = (transform.position - player.position).normalized*(shootDistance/2f) + player.position;
            navAgent.SetDestination(destination);
        }
    }
}
