using Unity.Entities;

[GenerateAuthoringComponent]
public struct GravitySource : IComponentData
{
    public float Mass;
    public float AngularEffect;
    public float MinRadius;
    public float MaxRadius;
}
