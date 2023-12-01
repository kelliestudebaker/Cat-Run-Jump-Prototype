using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuSelect : MonoBehaviour
{
    public Text option1;
    public Text option2;
    public Text option3;

    public GameObject ControlsPanel;

    private int numberOfOptions = 3;
    private int selectedOption;

    void Start()
    {
        selectedOption = 1;
        option1.color = new Color32(255, 218, 0, 255);
        option2.color = new Color32(0, 0, 0, 255);
        option3.color = new Color32(0, 0, 0, 255);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        { //Input telling it to go up or down.
            selectedOption += 1;
            if (selectedOption > numberOfOptions) //If at end of list go back to top.
            {
                selectedOption = 1;
            }

            option1.color = new Color32(0, 0, 0, 255);
            option2.color = new Color32(0, 0, 0, 255);
            option3.color = new Color32(0, 0, 0, 255);

            switch (selectedOption) //Set the color to yellow to indicate which option you are on.
            {
                case 1:
                    option1.color = new Color32(255, 218, 0, 255);
                    break;
                case 2:
                    option2.color = new Color32(255, 218, 0, 255);
                    break;
                case 3:
                    option3.color = new Color32(255, 218, 0, 255);
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        { //Input telling it to go up or down.
            selectedOption -= 1;
            if (selectedOption < 1) //If at end of list go back to top.
            {
                selectedOption = numberOfOptions;
            }

            option1.color = new Color32(0, 0, 0, 255);
            option2.color = new Color32(0, 0, 0, 255);
            option3.color = new Color32(0, 0, 0, 255);

            switch (selectedOption) //Set the color to yellow to indicate which option you are on.
            {
                case 1:
                    option1.color = new Color32(255, 218, 0, 255);
                    break;
                case 2:
                    option2.color = new Color32(255, 218, 0, 255);
                    break;
                case 3:
                    option3.color = new Color32(255, 218, 0, 255);
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
        {
            Debug.Log("Option: " + selectedOption);

            switch (selectedOption) //Set the color to yellow to indicate which option you are on.
            {
                case 1:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Start the game.
                    break;
                case 2:
                    ControlsPanel.SetActive(true); //Show the panel that explains the controls.
                    break;
                case 3:
                    UnityEditor.EditorApplication.isPlaying = false; //Quit the game.
                    //Application.Quit();
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 0"))
        {
            ControlsPanel.SetActive(false);
        }

    }
}