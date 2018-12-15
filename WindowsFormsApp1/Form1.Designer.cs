namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.bw = new System.ComponentModel.BackgroundWorker();
            this.lblInfo = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSetting = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.btnWriteFile = new System.Windows.Forms.PictureBox();
            this.btnConnection = new System.Windows.Forms.PictureBox();
            this.btnExtract = new System.Windows.Forms.PictureBox();
            this.btnOpenFileAwl = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnWriteFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnConnection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExtract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenFileAwl)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.AllowDrop = true;
            this.listBox1.BackColor = System.Drawing.SystemColors.Control;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(12, 96);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(201, 512);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // listBox2
            // 
            this.listBox2.AllowDrop = true;
            this.listBox2.BackColor = System.Drawing.SystemColors.Control;
            this.listBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 16;
            this.listBox2.Location = new System.Drawing.Point(236, 96);
            this.listBox2.Name = "listBox2";
            this.listBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox2.Size = new System.Drawing.Size(652, 512);
            this.listBox2.TabIndex = 11;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // bw
            // 
            this.bw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_DoWork);
            this.bw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bw_ProgressChanged);
            this.bw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_RunWorkerCompleted);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(347, 33);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(91, 32);
            this.lblInfo.TabIndex = 12;
            this.lblInfo.Text = "Texte";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(274, 10);
            this.progressBar1.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(295, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // btnSetting
            // 
            this.btnSetting.Image = global::OnlineToInitial.Properties.Resources.icons8_automatique_50;
            this.btnSetting.Location = new System.Drawing.Point(236, 24);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(50, 50);
            this.btnSetting.TabIndex = 21;
            this.btnSetting.TabStop = false;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = global::OnlineToInitial.Properties.Resources.icons8_sortie_50;
            this.btnClose.Location = new System.Drawing.Point(291, 24);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(50, 50);
            this.btnClose.TabIndex = 20;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnWriteFile
            // 
            this.btnWriteFile.Image = global::OnlineToInitial.Properties.Resources.icons8_enregistrer_sous_50;
            this.btnWriteFile.Location = new System.Drawing.Point(124, 24);
            this.btnWriteFile.Name = "btnWriteFile";
            this.btnWriteFile.Size = new System.Drawing.Size(50, 50);
            this.btnWriteFile.TabIndex = 19;
            this.btnWriteFile.TabStop = false;
            this.btnWriteFile.Click += new System.EventHandler(this.btnWriteFile_Click);
            // 
            // btnConnection
            // 
            this.btnConnection.Image = global::OnlineToInitial.Properties.Resources.icons8_connecté_50;
            this.btnConnection.Location = new System.Drawing.Point(180, 24);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(50, 50);
            this.btnConnection.TabIndex = 18;
            this.btnConnection.TabStop = false;
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // btnExtract
            // 
            this.btnExtract.Image = global::OnlineToInitial.Properties.Resources.icons8_précision_50;
            this.btnExtract.Location = new System.Drawing.Point(68, 24);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(50, 50);
            this.btnExtract.TabIndex = 17;
            this.btnExtract.TabStop = false;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // btnOpenFileAwl
            // 
            this.btnOpenFileAwl.Image = global::OnlineToInitial.Properties.Resources.icons8_dossier_50;
            this.btnOpenFileAwl.Location = new System.Drawing.Point(12, 24);
            this.btnOpenFileAwl.Name = "btnOpenFileAwl";
            this.btnOpenFileAwl.Size = new System.Drawing.Size(50, 50);
            this.btnOpenFileAwl.TabIndex = 16;
            this.btnOpenFileAwl.TabStop = false;
            this.btnOpenFileAwl.Click += new System.EventHandler(this.btnOpenFileAwl_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 620);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnWriteFile);
            this.Controls.Add(this.btnConnection);
            this.Controls.Add(this.btnExtract);
            this.Controls.Add(this.btnOpenFileAwl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.btnSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnWriteFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnConnection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExtract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenFileAwl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer;
        private System.ComponentModel.BackgroundWorker bw;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox btnOpenFileAwl;
        private System.Windows.Forms.PictureBox btnExtract;
        private System.Windows.Forms.PictureBox btnConnection;
        private System.Windows.Forms.PictureBox btnWriteFile;
        private System.Windows.Forms.PictureBox btnClose;
        private System.Windows.Forms.PictureBox btnSetting;
    }
}

