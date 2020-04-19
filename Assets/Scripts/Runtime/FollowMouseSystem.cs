using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

public class FollowMouseSystem : ComponentSystem
{
    protected override void OnUpdate() {
        var dt = Time.DeltaTime;
        var input = World.GetExistingSystem<Unity.Tiny.Input.InputSystem>();
        float2? mousePos = null;
        if (input.IsMousePresent()) {
            mousePos = new float2(5f*16f/9f, 5f) * CameraUtil.ScreenPointToViewportPoint(EntityManager, input.GetInputPosition());
        } else if (input.TouchCount() > 0) {
            mousePos = new float2(input.GetTouch(0).x, input.GetTouch(0).y);
        }
        if (mousePos.HasValue) {
            var targetPos = mousePos.Value;
            Entities.ForEach((ref FollowMouse followMouse, ref Translation pos) => {
                pos.Value = new float3(math.lerp(pos.Value.xy, targetPos.xy, followMouse.Speed), pos.Value.z);
            });
        }
    }
}
