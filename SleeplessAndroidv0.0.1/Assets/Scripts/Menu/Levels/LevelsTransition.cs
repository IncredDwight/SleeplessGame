using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsTransition : MonoBehaviour
{
    private GameObject _transitionPrefab;
    private Animator _transitionAnimator;
    private float _transitionTime = 4.5f;

    private Slider _progressBar;

    private int _targetScore = 250;
    private int _currentScore;

    [SerializeField]
    private string[] _scenesNames;

    private void Awake()
    {
        _progressBar = GameObject.Find("LevelProgressBar").GetComponent<Slider>();
        _transitionPrefab = GameObject.Find("LoaderLevelPanel");
        _transitionAnimator = _transitionPrefab.GetComponent<Animator>();
        _progressBar.maxValue = _targetScore;
        _progressBar.value = 0;
    }

    private void Update()
    {
        _progressBar.value = Mathf.MoveTowards(_progressBar.value, _currentScore, Time.deltaTime * 35);
        if (Input.GetKeyDown(KeyCode.L))
            EndLevel();
    }

    private void EndLevel()
    {
        int sceneIndex = Random.Range(0, _scenesNames.Length);

        if (_scenesNames[sceneIndex] != SceneManager.GetActiveScene().name)
        {
            StartCoroutine(Transition(sceneIndex));
        }
        else
            EndLevel();
    }

    private IEnumerator Transition(int sceneIndex)
    {
        _transitionAnimator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadSceneAsync(_scenesNames[sceneIndex]);
    }

    public void AddScore(int score)
    {
        _currentScore += score;
        if (_currentScore >= _targetScore)
        {
            _currentScore = _targetScore;
            EndLevel();
        }
    }
}
