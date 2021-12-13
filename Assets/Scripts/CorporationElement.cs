using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorporationElement : MonoBehaviour
{
    private string _sceneName;
    [SerializeField] private UnityEngine.UI.Text _text;
    private TopScene _main;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SelectButton()
    {
        _main.LoadScene(_sceneName);
    }

    public void Init(TopScene main, Corporation corporation)
    {
        _sceneName = corporation.SceneName;
        _main = main;

        _text.text = corporation.Name;
        _text.color = corporation.Color;
    }
}
