using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace crossword
{
    public partial class Main : Form
    {
        Clues clue_window = new Clues();
        List<id_cells> idc = new List<id_cells>();
        public String puzzle_file = Application.StartupPath + "\\puzzles\\puzzle_1.pzl";
        public Main()
        {
            buildWordList();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitBoard();
            formatCell(5, 5, "B");
            clue_window.SetDesktopLocation(this.Location.X + this.Width + 1, this.Location.Y);
            clue_window.StartPosition = FormStartPosition.Manual;
            clue_window.Show();
            clue_window.clue_board.AutoResizeColumn(0); 
        }
        private void InitBoard()
        {
            board.BackgroundColor = Color.Black;
            board.DefaultCellStyle.BackColor = Color.Black;
            for(int i =0;i < 21; i++)
            {
                board.Rows.Add();

            }
            foreach(DataGridViewColumn c in board.Columns)
            {
                c.Width = board.Width / board.Columns.Count;
            }
            foreach (DataGridViewRow r in board.Rows)
            {
                r.Height = board.Width / board.Rows.Count;
            }
            for (int row= 0; row < board.Rows.Count; row++)
            {
                for (int col=0; col < board.Columns.Count; col++)
                {
                    board[col, row].ReadOnly = true;
                }
            }
            foreach(id_cells i in idc) {
                int start_col = i.x;
                int start_row = i.y;
                char[] word =i.word.ToCharArray();
                
                for(int j = 0; j < word.Length; j++)
                {
                    if (i.direction.ToUpper() == "ACROSS")
                    {
                        formatCell(start_row, start_col + j, word[j].ToString());
                    }
                    if (i.direction.ToUpper() == "DOWN")
                    {
                        formatCell(start_row+j, start_col , word[j].ToString());
                    }
                }
            }
        }
        private void formatCell(int row, int col, String letter)
        {
            DataGridViewCell c = board[col, row];
            c.Style.BackColor = Color.White;
            c.ReadOnly = false;
            c.Style.SelectionBackColor = Color.Cyan;
            c.Tag = letter.ToUpper();
        }
        private void buildWordList()
        {
            String line = "";
            using (StreamReader s = new StreamReader(puzzle_file))
            {
                line = s.ReadLine();//ignore
                while((line= s.ReadLine()) != null)
                {
                    String[] l = line.Split('|');
                    idc.Add(new id_cells(Int32.Parse(l[0]), Int32.Parse(l[1]), l[2], l[3], l[4], l[5]));
                    clue_window.clue_board.Rows.Add(new String[] { l[3], l[2], l[5] });
                }
            }
        }
        private void openPuzzeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = "Puzzle Files|*.pzl";
            if (OFD.ShowDialog().Equals(DialogResult.OK))
            {
                puzzle_file = OFD.FileName;
                board.Rows.Clear();
                clue_window.clue_board.Rows.Clear();
                idc.Clear();
                buildWordList();
                InitBoard();
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Proiect facut de Murgu Andrei, Udrea Rares, Carasel Andrei si Mihai", "Credits");
        }



        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            clue_window.SetDesktopLocation(this.Location.X + this.Width + 1, this.Location.Y);
        }

        private void board_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                board[e.ColumnIndex, e.RowIndex].Value = board[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper();
            }
            catch { }
            try
            {
                if(board[e.ColumnIndex, e.RowIndex].Value.ToString().Length >1)
                    board[e.ColumnIndex, e.RowIndex].Value=board[e.ColumnIndex, e.RowIndex].Value.ToString().Substring(0,1);
            }
            catch { }
            try
            {
                if (board[e.ColumnIndex, e.RowIndex].Value.ToString().Equals(board[e.ColumnIndex, e.RowIndex].Tag.ToString().ToUpper()))
                    board[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.DarkGreen;
                else
                    board[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.DarkRed;
            }
            catch { }
        }

        private void board_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            String number = "";
            if(idc.Any(c => (number = c.number) != ""&& c.x == e.ColumnIndex && c.y == e.RowIndex)){
                Rectangle r = new Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height);
                e.Graphics.FillRectangle(Brushes.White, r);
                Font f = new Font(e.CellStyle.Font.FontFamily, 7);
                e.Graphics.DrawString(number, f, Brushes.Black, r);
                e.PaintContent(e.ClipBounds);
                e.Handled = true;
            }
        }
    }
    public class id_cells
    {
        public int x;
        public int y;
        public String direction;
        public String number;
        public String word;
        public String clue;
        public id_cells(int x, int y, String d, String n, String w, String c)
        {
            this.x = x;
            this.y = y;
            this.direction = d;
            this.number = n;
            this.word = w;
            this.clue = c;

        }
    }
}

