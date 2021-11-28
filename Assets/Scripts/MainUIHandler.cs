using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
   public void restart()
    {
        SceneManager.LoadScene(0);
    }


}
