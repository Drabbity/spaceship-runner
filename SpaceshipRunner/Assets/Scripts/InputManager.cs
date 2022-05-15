using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private TextAsset _keyBindsTxt;

    private Dictionary<string, string> _keyBindsDict = new Dictionary<string, string>();

    protected override void Awake()
    {
        base.Awake();
        KeyBindsTxtToDict();
    }

    public bool IsKeyPressed(string keyName)
    {
        return Input.GetKeyDown(_keyBindsDict[keyName]);
    }
    public bool IsKeyHolded(string keyName)
    {
        return Input.GetKey(_keyBindsDict[keyName]);
    }
    public bool IsKeyReleased(string keyName)
    {
        return Input.GetKeyUp(_keyBindsDict[keyName]);
    }

    private void KeyBindsTxtToDict()
    {
        string[] keyBindsStr = _keyBindsTxt.text.Split('\n');
        foreach (string keyBindLine in keyBindsStr)
        {
            string[] keyBind = keyBindLine.Split(new char[] { ' ', (char)13 });
            _keyBindsDict.Add(keyBind[0], keyBind[1]);
        }
    }
}
