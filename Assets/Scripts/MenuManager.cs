using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    /// <summary>
    /// First slider shown on the options menu
    /// </summary>
    [SerializeField] private Scrollbar optionOneSlider;
    /// <summary>
    /// Landing Page Parent Object
    /// </summary>
    [SerializeField] private GameObject mainMenu;
    /// <summary>
    /// Options Page Parent Object
    /// </summary>
    [SerializeField] private GameObject optionMenu;
    /// <summary>
    /// SFX played when the play button is clicked
    /// </summary>
    [SerializeField] private AudioSource playSFX;
    /// <summary>
    /// SFX played when the exit button is pressed
    /// </summary>
    [SerializeField] private AudioSource exitSFX;
    /// <summary>
    /// SFX played when the options button is pressed
    /// </summary>
    [SerializeField] private AudioSource optionSFX;
    /// <summary>
    /// Background music played on the menu
    /// </summary>
    [SerializeField] private AudioSource bgm;
    // Start is called before the first frame update
    void Start()
    {
        bgm.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Executes a change in value based on the first option slider being changed
    /// Current implementation changes the value of volume from the AudioListener
    /// </summary>
    public void OnOptionOneChange()
    {
        AudioListener.volume = optionOneSlider.value;
    }

    public void PlayButtonClicked()
    {
        playSFX.Play();
        bgm.Pause();
        SceneManager.LoadScene("Main Level Scene");
    }

    public void ExitButtonClicked()
    {
        exitSFX.Play();
        Application.Quit();
    }

    public void OptionsButtonClicked()
    {
        optionSFX.Play();
        mainMenu.SetActive(!mainMenu.activeInHierarchy);
        optionMenu.SetActive(!optionMenu.activeInHierarchy);
    }
}
