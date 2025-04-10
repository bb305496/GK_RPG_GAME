using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using TMPro;

public class ChasingWarningUI : MonoBehaviour
{
    //public GameObject warningUI; // np. Image z Canvas
    private EnemyMovement[] enemies;
    private float warningTimer = 0f;
    public float warningDuration = 2f;
    public GameObject hpGlowAnimator; // lub Image / CanvasGroup jeœli wolisz inny typ
    public bool isChased = false;



    void Start()
    {
        enemies = UnityEngine.Object.FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None);

        UnityEngine.Debug.Log($"Znaleziono przeciwników: {enemies.Length}");
        //warningUI.SetActive(false);
    }


    void Update()
    {
        enemies = UnityEngine.Object.FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None);


        foreach (EnemyMovement enemy in enemies)
        {
            if (enemy.CurrentState == EnemyState.Chasing || enemy.CurrentState == EnemyState.Attacking)
            {
                isChased = true;
                break;
            }
            else
                isChased = false;
        }

        if (isChased)
        {
            warningTimer = warningDuration; // resetuj licznik
        }
        else
        {
            warningTimer -= Time.deltaTime;
        }

        if (warningTimer > 0)
        {
            if (!hpGlowAnimator.gameObject.activeSelf)
                hpGlowAnimator.gameObject.SetActive(true);
        }
        else
        {
            hpGlowAnimator.gameObject.SetActive(false);
        }

    }

    public bool IsBeingChased()
    {
        return warningTimer > 0;
    }


}
