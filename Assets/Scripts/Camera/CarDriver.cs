using UnityEngine;


public class CarDriver : MonoBehaviour
{
    public Transform[] ForwardWhells;
    private Transform _transform;
    [SerializeField]
    private float _speed = 0.0f;
    [SerializeField]
    private float _gravityForce = -1.0f;
    [SerializeField]
    private KeyCode _btnW = KeyCode.W;
    [SerializeField]
    private KeyCode _btnS = KeyCode.S;
    private Vector3 _moveVector;
    private CharacterController _characterController;
    private Animator _animator;

    private void Start()
    {
        _transform = transform;
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        GravityForce();
        MoveCar();
        RotateWhels();
        SpeedBoxUpper();
    }

    private void FixedUpdate()
    {
        //MoveCar();
    }

    private void MoveCar()
    {
        _moveVector = Vector3.zero;
        if (Input.GetKey(_btnW) && _speed < 30)
        {
            _speed += Time.deltaTime;
            _moveVector.z += _speed;
            LookForward();
        }
        else if (Input.GetKey(_btnS))
            _moveVector.z -= _speed * Time.deltaTime;
        else if (Input.GetKey(_btnW))
            _moveVector.z = _speed;
        else if (_speed > 0)
        {
            _speed -= Time.deltaTime;
            _moveVector.z += _speed;
        }
        if (_moveVector.z > 0)
            _animator.SetFloat("Speed", 1);
        else if (_moveVector.z < 0)
        {
            _speed /= 2;
            _animator.SetFloat("Speed", _speed);
        }
        else
            _animator.SetFloat("Speed", 0);
        _moveVector.y = _gravityForce;
        _characterController.Move(_moveVector * Time.deltaTime);
    }

    private void LookForward()
    {
        if (Vector3.Angle(Vector3.forward, _moveVector) > 1.0f || Vector3.Angle(Vector3.forward, _moveVector) == 0)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, _moveVector, _speed, 0.0f);
            _transform.rotation = Quaternion.LookRotation(direct);
        }
    }

    private void GravityForce()
    {
        if (!_characterController.isGrounded)
            _gravityForce -= 20.0f * Time.deltaTime;
        else _gravityForce = default;
    }

    private void RotateWhels()
    {
        //if (Input.GetKey(KeyCode.D))
        //    _animator.SetFloat("RangeRotate", 1);
        //else if(Input.GetKey(KeyCode.A))
        //    _animator.SetFloat("RangeRotate", -1);
        //else
        //    _animator.SetFloat("RangeRotate", 0);
    }

    private void SpeedBoxUpper()
    {
        if (Input.GetKeyDown(KeyCode.E))
            _speed += 1.0f;
    }
}
