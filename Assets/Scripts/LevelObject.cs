/*
 * Implement a scriptable object that can be created editor right-click context menu. The object should have the following properties:
 * Level number
 * Camera background color
 * Level prefab (You can use cubes, spheres, etc. placed randomly)
 * Win condition (Use an enum, "Clear all area" or "Clear specific area")
 * Write a level manager that loads the created level objects and initiates the level
 * Make the system testable in the scene LevelTest
 * */

using UnityEngine;
//Create new Level.
[CreateAssetMenu(fileName = "levelObject", menuName = "ScriptableObjects/LevelConfig")]
public class LevelObject : ScriptableObject
{
    // Scriptable Level objects with properties.

    public int level= 1;
    public Color backgroundColor;
    public GameObject[] levelPrefab;
    public WinCondition Condition;
    public enum WinCondition
    {
        ClearAllArea,
        ClearSpecificArea
    }
    // Go to LevelManager for the rest.
}