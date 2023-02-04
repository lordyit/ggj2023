using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public int Lives = 3;

    public void ReduceLife(int damage)
    {
        Lives -= damage;
    }
}
