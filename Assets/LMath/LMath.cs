
using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace Lockstep.Math
{
    public static partial class LMath
    {
        public static readonly LFloat PIHalf = new LFloat(true, 1571);
        public static readonly LFloat PI = new LFloat(true, 3142);
        public static readonly LFloat PI2 = new LFloat(true, 6283);
        public static readonly LFloat Rad2Deg = 180 / PI;
        public static readonly LFloat Deg2Rad = PI / 180;



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Atan2(LFloat y, LFloat x)
        {
            return Atan2(y._val, x._val);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Atan2(int y, int x)
        {
            if (x == 0)
            {
                if (y > 0) return PIHalf;
                else if (y < 0) { return -PIHalf; }
                else return LFloat.zero;
            }
            if (y == 0)
            {
                if (x > 0) return LFloat.zero;
                else if (x < 0) { return PI; }
                else return LFloat.zero;
            }

            int num;
            int num2;
            if (x < 0)
            {
                if (y < 0)
                {
                    x = -x;
                    y = -y;
                    num = 1;
                }
                else
                {
                    x = -x;
                    num = -1;
                }

                num2 = -31416;
            }
            else
            {
                if (y < 0)
                {
                    y = -y;
                    num = -1;
                }
                else
                {
                    num = 1;
                }

                num2 = 0;
            }

            int dIM = LUTAtan2.DIM;
            long num3 = (long)(dIM - 1);
            long b = (long)((x >= y) ? x : y);
            int num4 = (int)((long)x * num3 / b);
            int num5 = (int)((long)y * num3 / b);
            int num6 = LUTAtan2.table[num5 * dIM + num4];
            return new LFloat(true, (long)((num6 + num2) * num) / 10);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Acos(LFloat val)
        {
            int num = (int)(val._val * (long)LUTAcos.HALF_COUNT / LFloat.P1000) +
                      LUTAcos.HALF_COUNT;
            num = Clamp(num, 0, LUTAcos.COUNT);
            return new LFloat(true, (long)LUTAcos.table[num] / 10);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Asin(LFloat val)
        {
            int num = (int)(val._val * (long)LUTAsin.HALF_COUNT / LFloat.P1000) +
                      LUTAsin.HALF_COUNT;
            num = Clamp(num, 0, LUTAsin.COUNT);
            return new LFloat(true, (long)LUTAsin.table[num] / 10);
        }

        //ccw

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Sin(LFloat radians)
        {
            int index = LUTSinCos.getIndex(radians);
            return new LFloat(true, (long)LUTSinCos.sin_table[index] / 10);
        }

        //ccw

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Cos(LFloat radians)
        {
            int index = LUTSinCos.getIndex(radians);
            return new LFloat(true, (long)LUTSinCos.cos_table[index] / 10);
        }
        //ccw

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SinCos(out LFloat s, out LFloat c, LFloat radians)
        {
            int index = LUTSinCos.getIndex(radians);
            s = new LFloat(true, (long)LUTSinCos.sin_table[index] / 10);
            c = new LFloat(true, (long)LUTSinCos.cos_table[index] / 10);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Sqrt32(uint a)
        {
            uint num = 0u;
            uint num2 = 0u;
            for (int i = 0; i < 16; i++)
            {
                num2 <<= 1;
                num <<= 2;
                num += a >> 30;
                a <<= 2;
                if (num2 < num)
                {
                    num2 += 1u;
                    num -= num2;
                    num2 += 1u;
                }
            }

            return num2 >> 1 & 65535u;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Sqrt64(ulong a)
        {
            ulong num = 0uL;
            ulong num2 = 0uL;
            for (int i = 0; i < 32; i++)
            {
                num2 <<= 1;
                num <<= 2;
                num += a >> 62;
                a <<= 2;
                if (num2 < num)
                {
                    num2 += 1uL;
                    num -= num2;
                    num2 += 1uL;
                }
            }

            return num2 >> 1 & 0xffffffffu;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sqrt(int a)
        {
            if (a <= 0)
            {
                return 0;
            }

            return (int)LMath.Sqrt32((uint)a);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sqrt(long a)
        {
            if (a <= 0L)
            {
                return 0;
            }

            if (a <= (long)(0xffffffffu))
            {
                return (int)LMath.Sqrt32((uint)a);
            }

            return (int)LMath.Sqrt64((ulong)a);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Sqrt(LFloat a)
        {
            if (a._val <= 0)
            {
                return LFloat.zero;
            }

            return new LFloat(true, Sqrt((long)a._val * LFloat.P1000));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Sqr(LFloat a)
        {
            return a * a;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Clamp(long a, long min, long max)
        {
            if (a < min)
            {
                return min;
            }

            if (a > max)
            {
                return max;
            }

            return a;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Clamp(LFloat a, LFloat min, LFloat max)
        {
            if (a < min)
            {
                return min;
            }

            if (a > max)
            {
                return max;
            }

            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Clamp01(LFloat a)
        {
            if (a < LFloat.zero)
            {
                return LFloat.zero;
            }

            if (a > LFloat.one)
            {
                return LFloat.one;
            }

            return a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool SameSign(LFloat a, LFloat b)
        {
            return (long)a._val * b._val > 0L;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Abs(int val)
        {
            if (val < 0)
            {
                return -val;
            }

            return val;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Abs(long val)
        {
            if (val < 0L)
            {
                return -val;
            }

            return val;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Abs(LFloat val)
        {
            if (val._val < 0)
            {
                return new LFloat(true, -val._val);
            }

            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 Abs(LFloat2 val)
        {
            return new LFloat2(true, math.abs(val._val));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Sign(LFloat val)
        {
            return System.Math.Sign(val._val);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Round(LFloat val)
        {
            if (val <= 0)
            {
                var remainder = (-val._val) % LFloat.P1000;
                if (remainder > LFloat.HalfP1000)
                {
                    return new LFloat(true, val._val + remainder - LFloat.P1000);
                }
                else
                {
                    return new LFloat(true, val._val + remainder);
                }
            }
            else
            {
                var remainder = (val._val) % LFloat.P1000;
                if (remainder > LFloat.HalfP1000)
                {
                    return new LFloat(true, val._val - remainder + LFloat.P1000);
                }
                else
                {
                    return new LFloat(true, val._val - remainder);
                }
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Max(long a, long b)
        {
            return (a <= b) ? b : a;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Max(int a, int b)
        {
            return (a <= b) ? b : a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Max(LFloat a, LFloat b)
        {
            return (a <= b) ? b : a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 Max(LFloat2 a, LFloat2 b)
        {
            return math.all(a <= b) ? b : a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 Max(LFloat3 a, LFloat3 b)
        {
            return math.all(a <= b) ? b : a;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Min(long a, long b)
        {
            return (a > b) ? b : a;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Min(int a, int b)
        {
            return (a > b) ? b : a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Min(LFloat a, LFloat b)
        {
            return (a > b) ? b : a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 Min(LFloat2 a, LFloat2 b)
        {
            return math.all(a > b) ? b : a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 Min(LFloat3 a, LFloat3 b)
        {
            return math.all(a > b) ? b : a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Min(params int[] values)
        {
            int length = values.Length;
            if (length == 0)
                return 0;
            int num = values[0];
            for (int index = 1; index < length; ++index)
            {
                if (values[index] < num)
                    num = values[index];
            }
            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Min(params LFloat[] values)
        {
            int length = values.Length;
            if (length == 0)
                return LFloat.zero;
            LFloat num = values[0];
            for (int index = 1; index < length; ++index)
            {
                if (values[index] < num)
                    num = values[index];
            }
            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Max(params int[] values)
        {
            int length = values.Length;
            if (length == 0)
                return 0;
            int num = values[0];
            for (int index = 1; index < length; ++index)
            {
                if (values[index] > num)
                    num = values[index];
            }
            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Max(params LFloat[] values)
        {
            int length = values.Length;
            if (length == 0)
                return LFloat.zero;
            var num = values[0];
            for (int index = 1; index < length; ++index)
            {
                if (values[index] > num)
                    num = values[index];
            }
            return num;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FloorToInt(LFloat a)
        {
            var val = a._val;
            if (val < 0)
            {
                val = val - LFloat.P1000 + 1;
            }
            return val / LFloat.P1000;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat ToLFloat(float a)
        {
            return new LFloat(true, (int)(a * LFloat.P1000));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat ToLFloat(int a)
        {
            return new LFloat(true, (int)(a * LFloat.P1000));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat ToLFloat(long a)
        {
            return new LFloat(true, (int)(a * LFloat.P1000));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Lerp(LFloat a, LFloat b, LFloat f)
        {
            return new LFloat(true, (int)(((long)(b._val - a._val) * f._val) / LFloat.P1000) + a._val);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat InverseLerp(LFloat a, LFloat b, LFloat value)
        {
            if (a != b)
                return Clamp01(((value - a) / (b - a)));
            return LFloat.zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat2 Lerp(LFloat2 a, LFloat2 b, LFloat f)
        {
            return new LFloat2(true, (b._val - a._val) * f._val / LFloat.P1000 + a._val);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 Lerp(LFloat3 a, LFloat3 b, LFloat f)
        {
            return new LFloat3(true, (b._val - a._val) * f._val / LFloat.P1000 + a._val);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo(int x)
        {
            return (x & x - 1) == 0;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CeilPowerOfTwo(int x)
        {
            x--;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            x++;
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Floor(LFloat val)
        {
            int x = val._val;
            if (x > 0)
            {
                x /= LFloat.P1000;
            }
            else
            {
                if (x % LFloat.P1000 == 0)
                {
                    x /= LFloat.P1000;
                }
                else
                {
                    x = x / LFloat.P1000 - 1;
                }
            }

            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Ceil(LFloat val)
        {
            int x = val._val;
            if (x < 0)
            {
                x /= LFloat.P1000;
            }
            else
            {
                if (x % LFloat.P1000 == 0)
                {
                    x /= LFloat.P1000;
                }
                else
                {
                    x = x / LFloat.P1000 + 1;
                }
            }

            return x;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 Select(LFloat3 a, LFloat3 b, bool c)
        {
            return c ? b : a;
        }

        public static LFloat Dot(LFloat2 u, LFloat2 v)
        {
            return new LFloat(true, ((long)u._val.x * v._val.x + (long)u._val.y * v._val.y) / LFloat.P1000);
        }
        public static long DotLong(LFloat2 u, LFloat2 v)
        {
            return ((long)u._val.x * v._val.x + (long)u._val.y * v._val.y) / LFloat.P1000;
        }

        public static LFloat Dot(LFloat3 lhs, LFloat3 rhs)
        {
            var val = ((long)lhs._val.x) * rhs._val.x + ((long)lhs._val.y) * rhs._val.y + ((long)lhs._val.z) * rhs._val.z;
            return new LFloat(true, val / LFloat.P1000);
            ;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat RSqrt(LFloat x) { return LFloat.one / Sqrt(x); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 RSqrt(LFloat3 x) { return LFloat.one / Sqrt(x); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 Sqrt(LFloat3 x)
        {
            return new LFloat3(Sqrt(x._val.x / LFloat.P1000), Sqrt(x._val.y / LFloat.P1000), Sqrt(x._val.z / LFloat.P1000));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Length(LFloat2 x)
        {
            return Sqrt(Dot(x, x));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat LengthSq(LFloat2 x)
        {
            return Dot(x, x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat LengthSq(LFloat3 x)
        {
            return Dot(x, x);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat DistanceSq(LFloat2 x, LFloat2 y)
        {
            return LengthSq(y - x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat3 Cross(LFloat3 lhs, LFloat3 rhs)
        {
            return new LFloat3(true,
                ((long)lhs._val.y * rhs._val.z - (long)lhs._val.z * rhs._val.y) / LFloat.P1000,
                ((long)lhs._val.z * rhs._val.x - (long)lhs._val.x * rhs._val.z) / LFloat.P1000,
                ((long)lhs._val.x * rhs._val.y - (long)lhs._val.y * rhs._val.x) / LFloat.P1000
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Cross(LFloat2 u, LFloat2 v)
        {
            return new LFloat(true, ((long)u._val.x * v._val.y - (long)u._val.y * v._val.x) / LFloat.P1000);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat GetAngle(LComplex c)
        {
            return LMath.Atan2(c.Real, c.Imaginary);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LFloat Mod(LComplex c)
        {
            return LMath.Sqrt(c.Real * c.Real + c.Imaginary * c.Imaginary);
        }
    }
}