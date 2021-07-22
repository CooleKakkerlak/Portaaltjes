using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalConnector : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Portal> portals = new List<Portal>();

    private void Start()
    {       
        //InvokeRepeating("ConnectPortals", 0.1f, 5);
    }

    public void ConnectPortal(Portal p)
    {
        portals.Add(p);
        Debug.Log(portals.Count);
    }

    public void ConnectPortals()
    {
        int i = portals.Count;
        Portal[] temp = portals.ToArray();
        Debug.Log("connecting " + i + " portals");

        while(i > 0)
        {
            int r1 = Random.Range(0, i);
            Portal p1 = temp[r1];
            temp[r1] = temp[i-1];
            i--;

            int r2 = Random.Range(0, i);
            Portal p2 = temp[r2];
            temp[r2] = temp[i-1];
            i--;

            Debug.Log("connecting " + p1.gameObject.name + " to " +  p2.gameObject.name);
            p1.ConnectToPortal(p2);
        }
    }
}
