using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float _speed;
    public float Speed { get { return _speed; } set { _speed = value; } }

    private void Start()
    {
        //Speeed =?
        if (_speed == 0)
        {
            _speed = 2.5f;
        }
    }



    public void Move(float x, float y)
    {

        if (x > 0 & y > 0)
        {
            x = 0.75f;
            y = 0.75f;
        }
        else if (x < 0 & y < 0)
        {
            x = -0.75f;
            y = -0.75f;
        }
        else if (x > 0 & y < 0)
        {
            x = 0.75f;
            y = -0.75f;
        }
        else if (x < 0 & y > 0)
        {
            x = -0.75f;
            y = 0.75f;
        }

        Vector3 newPosition = new Vector3(transform.position.x + x * _speed, 0,
                                            transform.position.z + y * _speed);

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);

    }

    public void MouseLook(Vector3 mousePos)
    {
        Vector3 difference = mousePos - transform.position;
        difference.Normalize();

        float rotationY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }


}
