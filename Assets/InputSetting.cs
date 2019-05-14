using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class InputSetting
{
    Dictionary<string, KeyCode> Inputs;

    private static InputSetting Instance;
    private Array ListKeyCode = Enum.GetValues(typeof(KeyCode));

    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Debug.Log("OnRuntimeMethodLoad");
        Instance = new InputSetting();
    }

    bool LoadSave = false;
    public InputSetting()
    {
        Inputs = new Dictionary<string, KeyCode>();
        string save = PlayerPrefs.GetString("InputSetting");

        if (save.Length > 0)
        {
            save = save.Remove(0, 1);
            Debug.Log(save);
            string[] lines = save.Split(new string[] { "\n" }, StringSplitOptions.None);
            foreach (string line in lines)
            {
                if (line.Length > 0)
                {
                    string[] data = line.Split('-');
                    Inputs.Add(data[0], (KeyCode)ListKeyCode.GetValue(int.Parse(data[1])));
                }
            }

            LoadSave = true;
        }
        else
            LoadSave = false;
    }

    public static bool IsLoadSave()
    {
        return Instance.Interne_IsLoadSave();
    }
    private bool Interne_IsLoadSave()
    {
        return LoadSave;
    }

    public static void SaveInput()
    {
        Instance.Interne_SaveInput();
    }

    private void Interne_SaveInput()
    {
        string data = "$";
        foreach (KeyValuePair<string, KeyCode> entry in Inputs)
        {
            Debug.Log(entry.Key);
            data += entry.Key + "-" + (int)entry.Value + "\n";
        }

        Debug.Log(data);
        PlayerPrefs.SetString("InputSetting", data);
    }

    public static KeyCode GetKey(string name)
    {
      return Instance.Interne_GetKey(name);
    }

    private KeyCode Interne_GetKey(string name)
    {
        return Inputs[name];
    }

    public static void SetKey(string name, KeyCode key)
    {
        Instance.Interne_SetKey(name, key);
    }

    private void Interne_SetKey(string name, KeyCode key)
    {
        if (Inputs.ContainsKey(name))
            Inputs[name] = key;
        else
            Inputs.Add(name, key);
    }
}
