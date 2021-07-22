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
    public bool isPlayer = false;
    private Animation m_handObjectAnim;

    // Start is called before the first frame update
    void Start()
    {
        m_handObjectAnim = handObject.GetComponent<Animation>();
    }
    
    [ClientRpc]
    void SlapClientRpc()
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
            SlapClientRpc();
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
