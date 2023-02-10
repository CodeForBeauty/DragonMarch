using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class skinShopLogic : MonoBehaviour
{
    public VisualElement root;
    public UIDocument shop;

    private Label cristals;

    private void Start()
    {
        // get root component
        root = GetComponent<UIDocument>().rootVisualElement;

        // set event to back button
        root.Q<Button>("back").clicked += () => back();

        // get cristals count UI element and set cristals count
        cristals = root.Q<Label>("cristals");
        cristals.text = GameSaveLoad.getCristals().ToString();

        // disable buy buttons
        for (int i = 1; i < 5; i++)
        {
            var tmp = root.Q<VisualElement>("skin" + i.ToString());
            tmp.Q<Button>("buy").SetEnabled(false);
        }
        // hide UI
        root.visible = false;
    }

    // event function to go back to shop page
    void back()
    {
        shop.rootVisualElement.visible = true;
        root.visible = false;
    }
}
