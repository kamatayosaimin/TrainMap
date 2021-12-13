using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : SingletonBehaviour<Core>
{
    [SerializeField] private int _targetFrameRate = 60;
    [SerializeField] private float _mapScaler, _mouseScrollWheelMapScaler, _stationTextAngle;
    [SerializeField] private Vector3 _stationTextOffset;

    public float MapScaler
    {
        get
        {
            return _mapScaler;
        }
    }

    public float MouseScrollWheelMapScaler
    {
        get
        {
            return _mouseScrollWheelMapScaler;
        }
    }

    public Vector3 StationTextOffset
    {
        get
        {
            return _stationTextOffset;
        }
    }


    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = _targetFrameRate;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Quaternion GetStationTextRotation()
    {
        Vector3 eulerAngles = Vector3.forward * _stationTextAngle;

        return Quaternion.Euler(eulerAngles);
    }
}
