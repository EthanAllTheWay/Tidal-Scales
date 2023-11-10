using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class ConductorIT
{
    // A Test behaves as an ordinary method
    [Test]
    public void AudioSettingsITSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator AudioSettingsITWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;

    }

    [UnityTest]
    public IEnumerator UpdateOffsetTest()

    {
        /*
         * I am not dealing with assembly definitions right now. I am getting an error when I create a new assembly definition 
         * in the scripts class. Here is the error:
         * Assets\Scripts\Indicator.cs(21,5): error CS0246: The type or namespace name 'InputAction' could 
         * not be found (are you missing a using directive or an assembly reference?)
         */
        //Conductor.instance;

        // The current dspTime.
        float dspTime = (float)AudioSettings.dspTime; // dspTime = 0

        // The dspTime when the song started.
        float startDspTime = dspTime; 
        float songPosition = dspTime;
        float offset = 0;

        Debug.Log("Playing Game...");
        // Song plays for 3 seconds.
        yield return new WaitForSeconds(3);

        // Update variables.
        dspTime = (float)AudioSettings.dspTime; // dspTime = 3
        songPosition = dspTime - offset; // songposition = 3
        Debug.Log("dspTime: " + dspTime + "  songPosition: " + songPosition);
        //Assert.AreEqual(dspTime, songPosition);

        Debug.Log("Game paused...");
        // Pause Game for 3 seconds.
        yield return new WaitForSeconds(3);

        // Unpause game. Update variables.
        dspTime = (float)AudioSettings.dspTime; // dspTime = 6
        offset = dspTime - songPosition; // offset = 3
        songPosition = dspTime - offset; // songPosition = 3
        Debug.Log("dspTime: " + dspTime + "  songPosition: " + songPosition);

        Debug.Log("Playing Game...");
        // Play game for 10 seconds.
        yield return new WaitForSeconds(10);

        // Unpause game. Update variables.
        dspTime = (float)AudioSettings.dspTime; // dspTime = 16
        songPosition = dspTime - offset; // songPosition = 13
        Debug.Log("dspTime: " + dspTime + "  songPosition: " + songPosition);
        yield return null;
    }

    /// <summary>
    /// This is a big no to try to assign a parameter in a method. The offset variable will not 
    /// be updated outside this method. Learned this the hard way.
    /// https://stackoverflow.com/a/555481 
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="dspTime"></param>
    /// <param name="songPosition"></param>
    public void UpdateOffset(float offset, float dspTime, float songPosition)
    {
        offset = dspTime - songPosition; 
    }
}
