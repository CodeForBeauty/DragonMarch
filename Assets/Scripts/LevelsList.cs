using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsList : MonoBehaviour
{
    // Contain all levels list
    public GameObject[] levelList;
    public MainGameLogic mainGameLogic;

    private void Start()
    {
        // get dificulty data fron LeveDificulty
        LevelScroll levelS = GetComponent<LevelScroll>();
        GameObject level = Instantiate(levelList[GameSaveLoad.currentLevel-1], transform);
        levelS.humansMultiplyer = level.GetComponent<LevelDificulty>().humansMultiplyer;
        levelS.starDivider = level.GetComponent<LevelDificulty>().starDivider;
        levelS.speed = level.GetComponent<LevelDificulty>().speed;
        mainGameLogic.MaxHumans = level.GetComponent <LevelDificulty>().maxHumans;
    }
}
