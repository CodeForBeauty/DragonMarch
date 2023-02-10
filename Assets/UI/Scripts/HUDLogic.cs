using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDLogic : MonoBehaviour
{
    // get main logic to get data
    public MainGameLogic mainGameLogic;

    // store labels and button
    private Label coins;
    private Label timer;
    private Label humans;
    private Button pause;
    // level time counting
    private float time = 0;

    private void Start()
    {
        // get UIDocument component
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        // geet labels and button and assign them to variables
        coins =  root.Q<Label>("Coins");
        timer =  root.Q<Label>("Timer");
        humans =  root.Q<Label>("Humans");
        pause = root.Q<Button>("Pause");
        // assign function to button when it's pressed
        pause.clicked += () => mainGameLogic.Pause();
    }

    void Update()
    {
        // add delta time to level time counter
        time += Time.deltaTime;
        // get coins count and humans count from game logic and set as text to labels
        coins.text = mainGameLogic.coinsCounter.ToString();
        humans.text = mainGameLogic.MaxHumans.ToString();
        // format time
        timer.text = Mathf.Floor(time / 60).ToString() + ":" + Mathf.Floor(time % 60).ToString();
    }
}
