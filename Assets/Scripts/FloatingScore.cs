using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScore : MonoBehaviour
{
    public float screenTime = 1f;
    public Vector3 placement = new Vector3(0, 2, 0); 
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, screenTime);
        transform.localPosition += placement;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
