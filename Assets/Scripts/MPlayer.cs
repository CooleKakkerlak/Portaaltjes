using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

public class MPlayer : NetworkBehaviour
{
    public NetworkVariableVector3 Position = new NetworkVariableVector3(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.ServerOnly,
        ReadPermission = NetworkVariablePermission.Everyone
    });

    [ClientRpc]
    void TestClientRpc()
    {
        if (IsClient && !IsLocalPlayer)
        {
            transform.position = Position.Value;
        }
    }

    [ServerRpc]
    void TestServerRpc(Vector3 value)
    {
        if (IsServer)
        {
            Position.Value = value;
        }
    }

    void Start()
    {
        if (!IsLocalPlayer)
        {
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            GetComponent<FPSController>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
            Destroy(transform.Find("FirstPersonCharacter").gameObject);
            Destroy(transform.Find("cam").gameObject);
        }
    }

    void Update()
    {
        if (!IsLocalPlayer)
        {
            transform.position = Position.Value;
        }
        else
        {
            TestServerRpc(transform.position);
        }
    }
}