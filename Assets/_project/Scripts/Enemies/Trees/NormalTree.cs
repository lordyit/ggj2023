using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTree : MonoBehaviour
{
    public List<TreeRoots> _myRoots;
    [SerializeField] private GameObject _enemyOne;
    [SerializeField] private GameObject _root;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private float _lives;
    [SerializeField] private Transform[] _summonRootSpawn;

    private SpriteRenderer _sprite;
    private WaitForSeconds _waitTakeDamage;

    private float timer = 0;
    private bool _canTakeDamage = true;
    private void Awake()
    {
        SetUpComponents();
        _waitTakeDamage = new WaitForSeconds(0.1f);
    }
    private void SetUpComponents()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }
    
    private void SummonMinion()
    {
        if (_myRoots.Count == 0) return;

        int randomRoot = Random.Range(0, _myRoots.Count);
        int randomSpawn = Random.Range(0, _myRoots[randomRoot].SpawnSpots.Length);

        GameObject enemy = Instantiate(_enemyOne, _myRoots[randomRoot].SpawnSpots[randomSpawn]);
    }

    private void TimerToSpawn()
    {
        timer += Time.deltaTime;
        if (timer < timeToSpawn) return;

        if (_myRoots.Count != 0) SummonMinion();
        else
        {
            int randomSeed = Random.Range(0, _summonRootSpawn.Length);
            TreeRoots root = Instantiate(_root, _summonRootSpawn[randomSeed]).GetComponent<TreeRoots>();
            root.gameObject.transform.SetParent(null);
            AddRoot(root);
        }
        timer = 0;
    }

    public void AddRoot(TreeRoots root)
    {
        _myRoots.Add(root);
    }

    public void RemoveRoot(TreeRoots root)
    {
        for (int i = 0; i < _myRoots.Count; i++)
        {
            if (_myRoots[i] == root)
            {
                _myRoots.RemoveAt(i);
            }
        }
    }

    private void TakeDamage(int damage)
    {
        if (_myRoots.Count > 0) return;
        if (!_canTakeDamage) return;

        _lives -= damage;
        if (_lives <= 0)
        {
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

    private void Update()
    {
        TimerToSpawn();
    }
}
