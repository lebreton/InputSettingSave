using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInputSetting : MonoBehaviour
{
    [SerializeField]
    private GameObject Panel;

    void Start()
    {
        if (InputSetting.IsLoadSave())
        {
            InputSetting.SetKey("forward", KeyCode.Z);
            InputSetting.SetKey("backward", KeyCode.S);
            InputSetting.SetKey("rightward", KeyCode.D);
            InputSetting.SetKey("leftward", KeyCode.Q);
        }
        Panel.SetActive(false);
    }

    string key_Input_Name;
    Text Value_Input_Name;

    public void XSetInput(GameObject obj)
    {
        Value_Input_Name = obj.GetComponent<Text>();
    }

    public void SetInput(string name)
    {
        Panel.SetActive(true);
        key_Input_Name = name;
    }

    public void SaveInput()
    {
        InputSetting.SaveInput();
    }

    void LateUpdate()
    {
        if (Panel.activeSelf)
        {
            Array all = Enum.GetValues(typeof(KeyCode));
            foreach (KeyCode e in all)
            {
                if (Input.GetKeyDown(e))
                {
                    InputSetting.SetKey(key_Input_Name, e);
                    Value_Input_Name.text = e.ToString();
                    Panel.SetActive(false);
                }
            }
        }

        if (Input.GetKeyDown(InputSetting.GetKey("forward"))){
            Debug.Log("J'avance !");
        }
    }
}
