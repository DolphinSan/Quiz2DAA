namespace Slidingbox {
    partial class Form1 {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelBoard;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnShuffle;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.Label lblMoves;
        private System.Windows.Forms.Label lblOptimal;
        private System.Windows.Forms.Timer animationTimer;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.panelBoard = new System.Windows.Forms.Panel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnShuffle = new System.Windows.Forms.Button();
            this.btnSolve = new System.Windows.Forms.Button();
            this.lblMoves = new System.Windows.Forms.Label();
            this.lblOptimal = new System.Windows.Forms.Label();
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            
            this.panelBoard.Location = new System.Drawing.Point(12, 12);
            this.panelBoard.Name = "panelBoard";
            this.panelBoard.Size = new System.Drawing.Size(300, 300);
            this.panelBoard.TabIndex = 0;
             
            this.btnNew.Location = new System.Drawing.Point(330, 20);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(120, 30);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "New Game";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.BtnNew_Click);
            
            this.btnShuffle.Location = new System.Drawing.Point(330, 60);
            this.btnShuffle.Name = "btnShuffle";
            this.btnShuffle.Size = new System.Drawing.Size(120, 30);
            this.btnShuffle.TabIndex = 2;
            this.btnShuffle.Text = "Shuffle";
            this.btnShuffle.UseVisualStyleBackColor = true;
            this.btnShuffle.Click += new System.EventHandler(this.BtnShuffle_Click);
             
            this.btnSolve.Location = new System.Drawing.Point(330, 100);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(120, 30);
            this.btnSolve.TabIndex = 3;
            this.btnSolve.Text = "Solve (A*)";
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.BtnSolve_Click);
            
            this.lblMoves.AutoSize = true;
            this.lblMoves.Location = new System.Drawing.Point(330, 150);
            this.lblMoves.Name = "lblMoves";
            this.lblMoves.Size = new System.Drawing.Size(92, 17);
            this.lblMoves.TabIndex = 4;
            this.lblMoves.Text = "Moves: 0";
            
            this.lblOptimal.AutoSize = true;
            this.lblOptimal.Location = new System.Drawing.Point(330, 180);
            this.lblOptimal.Name = "lblOptimal";
            this.lblOptimal.Size = new System.Drawing.Size(122, 17);
            this.lblOptimal.TabIndex = 5;
            this.lblOptimal.Text = "Optimal: calculating";
             
            this.animationTimer.Interval = 400;
            this.animationTimer.Tick += new System.EventHandler(this.AnimationTimer_Tick);
            
            this.ClientSize = new System.Drawing.Size(470, 330);
            this.Controls.Add(this.lblOptimal);
            this.Controls.Add(this.lblMoves);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.btnShuffle);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.panelBoard);
            this.Name = "Form1";
            this.Text = "Slidingbox";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

