using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Lockstep.Math
{
    [Serializable]
    public struct LFloat4x4
    {
        public int4x4 _val;

        /// <summary>float4x4 identity transform.</summary>
        public static readonly LFloat4x4 identity = new LFloat4x4(true, 1000, 0, 0, 0, 0, 1000, 0, 0, 0, 0, 1000, 0, 0, 0, 0, 1000);



        /// <summary>
        /// 传入的是正常数放大1000 的数值
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat4x4(bool isUseRawVal, int m00, int m01, int m02, int m03,
                        int m10, int m11, int m12, int m13,
                        int m20, int m21, int m22, int m23,
                        int m30, int m31, int m32, int m33)
        {
            _val = new int4x4(m00, m10, m20, m30,
                              m01, m11, m21, m31,
                              m02, m12, m22, m32,
                              m03, m13, m23, m33);
        }

#if UNITY_EDITOR
        /// <summary>
        /// 直接使用浮点型 进行构造 警告!!! 仅应该在Editor模式下使用，不应该在正式代码中使用,避免出现引入浮点的不确定性
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat4x4(bool shouldOnlyUseInEditor, float4x4 val)
        {
            this._val = new int4x4((int4)(val.c0 * LFloat.P1000),
                                   (int4)(val.c1 * LFloat.P1000),
                                   (int4)(val.c2 * LFloat.P1000),
                                   (int4)(val.c3 * LFloat.P1000));
        }
#endif
    }
}
