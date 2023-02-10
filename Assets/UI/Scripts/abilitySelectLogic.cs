using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class abilitySelectLogic : MonoBehaviour
{
    // get main logic
    public MainGameLogic mainGameLogic;

    public VisualElement root;

    private Button infinite;
    private Button reduce;

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        infinite = root.Q<Button>("infinite");
        reduce = root.Q<Button>("reduce");

        if (PlayerPrefs.GetInt("flagAdd") == 0)
        {
            infinite.SetEnabled(false);
        }

        if (PlayerPrefs.GetInt("reduceAdd") == 0)
        {
            reduce.SetEnabled(false);
        }

        mainGameLogic.Pause();
        
        infinite.clicked += () => activateInfinite();
        reduce.clicked += () => activateReduce();
        root.Q<Button>("full").clicked += () => start();
    }

    private void activateInfinite()
    {
        GameSaveLoad.setAddition("flagAdd", PlayerPrefs.GetInt("flagAdd") - 1);
        mainGameLogic.isInfinite = true;
        infinite.SetEnabled(false);
    }

    private void activateReduce()
    {
        GameSaveLoad.setAddition("reduceAdd", PlayerPrefs.GetInt("reduceAdd") - 1);
        mainGameLogic.ReduceSize();
        reduce.SetEnabled(false);
    }

    void start()
    {
        mainGameLogic.Continue();
        Destroy(gameObject);
    }
}
