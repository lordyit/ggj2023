using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public static List<EnemyOne> EnemyOne;
    public static List<EnemyTwo> EnemyTwo;
    public static List<NormalTree> NormalTree;

    [SerializeField] Portal _portal;

    [SerializeField] SpriteRenderer corruptedGround = null;
    [SerializeField] SpriteRenderer cleanGround = null;

    private bool runGroundAnimation;
    private float groundAnimationEndTime;
    const float GROUND_ANIMATION_DURATION = 1.0f;

    private void Awake()
    {
        Instance = this;
        EnemyOne = new List<EnemyOne>();
        EnemyTwo = new List<EnemyTwo>();
        NormalTree = new List<NormalTree>();
    }

    public void CheckEndLevel()
    {
        if (NormalTree.Count > 0) return;
        
        // NOTE: commented out obligation to kill all enemies so the design of only killing the tree could be tested
        //if (EnemyOne.Count > 0) return;
        //if (EnemyTwo.Count > 0) return;

        for (int e = 0; e < EnemyOne.Count; e++)
        {
            EnemyOne[e].Die();
        }
        for (int e = 0; e < EnemyTwo.Count; e++)
        {
            EnemyTwo[e].Die();
        }

        _portal.OpenGate();

        // Start ground healing animation
        {
            runGroundAnimation = true;
            groundAnimationEndTime = Time.time + GROUND_ANIMATION_DURATION;
        }
    }

    public void ActiveReloadLevel()
    {
        StartCoroutine(ReloadLevel());
    }
    IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        if (runGroundAnimation)
        {
            Color auxColor = Color.white;

            auxColor.a = Mathf.Clamp01((groundAnimationEndTime - Time.time)/GROUND_ANIMATION_DURATION);
            corruptedGround.color = auxColor;

            auxColor.a = 1.0f - Mathf.Clamp01((groundAnimationEndTime - Time.time) / GROUND_ANIMATION_DURATION);
            cleanGround.color = auxColor;

            if (Time.time > groundAnimationEndTime)
            {
                runGroundAnimation = false;
            }
        }
    }
}
