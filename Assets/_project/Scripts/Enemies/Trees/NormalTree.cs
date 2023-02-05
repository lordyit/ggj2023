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

    [Header("Spawner")]
    [SerializeField] private bool _spawnerTree;

    private SpriteRenderer _sprite;
    private WaitForSeconds _waitTakeDamage;

    private float timer = 0;
    private bool _canTakeDamage = true;
    private void Awake()
    {
        SetUpComponents();
        _waitTakeDamage = new WaitForSeconds(0.1f);
    }
    private void OnEnable()
    {
        StartCoroutine(AddtoList());
    }
    IEnumerator AddtoList()
    {
        yield return new WaitForSeconds(0.1f);
        LevelManager.NormalTree.Add(this);
    }
    private void OnDisable()
    {
        LevelManager.NormalTree.Remove(this);
        LevelManager.Instance.CheckEndLevel();
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

        GameObject enemy = Instantiate(_enemyOne, _myRoots[randomRoot].SpawnSpots[randomSpawn].position, Quaternion.identity);
        enemy.gameObject.transform.SetParent(null);
        FeelManager.Instance.HitVfxActive(enemy.transform, FeelManager.Instance.burstVfx);
    }

    private void TimerToSpawn()
    {
        if (!_spawnerTree) return;

        timer += Time.deltaTime;
        if (timer < timeToSpawn) return;

        if (_myRoots.Count != 0) SummonMinion();
        else
        {
            int randomSeed = Random.Range(0, _summonRootSpawn.Length);
            TreeRoots root = Instantiate(_root, _summonRootSpawn[randomSeed].position,
                _summonRootSpawn[randomSeed].rotation).GetComponent<TreeRoots>();
            AddRoot(root);
            root._myTree = this;
            FeelManager.Instance.HitVfxActive(root.gameObject.transform, FeelManager.Instance.burstVfx);
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
        if (!CanTakeDamage()) return;
        

        FeelManager.Instance.ShakeCamera(5, 0.1f);
        FeelManager.Instance.HitVfxActive(transform, FeelManager.Instance.HitVfx);
        EnemySound.PlayTakeDamageSFX();

        _lives -= damage;
        if (_lives <= 0)
        {
            Destroy(this.gameObject);
            SoundManager.Instance.PlaySFX(SoundManager.AudioClipID.TREE_DEATH);
        }
        else
        {
            StartCoroutine(TakeDamageCr());
        }
    }

    private bool CanTakeDamage()
    {
        // NOTE: Tree protection is deactivated to test design of hitting it while there are enemies left
        //if (LevelManager.EnemyOne.Count == 0 &&
        //    LevelManager.EnemyTwo.Count == 0) return true;

        //return false;

        return true;
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
