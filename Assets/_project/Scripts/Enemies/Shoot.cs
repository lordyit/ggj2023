using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [HideInInspector] public Vector3 Direction;
    [SerializeField] private float _speed;

    private void Start()
    {
        Destroy(this.gameObject, 10);
    }

    private void Behaviour()
    {
        transform.Translate(Direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerCombat>(out PlayerCombat playerCombat))
        {
            playerCombat.TakeDamage(1);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Behaviour();
    }
}
