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
        [DllImport(@"C:\Users\Richard\Documents\Visual Studio 2015\Projects\C++DLL\DailyMemory_KORA_DLL\Debug\KORA_DLL.dll")]

        
        public static extern unsafe double myKODRA_Price(System.Int32 no_asset, System.Int32 no_watchday, double y, double r,
            System.Int32 g, double H, double couponRate, double fixcouponRate, double put_k, 
            double[] spot, double[] spot2, double[] vol, double[] corr, double[] stk);


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime time_start = DateTime.Now;  //計時開始 取得目前時間
            System.Int32 no_asset = 3;
            System.Int32 no_watchday = 106;
            double y = 0.0065;
            double r = 0.02;
            System.Int32 g = 23;
            double H = 1.0;
            double couponRate = 0.11;
            double fixcouponRate = 0.01;
            double put_k = 0.85;
            System.Int32 no_day = no_watchday + g;

            double[] spot = new double[] { 22, 300, 25 };
            double[] spot2 = new double[] { 22, 300, 25 };
            double[] vol = new double[] { 0.42360385963825, 0.233820701622964, 0.308722283764962 };
            double[] corr = new double[] { 1, 0.118744878050298, 0.0180315386099429, 0.118744878050298, 1, 0.300146377714237, 0.0180315386099429, 0.300146377714237, 1 };
            double[] stk = new double[381];
            /*
            stk[0*(no_day+1)] = spot[0];
            stk[1*(no_day+1)] = spot[1];
            stk[2*(no_day+1)] = spot[2];
            */
            stk[0] = spot[0];
            stk[1] = spot[1];
            stk[2] = spot[2];
            double price = 0;
            price = myKODRA_Price(no_asset, no_watchday, y, r, g, H, couponRate, fixcouponRate, put_k, spot, spot2, vol, corr, stk);

            DateTime time_end = DateTime.Now;  //計時結束 取得目前時間
            double timediff = ((TimeSpan)(time_end - time_start)).TotalMilliseconds;  //毫秒

            textBox1.Text = "price = "+Convert.ToString(price*100) + "%";
            MessageBox.Show("KORA price = " + price*100 + "%" + Environment.NewLine +
                            "花費時間 =" + timediff/1000 + "秒");

            


        }
    }
}
