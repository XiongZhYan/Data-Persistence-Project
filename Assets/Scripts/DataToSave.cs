using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataToSave : MonoBehaviour
{
    public static DataToSave instance;
    public string Name;
    private void Awake()
    {
        if(instance !=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
