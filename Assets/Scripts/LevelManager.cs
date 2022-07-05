using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<LevelObject> m_levelObjects;
    [SerializeField] private Transform m_parentObject;
    private LevelObject _levelObjectSO;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Start()
    {
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        //Endless loop for game level system with PlayerPrefs and level.
        var levelIndex = GetLevelIndex();
        _levelObjectSO = levelIndex % m_levelObjects.Count == 0 ? m_levelObjects[0] : m_levelObjects.Find(x => x.level == levelIndex % m_levelObjects.Count);
        foreach (var obj in _levelObjectSO.levelPrefab)
        {
            var levelObject = Instantiate(obj,m_parentObject);
            levelObject.transform.localPosition = Random.insideUnitCircle * 10;
        }

        _camera.backgroundColor = _levelObjectSO.backgroundColor;

    }

    [Button]
    private void NextLevel()
    {
        if (_levelObjectSO.Condition == LevelObject.WinCondition.ClearAllArea)
        {
            foreach (Transform t in m_parentObject)
            {
                Destroy(t.gameObject);
                // Win condition for next level ClearAllArea so objects destroyed.
            }
        }
        
        if (_levelObjectSO.Condition == LevelObject.WinCondition.ClearSpecificArea)
        {
          // Do something...
        }
        //Win condition for next level ClearSpecificArea so objects stays.
        
        SetLevelIndex(GetLevelIndex()+1);
        InitializeLevel();
    }

    //set PlayerPrefs to level where they left.
    private int GetLevelIndex()
    {
        return PlayerPrefs.GetInt("LevelIndex", 0);
    }
    private void SetLevelIndex(int index)
    {
         PlayerPrefs.SetInt("LevelIndex", index);
    }
 
}
