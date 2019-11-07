namespace MVVMCalc.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 計算を行うクラス
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// 計算方法を表すCalculateTypeと実際の処理のマップ
        /// </summary>
        private static readonly Dictionary<CalculateType, Func<double, double, double>> calcMap = new Dictionary<CalculateType, Func<double, double, double>>
        {
            // 未指定
            { 
                CalculateType.None, 
                (x, y) => 
                {
                    throw new InvalidOperationException();
                }
            },
            // 足し算
            {
                CalculateType.Add,
                (x, y) => x + y
            },
            // 引き算
            {
                CalculateType.Sub,
                (x, y) => x - y
            },
            // 掛け算
            {
                CalculateType.Mul,
                (x, y) => x * y
            },
            // 割り算
            {
                CalculateType.Div,
                (x, y) => x / y
            }
        };

        /// <summary>
        /// 渡された値の指定された計算結果を返す。
        /// opにCalculateType.Noneを渡すとInvalidOperationExceptionをスローします。
        /// </summary>
        /// <param name="x">左辺値</param>
        /// <param name="y">右辺値</param>
        /// <param name="op">計算方法</param>
        /// <returns>計算結果</returns>
        public double Execute(double x, double y, CalculateType op)
        {
            return calcMap[op](x, y);
        }
    }
}
