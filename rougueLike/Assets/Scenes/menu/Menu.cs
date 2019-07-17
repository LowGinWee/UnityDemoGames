using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject PanelHowToPlay;

    public void StartGame()
    {
        SceneManager.LoadScene("play");
    }

    public void HowToPlay()
    {
        PanelHowToPlay.SetActive(true);
    }

    public void ClosePanel()
    {
        PanelHowToPlay.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
