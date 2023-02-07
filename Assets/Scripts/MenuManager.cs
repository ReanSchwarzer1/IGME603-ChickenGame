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
    [SerializeField] private AudioClip playSFX;
    /// <summary>
    /// SFX played when the exit button is pressed
    /// </summary>
    [SerializeField] private AudioClip exitSFX;
    /// <summary>
    /// SFX played when the options button is pressed
    /// </summary>
    [SerializeField] private AudioClip optionSFX;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
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
        audioSource.clip = playSFX;
        audioSource.Play();
        SceneManager.LoadSceneAsync("Tile MAP Main Level Scene");
    }

    public void ExitButtonClicked()
    {
        audioSource.clip = exitSFX;
        audioSource.Play();
        Application.Quit();
    }

    public void OptionsButtonClicked()
    {
        audioSource.clip = optionSFX;
        audioSource.Play();
        mainMenu.SetActive(!mainMenu.activeInHierarchy);
        optionMenu.SetActive(!optionMenu.activeInHierarchy);
    }
}
