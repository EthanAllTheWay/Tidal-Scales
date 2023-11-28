using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScore : MonoBehaviour
{
    public float screenTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, screenTime);
    }
}
