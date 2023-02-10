using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuLogic : MonoBehaviour
{
    // Main menu logic
    public LevelSelectLogic levelSelect;
    public VolumeSettings volumeSettings;
    public shopLogic shopElem;

    // store labels and button
    private Button play;
    private Button shop;
    private Button settings;

    public VisualElement root;

    private void Start()
    {
        // get UIDocument component
        root = GetComponent<UIDocument>().rootVisualElement;
        // geet labels and button and assign them to variables
        play = root.Q<Button>("Play");
        shop = root.Q<Button>("Shop");
        settings = root.Q<Button>("Settings");

        play.clicked += () => Play();
        shop.clicked += () => Shop();
        settings.clicked += () => Settings();

        levelSelect.root.visible = false;
    }

    private void Play()
    {
        // show level select menu
        root.visible = false;
        levelSelect.root.visible = true;
    }

    private void Shop()
    {
        // open shop
        root.visible = false;
        shopElem.root.visible = true;
    }
    private void Settings()
    {
        // open settings
        root.visible = false;
        volumeSettings.root.visible = true;
    }
}
