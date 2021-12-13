using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    private Image _mapImage;

    public RectTransform RectTransform
    {
        get
        {
            return _mapImage.rectTransform;
        }
    }

    void Awake()
    {
        _mapImage = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetMapSprite(Sprite sprite)
    {
        _mapImage.sprite = sprite;
    }

    public StationPoint[] GetStationPoints()
    {
        return GetComponentsInChildren<StationPoint>();
    }
}
