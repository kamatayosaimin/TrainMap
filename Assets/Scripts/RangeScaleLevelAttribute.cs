using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class RangeScaleLevelAttribute : PropertyAttribute
{
    private int _min, _max;

    public int Min
    {
        get
        {
            return _min;
        }
    }

    public int Max
    {
        get
        {
            return _max;
        }
    }

    public RangeScaleLevelAttribute()
    {
        _min = MapScene.MinScaleLevel;
        _max = MapScene.MaxScaleLevel;
    }
}
