namespace VNAControl.Forms
{
    using BasicLibraries.VNAControl.Format;
    using System.Drawing;
    using System.Windows.Forms;
    using ZedGraph;

    internal partial class FrmCalChart : Form
    {
        internal VNADataFormat TestData { get; set; }
        internal FrmCalChart()
        {
            InitializeComponent();
            InitZed(zedGraphControl1);
        }

        /// <summary>
        /// 初始化Chart
        /// </summary>
        /// <param name="zed"></param>
        private void InitZed(ZedGraphControl zed)
        {
            GraphPane pane = zed.GraphPane;
            pane.Title.Text = "";
            pane.XAxis.Title.Text = "Frequency(MHz)";
            pane.YAxis.Title.Text = "Value";
            pane.Border.IsVisible = false;
            pane.Fill = new Fill(Color.White, Color.FromArgb(200, 200, 255), 45.0f);
            pane.Border = new Border(false, Color.Gray, 2.0F);    // 取消图表边框
            pane.Legend.Border = new Border(false, Color.Black, 10);   // 取消图例边框
            pane.Legend.FontSpec.IsBold = false;
            pane.Legend.FontSpec.Size = 8f;
            pane.Legend.FontSpec.Family = "楷体";
            pane.Chart.Border.IsVisible = false;//首先设置边框为无
            pane.XAxis.MajorTic.IsOpposite = false;  //X轴对面轴大间隔为无
            pane.XAxis.MinorTic.IsOpposite = false;  //X轴对面轴小间隔为无
            pane.YAxis.MajorTic.IsOpposite = false;  //Y轴对面轴大间隔为无
            pane.YAxis.MinorTic.IsOpposite = false;  //Y轴对面轴小间隔为无
        }

        /// <summary>
        /// 画曲线
        /// </summary>
        /// <param name="data"></param>
        public void UpdateZed(VNADataFormat data)
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            if (data.MajorValueOfTheData != null)
            {
                double[] d = new double[data.Frequency.Length];
                for (int i = 0; i < data.Frequency.Length; i++)
                {
                    d[i] = data.Frequency[i] / 1000000;
                }
                zedGraphControl1.GraphPane.AddCurve("Cal", d, data.MajorValueOfTheData, Color.Green, SymbolType.None);
            }
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
            zedGraphControl1.Refresh();
        }
    }
}
