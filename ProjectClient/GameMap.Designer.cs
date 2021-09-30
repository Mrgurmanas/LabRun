
namespace ProjectClient
{
    partial class GameMap
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
            this.txtPlayerId = new System.Windows.Forms.Label();
            this.txtPlayer2ID = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPlayerId
            // 
            this.txtPlayerId.AutoSize = true;
            this.txtPlayerId.Location = new System.Drawing.Point(12, 9);
            this.txtPlayerId.Name = "txtPlayerId";
            this.txtPlayerId.Size = new System.Drawing.Size(49, 20);
            this.txtPlayerId.TabIndex = 0;
            this.txtPlayerId.Text = "Player";
            // 
            // txtPlayer2ID
            // 
            this.txtPlayer2ID.AutoSize = true;
            this.txtPlayer2ID.Location = new System.Drawing.Point(662, 9);
            this.txtPlayer2ID.Name = "txtPlayer2ID";
            this.txtPlayer2ID.Size = new System.Drawing.Size(57, 20);
            this.txtPlayer2ID.TabIndex = 1;
            this.txtPlayer2ID.Text = "Player2";
            // 
            // GameMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtPlayer2ID);
            this.Controls.Add(this.txtPlayerId);
            this.Name = "GameMap";
            this.Text = "Map";
            this.Load += new System.EventHandler(this.GameMap_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtPlayerId;
        private System.Windows.Forms.Label txtPlayer2ID;
    }
}