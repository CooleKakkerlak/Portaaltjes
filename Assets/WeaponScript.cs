using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject hand;
    private Animation anim;
    private Collider col;
    public MPlayer m_player;

    private void Start()
    {
        anim = hand.GetComponent<Animation>();
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
            var nid = otherObject.GetComponentInParent<MPlayer>().NetworkObjectId;
            Debug.Log($"NID: {nid}");
            m_player.OnSlap(nid);
        }
    }
}
