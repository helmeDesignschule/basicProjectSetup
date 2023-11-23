
using UnityEngine;

public static class LineRendererExtensions
{
    public static int FindClosestIndexToPosition(this LineRenderer line, Vector3 targetPosition)
    {
        int closestIndex = -1;
        float sqrDistance = float.MaxValue;
        for (int index = 0; index < line.positionCount; ++index)
        {
            var fromMouseToPoint = targetPosition - line.GetPosition(index);
            var newSqrDistance = fromMouseToPoint.sqrMagnitude;
            if (newSqrDistance < sqrDistance)
            {
                closestIndex = index;
                sqrDistance = newSqrDistance;
            }
        }

        return closestIndex;
    }

    public static void AddPoint(this LineRenderer line, Vector3 position)
    {
        line.positionCount++;
        line.SetPosition(line.positionCount - 1, position);
    }

    public static void InsertPointAtIndex(this LineRenderer line, int insertIndex, Vector3 position)
    {
        if (line.positionCount <= 1)
        {
            line.AddPoint(position);
            return;
        }
        if (insertIndex < 0)
            return;
        if (insertIndex >= line.positionCount)
        {
            line.AddPoint(position);
            return;
        }

        line.positionCount++;
        for (int index = line.positionCount-1; index > insertIndex; index--)
        {
            line.SetPosition(index, line.GetPosition(index-1));
        }
        line.SetPosition(insertIndex, position);
    }

    public static void RemovePoint(this LineRenderer line, int deletionIndex)
    {
        if (deletionIndex < 0 || line.positionCount <= deletionIndex)
            return;

        if (line.positionCount - 1 == deletionIndex)
        {
            line.positionCount--;
            return;
        }

        for (int index = deletionIndex; index < line.positionCount - 1; index++)
        {
            line.SetPosition(index, line.GetPosition(index+1));
        }
        line.positionCount--;
    }

    public static void TurnIntoCircle(this LineRenderer line, float distanceBetweenSegments, float radius, float fillAlpha = 1)
    {
        fillAlpha = Mathf.Clamp01(fillAlpha);
        var circumference = 2 * radius * Mathf.PI;
        var segments = Mathf.CeilToInt(circumference / distanceBetweenSegments);
        line.useWorldSpace = false;
        line.positionCount = segments;
        line.loop = fillAlpha >= .999f;

        var pointCount = Mathf.FloorToInt(segments * fillAlpha);
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
        }

        line.positionCount = pointCount;
        line.SetPositions(points);

        if (!line.loop)
        {
            var rad = Mathf.Deg2Rad * (360f * fillAlpha);
            line.AddPoint(new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius));
        }
    }

    public static float GetTotalLength(this LineRenderer line)
    {
        float length = 0;
        for (int index = 0; index < line.positionCount - 1; index++)
        {
            length += Vector3.Distance(line.GetPosition(index), line.GetPosition(index+1));
        }
        return length;
    }

    public static void Woblify(this LineRenderer line, float wobbleAmplitude, float wobbleIntensity)
    {
        float totalLength = line.GetTotalLength();

        float randomOffset = Random.value * 1000;
        
        wobbleIntensity *= totalLength;
        
        var curve = AnimationCurve.Linear(0, 1, 1, 1);

        float currentCheckedLength = 0;
        for (int index = 1; index < line.positionCount; ++index)
        {
            currentCheckedLength += Vector3.Distance(line.GetPosition(index - 1), line.GetPosition(index));
            float alpha = currentCheckedLength / totalLength;

            var wobblePower = (Mathf.PerlinNoise1D(wobbleIntensity * alpha + randomOffset) - .5f) * 2 * wobbleAmplitude;
            wobblePower += 1;
            curve.AddKey(alpha, wobblePower);
        }

        line.widthCurve = curve;
    }
}
