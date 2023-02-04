using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwoAnimEvents : MonoBehaviour
{
    private EnemyTwo _enemyTwo;
    private WaitForSeconds _canShootAgain;
    [SerializeField] float _shootAgainTimer;

    private void Awake()
    {
        SetUpComponents();
    }

    private void SetUpComponents()
    {
        _enemyTwo = GetComponentInParent<EnemyTwo>();
        _canShootAgain = new WaitForSeconds(_shootAgainTimer);
    }

    private void CanShootAgain()
    {
        StartCoroutine(CanShootAgainCr());
    }

    IEnumerator CanShootAgainCr()
    {
        yield return _canShootAgain;
        _enemyTwo.CanAttack = true;
    }

    private void ShootEvent()
    {
        _enemyTwo.Shoot();
    }
}
