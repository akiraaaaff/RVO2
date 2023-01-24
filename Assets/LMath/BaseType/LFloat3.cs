using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace Lockstep.Math
{
    [Serializable]
    public struct LFloat3
    {
        public int3 _val;

        public static readonly LFloat3 zero = new LFloat3(true, 0, 0, 0);
        public static readonly LFloat3 one = new LFloat3(true, LFloat.P1000, LFloat.P1000, LFloat.P1000);
        public static readonly LFloat3 half = new LFloat3(true, LFloat.P1000 / 2, LFloat.P1000 / 2, LFloat.P1000 / 2);

        public static readonly LFloat3 forward = new LFloat3(true, 0, 0, LFloat.P1000);
        public static readonly LFloat3 up = new LFloat3(true, 0, LFloat.P1000, 0);
        public static readonly LFloat3 right = new LFloat3(true, LFloat.P1000, 0, 0);
        public static readonly LFloat3 back = new LFloat3(true, 0, 0, -LFloat.P1000);
        public static readonly LFloat3 down = new LFloat3(true, 0, -LFloat.P1000, 0);
        public static readonly LFloat3 left = new LFloat3(true, -LFloat.P1000, 0, 0);


        public LFloat3 normalized
        {
            get
            {
                long num = (long)((long)this._val.x << 7);
                long num2 = (long)((long)this._val.y << 7);
                long num3 = (long)((long)this._val.z << 7);
                long num4 = num * num + num2 * num2 + num3 * num3;
                if (num4 == 0L)
                {
                    return LFloat3.zero;
                }

                var ret = new LFloat3();
                long b = (long)LMath.Sqrt(num4);
                long num5 = LFloat.P1000;
                ret._val.x = (int)(num * num5 / b);
                ret._val.y = (int)(num2 * num5 / b);
                ret._val.z = (int)(num3 * num5 / b);
                return ret;
            }
        }


        #region 构造函数

        /// <summary>
        /// 传入的是正常数放大1000 的数值
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat3(bool isUseRawVal, int x, int y, int z)
        {
            this._val = new(x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat3(int x, int y, int z)
        {
            this._val = new(x, y, z);
            this._val *= LFloat.P1000;
        }

        /// <summary>
        /// 传入的是正常数放大1000 的数值
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat3(bool isUseRawVal, long x, long y, long z)
        {
            this._val = new((int)x, (int)y, (int)z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat3(long x, long y, long z)
        {
            this._val = new((int)x, (int)y, (int)z);
            this._val *= LFloat.P1000;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat3(long xyz)
        {
            this._val = new((int)xyz);
            this._val *= LFloat.P1000;
        }

        /// <summary>
        /// 传入的是正常数放大1000 的数值
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat3(bool isUseRawVal, int3 val)
        {
            this._val = val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat3(int3 val)
        {
            this._val = val * LFloat.P1000;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat3(LFloat x, LFloat y, LFloat z)
        {
            this._val = new(x._val, y._val, z._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat3(LFloat xyz)
        {
            this._val = xyz._val;
        }

        /// <summary>
        /// 传入的是正常数放大1000 的数值
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat3(bool isUseRawVal, int xyz)
        {
            this._val = xyz;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat3(int xyz)
        {
            this._val = xyz;
            this._val *= LFloat.P1000;
        }

#if UNITY_EDITOR
        /// <summary>
        /// 直接使用浮点型 进行构造 警告!!! 仅应该在Editor模式下使用，不应该在正式代码中使用,避免出现引入浮点的不确定性
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat3(bool shouldOnlyUseInEditor, Vector3 val)
        {
            this._val = new int3((int)(val.x * LFloat.P1000), (int)(val.y * LFloat.P1000), (int)(val.z * LFloat.P1000));
        }
#endif
        #endregion

        #region 运算函数

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 operator +(LFloat3 a, LFloat3 b)
        {
            return new LFloat3(true, a._val + b._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 operator +(LFloat3 a, LFloat b)
        {
            return new LFloat3(true, a._val.x + b._val, a._val.y + b._val, a._val.z + b._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 operator +(LFloat3 a, int b)
        {
            var longB = (long)b * LFloat.P1000;
            return new LFloat3(true, a._val.x + longB, a._val.y + longB, a._val.z + longB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 operator -(LFloat3 a, LFloat3 b)
        {
            return new LFloat3(true, a._val - b._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 operator -(LFloat3 a, LFloat b)
        {
            return new LFloat3(true, a._val.x - b._val, a._val.y - b._val, a._val.z - b._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 operator -(LFloat3 a, int b)
        {
            var longB = (long)b * LFloat.P1000;
            return new LFloat3(true, a._val.x - longB, a._val.y - longB, a._val.z - longB);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 operator -(LFloat3 lhs)
        {
            lhs._val = -lhs._val;
            return lhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 operator *(LFloat3 lhs, LFloat3 rhs)
        {
            lhs._val = (lhs._val / LFloat.P1000) * (rhs._val / LFloat.P1000);
            return lhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 operator *(LFloat rhs, LFloat3 lhs)
        {
            lhs._val = lhs._val * rhs._val / LFloat.P1000;
            return lhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 operator *(LFloat3 lhs, LFloat rhs)
        {
            lhs._val = lhs._val * rhs._val / LFloat.P1000;
            return lhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 operator *(int rhs, LFloat3 lhs)
        {
            lhs._val *= rhs;
            return lhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 operator *(LFloat3 lhs, int rhs)
        {
            lhs._val *= rhs;
            return lhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 operator /(LFloat3 lhs, LFloat3 rhs)
        {
            lhs._val = (lhs._val * LFloat.P1000) * (rhs._val * LFloat.P1000);
            return lhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 operator /(LFloat3 lhs, LFloat rhs)
        {
            lhs._val = lhs._val * LFloat.P1000 / rhs._val;
            return lhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 operator /(LFloat lhs, LFloat3 rhs)
        {
            rhs._val = lhs._val / (rhs._val * LFloat.P1000);
            return rhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 operator /(LFloat3 lhs, int rhs)
        {
            lhs._val /= rhs;
            return lhs;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator >(LFloat3 a, LFloat3 b)
        {
            return a._val > b._val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator <(LFloat3 a, LFloat3 b)
        {
            return a._val < b._val;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator >=(LFloat3 a, LFloat3 b)
        {
            return a._val >= b._val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator <=(LFloat3 a, LFloat3 b)
        {
            return a._val <= b._val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator ==(LFloat3 a, LFloat3 b)
        {
            return a._val == b._val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator !=(LFloat3 a, LFloat3 b)
        {
            return a._val != b._val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator >(LFloat3 a, int b)
        {
            return a._val > (b * LFloat.P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator <(LFloat3 a, int b)
        {
            return a._val < (b * LFloat.P1000);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator >=(LFloat3 a, int b)
        {
            return a._val >= (b * LFloat.P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator <=(LFloat3 a, int b)
        {
            return a._val <= (b * LFloat.P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator ==(LFloat3 a, int b)
        {
            return a._val == (b * LFloat.P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool3 operator !=(LFloat3 a, int b)
        {
            return a._val != (b * LFloat.P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator LFloat3(LFloat2 v)
        {
            return new LFloat3(true, v._val.x, 0, v._val.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator LFloat2(LFloat3 v)
        {
            return new LFloat2(true, v._val.x, v._val.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object o)
        {
            if (o == null)
            {
                return false;
            }

            LFloat3 vInt = (LFloat3)o;
            return this._val.x == vInt._val.x && this._val.y == vInt._val.y && this._val.z == vInt._val.z;
        }
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat3 Normalize()
        {
            return Normalize((LFloat)1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat3 Normalize(LFloat newMagn)
        {
            long num = (long)(this._val.x * 100);
            long num2 = (long)(this._val.y * 100);
            long num3 = (long)(this._val.z * 100);
            long num4 = num * num + num2 * num2 + num3 * num3;
            if (num4 == 0L)
            {
                return this;
            }

            long b = (long)LMath.Sqrt(num4);
            long num5 = newMagn._val;
            this._val.x = (int)(num * num5 / b);
            this._val.y = (int)(num2 * num5 / b);
            this._val.z = (int)(num3 * num5 / b);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat3 RotateY(LFloat degree)
        {
            LFloat s;
            LFloat c;
            LMath.SinCos(out s, out c, new LFloat(true, degree._val * 31416L / 1800000L));
            LFloat3 vInt;
            vInt._val.x = (int)(((long)this._val.x * s._val + (long)this._val.z * c._val) / LFloat.P1000);
            vInt._val.z = (int)(((long)this._val.x * -c._val + (long)this._val.z * s._val) / LFloat.P1000);
            vInt._val.y = 0;
            return vInt.normalized;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return this._val.x * 73856093 ^ this._val.y * 19349663 ^ this._val.z * 83492791;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("({0},{1},{2})", _val.x * LFloat.P0001, _val.y * LFloat.P0001,
                 _val.z * LFloat.P0001);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float3 ToFloat3()
        {
            var x = _val.x * LFloat.P0001;
            var y = _val.y * LFloat.P0001;
            var z = _val.z * LFloat.P0001;
            return new float3(x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float2 ToFloat2()
        {
            var x = _val.x * LFloat.P0001;
            var y = _val.y * LFloat.P0001;
            return new float2(x, y);
        }
    }
}