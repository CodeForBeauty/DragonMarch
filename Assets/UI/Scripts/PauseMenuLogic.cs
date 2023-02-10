using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuLogic : MonoBehaviour
{
    // get main logic to get data
    public MainGameLogic mainGameLogic;

    // store labels and button
    private Button restart;
    private Button contin;
    private Button home;

    private void Start()
    {
        // get UIDocument component
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        // geet buttons and assign them to variables
        restart = root.Q<Button>("Restart");
        contin = root.Q<Button>("Continue");
        home = root.Q<Button>("Home");
        // assign functions from MainGameLogic to buttons
        restart.clicked += () => mainGameLogic.Reload();
        contin.clicked += () => mainGameLogic.Continue();
        home.clicked += () => mainGameLogic.Home();
    }
}
