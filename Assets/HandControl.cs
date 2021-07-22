using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;

public class HandControl :  NetworkBehaviour
{
    public GameObject handObject;
    private Animation m_handObjectAnim;

    // Start is called before the first frame update
    void Start()
    {
        m_handObjectAnim = handObject.GetComponent<Animation>();
    }
    
    [ClientRpc]
    void SlapClientRpc(int value)
    {
        if (IsClient && !IsLocalPlayer)
        {
            m_handObjectAnim.Play();
        }
    }

    [ServerRpc]
    void SlapServerRpc()
    {
        if (IsServer)
        {
           SlapClientRpc(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsLocalPlayer)
        {
            m_handObjectAnim.Play();
            SlapServerRpc();
        }
    }
}
