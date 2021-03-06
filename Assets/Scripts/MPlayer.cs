using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

public class MPlayer : PortalTraveller
{
    public GameObject Hand;
    public bool IsTikker = true;

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
            transform.Find("Soldier").tag = "Hittable";
            // Old character
            //GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            //Destroy(transform.Find("FirstPersonCharacter").gameObject);
        }
    }

    void Update()
    {
        Hand.SetActive(IsTikker);
        if (!IsLocalPlayer)
        {
            transform.position = Vector3.Lerp(Position.Value, transform.position, 0.8f);
            transform.rotation = Quaternion.Lerp(Rotation.Value, transform.rotation, 0.9f);
        }
        else
        {
            UpdServerRpc(transform.position, transform.rotation);
        }
    }

    public void OnSlap(ulong networkId)
    {
        if (!IsLocalPlayer)
            return;
        
        OnSlapServerRpc(networkId);
    }
    
    [ClientRpc]
    void OnSlapClientRpc(ulong nid)
    {
        Debug.Log($"(Client) Slapped NID: {nid}, Self: {NetworkObjectId}");
        IsTikker = NetworkObjectId == nid;
    }

    [ServerRpc]
    void OnSlapServerRpc(ulong nid)
    {
        Debug.Log($"NID: {nid}");
        if (IsServer)
        {
            IsTikker = NetworkObjectId == nid;
            Debug.Log($"(Server) Slapped NID: {nid}, Self: {NetworkObjectId}");
            OnSlapClientRpc(nid);

            foreach(var p in (FindObjectsOfType(typeof(MPlayer)) as MPlayer[]))
            {
                p.OnSlapClientRpc(nid);
            }
        }
    }
}