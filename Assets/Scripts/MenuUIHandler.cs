using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    // Start is called before the first frame update
  public void startNew()
    {
        if(inputField.GetComponent<TMP_InputField>().text !="")
        {
            SceneManager.LoadScene(1);
            DataToSave.instance.Name = inputField.GetComponent<TMP_InputField>().text;
        }
        else
        {
            Debug.Log("Enter name");
        }
        
    }

    public void Exit()
    {

#if UNITY_EDITOR

       UnityEditor.EditorApplication.ExitPlaymode();

#else
        Application.Quit();
#endif
    }
}
