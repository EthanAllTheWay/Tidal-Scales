using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float exitDuration;
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
            // TODO: Change this. Currently used to pause the Coroutine from moving the object.
            while (GameUIController.gamePaused)
            {
                yield return null;
            }
            transform.position = Vector3.Lerp(startPoint, endPoint, interpolationVariable / Conductor.instance.prespawnBeats);
            interpolationVariable = Conductor.instance.prespawnBeats - (beatOfThisNote - Conductor.instance.songPositionInBeats);
            yield return null;
        }
        transform.position = Vector3.Lerp(startPoint, endPoint, 1);
        StartCoroutine(Escape());
    }
    // This continues with the fish move.
    IEnumerator Escape()
    {
        startPoint = transform.position;
        endPoint += new Vector3(0, 0, 5);
        float timeElapsed = 0;
        while (timeElapsed < exitDuration)
        {
            transform.position = Vector3.Lerp(startPoint, endPoint, timeElapsed / exitDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = Vector3.Lerp(startPoint, endPoint, 1);
        Destroy(gameObject);
    }
}
