﻿using KPU.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _agent;

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {

        EventManager.On("game_ended", Hide);
    }

    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("Speed", _agent.speed);
    }
    public void ForwardTarget(object obj)
    {
        GameObject target = GameObject.Find("Portal");
        if(target)
            _agent.SetDestination(target.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {       
        if (other.gameObject.CompareTag("Portal"))
        {
            GameManager.Instance.SetEndState(false);
            EventManager.Emit("game_ended");
        }
    }

    private void Hide(object obj) => gameObject.SetActive(false);
}
