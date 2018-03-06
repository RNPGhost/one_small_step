using UnityEngine;

public class UIDirectionStabilizer : MonoBehaviour {
  
  [SerializeField]
  private Camera _camera;

  private Quaternion _rotation;

  private void Start()
  {
    Vector3 look_direction = new Vector3(0, 0, _camera.transform.forward.z);
    transform.rotation = Quaternion.LookRotation(look_direction);
    _rotation = transform.rotation;
  }

  private void LateUpdate()
  {
    transform.rotation = _rotation;
  }
}
