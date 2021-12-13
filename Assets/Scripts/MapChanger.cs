using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapChanger : MonoBehaviour
{

    [System.Serializable]
    class StationData
    {
        [SerializeField] [RangeScaleLevel] private int _scaleLevel = MapScene.MinScaleLevel;
        [SerializeField] private StationPoint _point;

        public StationPoint Point
        {
            get
            {
                return _point;
            }
        }

        public void SetScale(int scaleLevel)
        {
            _point.SetTextActive(scaleLevel >= _scaleLevel);
        }
    }

    [Multiline] [SerializeField] private string _lineName;
    [SerializeField] private Color _lineColor = Color.white;
    [SerializeField] private Sprite _mapSprite;
    private LineElement _lineElement;
    [SerializeField] private StationData[] _stationDatas;

    public Sprite MapSprite
    {
        get
        {
            return _mapSprite;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Init(LineElement lineElement, MapScene main)
    {
        _lineElement = lineElement;

        _lineElement.Init(this, main, _lineColor, _lineName);
    }

    public void SetLineElement(bool value)
    {
        _lineElement.SetEnable(value);
    }

    public void SetScale(int scaleLevel)
    {
        foreach (var d in _stationDatas)
            d.SetScale(scaleLevel);
    }

    public bool HasStationPoint(StationPoint stationPoint)
    {
        return _stationDatas.Select(d => d.Point).Contains(stationPoint);
    }
}
