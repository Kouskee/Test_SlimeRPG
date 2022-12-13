using NaughtyBezierCurves;
using UnityEngine;

namespace Enemy
{
    public class Movement : MonoBehaviour
    {
        [SerializeField, Range(1, 10)] private float _slowingDown;
    
        private BezierCurve3D _curve;
        private float _offset;
        private float _time;

        public void Init(BezierCurve3D curve, float offset)
        {
            _curve = curve;
            _offset = offset;
        }

        private void Update()
        {
            _time = Mathf.Clamp01(_time + Time.deltaTime / _slowingDown);
            transform.position = _curve.GetPoint(_time) + new Vector3(0, 0, _offset);
            transform.rotation = _curve.GetRotation(_time, Vector3.up);
        }
    }
}