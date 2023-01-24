using System;
using System.Runtime.CompilerServices;

namespace Lockstep.Math
{
    [Serializable]
    public struct LComplex
    {
        public LFloat Real;//实部
        public LFloat Imaginary;//虚部


        public static readonly LComplex ImaginaryOne = new LComplex(LFloat.zero, LFloat.one);



        public LComplex(LFloat real, LFloat imaginary)
        {
            this.Real = real;
            this.Imaginary = imaginary;
        }

        /// <summary>
        /// 传入的是正常数放大1000 的数值
        /// </summary>
        /// <param name="rawVal"></param>
        public LComplex(bool isUseRawVal, int real, int imaginary)
        {
            this.Real = new LFloat(true, real);
            this.Imaginary = new LFloat(true, imaginary);
        }

        public LComplex(int real, int imaginary)
        {
            this.Real = new LFloat(real);
            this.Imaginary = new LFloat(imaginary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LComplex operator +(LComplex a, LComplex b)
        {
            return new LComplex(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LComplex operator -(LComplex a, LComplex b)
        {
            return new LComplex(a.Real - b.Real, a.Imaginary - b.Imaginary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LComplex operator *(LComplex a, LComplex b)
        {
            return new LComplex(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LComplex operator /(LComplex a, LComplex b)
        {
            var real = (a.Real * b.Real + a.Imaginary * b.Imaginary) / (b.Real * b.Real + b.Imaginary + b.Imaginary);
            var image = (a.Imaginary * b.Real - a.Real * b.Imaginary) / (b.Real * b.Real + b.Imaginary + b.Imaginary);
            return new LComplex(real, image);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LComplex operator +(LComplex a, LFloat b)
        {
            return new LComplex(a.Real + b, a.Imaginary + b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LComplex operator +(LFloat b, LComplex a)
        {
            return new LComplex(a.Real + b, a.Imaginary + b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LComplex operator -(LComplex a, LFloat b)
        {
            return new LComplex(a.Real - b, a.Imaginary - b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LComplex operator *(LFloat b, LComplex a)
        {
            return new LComplex(a.Real * b - a.Imaginary * b, a.Real * b + a.Imaginary * b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LComplex operator *(LComplex a, LFloat b)
        {
            return new LComplex(a.Real * b - a.Imaginary * b, a.Real * b + a.Imaginary * b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LComplex operator /(LComplex a, LFloat b)
        {
            var real = (a.Real * b + a.Imaginary * b) / b;
            var image = (a.Imaginary * b - a.Real * b) / b;
            return new LComplex(real, image);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(LComplex a, LComplex b)
        {
            return (a.Real == b.Real && a.Imaginary == b.Imaginary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(LComplex a, LComplex b)
        {
            return (a.Real != b.Real || a.Imaginary != b.Imaginary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            if (obj is LComplex)
            {
                LComplex com = (LComplex)obj;
                return (com.Real == this.Real && com.Imaginary == this.Imaginary);
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(LComplex other)
        {
            return other.Real == this.Real && other.Imaginary == this.Imaginary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("<{0} , {1}>", this.Real / LFloat.P1000, this.Imaginary / LFloat.P1000);
        }
    }
}
