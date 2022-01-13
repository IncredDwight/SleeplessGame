using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CorridorButton : MonoBehaviour
{
    [SerializeField]
    private string _sceneName;

    private SpriteRenderer _color;
    private Animator _loaderLevelPanel;

    private void Start()
    {
        _loaderLevelPanel = GameObject.Find("LoaderLevelPanel").GetComponent<Animator>();
        _color = GetComponent<SpriteRenderer>();
    }

    private void OnMouseUp()
    {
        _color.material.color = Color.white;
        StartCoroutine(Load());
    }

    private void OnMouseDown()
    {
        _color.material.color = Color.gray;
    }

    private IEnumerator Load()
    {
        _loaderLevelPanel.GetComponent<Animator>().SetTrigger("FadeIn");
        AsyncOperation async = SceneManager.LoadSceneAsync(_sceneName);
        async.allowSceneActivation = false;
        StartCoroutine(AudioManager.Instance.AudioFadeOut(2));
        yield return new WaitForSeconds(4.5f);
        async.allowSceneActivation = true;
    }
}
