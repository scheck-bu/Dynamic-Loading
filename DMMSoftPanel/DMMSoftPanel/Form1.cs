using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DMMInterface;
using DmmAdditionalInterface;


namespace DMMSoftPanel
{
    /// <summary>
    /// Soft Panel for testing DMM drivers
    /// </summary>
    public partial class Form1 : Form
    {
        IDMM SimpleDmm;
        IDmmPlus AdvancedDmm;
        dynamic dmm = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Measure_btn_Click(object sender, EventArgs e)
        {
            double reading = 0;
            if (DC_Volts_rb.Checked)
                reading = dmm.DC.Voltage.measure();
            else if (AC_Volts_rb.Checked)
                reading = dmm.AC.Voltage.measure();
            else if (Resistance_rb.Checked)
            {
                dmm.Resistance._2W.Range = IDMM.IResistance.I2W.RangeEnum._1M;
                reading = dmm.Resistance._2W.measure();
                IDMM.IResistance.I2W.RangeEnum r = dmm.Resistance._2W.Range;
                
            }
            else if (DC_Current_rb.Checked)
                reading = dmm.DC.Current.measure();
            else if (AC_Current_rb.Checked)
                reading = dmm.AC.Current.measure();
            else if (Capacitance_rb.Checked)
                reading = dmm.Capacitance.measure();
            else if (Inductance_rb.Checked)
                reading = dmm.Inductance.measure();

            Reading_tb.Text = reading.ToString();
        }

        object CreateInstrument(Assembly lib, Object[] args, string instrumentType)
        {
            object Instrument = null;
            Type[] ts;
            ts = lib.GetTypes();
            Type instType = null;

            int cntr = 0;

            if (instrumentType != "")
            {
                for (cntr = 0; cntr < ts.Length; cntr++)
                {
                    if (ts[cntr].Name == instrumentType) break;
                }
            }
            instType = ts[cntr];

            try
            {
                Instrument = Activator.CreateInstance(instType, args);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                throw new Exception("DMM:CreateInstrument: " + ex.Message);
            }

            return Instrument;
        }

        private void Close_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Open_btn_Click(object sender, EventArgs e)
        {
            Assembly instrumentLibrary = Assembly.LoadFrom(Driver_tb.Text);

            SimpleDmm = (IDMM)CreateInstrument(instrumentLibrary, null, "");
            dmm = SimpleDmm;
   
        }
    }
}
