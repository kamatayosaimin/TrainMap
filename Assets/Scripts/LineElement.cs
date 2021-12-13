using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineElement : MonoBehaviour
{
    [SerializeField] private Image _lineColorImage;
    private Image _bgImage;
    [SerializeField] private Text _text;
    private MapChanger _mapChanger;
    private MapScene _main;

    void Awake()
    {
        _bgImage = GetComponent<Image>();
    }

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
        _main.SetMapChanger(_mapChanger);
    }

    public void Init(MapChanger mapChanger, MapScene main, Color lineColor, string lineName)
    {
        _mapChanger = mapChanger;
        _main = main;

        _lineColorImage.color = lineColor;

        _text.text = lineName;

        SetEnable(false);
    }

    public void SetEnable(bool value)
    {
        _bgImage.color = value ? new Color(1f, 1f, 0f, 1f) : Color.white;
    }
}
