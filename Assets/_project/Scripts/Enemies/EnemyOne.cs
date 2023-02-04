using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyOne : MonoBehaviour
{
    [SerializeField] private float distance;

    public int MyDamage;
    public bool Attacking = false;

    private NavMeshAgent _agent;
    private SpriteRenderer _sprite;
    private Animator _animator;
    private Transform _playerPosition;

    private bool _chasePlayer = true;

    private void Awake()
    {
        SetUpComponents();
    }
    void Start()
    {
        SetUpVariables();
        _agent.isStopped = true;
    }

    private void SetUpComponents()
    {
        _agent = GetComponent<NavMeshAgent>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void SetUpVariables()
    {
        _playerPosition = GameManager.Instance.PlayerPosition;
    }

    private void ChasePlayer()
    {
        if (!_chasePlayer) return;
        if (Attacking)
        {
            _agent.isStopped = true;
            return;
        }

        if (Vector3.Distance(transform.position, _playerPosition.position) < distance)
        {
            _agent.isStopped = false;
            _agent.destination = _playerPosition.position;
            if (transform.position.x - _playerPosition.position.x < 0) _sprite.flipX = true;
            else _sprite.flipX = false;
        }
    }

    private void MoveAnimation()
    {
        if (!_agent.isStopped)
            _animator.SetInteger("move", 1);
        else
            _animator.SetInteger("move", 0);

        if (Vector3.Distance(transform.position, _agent.destination) < 1)
            _animator.SetInteger("move", 0);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            _chasePlayer = false;
            _agent.isStopped = true;
            if (!Attacking)
                _animator.SetTrigger("attack");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            _chasePlayer = true;
            _agent.isStopped = false;
        }
    }

    void Update()
    {
        ChasePlayer();
        MoveAnimation();
    }
}
