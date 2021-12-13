using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadUI : MonoBehaviour
{
    [SerializeField] private int _count = 3;
    [SerializeField] private float _span;
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TextState());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetSlider(float value)
    {
        _slider.value = value;
    }

    IEnumerator TextState()
    {
        while (true)
        {
            string value = "Please Wait";

            for (int i = 0; i <= _count; i++)
            {
                _text.text = value;

                yield return new WaitForSeconds(_span);

                value += ".";
            }
        }
    }
}
