using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;

    void Start()
    {
        AddNonTriggerBoxCollider();
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);


        //var parentscale = gameObject.transform.localScale;
        //var newobject = Instantiate(deathFX, transform.position, Quaternion.identity);
        //parentscale = new Vector3(parentscale.x * 0.1f, parentscale.y * 0.1f, parentscale.z * 0.1f);
        //newobject.transform.localScale = parentscale;
        //Destroy(gameObject);
    }
}
