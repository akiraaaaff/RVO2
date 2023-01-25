using System;
using System.Runtime.CompilerServices;

namespace Lockstep.Math
{
    [Serializable]
    public struct LFloat
    {
        public const int P1000 = 1000;
        public const int HalfP1000 = P1000 / 2;
        public const float P0001 = 0.001f;

        public int _val;

        public static readonly LFloat zero = new LFloat(true, 0);
        public static readonly LFloat one = new LFloat(true, LFloat.P1000);
        public static readonly LFloat negOne = new LFloat(true, -LFloat.P1000);
        public static readonly LFloat half = new LFloat(true, LFloat.P1000 / 2);
        public static readonly LFloat EPSILON = new LFloat(true, 1);

        public static readonly LFloat MaxValue = new LFloat(true, int.MaxValue);
        public static readonly LFloat MinValue = new LFloat(true, int.MinValue);



        #region 构造函数

        /// <summary>
        /// 传入的是正常数放大1000 的数值
        /// </summary>
        /// <param name="rawVal"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat(bool isUseRawVal, int rawVal)
        {
            this._val = rawVal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat(int val)
        {
            this._val = val * LFloat.P1000;
        }

        /// <summary>
        /// 传入的是正常数放大1000 的数值
        /// </summary>
        /// <param name="rawVal"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat(bool isUseRawVal, long rawVal)
        {
            this._val = (int)(rawVal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat(long val)
        {
            this._val = (int)(val * LFloat.P1000);
        }

#if UNITY_EDITOR
        /// <summary>
        /// 直接使用浮点型 进行构造 警告!!! 仅应该在Editor模式下使用，不应该在正式代码中使用,避免出现引入浮点的不确定性
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LFloat(bool shouldOnlyUseInEditor, float val)
        {
            this._val = (int)(val * LFloat.P1000);
        }
#endif
        #endregion

        #region override operator 

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(LFloat a, LFloat b)
        {
            return a._val < b._val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(LFloat a, LFloat b)
        {
            return a._val > b._val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(LFloat a, LFloat b)
        {
            return a._val <= b._val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(LFloat a, LFloat b)
        {
            return a._val >= b._val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(LFloat a, LFloat b)
        {
            return a._val == b._val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(LFloat a, LFloat b)
        {
            return a._val != b._val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat operator +(LFloat a, LFloat b)
        {
            return new LFloat(true, a._val + b._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat operator -(LFloat a, LFloat b)
        {
            return new LFloat(true, a._val - b._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat operator *(LFloat a, LFloat b)
        {
            long val = ((long)a._val) * b._val / LFloat.P1000;
            return new LFloat(true, (int)(val));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat operator /(LFloat a, LFloat b)
        {
            long val = ((long)a._val) / b._val * LFloat.P1000;
            return new LFloat(true, (int)(val));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat operator -(LFloat a)
        {
            return new LFloat(true, -a._val);
        }


        #region adapt for int

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat operator +(LFloat a, int b)
        {
            return new LFloat(true, a._val + b * P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat operator -(LFloat a, int b)
        {
            return new LFloat(true, a._val - b * P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat operator *(LFloat a, int b)
        {
            return new LFloat(true, (a._val * b));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat operator /(LFloat a, int b)
        {
            return new LFloat(true, (a._val) / b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat operator +(int a, LFloat b)
        {
            return new LFloat(true, b._val + a * P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat operator -(int a, LFloat b)
        {
            return new LFloat(true, a * P1000 - b._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat operator *(int a, LFloat b)
        {
            return new LFloat(true, (b._val * a));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat operator /(int a, LFloat b)
        {
            return new LFloat(true, (int)((long)(a * P1000 * P1000) / b._val));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(LFloat a, int b)
        {
            return a._val < (b * P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(LFloat a, int b)
        {
            return a._val > (b * P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(LFloat a, int b)
        {
            return a._val <= (b * P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(LFloat a, int b)
        {
            return a._val >= (b * P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(LFloat a, int b)
        {
            return a._val == (b * P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(LFloat a, int b)
        {
            return a._val != (b * P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(int a, LFloat b)
        {
            return (a * P1000) < (b._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(int a, LFloat b)
        {
            return (a * P1000) > (b._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(int a, LFloat b)
        {
            return (a * P1000) <= (b._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(int a, LFloat b)
        {
            return (a * P1000) >= (b._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(int a, LFloat b)
        {
            return (a * P1000) == (b._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(int a, LFloat b)
        {
            return (a * P1000) != (b._val);
        }

        #endregion

        #endregion

        #region override object func 

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            return obj is LFloat && ((LFloat)obj)._val == _val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(LFloat other)
        {
            return _val == other._val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(LFloat other)
        {
            return _val.CompareTo(other._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return _val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return (_val * LFloat.P0001).ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider provider)
        {
            return (_val * LFloat.P0001).ToString(format, provider);
        }

        #endregion

        #region override type convert 

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator LFloat(int value)
        {
            return new LFloat(true, value * P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator int(LFloat value)
        {
            return value._val / P1000;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator LFloat(long value)
        {
            return new LFloat(true, value * P1000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator long(LFloat value)
        {
            return value._val / P1000;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator LFloat(float value)
        {
            return new LFloat(true, (long)(value * P1000));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator float(LFloat value)
        {
            return (float)value._val * LFloat.P0001;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator LFloat(double value)
        {
            return new LFloat(true, (long)(value * P1000));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator double(LFloat value)
        {
            return (double)value._val * LFloat.P0001;
        }

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ToInt()
        {
            return _val / LFloat.P1000;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ToLong()
        {
            return _val / LFloat.P1000;
        }

        // TODO Dummy
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float ToFloat()
        {
            return _val * LFloat.P0001;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ToDouble()
        {
            return _val * LFloat.P0001;
        }
    }
}