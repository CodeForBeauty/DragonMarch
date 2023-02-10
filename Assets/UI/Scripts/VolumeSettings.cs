using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VolumeSettings : MonoBehaviour
{
    private Slider SFXSlider;
    private Slider MusicSlider;
    private Button backBtn;

    public UIDocument mainMenu;

    public VisualElement root;
    void Start()
    {
        // get root element
        root = GetComponent<UIDocument>().rootVisualElement;

        // get slider elements and button
        SFXSlider = root.Q<Slider>("SFXVolume");
        MusicSlider = root.Q<Slider>("MusicVolume");
        backBtn = root.Q<Button>("back");

        // register event function for slider change
        SFXSlider.RegisterValueChangedCallback(v => { PlayerPrefs.SetFloat("SFXVolume", v.newValue); });
        MusicSlider.RegisterValueChangedCallback(v => { PlayerPrefs.SetFloat("MusicVolume", v.newValue); });

        // set event for back button
        backBtn.clicked += () => back();

        // check if there is saved settings of volume and if there is change slider value
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        // hide UI
        root.visible = false;
    }

    // event function
    void back()
    {
        mainMenu.rootVisualElement.visible = true;
        root.visible = false;
    }
}
