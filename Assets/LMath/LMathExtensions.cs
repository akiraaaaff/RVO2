using Unity.Mathematics;

namespace Lockstep.Math
{
    static class LMathExtensions
    {
        // TODO Dummy
        public static LFloat2 ToLFloat2(this float2 f) => new(true,f);
        // TODO Dummy
        public static LFloat3 ToLFloat3(this float3 f) => new(true, f);
        // TODO Dummy
        public static LFloat ToLFloat(this float f) => new(true, f);
    }
}