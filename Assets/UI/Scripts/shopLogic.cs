using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class shopLogic : MonoBehaviour
{
    public VisualElement root;
    public UIDocument mainMenu;
    public UIDocument skinShop;

    private ProgressBar flag;
    private ProgressBar random;
    private ProgressBar reduce;
    private ProgressBar cristal;

    private Label coins;
    private Label cristals;

    private void Start()
    {
        // get root element of UI
        root = GetComponent<UIDocument>().rootVisualElement;

        // get coins and cristals elements of UI and setting count
        coins = root.Q<Label>("coins");
        coins.text = GameSaveLoad.getCoins().ToString();
        cristals = root.Q<Label>("cristals");
        cristals.text = GameSaveLoad.getCristals().ToString();

        // set events for back and skins buttons
        root.Q<Button>("back").clicked += () => back();
        root.Q<Button>("skins").clicked += () => skins();

        // get progress bars of UI
        flag = root.Q<ProgressBar>("flagProgress");
        random = root.Q<ProgressBar>("randomProgress");
        reduce = root.Q<ProgressBar>("reduceProgress");
        cristal = root.Q<ProgressBar>("cristalProgress");

        // set progress of progress bars
        flag.lowValue = PlayerPrefs.GetInt("flag");
        random.lowValue = PlayerPrefs.GetInt("random");
        reduce.lowValue = PlayerPrefs.GetInt("reduce");
        cristal.lowValue = PlayerPrefs.GetInt("cristal");

        // set events for adding buttons
        root.Q<Button>("flag").clicked += () => add("flag", flag);
        root.Q<Button>("random").clicked += () => add("random", random);
        root.Q<Button>("reduce").clicked += () => add("reduce", reduce);
        root.Q<Button>("cristal").clicked += () => add("cristal", cristal);
        // hide UI
        root.visible = false;
    }

    // function for adding progress for ability
    void add(string to, ProgressBar bar)
    {
        // check if player has enough coins
        if (GameSaveLoad.getCoins() > 49)
        {
            // add 1 to existing ability progress count and set progress of the progress bar
            int value = PlayerPrefs.GetInt(to) + 1;
            GameSaveLoad.setAddition(to, value);
            bar.lowValue = value;
            // subtract 50 of current coins count and saving
            int coin = GameSaveLoad.getCoins() - 50;
            GameSaveLoad.setCoins(coin);
            // set coins count on UI element
            coins.text = coin.ToString();
            // if bar is full
            if (value >= 10)
            {
                // add 1 to ability count
                int val = PlayerPrefs.GetInt(to + "Add") + 1;
                // if ability is cristal change text of cristals UI element
                if (to == "cristal")
                {
                    cristals.text = val.ToString();
                }
                // set ability count and save
                GameSaveLoad.setAddition(to + "Add", val);
                GameSaveLoad.setAddition(to, 0);
                // reset bar progress
                bar.lowValue = 0;
            }
        }
    }

    // event function to get back to main menu
    void back()
    {
        mainMenu.rootVisualElement.visible = true;
        root.visible = false;
    }

    // event function to get to skins page
    void skins()
    {
        skinShop.rootVisualElement.visible = true;
        root.visible = false;
    }
}
