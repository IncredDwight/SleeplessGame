using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    private GameObject _context;

    private void OnEnable()
    {
        _context = transform.GetChild(0).gameObject;
        _context.SetActive(false);
        StartCoroutine(Activate());
    }

    public IEnumerator Activate()
    {
        yield return new WaitForSeconds(0.5f);
        _context.SetActive(true);
    }
}
