using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Button buton;
    public TMP_InputField widthField;
    public TMP_InputField heightField;
    public TMP_InputField bombField;

    public static int width;
    public static int height;
    public static int bomb;


    // Start is called before the first frame update
    void Start()
    {
        buton.interactable= false;
    }

    // Update is called once per frame
    void Update()
    {
        widthField.onValueChanged.AddListener(delegate { ChangeWidth(); });
        heightField.onValueChanged.AddListener(delegate { ChangeHeight(); });
        bombField.onValueChanged.AddListener(delegate { ChangeBomb(); });

        if (widthField.text != "" && heightField.text != "" && bombField.text != ""  && (width*height) > bomb && width > 1 && height > 1)
            buton.interactable = true;
        else 
            buton.interactable = false;


    }

    void ChangeWidth() { width = int.Parse(widthField.text); }
    void ChangeHeight() { height = int.Parse(heightField.text); }
    void ChangeBomb() { bomb = int.Parse(bombField.text); }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
