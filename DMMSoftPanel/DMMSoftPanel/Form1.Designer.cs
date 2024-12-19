namespace DMMSoftPanel
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Measure_btn = new System.Windows.Forms.Button();
            this.Close_btn = new System.Windows.Forms.Button();
            this.Reading_tb = new System.Windows.Forms.TextBox();
            this.DC_Volts_rb = new System.Windows.Forms.RadioButton();
            this.AC_Volts_rb = new System.Windows.Forms.RadioButton();
            this.Resistance_rb = new System.Windows.Forms.RadioButton();
            this.AC_Current_rb = new System.Windows.Forms.RadioButton();
            this.DC_Current_rb = new System.Windows.Forms.RadioButton();
            this.Driver_tb = new System.Windows.Forms.TextBox();
            this.Open_btn = new System.Windows.Forms.Button();
            this.Capacitance_rb = new System.Windows.Forms.RadioButton();
            this.Inductance_rb = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Measure_btn
            // 
            this.Measure_btn.Location = new System.Drawing.Point(204, 196);
            this.Measure_btn.Name = "Measure_btn";
            this.Measure_btn.Size = new System.Drawing.Size(75, 23);
            this.Measure_btn.TabIndex = 0;
            this.Measure_btn.Text = "Measure";
            this.Measure_btn.UseVisualStyleBackColor = true;
            this.Measure_btn.Click += new System.EventHandler(this.Measure_btn_Click);
            // 
            // Close_btn
            // 
            this.Close_btn.Location = new System.Drawing.Point(323, 276);
            this.Close_btn.Name = "Close_btn";
            this.Close_btn.Size = new System.Drawing.Size(75, 23);
            this.Close_btn.TabIndex = 1;
            this.Close_btn.Text = "Close";
            this.Close_btn.UseVisualStyleBackColor = true;
            this.Close_btn.Click += new System.EventHandler(this.Close_btn_Click);
            // 
            // Reading_tb
            // 
            this.Reading_tb.Location = new System.Drawing.Point(298, 199);
            this.Reading_tb.Name = "Reading_tb";
            this.Reading_tb.Size = new System.Drawing.Size(100, 20);
            this.Reading_tb.TabIndex = 2;
            // 
            // DC_Volts_rb
            // 
            this.DC_Volts_rb.AutoSize = true;
            this.DC_Volts_rb.Checked = true;
            this.DC_Volts_rb.Location = new System.Drawing.Point(207, 112);
            this.DC_Volts_rb.Name = "DC_Volts_rb";
            this.DC_Volts_rb.Size = new System.Drawing.Size(66, 17);
            this.DC_Volts_rb.TabIndex = 3;
            this.DC_Volts_rb.TabStop = true;
            this.DC_Volts_rb.Text = "DC Volts";
            this.DC_Volts_rb.UseVisualStyleBackColor = true;
            // 
            // AC_Volts_rb
            // 
            this.AC_Volts_rb.AutoSize = true;
            this.AC_Volts_rb.Location = new System.Drawing.Point(298, 112);
            this.AC_Volts_rb.Name = "AC_Volts_rb";
            this.AC_Volts_rb.Size = new System.Drawing.Size(65, 17);
            this.AC_Volts_rb.TabIndex = 4;
            this.AC_Volts_rb.Text = "AC Volts";
            this.AC_Volts_rb.UseVisualStyleBackColor = true;
            // 
            // Resistance_rb
            // 
            this.Resistance_rb.AutoSize = true;
            this.Resistance_rb.Location = new System.Drawing.Point(380, 89);
            this.Resistance_rb.Name = "Resistance_rb";
            this.Resistance_rb.Size = new System.Drawing.Size(78, 17);
            this.Resistance_rb.TabIndex = 5;
            this.Resistance_rb.Text = "Resistance";
            this.Resistance_rb.UseVisualStyleBackColor = true;
            // 
            // AC_Current_rb
            // 
            this.AC_Current_rb.AutoSize = true;
            this.AC_Current_rb.Location = new System.Drawing.Point(298, 89);
            this.AC_Current_rb.Name = "AC_Current_rb";
            this.AC_Current_rb.Size = new System.Drawing.Size(76, 17);
            this.AC_Current_rb.TabIndex = 7;
            this.AC_Current_rb.Text = "AC Current";
            this.AC_Current_rb.UseVisualStyleBackColor = true;
            // 
            // DC_Current_rb
            // 
            this.DC_Current_rb.AutoSize = true;
            this.DC_Current_rb.Location = new System.Drawing.Point(207, 89);
            this.DC_Current_rb.Name = "DC_Current_rb";
            this.DC_Current_rb.Size = new System.Drawing.Size(77, 17);
            this.DC_Current_rb.TabIndex = 6;
            this.DC_Current_rb.Text = "DC Current";
            this.DC_Current_rb.UseVisualStyleBackColor = true;
            // 
            // Driver_tb
            // 
            this.Driver_tb.Location = new System.Drawing.Point(12, 27);
            this.Driver_tb.Name = "Driver_tb";
            this.Driver_tb.Size = new System.Drawing.Size(623, 20);
            this.Driver_tb.TabIndex = 8;
            // 
            // Open_btn
            // 
            this.Open_btn.Location = new System.Drawing.Point(641, 27);
            this.Open_btn.Name = "Open_btn";
            this.Open_btn.Size = new System.Drawing.Size(75, 23);
            this.Open_btn.TabIndex = 9;
            this.Open_btn.Text = "Open";
            this.Open_btn.UseVisualStyleBackColor = true;
            this.Open_btn.Click += new System.EventHandler(this.Open_btn_Click);
            // 
            // Capacitance_rb
            // 
            this.Capacitance_rb.AutoSize = true;
            this.Capacitance_rb.Location = new System.Drawing.Point(380, 112);
            this.Capacitance_rb.Name = "Capacitance_rb";
            this.Capacitance_rb.Size = new System.Drawing.Size(85, 17);
            this.Capacitance_rb.TabIndex = 10;
            this.Capacitance_rb.Text = "Capacitance";
            this.Capacitance_rb.UseVisualStyleBackColor = true;
            // 
            // Inductance_rb
            // 
            this.Inductance_rb.AutoSize = true;
            this.Inductance_rb.Location = new System.Drawing.Point(207, 135);
            this.Inductance_rb.Name = "Inductance_rb";
            this.Inductance_rb.Size = new System.Drawing.Size(79, 17);
            this.Inductance_rb.TabIndex = 11;
            this.Inductance_rb.Text = "Inductance";
            this.Inductance_rb.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Driver:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 320);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Inductance_rb);
            this.Controls.Add(this.Capacitance_rb);
            this.Controls.Add(this.Open_btn);
            this.Controls.Add(this.Driver_tb);
            this.Controls.Add(this.AC_Current_rb);
            this.Controls.Add(this.DC_Current_rb);
            this.Controls.Add(this.Resistance_rb);
            this.Controls.Add(this.AC_Volts_rb);
            this.Controls.Add(this.DC_Volts_rb);
            this.Controls.Add(this.Reading_tb);
            this.Controls.Add(this.Close_btn);
            this.Controls.Add(this.Measure_btn);
            this.Name = "Form1";
            this.Text = "DMM Soft Panel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Measure_btn;
        private System.Windows.Forms.Button Close_btn;
        private System.Windows.Forms.TextBox Reading_tb;
        private System.Windows.Forms.RadioButton DC_Volts_rb;
        private System.Windows.Forms.RadioButton AC_Volts_rb;
        private System.Windows.Forms.RadioButton Resistance_rb;
        private System.Windows.Forms.RadioButton AC_Current_rb;
        private System.Windows.Forms.RadioButton DC_Current_rb;
        private System.Windows.Forms.TextBox Driver_tb;
        private System.Windows.Forms.Button Open_btn;
        private System.Windows.Forms.RadioButton Capacitance_rb;
        private System.Windows.Forms.RadioButton Inductance_rb;
        private System.Windows.Forms.Label label1;
    }
}

