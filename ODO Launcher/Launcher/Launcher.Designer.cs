namespace ODO_Launcher
{
    partial class Launcher
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
            this.navPanel = new System.Windows.Forms.Panel();
            this.minimizeButton = new System.Windows.Forms.PictureBox();
            this.exitButton = new System.Windows.Forms.PictureBox();
            this.navPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minimizeButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitButton)).BeginInit();
            this.SuspendLayout();
            // 
            // navPanel
            // 
            this.navPanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.navPanel.Controls.Add(this.minimizeButton);
            this.navPanel.Controls.Add(this.exitButton);
            this.navPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.navPanel.Location = new System.Drawing.Point(0, 0);
            this.navPanel.Name = "navPanel";
            this.navPanel.Size = new System.Drawing.Size(800, 25);
            this.navPanel.TabIndex = 0;
            this.navPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.NavPanel_MouseMove);
            // 
            // minimizeButton
            // 
            this.minimizeButton.BackgroundImage = global::ODO_Launcher.Properties.Resources.minus;
            this.minimizeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.minimizeButton.Location = new System.Drawing.Point(756, 5);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(15, 15);
            this.minimizeButton.TabIndex = 1;
            this.minimizeButton.TabStop = false;
            this.minimizeButton.Click += new System.EventHandler(this.MinimizeButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.BackgroundImage = global::ODO_Launcher.Properties.Resources.x;
            this.exitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.exitButton.Location = new System.Drawing.Point(777, 5);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(15, 15);
            this.exitButton.TabIndex = 0;
            this.exitButton.TabStop = false;
            this.exitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.navPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Launcher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ODO Launcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Launcher_FormClosing);
            this.Load += new System.EventHandler(this.Launcher_Load);
            this.Shown += new System.EventHandler(this.Launcher_Shown);
            this.navPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.minimizeButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel navPanel;
        private System.Windows.Forms.PictureBox exitButton;
        private System.Windows.Forms.PictureBox minimizeButton;
    }
}

