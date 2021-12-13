using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Corporation
{
    [SerializeField] private string _sceneName;
    [Multiline] [SerializeField] private string _name;
    [SerializeField] private Color _color = Color.white;

    public string SceneName
    {
        get
        {
            return _sceneName;
        }
    }

    public string Name
    {
        get
        {
            return _name;
        }
    }

    public Color Color
    {
        get
        {
            return _color;
        }
    }
}
