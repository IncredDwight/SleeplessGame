using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowOpener : MonoBehaviour
{
    [SerializeField]
    private GameObject _window;

    private bool _opened;

    public void Open()
    {
        _window.SetActive(true);
    }
}
