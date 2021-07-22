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

    public NetworkVariableQuaternion Rotation = new NetworkVariableQuaternion(new NetworkVariableSettings
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
    void UpdServerRpc(Vector3 pos, Quaternion rot)
    {
        if (IsServer)
        {
            Position.Value = pos;
            Rotation.Value = rot;
        }
    }

    void Start()
    {
        if (!IsLocalPlayer)
        {
            GetComponent<FPSController>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
            GetComponent<HandControl>().enabled = false;
            Destroy(transform.Find("cam").gameObject);
            // Old character
            //GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            //Destroy(transform.Find("FirstPersonCharacter").gameObject);
        }
    }

    void Update()
    {
        if (!IsLocalPlayer)
        {
            transform.position = Position.Value;
            transform.rotation = Rotation.Value;
        }
        else
        {
            UpdServerRpc(transform.position, transform.rotation);
        }
    }
}