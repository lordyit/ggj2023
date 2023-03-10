using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRoots : MonoBehaviour
{
    public Transform[] SpawnSpots;
    public NormalTree _myTree;

    [SerializeField] private int lives;

    private SpriteRenderer _sprite;
    private WaitForSeconds _waitTakeDamage;
    private bool _canTakeDamage = true;

    private void Awake()
    {
        SetUpComponents();
    }

    private void Start()
    {
        _waitTakeDamage = new WaitForSeconds(0.1f);
    }

    private void SetUpComponents()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void TakeDamage(int damage)
    {
        if (!_canTakeDamage) return;

        FeelManager.Instance.ShakeCamera(5, 0.1f);
        FeelManager.Instance.HitVfxActive(transform, FeelManager.Instance.HitVfx);
        lives -= damage;
        if (lives <= 0)
        {
            _myTree.RemoveRoot(this);
            Destroy(this.gameObject);
        }
        else
        {
            StartCoroutine(TakeDamageCr());
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Sword>())
        {
            TakeDamage(1);
        }
    }
}
