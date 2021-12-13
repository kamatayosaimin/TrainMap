using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneBehaviour : MonoBehaviour
{
    [SerializeField] private Core _corePrefab;
    [SerializeField] private LoadUI _loadUI;

    protected virtual void Awake()
    {
        if (!Core.Instance)
            Instantiate(_corePrefab);
    }

    public void LoadScene(string sceneName)
    {
        AsyncOperation load = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

        if (load == null)
            return;

        _loadUI.gameObject.SetActive(true);

        StartCoroutine(LoadState(load));
    }

    IEnumerator LoadState(AsyncOperation load)
    {
        while (true)
        {
            _loadUI.SetSlider(load.progress);

            yield return null;
        }
    }
}
