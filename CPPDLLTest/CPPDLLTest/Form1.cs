using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;  //引用C++的DLL,需加此行

namespace CPPDLLTest
{
    public partial class Form1 : Form
    {
        [DllImport(@"C:\Users\Richard\Documents\Visual Studio 2015\Projects\C++\DailyMemory_KORA\Debug\DailyMemory_KORA.dll")]
        public static extern unsafe double myDRAKO_Price(System.Int32 no_asset, System.Int32 no_watchday, System.Int32 LBarrier, double y, double r,
            System.Int32 g, double H, double couponRate, double fixcouponRate, double put_k, double L,
            double[] spot, double[] div, double[] vol, double[] corr, double[] stk, System.Int32 n_trial);


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime time_start = DateTime.Now;  //計時開始 取得目前時間
            System.Int32 no_asset = 3;
            System.Int32 no_watchday = 106;
            System.Int32 LBarrier = 0;
            double y = 0.02;
            double r = 0.02;
            System.Int32 g = 21;
            double H = 1.0;
            double couponRate = 0.11;
            double fixcouponRate = 0.01;
            double put_k = 0.85;
            double L = 0.85;
            System.Int32 no_day = no_watchday + g;
            System.Int32 n_trial = 10000;

            double[] spot = new double[] { 22, 300, 25 };
            double[] div = new double[] { 0.02, 0.02, 0.02 };
            double[] vol = new double[] { 0.42360385963825, 0.233820701622964, 0.308722283764962 };
            double[] corr = new double[] { 1, 0.118744878050298, 0.0180315386099429, 0.118744878050298, 1, 0.300146377714237, 0.0180315386099429, 0.300146377714237, 1 };
            double[] stk = new double[381];

            double price = 0;
            price = myDRAKO_Price(no_asset, no_watchday, LBarrier, y, r,
                            g, H, couponRate, fixcouponRate, put_k, L,
                            spot, div, vol, corr, stk, n_trial);

            DateTime time_end = DateTime.Now;  //計時結束 取得目前時間
            double timediff = ((TimeSpan)(time_end - time_start)).TotalMilliseconds;  //毫秒

            textBox1.Text = "price = "+Convert.ToString(price*100) + "%";
            MessageBox.Show("KORA price = " + price*100 + "%" + Environment.NewLine +
                            "花費時間 =" + timediff/1000 + "秒");

            


        }
    }
}
