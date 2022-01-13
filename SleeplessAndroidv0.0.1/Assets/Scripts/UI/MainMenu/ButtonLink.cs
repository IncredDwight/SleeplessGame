using UnityEngine;

public class ButtonLink : MonoBehaviour
{
    [SerializeField]
    private string _link;

    public void OpenLink()
    {
        Application.OpenURL(_link);
    }
}
