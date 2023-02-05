using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneAnimEvents : MonoBehaviour
{
    GameManager _gameManager;
    PlayerCombat _playerCombat;
    EnemyOne _enemyOne;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _playerCombat = _gameManager.PlayerPosition.GetComponent<PlayerCombat>();
        _enemyOne = GetComponentInParent<EnemyOne>();
    }
    private void AttackEvent()
    {
        if (Vector3.Distance(transform.position, _gameManager.PlayerPosition.position) < 4.5f)
        {
            _playerCombat.TakeDamage(_enemyOne.MyDamage);
        }
    }

    private void StartAttack()
    {
        SoundManager.Instance.PlaySFX(SoundManager.AudioClipID.ENEMY_1_ATTACK);
        _enemyOne.Attacking = true;
    }

    private void FinishAttack()
    {
        _enemyOne.Attacking = false;
    }
}
