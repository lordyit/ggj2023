using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyOne : MonoBehaviour
{
    [SerializeField] private float distance;

    public int MyDamage;
    public bool Attacking = false;

    private EnemyStatus _enemyStatus;
    private NavMeshAgent _agent;
    private SpriteRenderer _sprite;
    private Animator _animator;
    private Transform _playerPosition;
    private WaitForSeconds _waitTakeDamage;

    private bool _chasePlayer = true;
    private bool _canTakeDamage = true;

    private void Awake()
    {
        SetUpComponents();
    }
    void Start()
    {
        SetUpVariables();
        _agent.isStopped = true;
        _waitTakeDamage = new WaitForSeconds(0.1f);
    }

    private void OnEnable()
    {
        StartCoroutine(AddtoList());
    }

    IEnumerator AddtoList()
    {
        yield return new WaitForSeconds(0.1f);
        LevelManager.EnemyOne.Add(this);
    }

    private void OnDisable()
    {
        LevelManager.EnemyOne.Remove(this);
        LevelManager.Instance.CheckEndLevel();
    }
    private void SetUpComponents()
    {
        _agent = GetComponent<NavMeshAgent>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        _enemyStatus = GetComponent<EnemyStatus>();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Sword>())
        {
            if (!_canTakeDamage) return;

            _canTakeDamage = false;
            _enemyStatus.ReduceLife(1);
            StartCoroutine(TakeDamageCr());
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            _chasePlayer = false;
            if (_agent.isActiveAndEnabled)
            {
                _agent.isStopped = true;
            }
            else
            {
                Debug.LogError("[EnemyOne] agent is not active or is not enabled.", this);
            }
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

    IEnumerator TakeDamageCr()
    {
        FeelManager.Instance.ShakeCamera(5, 0.1f);
        FeelManager.Instance.HitVfxActive(transform, FeelManager.Instance.HitVfx);
        if (_enemyStatus.Lives <= 0) Die();
        for (int i = 0; i < 5; i++)
        {
            _sprite.enabled = !_sprite.enabled;
            yield return _waitTakeDamage;
        }
        _sprite.enabled = true;
        _canTakeDamage = true;
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    void Update()
    {
        ChasePlayer();
        MoveAnimation();
    }
}
