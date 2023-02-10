using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveLoad : MonoBehaviour
{
    /*
     * Level data save and load class
     */
    public const int lastLevel = 30;
    public static int currentLevel = 1;
    
    // save level stats such as is player won the level and stars player get
    public static void saveLevelStats(int level, int stars, string isWin)
    {
        PlayerPrefs.SetString("isWon"+level.ToString(), isWin);
        PlayerPrefs.SetInt(level.ToString(), stars);
        PlayerPrefs.Save();
    }

    // set coins count
    public static void setCoins(int value)
    {
        PlayerPrefs.SetInt("coins", value);
        PlayerPrefs.Save();
    }
    
    // set cristals count
    public static void setCristals(int value)
    {
        PlayerPrefs.SetInt("cristalAdd", value);
        PlayerPrefs.Save();
    }

    // add cristals to current cristals
    public static int addCristals(int count)
    {
        int val = getCristals() + count;
        setCristals(val);
        return val;
    }

    // set additional ability count by name(flag, random, reduce, cristal)
    public static void setAddition(string addition, int value)
    {
        PlayerPrefs.SetInt(addition, value);
        PlayerPrefs.Save();
    }

    // load saved level stats
    public static int loadLevelStats(int level)
    {
        return PlayerPrefs.GetInt(level.ToString());
    }

    // load is player won level by level number
    public static string loadIsLevelWon(int level)
    {
        return PlayerPrefs.GetString("isWon"+level.ToString());
    }

    // get coins count
    public static int getCoins()
    {
        return PlayerPrefs.GetInt("coins");
    }
    
    // get cristals count
    public static int getCristals()
    {
        return PlayerPrefs.GetInt("cristalAdd");
    }
}
