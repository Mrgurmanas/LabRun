
namespace ProjectClient
{
    partial class Form1
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
            this.txtConnection = new System.Windows.Forms.Label();
            this.btnJoin = new System.Windows.Forms.Button();
            this.btnLeave = new System.Windows.Forms.Button();
            this.txtConnectionError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtConnection
            // 
            this.txtConnection.AutoSize = true;
            this.txtConnection.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtConnection.Location = new System.Drawing.Point(12, 9);
            this.txtConnection.Name = "txtConnection";
            this.txtConnection.Size = new System.Drawing.Size(169, 23);
            this.txtConnection.TabIndex = 0;
            this.txtConnection.Text = "Connecting to server";
            // 
            // btnJoin
            // 
            this.btnJoin.Location = new System.Drawing.Point(12, 77);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(94, 29);
            this.btnJoin.TabIndex = 1;
            this.btnJoin.Text = "Join";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // btnLeave
            // 
            this.btnLeave.Location = new System.Drawing.Point(112, 77);
            this.btnLeave.Name = "btnLeave";
            this.btnLeave.Size = new System.Drawing.Size(94, 29);
            this.btnLeave.TabIndex = 2;
            this.btnLeave.Text = "Leave";
            this.btnLeave.UseVisualStyleBackColor = true;
            this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
            // 
            // txtConnectionError
            // 
            this.txtConnectionError.AutoSize = true;
            this.txtConnectionError.Location = new System.Drawing.Point(12, 32);
            this.txtConnectionError.Name = "txtConnectionError";
            this.txtConnectionError.Size = new System.Drawing.Size(0, 20);
            this.txtConnectionError.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtConnectionError);
            this.Controls.Add(this.btnLeave);
            this.Controls.Add(this.btnJoin);
            this.Controls.Add(this.txtConnection);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtConnection;
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.Button btnLeave;
        private System.Windows.Forms.Label txtConnectionError;
    }
}

