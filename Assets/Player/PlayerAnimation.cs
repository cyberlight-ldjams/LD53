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

    private Vector3 prevPos;

    private Vector3 curPos;

    private float updateInterval = 0.05f;

    private float timeSinceUpdate;

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

        if (vel > runThreshold)
        {
            //run
            anim.SetBool("Running", true);
            anim.SetBool("Walking", true);
        } else if (vel > walkThreshold)
        {
            //walk
            anim.SetBool("Running", false);
            anim.SetBool("Walking", true);
        } else
        {
            //idle
            anim.SetBool("Running", false);
            anim.SetBool("Walking", false);
        }
    }
}
