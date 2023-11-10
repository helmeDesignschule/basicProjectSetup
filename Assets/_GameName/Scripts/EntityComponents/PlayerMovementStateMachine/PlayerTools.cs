using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTools
{
    private PlayerReferences _references;
    public PlayerTools(PlayerReferences references)
    {
        _references = references;
    }

    //we wrote a simple tool to make capsule casting from our current position a bit easier, so we don't have to write too much
    //boiler plate code.
    public bool CapsuleCast(Vector3 direction, float length, out RaycastHit hit, float backwardsOffset = 0)
    {
        var capsule = _references.Transform.GetComponent<CapsuleCollider>();
        var point1 = _references.Position;
        point1 += Vector3.up * capsule.radius;
        point1 -= direction * backwardsOffset;

        var point2 = point1;
        point2 += Vector3.up * (capsule.height - capsule.radius * 2);
        point2 -= direction * backwardsOffset;
        hit = new RaycastHit();
        return Physics.CapsuleCast(point1, point2, capsule.radius * .9999f, direction, out hit, length + backwardsOffset);
    }
}
