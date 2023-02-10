using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoseScreenLogic : MonoBehaviour
{
    // get main logic to get data
    public MainGameLogic mainGameLogic;
    public Texture2D starDisabled;
    public Texture2D starEnabled;
    public Texture2D humansLeftDisabled;

    // store labels and button
    private Label coins;
    private Label humans;
    private Button restart;
    private Button home;

    private VisualElement root;

    private void Start()
    {
        // get UIDocument component
        root = GetComponent<UIDocument>().rootVisualElement;
        // geet labels and button and assign them to variables
        coins = root.Q<Label>("Coins");
        humans = root.Q<Label>("Humans");
        restart = root.Q<Button>("Restart");
        home = root.Q<Button>("Home");

        restart.clicked += () => mainGameLogic.Reload();
        home.clicked += () => mainGameLogic.Home();
    }

    void Update()
    {
        // get coins count and humans count from game logic and set as text to labels
        coins.text = "+" + mainGameLogic.coinsCounter.ToString();
        humans.text = mainGameLogic.MaxHumans.ToString();
        // show X if humans not left
        if (mainGameLogic.MaxHumans < 1)
        {
            root.Q<VisualElement>("HumansIsLeft").style.backgroundImage = humansLeftDisabled;
        }
        // show stars
        if (mainGameLogic.stars > 2)
        {
            root.Q<VisualElement>("star3").style.backgroundImage = starEnabled;
            root.Q<VisualElement>("star2").style.backgroundImage = starEnabled;
            root.Q<VisualElement>("star1").style.backgroundImage = starEnabled;
        }
        else if (mainGameLogic.stars == 2)
        {
            root.Q<VisualElement>("star3").style.backgroundImage = starDisabled;
            root.Q<VisualElement>("star2").style.backgroundImage = starEnabled;
            root.Q<VisualElement>("star1").style.backgroundImage = starEnabled;
        }
        else if (mainGameLogic.stars == 1)
        {
            root.Q<VisualElement>("star3").style.backgroundImage = starDisabled;
            root.Q<VisualElement>("star2").style.backgroundImage = starDisabled;
            root.Q<VisualElement>("star1").style.backgroundImage = starEnabled;
        }
        else
        {
            root.Q<VisualElement>("star3").style.backgroundImage = starDisabled;
            root.Q<VisualElement>("star2").style.backgroundImage = starDisabled;
            root.Q<VisualElement>("star1").style.backgroundImage = starDisabled;
        }
    }
}
