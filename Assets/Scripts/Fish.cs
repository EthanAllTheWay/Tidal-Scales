using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    Vector3 startPoint;
    Vector3 endPoint;
    float beatOfThisNote;

    //This sets necesary values.
    public void InitializeValues(Vector3 initialPosition, Vector3 endPosition, float beat)
    {
        startPoint = initialPosition;
        endPoint = endPosition;
        beatOfThisNote = beat;
        StartCoroutine(Move());
    }

    //This moves the object using interpolation.
    IEnumerator Move()
    {
        float interpolationVariable = Conductor.instance.prespawnBeats - (beatOfThisNote - Conductor.instance.songPositionInBeats);
        while (interpolationVariable < Conductor.instance.prespawnBeats)
        {
            transform.position = Vector3.Lerp(startPoint, endPoint, interpolationVariable / Conductor.instance.prespawnBeats);
            interpolationVariable = Conductor.instance.prespawnBeats - (beatOfThisNote - Conductor.instance.songPositionInBeats);
            yield return null;
        }
    }
}
