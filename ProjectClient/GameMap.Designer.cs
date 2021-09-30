
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
            this.components = new System.ComponentModel.Container();
            this.txtPlayerId = new System.Windows.Forms.Label();
            this.txtPlayer2Id = new System.Windows.Forms.Label();
            this.GameTime = new System.Windows.Forms.Timer(this.components);
            this.canvas = new System.Windows.Forms.Panel();
            this.canvas.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPlayerId
            // 
            this.txtPlayerId.AutoSize = true;
            this.txtPlayerId.ForeColor = System.Drawing.Color.White;
            this.txtPlayerId.Location = new System.Drawing.Point(143, 9);
            this.txtPlayerId.Name = "txtPlayerId";
            this.txtPlayerId.Size = new System.Drawing.Size(49, 20);
            this.txtPlayerId.TabIndex = 0;
            this.txtPlayerId.Text = "Player";
            // 
            // txtPlayer2Id
            // 
            this.txtPlayer2Id.AutoSize = true;
            this.txtPlayer2Id.ForeColor = System.Drawing.Color.White;
            this.txtPlayer2Id.Location = new System.Drawing.Point(536, 9);
            this.txtPlayer2Id.Name = "txtPlayer2Id";
            this.txtPlayer2Id.Size = new System.Drawing.Size(57, 20);
            this.txtPlayer2Id.TabIndex = 1;
            this.txtPlayer2Id.Text = "Player2";
            // 
            // GameTime
            // 
            this.GameTime.Enabled = true;
            this.GameTime.Tick += new System.EventHandler(this.GameTimer);
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.Black;
            this.canvas.Controls.Add(this.txtPlayerId);
            this.canvas.Controls.Add(this.txtPlayer2Id);
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(982, 953);
            this.canvas.TabIndex = 2;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            // 
            // GameMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 953);
            this.Controls.Add(this.canvas);
            this.Name = "GameMap";
            this.Text = "Map";
            this.Load += new System.EventHandler(this.GameMap_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyIsPressed);
            this.canvas.ResumeLayout(false);
            this.canvas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label txtPlayerId;
        private System.Windows.Forms.Label txtPlayer2Id;
        private System.Windows.Forms.Timer GameTime;
        private System.Windows.Forms.Panel canvas;
    }
}