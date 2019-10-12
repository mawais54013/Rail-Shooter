using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 12;
    [SerializeField] int healthPoints = 10;

    ScoreBoard scoreBoard;

    void Start()
    {
        AddNonTriggerBoxCollider();
    }

    private void AddNonTriggerBoxCollider()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        scoreBoard.ScoreHit(scorePerHit);
        healthPoints = healthPoints - 1;
        if(healthPoints <= 0)
        {
            KillEnemy();
        }


        //var parentscale = gameObject.transform.localScale;
        //var newobject = Instantiate(deathFX, transform.position, Quaternion.identity);
        //parentscale = new Vector3(parentscale.x * 0.1f, parentscale.y * 0.1f, parentscale.z * 0.1f);
        //newobject.transform.localScale = parentscale;
        //Destroy(gameObject);
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }
}
