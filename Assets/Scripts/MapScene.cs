using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class MapScene : SceneBehaviour
{
    private int _scaleLevel;
    private float _scale = MinScaleLevel;
    private bool _notFirst, _notSecond;
    private Vector2? _firstPosition, _secondPosition;
    [SerializeField] private RectTransform _stationTextParent, _lineParent;
    [SerializeField] private Transform _mapChengerParent;
    [SerializeField] private Text _stationTextPrefab;
    private Core _core;
    [SerializeField] private LineElement _lineElementPrefab;
    private MapChanger _currentMapChanger;
    [SerializeField] private MapController _mapController;
    private StationPoint[] _stationPoints;

    public const int MinScaleLevel = 1, MaxScaleLevel = 9;

    float Scale
    {
        get
        {
            return _scale;
        }
        set
        {
            _scale = Mathf.Clamp(value, MinScaleLevel, MaxScaleLevel + 1f);

            SetScale();
        }
    }

    RectTransform MapTransform
    {
        get
        {
            return _mapController.RectTransform;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        _stationPoints = _mapController.GetStationPoints();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _scaleLevel = (int)_scale;
        _core = Core.Instance;

        MapChanger[] mapChangers = _mapChengerParent.GetComponentsInChildren<MapChanger>();

        foreach (var c in mapChangers)
        {
            LineElement instance = Instantiate(_lineElementPrefab, _lineParent);

            c.Init(instance, this);
        }

        Quaternion stationTextRotation = _core.GetStationTextRotation();
        Vector3 stationTextOffset = _core.StationTextOffset;

        foreach (var p in _mapController.GetStationPoints())
        {
            Text instance = Instantiate(_stationTextPrefab, Vector3.zero, stationTextRotation, _stationTextParent);

            p.Init(instance, stationTextOffset);
        }

        SetMapChanger(mapChangers[0]);
        SetScale();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
#if UNITY_EDITOR
        MapEditor();

        return;
#endif

        MapApp();
    }

    public void SetMapChanger(MapChanger mapChanger)
    {
        if (_currentMapChanger == mapChanger)
            return;

        if (_currentMapChanger)
            _currentMapChanger.SetLineElement(false);

        _currentMapChanger = mapChanger;

        _currentMapChanger.SetLineElement(true);

        foreach (var p in _stationPoints)
            p.SetActive(_currentMapChanger.HasStationPoint(p));

        _currentMapChanger.SetScale(_scaleLevel);

        _mapController.SetMapSprite(_currentMapChanger.MapSprite);

        SetStationTextPosition();
    }

    void MapEditor()
    {
        float scaler = Input.GetAxis(AxisNameGOIS.Mouse_ScrollWheel) * _core.MouseScrollWheelMapScaler;
        bool? raycast = null;
        Vector3 mousePosition = Input.mousePosition;

        if (scaler != 0f)
        {
            raycast = Raycast(mousePosition);

            if (raycast.Value)
                Scale += scaler;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (raycast ?? Raycast(mousePosition))
                _firstPosition = mousePosition;

            return;
        }

        if (_firstPosition.HasValue)
        {
            if (!Input.GetMouseButton(0))
            {
                _firstPosition = null;

                return;
            }

            MapMove(mousePosition);

            _firstPosition = mousePosition;
        }
    }

    void MapApp()
    {
        Touch[] touches = Input.touches;

        int count = touches.Length;

        if (_firstPosition.HasValue)
        {
            MapAppTouched();

            return;
        }

        MapAppNotTouched();
    }

    void MapAppNotTouched()
    {
        Touch[] touches = Input.touches;

        int count = touches.Length;

        if (count < 1)
        {
            if (_notFirst)
                _notFirst = false;

            return;
        }

        if (_notFirst)
            return;

        Vector2 firstPosition = touches[0].position;

        if (!Raycast(firstPosition))
        {
            _notFirst = true;

            return;
        }

        _firstPosition = firstPosition;

        if (count < 2)
            return;

        Vector2 secondPosition = touches[1].position;

        if (Raycast(secondPosition))
            _secondPosition = secondPosition;
    }

    void MapAppTouched()
    {
        Touch[] touches = Input.touches;

        int count = touches.Length;

        if (count < 1)
        {
            _notSecond = false;
            _firstPosition = _secondPosition = null;

            return;
        }

        Vector2 firstPosition = touches[0].position;

        if (count < 2)
            MapAppOneTouching(firstPosition);
        else
            MapAppTwoTouching(firstPosition, touches[1].position);

        _firstPosition = firstPosition;
    }

    void MapAppOneTouching(Vector2 firstPosition)
    {
        if (_notSecond)
            _notSecond = false;

        if (_secondPosition.HasValue)
            _secondPosition = null;
        else
            MapMove(firstPosition);
    }

    void MapAppTwoTouching(Vector2 firstPosition, Vector2 secondPosition)
    {
        if (_notSecond)
        {
            MapMove(firstPosition);

            return;
        }

        if (_secondPosition.HasValue)
        {
            Vector2 previousDistance = firstPosition - secondPosition,
                currentDistance = (_firstPosition - _secondPosition).Value;

            Scale += (previousDistance.sqrMagnitude - currentDistance.sqrMagnitude) * _core.MapScaler;

            _secondPosition = secondPosition;

            return;
        }

        if (Raycast(secondPosition))
        {
            _secondPosition = secondPosition;

            return;
        }

        _notSecond = true;

        MapMove(firstPosition);
    }

    void MapMove(Vector2 firstPosition)
    {
        MapTransform.anchoredPosition += firstPosition - _firstPosition.Value;

        Clamp();
    }

    void Clamp()
    {
        bool isClamped = false;
        RectTransform mapTransform = MapTransform;

        Vector2 mapAnchoredPosition = mapTransform.anchoredPosition, mapHalfSize = mapTransform.sizeDelta / 2f;
        Vector3 max = mapTransform.TransformVector(mapHalfSize) - (Vector3)mapHalfSize;

        if (mapAnchoredPosition.x < -max.x)
        {
            isClamped = true;
            mapAnchoredPosition.x = -max.x;
        }

        if (mapAnchoredPosition.x > max.x)
        {
            isClamped = true;
            mapAnchoredPosition.x = max.x;
        }

        if (mapAnchoredPosition.y < -max.y)
        {
            isClamped = true;
            mapAnchoredPosition.y = -max.y;
        }

        if (mapAnchoredPosition.y > max.y)
        {
            isClamped = true;
            mapAnchoredPosition.y = max.y;
        }

        if (isClamped)
            mapTransform.anchoredPosition = mapAnchoredPosition;

        SetStationTextPosition();
    }

    void SetScale()
    {
        int previousScaleLevel = _scaleLevel;
        RectTransform mapTransform = MapTransform;
        System.Func<Vector2> end = () => mapTransform.sizeDelta / 2f * mapTransform.localScale;

        Vector2 pivot = mapTransform.anchoredPosition / end();

        _scaleLevel = _scale >= MaxScaleLevel ? MaxScaleLevel : (int)_scale;

        mapTransform.localScale = Vector3.one * _scale;
        mapTransform.anchoredPosition = pivot * end();

        Clamp();

        if (_scaleLevel != previousScaleLevel)
            _currentMapChanger.SetScale(_scaleLevel);
    }

    void SetStationTextPosition()
    {
        foreach (var p in _stationPoints.Where(p => p.IsActive()))
            p.SetTextPosition();
    }

    bool Raycast(Vector2 position)
    {
        List<RaycastResult> resultList = new List<RaycastResult>();
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);

        pointerEventData.position = position;

        EventSystem.current.RaycastAll(pointerEventData, resultList);

        return resultList.Count >= 1 && resultList[0].gameObject.GetComponent<MapController>();
    }
}
