using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwo : MonoBehaviour
{
    [SerializeField] private float _attackDistance;
    [SerializeField] private GameObject _shootGO;
    [SerializeField] private Transform _shootExit;

    private Animator _animator;
    private EnemyStatus _enemyStatus;
    private SpriteRenderer _sprite;
    private WaitForSeconds _waitTakeDamage;

    private bool _canTakeDamage = true;

    public bool CanAttack = true;

    private Transform _playerPosition;

    private void Awake()
    {
        SetUpComponents();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetUpVariables();
    }

    private void SetUpComponents()
    {
        _animator = GetComponentInChildren<Animator>();
        _enemyStatus = GetComponent<EnemyStatus>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void SetUpVariables()
    {
        _playerPosition = GameManager.Instance.PlayerPosition;
        _waitTakeDamage = new WaitForSeconds(0.1f);
    }

    private void AttackPlayer()
    {
        if (!CanAttack) return;

        if (Vector3.Distance(transform.position, _playerPosition.position) < _attackDistance)
        {
            _animator.SetTrigger("attack");
            CanAttack = false;
        }
    }

    public void Shoot()
    {
        Shoot _shoot = Instantiate(_shootGO, _shootExit).GetComponent<Shoot>();
        _shoot.Direction = (_playerPosition.position - _shootExit.position).normalized;
        _shoot.Direction.y = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Sword>())
        {
            if (!_canTakeDamage) return;

            _canTakeDamage = false;
            _enemyStatus.ReduceLife(1);

            if (_enemyStatus.Lives <= 0) Destroy(this.gameObject);
            else StartCoroutine(TakeDamageCr());
        }
    }

    IEnumerator TakeDamageCr()
    {
        for (int i = 0; i < 5; i++)
        {
            _sprite.enabled = !_sprite.enabled;
            yield return _waitTakeDamage;
        }
        _sprite.enabled = true;
        _canTakeDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        AttackPlayer();
    }
}
