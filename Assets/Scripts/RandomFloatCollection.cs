/*
 * A persistent type of asset, not living in any scene, is needed for a system.
 * It needs to be created inside the editor in the right-click context menu ("Create/Random/FloatCollection").
 * Item's inspector should show an array of floats, just like a regular behaviour's inspector.
 * There should be a button in the inspector that says "Generate" and it should populate the array shown, with random values between 0 and 1.
 * The generated values should persist between editor sessions, scene loads, etc. So make sure of that.
 * 
 * Code from UnityEditor namespace is not allowed in the build, but RandomFloatCollection class should make it to the build. 
 * Use preprocessor definitions to handle that. What would you do if for some reason you weren't allowed to use preprocessor definitions?
 * 
 * Continue implementing the class however you wish.
 * 
 */
//public class RandomFloatCollection...

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;


// I did not fully understand this part, so I could not give exactly what was asked.
public class RandomFloatCollection : OdinEditor
{
    [ShowInInspector, ReadOnly]
    private float[] _array;
    [ContextMenu("Create/Random/FloatCollection")]
    private static void DoSomething()
    {
        Debug.Log("Random numbers generated");
    }
    [Button]
    private void Generate()
    {
        //Win condition for next level ClearSpecificArea so objects stays.
        for (var i = 0; i < 10; i++)
        {
            _array[i] = Random.Range(0, 1);
        }
    }
}