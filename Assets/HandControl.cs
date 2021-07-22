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
    void SlapClientRpc(int value)
    {
        Debug.Log("zzzzzaazzz");
        if (IsClient && !IsLocalPlayer)
        {
            Debug.Log("zzzzzz");
           // m_handObjectAnim.Play();
            Debug.Log("aaaaaa");
        }
    }

    [ServerRpc]
    void SlapServerRpc()
    {
        if (IsServer)
        {
            Debug.Log("aaaabbb");
           SlapClientRpc(1);
            Debug.Log("AAAAA");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsLocalPlayer)
        {
            m_handObjectAnim.Play();
            Debug.Log("bbbbbb");
            SlapServerRpc();
            Debug.Log("cccccc");
        }
    }
}
