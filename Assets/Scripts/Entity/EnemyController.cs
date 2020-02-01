﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    private GameObject player;
    private Transform target;
    private NavMeshAgent agent;
    private float TimeInterval;

    public int dealDamage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG);
        target = player.transform;
        agent = GetComponentInParent<NavMeshAgent>();
        dealDamage = 10;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        TimeInterval += Time.deltaTime;
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance + 0.5)
            {   
                FaceTarget();
                if (TimeInterval >= 1)
                {
                Debug.Log("NO");
                TimeInterval = 0;
                Melee();
                }
            }
        }
    }

    void Melee()
    {
        PlayerStats stats = player.GetComponentInChildren<PlayerStats>();
        stats.TakeDamage(dealDamage);
        
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
