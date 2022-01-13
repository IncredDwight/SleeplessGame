
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private PlayerStats _playerController;
    private JoystickInput _joystick;

    private Vector3 _joystickInput;

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerStats>();
        _joystick = _playerController.JoystickInput;
        LineRendererSetUp();
    }

    private void Update()
    {
        Aim();
    }

    private void LineRendererSetUp()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.startWidth = 0.5f;
        _lineRenderer.endWidth = 0.5f;
        _lineRenderer.startColor = new Color(1, 0, 0, 0.5f);

        _lineRenderer.endColor = Color.red;
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    public void Aim()
    {
        _joystickInput.x = _joystick.ShootJoystick.Horizontal;
        _joystickInput.y = _joystick.ShootJoystick.Vertical;

        if (_joystickInput != Vector3.zero)
        {
            _lineRenderer.enabled = true;

            RaycastHit2D hit = Physics2D.Raycast(transform.GetChild(0).position, _joystickInput);
            _lineRenderer.SetPosition(0, _playerController.transform.position);

            _lineRenderer.SetPosition(1, hit.point);
            Debug.DrawLine(transform.position, hit.point);
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }
}
