using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartButton : MonoBehaviour
{
    public GameObject panel,PlayButton;
    public TMPro.TMP_Text Instruction;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            panel.gameObject.SetActive(false);
            Instruction.text = "";
            PlayButton.SetActive(false);
        }
    }
    public void PlayNightMode()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Instructions()
    {
        panel.gameObject.SetActive(true);
        Instruction.text = "Left Click:      Continute/Next\n"+
        "Right Click:     Adjust Camera View\n"+
        "Esc:        Close pictures/text boxes\n"+
        "W/A/S/D:       Move\n"+
        "Space:             Jump\n" +
        "\n(Press Esc to return)";
        PlayButton.SetActive(true);
        
    }

    public void Endings()
    {
        panel.gameObject.SetActive(true);
        Instruction.text = "Ending 1: Too Slow\n" +
                           "Ending 2: Unlucky Kid\n" +
                           "Ending 3: Frozen\n" +
                           "Ending 4: Fake Bus\n" +
                           "Ending 5: Suicide\n" +
                           "Ending 6: Not Identified\n" +
                           "Ending 7: You Make it\n" +
                           "Ending 8: Unhappy Subway\n" +
                           "\n(Press Esc to return)";
    }
    
    
}
