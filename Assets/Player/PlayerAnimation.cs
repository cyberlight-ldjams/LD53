using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimation : MonoBehaviour
{
    public NavMeshAgent nma;

    public Animator anim;

    public float runThreshold;

    public float walkThreshold;

    public bool hold;

    public bool pickUp;

    private Vector3 prevPos;

    private Vector3 curPos;

    private float updateInterval = 0.05f;

    private float timeSinceUpdate;

    public float pickUpDuration;

    private float pickUpTime;

    private PlayerMovement pm;

    void Start()
    {
        pm = this.gameObject.GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        if (timeSinceUpdate > updateInterval)
        {
            prevPos = curPos;
            curPos = nma.gameObject.transform.position;
            timeSinceUpdate = 0;
        } else
        {
            timeSinceUpdate += Time.fixedDeltaTime;
        }
    }

    private float GetPlayerSpeed()
    {
        return Vector3.Distance(prevPos, curPos);
    }

    void Update()
    { 
        float vel = GetPlayerSpeed();

        Debug.Log(vel);

        if (pickUp)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("Moving", false);
            anim.SetBool("Hold", true);
            if (pickUpTime < pickUpDuration)
            {
                pickUpTime += Time.deltaTime;
                pm.enabled = false;
            } else
            {
                pickUp = false;
                pickUpTime = 0;
                pm.enabled = true;
                anim.SetBool("Idle", true);
            }
        } else if (pm.HasItem())
        {
            anim.SetBool("Hold", true);
        } else
        {
            anim.SetBool("Hold", false);
        }

        if (vel > runThreshold)
        {
            //run
            anim.SetBool("Moving", true);
            anim.SetBool("Running", true);
            anim.SetBool("Walking", true);
            anim.SetBool("Idle", false);
        } else if (vel > walkThreshold)
        {
            //walk
            anim.SetBool("Moving", true);
            anim.SetBool("Running", false);
            anim.SetBool("Walking", true);
            anim.SetBool("Idle", false);
        } else
        {
            //idle
            anim.SetBool("Moving", false);
            anim.SetBool("Running", false);
            anim.SetBool("Walking", false);
            anim.SetBool("Idle", true);
        }
    }
}
