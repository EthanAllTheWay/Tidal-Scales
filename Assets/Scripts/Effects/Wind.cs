using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private ParticleSystem windPS;

    // Start is called before the first frame update
    void Start()
    {
        windPS = GetComponent<ParticleSystem>();
        StartCoroutine(TriggerWindEffect());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator TriggerWindEffect()
    {
        if (windPS.isPlaying)
        {
            yield return new WaitUntil(() => !windPS.isPlaying);
        }
        Destroy(this.gameObject);
    }
}
