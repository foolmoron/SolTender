using Unity.Entities;
using Unity.Transforms;
using Unity.U2D.Entities.Physics;
using Unity.Mathematics;

public class GravitySystem : ComponentSystem
{
    protected override void OnUpdate() {
        var dt = Time.DeltaTime;
        using (var sources = Entities.WithAll<GravitySource>().ToEntityQuery().ToEntityArray(Unity.Collections.Allocator.TempJob)) {
            Entities.ForEach((ref GravityVulnerable vulnerable, ref Translation pos, ref PhysicsVelocity vel) => {
                foreach (var s in sources) {
                    var source = EntityManager.GetComponentData<GravitySource>(s);
                    var sourcePos = EntityManager.GetComponentData<Translation>(s).Value.xy;
                    var vecToSource = sourcePos - pos.Value.xy;
                    var dist = math.length(vecToSource);
                    var dir = math.normalize(vecToSource);
                    if (dist < source.MinRadius) {
                        // nothing
                    } else if (dist > source.MaxRadius) {
                        vel.Linear += dir * -math.min(0.35f, math.dot(math.normalize(vel.Linear), dir)) * source.Mass * dt;
                    } else {
                        vel.Linear += dir * source.Mass / math.pow(dist, 1.25f) * dt;
                        vel.Angular += math.atan2(dir.y, dir.x) * source.AngularEffect / math.pow(dist, 0.3f) * dt;
                    }
                }
            });
        }
    }
}
