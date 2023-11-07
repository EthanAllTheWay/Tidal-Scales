using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private Vector3 startPoint;
    private Vector3 endPoint;
    private float beatOfThisNote;

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
