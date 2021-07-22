using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    private Animation anim;
    private Collider col;

    private void Start()
    {
        anim = GetComponent<Animation>();
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        col.enabled = anim.isPlaying;
    }

    void OnTriggerEnter(Collider other)
    {
        var otherObject = other.gameObject;
        if (otherObject.CompareTag("Hittable") )
        {
            Debug.Log("Auch");
        }
    }
}
