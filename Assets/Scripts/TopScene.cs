using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopScene : SceneBehaviour
{
    [SerializeField] private RectTransform _corporationParent;
    [SerializeField] private Corporation[] _corporations;
    [SerializeField] private CorporationElement _corporationElementPrefab;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var c in _corporations)
        {
            CorporationElement instance = Instantiate(_corporationElementPrefab, _corporationParent);

            instance.Init(this, c);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
