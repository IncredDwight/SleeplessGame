
using UnityEngine;
using UnityEngine.UI;

public class FpsShow : MonoBehaviour
{
    private Text _text;

    void Start()
    {
        _text = GetComponent<Text>();
    }

    void Update()
    {
        _text.text = "Fps: " + (int)(1f / Time.deltaTime);
    }
}
