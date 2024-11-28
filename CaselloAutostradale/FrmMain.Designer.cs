namespace CaselloAutostradale
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            GbBarrier = new GroupBox();
            PbBarrier = new PictureBox();
            BtnSimulation = new Button();
            GbStoplight = new GroupBox();
            PbStopLight = new PictureBox();
            GbConfigurazione = new GroupBox();
            LblAllowedPlates = new Label();
            TbAllowedPlates = new TextBox();
            BtnSendConfig = new Button();
            LblBarrierDelay = new Label();
            TbBarrierDelay = new TextBox();
            LblMotorSpeed = new Label();
            TbMotorDelay = new TextBox();
            GbManual = new GroupBox();
            BtnManualStoplight = new Button();
            BtnManualUpDown = new Button();
            GbRealTimeData = new GroupBox();
            LblRefused = new Label();
            TbRefused = new TextBox();
            LblCarCount = new Label();
            TbCarCount = new TextBox();
            LblLastPlate = new Label();
            TbLastPlate = new TextBox();
            TbSerial = new RichTextBox();
            GbBarrier.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PbBarrier).BeginInit();
            GbStoplight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PbStopLight).BeginInit();
            GbConfigurazione.SuspendLayout();
            GbManual.SuspendLayout();
            GbRealTimeData.SuspendLayout();
            SuspendLayout();
            // 
            // GbBarrier
            // 
            GbBarrier.BackColor = Color.White;
            GbBarrier.Controls.Add(PbBarrier);
            GbBarrier.Location = new Point(22, 18);
            GbBarrier.Name = "GbBarrier";
            GbBarrier.Size = new Size(1071, 442);
            GbBarrier.TabIndex = 0;
            GbBarrier.TabStop = false;
            GbBarrier.Text = "Status Barriera";
            // 
            // PbBarrier
            // 
            PbBarrier.Dock = DockStyle.Fill;
            PbBarrier.Location = new Point(3, 23);
            PbBarrier.Name = "PbBarrier";
            PbBarrier.Size = new Size(1065, 416);
            PbBarrier.SizeMode = PictureBoxSizeMode.Zoom;
            PbBarrier.TabIndex = 0;
            PbBarrier.TabStop = false;
            // 
            // BtnSimulation
            // 
            BtnSimulation.Location = new Point(12, 717);
            BtnSimulation.Name = "BtnSimulation";
            BtnSimulation.Size = new Size(94, 29);
            BtnSimulation.TabIndex = 1;
            BtnSimulation.Text = "Simula";
            BtnSimulation.UseVisualStyleBackColor = true;
            BtnSimulation.Click += Simulate;
            // 
            // GbStoplight
            // 
            GbStoplight.BackColor = Color.White;
            GbStoplight.Controls.Add(PbStopLight);
            GbStoplight.Location = new Point(1099, 18);
            GbStoplight.Name = "GbStoplight";
            GbStoplight.Size = new Size(339, 439);
            GbStoplight.TabIndex = 2;
            GbStoplight.TabStop = false;
            GbStoplight.Text = "Semaforo";
            // 
            // PbStopLight
            // 
            PbStopLight.Dock = DockStyle.Fill;
            PbStopLight.Location = new Point(3, 23);
            PbStopLight.Name = "PbStopLight";
            PbStopLight.Size = new Size(333, 413);
            PbStopLight.SizeMode = PictureBoxSizeMode.Zoom;
            PbStopLight.TabIndex = 0;
            PbStopLight.TabStop = false;
            // 
            // GbConfigurazione
            // 
            GbConfigurazione.Controls.Add(LblAllowedPlates);
            GbConfigurazione.Controls.Add(TbAllowedPlates);
            GbConfigurazione.Controls.Add(BtnSendConfig);
            GbConfigurazione.Controls.Add(LblBarrierDelay);
            GbConfigurazione.Controls.Add(TbBarrierDelay);
            GbConfigurazione.Controls.Add(LblMotorSpeed);
            GbConfigurazione.Controls.Add(TbMotorDelay);
            GbConfigurazione.Location = new Point(25, 466);
            GbConfigurazione.Name = "GbConfigurazione";
            GbConfigurazione.Size = new Size(615, 240);
            GbConfigurazione.TabIndex = 3;
            GbConfigurazione.TabStop = false;
            GbConfigurazione.Text = "Configurazione";
            // 
            // LblAllowedPlates
            // 
            LblAllowedPlates.AutoSize = true;
            LblAllowedPlates.Location = new Point(6, 136);
            LblAllowedPlates.Name = "LblAllowedPlates";
            LblAllowedPlates.Size = new Size(119, 20);
            LblAllowedPlates.TabIndex = 10;
            LblAllowedPlates.Text = "Targhe ammesse";
            // 
            // TbAllowedPlates
            // 
            TbAllowedPlates.Location = new Point(215, 133);
            TbAllowedPlates.Name = "TbAllowedPlates";
            TbAllowedPlates.Size = new Size(374, 27);
            TbAllowedPlates.TabIndex = 9;
            TbAllowedPlates.TextChanged += TbAllowedPlates_TextChanged;
            // 
            // BtnSendConfig
            // 
            BtnSendConfig.Enabled = false;
            BtnSendConfig.Location = new Point(500, 193);
            BtnSendConfig.Name = "BtnSendConfig";
            BtnSendConfig.Size = new Size(109, 29);
            BtnSendConfig.TabIndex = 6;
            BtnSendConfig.Text = "Salva";
            BtnSendConfig.UseVisualStyleBackColor = true;
            BtnSendConfig.Click += BtnSendConfig_Click;
            // 
            // LblBarrierDelay
            // 
            LblBarrierDelay.AutoSize = true;
            LblBarrierDelay.Location = new Point(6, 88);
            LblBarrierDelay.Name = "LblBarrierDelay";
            LblBarrierDelay.Size = new Size(203, 20);
            LblBarrierDelay.TabIndex = 8;
            LblBarrierDelay.Text = "Attesa chiusura (millisecondi)";
            // 
            // TbBarrierDelay
            // 
            TbBarrierDelay.Location = new Point(215, 85);
            TbBarrierDelay.Name = "TbBarrierDelay";
            TbBarrierDelay.Size = new Size(374, 27);
            TbBarrierDelay.TabIndex = 7;
            TbBarrierDelay.Text = "2000";
            TbBarrierDelay.TextChanged += TbMotorDelay_TextChanged;
            // 
            // LblMotorSpeed
            // 
            LblMotorSpeed.AutoSize = true;
            LblMotorSpeed.Location = new Point(6, 39);
            LblMotorSpeed.Name = "LblMotorSpeed";
            LblMotorSpeed.Size = new Size(187, 20);
            LblMotorSpeed.TabIndex = 6;
            LblMotorSpeed.Text = "Velocità movimento sbarra";
            // 
            // TbMotorDelay
            // 
            TbMotorDelay.Location = new Point(215, 36);
            TbMotorDelay.Name = "TbMotorDelay";
            TbMotorDelay.Size = new Size(374, 27);
            TbMotorDelay.TabIndex = 5;
            TbMotorDelay.Text = "3";
            TbMotorDelay.TextChanged += TbMotorDelay_TextChanged;
            // 
            // GbManual
            // 
            GbManual.Controls.Add(BtnManualStoplight);
            GbManual.Controls.Add(BtnManualUpDown);
            GbManual.Location = new Point(938, 466);
            GbManual.Name = "GbManual";
            GbManual.Size = new Size(497, 136);
            GbManual.TabIndex = 4;
            GbManual.TabStop = false;
            GbManual.Text = "Comandi manuali";
            // 
            // BtnManualStoplight
            // 
            BtnManualStoplight.Location = new Point(16, 89);
            BtnManualStoplight.Name = "BtnManualStoplight";
            BtnManualStoplight.Size = new Size(475, 36);
            BtnManualStoplight.TabIndex = 10;
            BtnManualStoplight.Text = "Verde/Rosso semaforo";
            BtnManualStoplight.UseVisualStyleBackColor = true;
            BtnManualStoplight.Click += BtnManualStoplight_Click;
            // 
            // BtnManualUpDown
            // 
            BtnManualUpDown.Location = new Point(16, 31);
            BtnManualUpDown.Name = "BtnManualUpDown";
            BtnManualUpDown.Size = new Size(475, 36);
            BtnManualUpDown.TabIndex = 9;
            BtnManualUpDown.Text = "Alza/Abbassa barriera";
            BtnManualUpDown.UseVisualStyleBackColor = true;
            BtnManualUpDown.Click += BtnManualUpDown_Click;
            // 
            // GbRealTimeData
            // 
            GbRealTimeData.Controls.Add(LblRefused);
            GbRealTimeData.Controls.Add(TbRefused);
            GbRealTimeData.Controls.Add(LblCarCount);
            GbRealTimeData.Controls.Add(TbCarCount);
            GbRealTimeData.Controls.Add(LblLastPlate);
            GbRealTimeData.Controls.Add(TbLastPlate);
            GbRealTimeData.Location = new Point(646, 466);
            GbRealTimeData.Name = "GbRealTimeData";
            GbRealTimeData.Size = new Size(286, 240);
            GbRealTimeData.TabIndex = 5;
            GbRealTimeData.TabStop = false;
            GbRealTimeData.Text = "Dati RealTime";
            // 
            // LblRefused
            // 
            LblRefused.AutoSize = true;
            LblRefused.Location = new Point(6, 170);
            LblRefused.Name = "LblRefused";
            LblRefused.Size = new Size(105, 20);
            LblRefused.TabIndex = 14;
            LblRefused.Text = "Veicoli rifiutati";
            // 
            // TbRefused
            // 
            TbRefused.Location = new Point(6, 193);
            TbRefused.Name = "TbRefused";
            TbRefused.ReadOnly = true;
            TbRefused.Size = new Size(267, 27);
            TbRefused.TabIndex = 13;
            TbRefused.Enter += TbLastPlate_Enter;
            // 
            // LblCarCount
            // 
            LblCarCount.AutoSize = true;
            LblCarCount.Location = new Point(6, 97);
            LblCarCount.Name = "LblCarCount";
            LblCarCount.Size = new Size(138, 20);
            LblCarCount.TabIndex = 12;
            LblCarCount.Text = "Conteggio sessione";
            // 
            // TbCarCount
            // 
            TbCarCount.Location = new Point(6, 120);
            TbCarCount.Name = "TbCarCount";
            TbCarCount.ReadOnly = true;
            TbCarCount.Size = new Size(267, 27);
            TbCarCount.TabIndex = 11;
            TbCarCount.Enter += TbLastPlate_Enter;
            // 
            // LblLastPlate
            // 
            LblLastPlate.AutoSize = true;
            LblLastPlate.Location = new Point(6, 29);
            LblLastPlate.Name = "LblLastPlate";
            LblLastPlate.Size = new Size(145, 20);
            LblLastPlate.TabIndex = 10;
            LblLastPlate.Text = "Ultima targa rilevata";
            // 
            // TbLastPlate
            // 
            TbLastPlate.Location = new Point(6, 52);
            TbLastPlate.Name = "TbLastPlate";
            TbLastPlate.ReadOnly = true;
            TbLastPlate.Size = new Size(267, 27);
            TbLastPlate.TabIndex = 9;
            TbLastPlate.Enter += TbLastPlate_Enter;
            // 
            // TbSerial
            // 
            TbSerial.Location = new Point(938, 617);
            TbSerial.Name = "TbSerial";
            TbSerial.Size = new Size(497, 89);
            TbSerial.TabIndex = 6;
            TbSerial.Text = "";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1450, 758);
            Controls.Add(TbSerial);
            Controls.Add(GbRealTimeData);
            Controls.Add(GbManual);
            Controls.Add(BtnSimulation);
            Controls.Add(GbConfigurazione);
            Controls.Add(GbStoplight);
            Controls.Add(GbBarrier);
            MaximizeBox = false;
            Name = "FrmMain";
            Text = "Casello Autostradale ITS Alto Adriatico";
            GbBarrier.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PbBarrier).EndInit();
            GbStoplight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PbStopLight).EndInit();
            GbConfigurazione.ResumeLayout(false);
            GbConfigurazione.PerformLayout();
            GbManual.ResumeLayout(false);
            GbRealTimeData.ResumeLayout(false);
            GbRealTimeData.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox GbBarrier;
        private PictureBox PbBarrier;
        private Button BtnSimulation;
        private GroupBox GbStoplight;
        private PictureBox PbStopLight;
        private GroupBox GbConfigurazione;
        private GroupBox GbManual;
        private GroupBox GbRealTimeData;
        private Label LblBarrierDelay;
        private TextBox TbBarrierDelay;
        private Label LblMotorSpeed;
        private TextBox TbMotorDelay;
        private Label LblCarCount;
        private TextBox TbCarCount;
        private Label LblLastPlate;
        private TextBox TbLastPlate;
        private Button BtnSendConfig;
        private Button BtnManualStoplight;
        private Button BtnManualUpDown;
        private Label LblAllowedPlates;
        private TextBox TbAllowedPlates;
        private Label LblRefused;
        private TextBox TbRefused;
        private RichTextBox TbSerial;
    }
}
