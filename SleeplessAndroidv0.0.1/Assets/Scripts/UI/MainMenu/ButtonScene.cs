using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonScene : MonoBehaviour
{
    AsyncOperation async;

    [SerializeField]
    private string _sceneName;

    private GameObject _loaderLevelPanel;

    private void Start()
    {
        _loaderLevelPanel = GameObject.Find("LoaderLevelPanel");
    }

    public void LoadScene()
    {
        StartCoroutine(Loader());
    }

    private void Update()
    {
        if (async != null && async.isDone)
            async.allowSceneActivation = true;
    }

    private IEnumerator Loader()
    {
        _loaderLevelPanel.GetComponent<Animator>().SetTrigger("FadeIn");
        async  = SceneManager.LoadSceneAsync(_sceneName);
        async.allowSceneActivation = false;
        StartCoroutine(AudioManager.Instance.AudioFadeOut(2));
        yield return new WaitForSeconds(4.5f);
        async.allowSceneActivation = true;
    }
}
