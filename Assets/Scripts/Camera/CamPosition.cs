using UnityEngine;


namespace LabirintsForCars
{
    public class CamPosition : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        //private float _zoom = 0.01f;
        //[SerializeField]
        //private float _zControl = 2;
        private float _yControl;

        private Transform _player;
        private Transform _camera;
        private Transform _targetPos;

        #endregion


        #region Unity Metods

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _targetPos = GameObject.Find("Target").transform;
            _camera = transform;
            _yControl = transform.position.y;
        }

        private void Update()
        {
            _camera.position = Vector3.Lerp( _camera.position, _targetPos.position, Time.deltaTime *5);
            _camera.rotation = Quaternion.LookRotation(_targetPos.forward * Time.deltaTime * 5);
            //if (Input.GetMouseButton(1))
            //    CameraRotateFromMouse();
        }

        //private void FixedUpdate()
        //{
        //    _camera.rotation = Quaternion.LookRotation(_player.position);
        //    _camera.position = Vector3.Lerp(_camera.position, GetCamPosition(_targetPos.position), Time.deltaTime * 10);
        //    CamZoom();
        //}

        #endregion


        #region Metods

        //private Vector3 GetCamPosition(Vector3 target)
        //{
        //    Vector3 camPos = target;
        //    camPos.z = target.z - _zControl;
        //    return camPos;
        //}

        //private void CameraRotateFromMouse()
        //{
        //    Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    _camera.position = _targetPos.position;
        //    vec.z = 0;

        //    _camera.rotation = Quaternion.LookRotation( _player.position, vec);
        //}

        //private void CamZoom()
        //{
        //    Vector2 zoom = Input.mouseScrollDelta;
        //    if (zoom.y != 0)
        //        _zControl = zoom.y > 0 ? _zControl -= _zoom : _zControl += _zoom;
        //}

        #endregion    
    }

}