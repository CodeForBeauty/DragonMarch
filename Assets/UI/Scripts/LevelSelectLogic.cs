using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelSelectLogic : MonoBehaviour
{
    // level selection menu's logic
    public UIDocument mainMenu;
    public string playScene;
    public VisualElement root;
    public Texture2D starDisabled;

    // store labels and button
    private Button btn1;
    private Button btn2;
    private Button btn3;
    private Button tmpBtn;
    private int tmpStars;
    private VisualElement starsBox;
    private bool firstNotPlayed = true;

    private void Start()
    {
        // get UIDocument component
        root = GetComponent<UIDocument>().rootVisualElement;
        // get thought all levels
        for (int i = 1; i <= GameSaveLoad.lastLevel; i++)
        {
            var index = i;
            root.Q<Button>(i.ToString()).clicked += () => Play(index);
            tmpBtn = root.Q<Button>(i.ToString());
            starsBox = tmpBtn.Q<VisualElement>("StarsBox");
            // if level not yet played lock it
            if (!firstNotPlayed)
            {
                tmpBtn.SetEnabled(false);
            }
            // if level played load level stats data
            if (PlayerPrefs.HasKey(i.ToString()))
            {
                tmpStars = GameSaveLoad.loadLevelStats(i);
                if (tmpStars > 2)
                {
                    continue;
                }
                else if (tmpStars > 1)
                {
                    starsBox.Q<VisualElement>("3").style.backgroundImage = starDisabled;
                }
                else if (tmpStars > 0)
                {
                    starsBox.Q<VisualElement>("2").style.backgroundImage = starDisabled;
                    starsBox.Q<VisualElement>("3").style.backgroundImage = starDisabled;
                }
                else
                {
                    starsBox.Q<VisualElement>("1").style.backgroundImage = starDisabled;
                    starsBox.Q<VisualElement>("2").style.backgroundImage = starDisabled;
                    starsBox.Q<VisualElement>("3").style.backgroundImage = starDisabled;
                }
                if (GameSaveLoad.loadIsLevelWon(i) == "false")
                {
                    firstNotPlayed = false;
                }
            }
            else
            {
                starsBox.visible = false;
                firstNotPlayed = false;
            }
        }
    }

    private void Play(int level)
    {
        print(level);
        // load level by level number
        GameSaveLoad.currentLevel = level;
        SceneManager.LoadScene(playScene);
    }
}
