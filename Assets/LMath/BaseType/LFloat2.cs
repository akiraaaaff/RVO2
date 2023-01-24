using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace Lockstep.Math
{
    [Serializable]
    public struct LFloat2
    {
        public int2 _val;

        public LFloat x => new LFloat(true, _val.x);
        public LFloat y => new LFloat(true, _val.y);

        public static readonly LFloat2 zero = new LFloat2(true, 0, 0);
        public static readonly LFloat2 one = new LFloat2(true, LFloat.P1000, LFloat.P1000);
        public static readonly LFloat2 half = new LFloat2(true, LFloat.P1000 / 2, LFloat.P1000 / 2);
        public static readonly LFloat2 up = new LFloat2(true, 0, LFloat.P1000);
        public static readonly LFloat2 down = new LFloat2(true, 0, -LFloat.P1000);
        public static readonly LFloat2 right = new LFloat2(true, LFloat.P1000, 0);
        public static readonly LFloat2 left = new LFloat2(true, -LFloat.P1000, 0);

        private static readonly int[] Rotations = new int[] {
            1,
            0,
            0,
            1,
            0,
            1,
            -1,
            0,
            -1,
            0,
            0,
            -1,
            0,
            -1,
            1,
            0
        };
        public LFloat2 normalized
        {
            get
            {
                LFloat2 result = new LFloat2(true, this._val.x, this._val.y);
                result.Normalize();
                return result;
            }
        }

        #region 构造函数

        /// <summary>
        /// 传入的是正常数放大1000 的数值
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat2(bool isUseRawVal, int x, int y)
        {
            this._val = new(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat2(int x, int y)
        {
            this._val = new(x, y);
            this._val *= LFloat.P1000;
        }
        /// <summary>
        /// 传入的是正常数放大1000 的数值
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat2(bool isUseRawVal, long x, long y)
        {
            this._val = new((int)x, (int)y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat2(long x, long y)
        {
            this._val = new((int)x, (int)y);
            this._val *= LFloat.P1000;
        }

        /// <summary>
        /// 传入的是正常数放大1000 的数值
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat2(bool isUseRawVal, long xy)
        {
            this._val = new((int)xy);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat2(long xy)
        {
            this._val = new((int)xy);
            this._val *= LFloat.P1000;
        }

        /// <summary>
        /// 传入的是正常数放大1000 的数值
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat2(bool isUseRawVal, int2 val)
        {
            this._val = val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat2(int2 val)
        {
            this._val = val * LFloat.P1000;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat2(LFloat x, LFloat y)
        {
            this._val = new(x._val, y._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat2(LFloat xy)
        {
            this._val = new(xy._val, xy._val);
        }

        /// <summary>
        /// 传入的是正常数放大1000 的数值
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat2(bool isUseRawVal, int xy)
        {
            this._val = xy;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat2(int xy)
        {
            this._val = xy;
            this._val *= LFloat.P1000;
        }

#if UNITY_EDITOR

        /// <summary>
        /// 直接使用浮点型 进行构造 警告!!! 仅应该在Editor模式下使用，不应该在正式代码中使用,避免出现引入浮点的不确定性
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat2(bool shouldOnlyUseInEditor, Vector2 val)
        {
            this._val = new int2((int)(val.x * LFloat.P1000), (int)(val.y * LFloat.P1000));
        }

        /// <summary>
        /// 直接使用浮点型 进行构造 警告!!! 仅应该在Editor模式下使用，不应该在正式代码中使用,避免出现引入浮点的不确定性
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat2(bool shouldOnlyUseInEditor, float x, float y)
        {
            this._val = new int2((int)(x * LFloat.P1000), (int)(y * LFloat.P1000));
        }

#endif
        #endregion

        #region 运算函数

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 operator +(LFloat2 a, LFloat2 b)
        {
            return new LFloat2(true, a._val + b._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 operator +(LFloat2 a, LFloat b)
        {
            return new LFloat2(true, a._val.x + b._val, a._val.y + b._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 operator +(LFloat2 a, int b)
        {
            var longB = (long)b * LFloat.P1000;
            return new LFloat2(true, a._val.x + longB, a._val.y + longB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 operator -(LFloat2 a, LFloat2 b)
        {
            return new LFloat2(true, a._val - b._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 operator -(LFloat2 a, LFloat b)
        {
            return new LFloat2(true, a._val.x - b._val, a._val.y - b._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 operator -(LFloat2 a, int b)
        {
            var longB = (long)b * LFloat.P1000;
            return new LFloat2(true, a._val.x - longB, a._val.y - longB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 operator -(LFloat2 lhs)
        {
            lhs._val = -lhs._val;
            return lhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 operator *(LFloat2 lhs, LFloat2 rhs)
        {
            lhs._val = (lhs._val / LFloat.P1000) * (rhs._val / LFloat.P1000);
            return lhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 operator *(LFloat rhs, LFloat2 lhs)
        {
            lhs._val = lhs._val * rhs._val / LFloat.P1000;
            return lhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 operator *(LFloat2 lhs, LFloat rhs)
        {
            lhs._val = lhs._val * rhs._val / LFloat.P1000;
            return lhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 operator *(int rhs, LFloat2 lhs)
        {
            lhs._val *= rhs;
            return lhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 operator *(LFloat2 lhs, int rhs)
        {
            lhs._val *= rhs;
            return lhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 operator /(LFloat2 lhs, LFloat2 rhs)
        {
            lhs._val = (lhs._val * LFloat.P1000) / (rhs._val * LFloat.P1000);
            return lhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 operator /(LFloat2 lhs, LFloat rhs)
        {
            lhs._val = lhs._val * LFloat.P1000 / rhs._val;
            return lhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 operator /(LFloat lhs, LFloat2 rhs)
        {
            rhs._val = lhs._val / (rhs._val * LFloat.P1000);
            return rhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 operator /(LFloat2 lhs, int rhs)
        {
            lhs._val /= rhs;
            return lhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator >(LFloat2 a, LFloat2 b)
        {
            return a._val > b._val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator <(LFloat2 a, LFloat2 b)
        {
            return a._val < b._val;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator >=(LFloat2 a, LFloat2 b)
        {
            return a._val >= b._val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator <=(LFloat2 a, LFloat2 b)
        {
            return a._val <= b._val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator ==(LFloat2 a, LFloat2 b)
        {
            return a._val == b._val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator !=(LFloat2 a, LFloat2 b)
        {
            return a._val != b._val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator >(LFloat2 a, int b)
        {
            return a._val > (b * LFloat.P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator <(LFloat2 a, int b)
        {
            return a._val < (b * LFloat.P1000);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator >=(LFloat2 a, int b)
        {
            return a._val >= (b * LFloat.P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator <=(LFloat2 a, int b)
        {
            return a._val <= (b * LFloat.P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator ==(LFloat2 a, int b)
        {
            return a._val == (b * LFloat.P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool2 operator !=(LFloat2 a, int b)
        {
            return a._val != (b * LFloat.P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator LFloat2(LFloat3 v)
        {
            return new LFloat2(true, v._val.x, v._val.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator LFloat3(LFloat2 v)
        {
            return new LFloat3(true, v._val.x, v._val.y, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }

            LFloat2 vInt = (LFloat2)o;
            return this._val.x == vInt._val.x && this._val.y == vInt._val.y;
        }
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Normalize()
        {
            long num = (long)(this._val.x * 100);
            long num2 = (long)(this._val.y * 100);
            long num3 = num * num + num2 * num2;
            if (num3 == 0L)
            {
                return;
            }

            long b = (long)LMath.Sqrt(num3);
            this._val.x = (int)(num * 1000L / b);
            this._val.y = (int)(num2 * 1000L / b);
        }

        /// <summary>
        /// clockwise 顺时针旋转  
        /// 1表示顺时针旋转 90 degree
        /// 2表示顺时针旋转 180 degree
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 Rotate(LFloat2 v, int r)
        {
            r %= 4;
            return new LFloat2(true,
                v._val.x * LFloat2.Rotations[r * 4] + v._val.y * LFloat2.Rotations[r * 4 + 1],
                v._val.x * LFloat2.Rotations[r * 4 + 2] + v._val.y * LFloat2.Rotations[r * 4 + 3]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat2 Rotate(LFloat deg)
        {
            var rad = LMath.Deg2Rad * deg;
            LFloat cos, sin;
            LMath.SinCos(out sin, out cos, rad);
            return new LFloat2(_val.x * cos - _val.y * sin, _val.x * sin + _val.y * cos);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return this._val.x * 49157 + this._val.y * 98317;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("({0},{1})", _val.x * LFloat.P0001, _val.y * LFloat.P0001);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float2 ToFloat2()
        {
            var x = _val.x * LFloat.P0001;
            var y = _val.y * LFloat.P0001;
            return new float2(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3 ToFloat3()
        {
            var x = _val.x * LFloat.P0001;
            var z = _val.y * LFloat.P0001;
            return new float3(x, 0, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3 ToFloat3(float y)
        {
            var x = _val.x * LFloat.P0001;
            var z = _val.y * LFloat.P0001;
            return new float3(x, y, z);
        }
    }
}