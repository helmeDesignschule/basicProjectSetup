using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineDrawer : MonoBehaviour
{
    private LineRenderer _line;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition.WithZ(10));
            var radius = Vector3.Distance(newPosition, transform.position);

            var angle = Vector3.SignedAngle(_line.transform.forward, newPosition - _line.transform.position, _line.transform.up);
            if (angle < 0)
                angle += 360;
            
            _line.TurnIntoCircle(.25f, radius, angle / 360f);
            _line.Woblify(.2f, .5f);
            // var closestIndex = _line.FindClosestIndexToPosition(newPosition);
            // _line.InsertPointAtIndex(closestIndex, newPosition);
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition.WithZ(10));
            var closestIndex = _line.FindClosestIndexToPosition(mousePosition);
            _line.RemovePoint(closestIndex);

            Vector3 v = new Vector3(1, 2, 3);
            v.Normalize();
            v /= v.magnitude;
        }
    }
}
