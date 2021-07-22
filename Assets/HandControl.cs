using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControl : MonoBehaviour
{
    public GameObject handObject;
    private Animation m_handObjectAnim;
    
    // Start is called before the first frame update
    void Start()
    {
        m_handObjectAnim = handObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_handObjectAnim.Play();
        }
    }
}
