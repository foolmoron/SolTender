using Unity.Entities;
using UnityEngine;

[GenerateAuthoringComponent]
public struct FollowMouse : IComponentData
{
    public float Speed;
}
