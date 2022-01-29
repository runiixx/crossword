namespace crossword
{
    partial class Clues
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
            this.clue_board = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.clue_board)).BeginInit();
            this.SuspendLayout();
            // 
            // clue_board
            // 
            this.clue_board.AllowUserToAddRows = false;
            this.clue_board.AllowUserToDeleteRows = false;
            this.clue_board.AllowUserToResizeColumns = false;
            this.clue_board.AllowUserToResizeRows = false;
            this.clue_board.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clue_board.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column3});
            this.clue_board.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clue_board.Location = new System.Drawing.Point(0, 0);
            this.clue_board.Name = "clue_board";
            this.clue_board.RowHeadersVisible = false;
            this.clue_board.Size = new System.Drawing.Size(384, 530);
            this.clue_board.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Nr.";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 50;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Intrebari";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Clues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 530);
            this.Controls.Add(this.clue_board);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Clues";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Clues";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Clues_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Clues_FormClosed);
            this.Load += new System.EventHandler(this.Clues_Load);
            ((System.ComponentModel.ISupportInitialize)(this.clue_board)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.DataGridView clue_board;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}