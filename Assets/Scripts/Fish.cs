using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float exitDuration;
    private Vector3 startPoint;
    private Vector3 endPoint;
    public float beatOfThisNote;
    public TextMeshProUGUI textMeshPro;

    //This sets necesary values.
    public void InitializeValues(Vector3 initialPosition, Vector3 endPosition, float beat, int n)
    {
        startPoint = initialPosition;
        endPoint = endPosition;
        beatOfThisNote = beat;
        textMeshPro.text = n.ToString();
        StartCoroutine(Move());
    }

    //This moves the object using interpolation.
    IEnumerator Move()
    {
        float interpolationVariable = Conductor.instance.prespawnBeats - (beatOfThisNote - Conductor.instance.songPositionInBeats);
        while (interpolationVariable < Conductor.instance.prespawnBeats)
        {
            // TODO: I don't like this solution to pause the game since this is definitely a code smell and
            // will lead to boiler plate code for all Coroutines that need to be paused as they would need
            // these 3 lines of code.
            if (GameUIController.gamePaused)
            {
                yield return new WaitUntil(() => !GameUIController.gamePaused);
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
            // TODO: See TODO in Move() IEnumerator method.
            if (GameUIController.gamePaused)
            {
                yield return new WaitUntil(() => !GameUIController.gamePaused);
            }
            transform.position = Vector3.Lerp(startPoint, endPoint, timeElapsed / exitDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = Vector3.Lerp(startPoint, endPoint, 1);
        Destroy(gameObject);
    }
}
