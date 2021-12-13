using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationPoint : MonoBehaviour
{
    [Multiline] [SerializeField] private string _name;
    private Vector3 _textOffset;
    [SerializeField] private RectTransform _textPoint;
    private Text _text;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Init(Text text, Vector3 textOffset)
    {
        _textOffset = textOffset;
        _text = text;

        _text.name = name;
        _text.text = _name;

        SetTextPosition();
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);

        SetTextActive(value);
    }

    public void SetTextActive(bool value)
    {
        _text.gameObject.SetActive(value);
    }

    public void SetTextPosition()
    {
        _text.rectTransform.position = _textPoint.position + _textOffset;
    }

    public bool IsActive()
    {
        return gameObject.activeInHierarchy;
    }
}
