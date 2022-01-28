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
        bool afisareClues = false;
        int clue_x, clue_y;
        public Main()
        {
            buildWordList(clue_window);
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
            afisareClues=true;
            //this.WindowState = FormWindowState.Minimized;
            
        }
        private void InitBoard() //initialize the game board
        {
            board.BackgroundColor = Color.Black; //every cells becomes black 
            board.DefaultCellStyle.BackColor = Color.Black;
            for(int i =0;i < 21; i++)
            {
                board.Rows.Add();

            }
            foreach(DataGridViewColumn c in board.Columns)
            {
                c.Width = board.Width / board.Columns.Count; //divides colums width to screen resolution
            }
            foreach (DataGridViewRow r in board.Rows) //divides rows height to screen resolution
            {
                r.Height = board.Width / board.Rows.Count;
            }
            for (int row= 0; row < board.Rows.Count; row++)
            {
                for (int col=0; col < board.Columns.Count; col++)
                {
                    board[col, row].ReadOnly = true;  // makes every cell non editable
                }
            }
            foreach(id_cells i in idc) { 
                int start_col = i.x;
                int start_row = i.y;
                char[] word =i.word.ToCharArray(); //converts strings to char array
                
                for(int j = 0; j < word.Length; j++)
                {
                    if (i.direction.ToUpper() == "ACROSS") //verify if the word should be displayed horizontally
                    {
                        formatCell(start_row, start_col + j, word[j].ToString()); //verify if the word should be displayed vertically
                    }
                    if (i.direction.ToUpper() == "DOWN") //verify if the word should be displayed vertically
                    {
                        formatCell(start_row+j, start_col , word[j].ToString());
                    }
                }
            }
        }
        private void formatCell(int row, int col, String letter) //formats any playable cell 
        {
            DataGridViewCell c = board[col, row]; //cell object
            c.Style.BackColor = Color.White; //applies the style of the playable cell 
            c.ReadOnly = false;
            c.Style.SelectionBackColor = Color.Cyan;
            c.Tag = letter.ToUpper();  // tag is used to verify if the leter the user enters the correct letter 
        }
        private void buildWordList(Clues clue) // parses the information from the file to a list
        {
            String line = "";
            using (StreamReader s = new StreamReader(puzzle_file)) //reads the file
            {
                line = s.ReadLine();//ignore
                while((line= s.ReadLine()) != null) //if there are lines of data
                {
                    String[] l = line.Split('|'); //splits the data 
                    idc.Add(new id_cells(Int32.Parse(l[0]), Int32.Parse(l[1]), l[2], l[3], l[4], l[5])); // adds data to a list of objects 
                    clue.clue_board.Rows.Add(new String[] { l[3], l[5] }); // adds the number clue and direction to the clue window
                }
            }
        }
        private void openPuzzeToolStripMenuItem_Click(object sender, EventArgs e) // function to open custom pzl file
        {
            OpenFileDialog OFD = new OpenFileDialog(); // creates a file dialog object 
            OFD.Filter = "Puzzle Files|*.pzl"; // filters files so the user can only open pzl files 
            if (OFD.ShowDialog().Equals(DialogResult.OK)) //checks if the user has selected a file 
            {
                puzzle_file = OFD.FileName; //changes the file location of the example to the custom file
                board.Rows.Clear(); //clears the board 
                clue_window.clue_board.Rows.Clear(); //clears the clue board 
                idc.Clear(); // clears the list of objects
                buildWordList(clue_window); 
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

            clue_x = this.Location.X + this.Width + 1;
            clue_y = this.Location.Y;
            clue_window.SetDesktopLocation(clue_x,clue_y);
            if (this.WindowState != FormWindowState.Minimized)
            {
                return;
            }
            clue_window.SetDesktopLocation(clue_x,clue_y);


        }

        private void locationChanged(object sender, EventArgs e, Clues clue)
        {
            clue_window.SetDesktopLocation(this.Location.X + this.Width + 1, this.Location.Y);
        }

        private void board_CellValueChanged(object sender, DataGridViewCellEventArgs e) //function that fires if the player enters a value to the boars
        {
            try
            {
                board[e.ColumnIndex, e.RowIndex].Value = board[e.ColumnIndex, e.RowIndex].Value.ToString().ToUpper(); // makes every letter to uppercase
            }
            catch { }
            try
            {
                if(board[e.ColumnIndex, e.RowIndex].Value.ToString().Length >1) //if the player enters too many chars to  a single cells the cell will take the first leter
                    board[e.ColumnIndex, e.RowIndex].Value=board[e.ColumnIndex, e.RowIndex].Value.ToString().Substring(0,1);
            }
            catch { }
            try
            {
                if (board[e.ColumnIndex, e.RowIndex].Value.ToString().Equals(board[e.ColumnIndex, e.RowIndex].Tag.ToString().ToUpper())) // if the letter matches the tag
                    board[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.DarkGreen;
                else
                    board[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.DarkRed;
            }
            catch { }
        }

        private void board_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) //function to draw the numbers on top of cells
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

        private void cluesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            clue_window.Show();
            
        }
        
        private void Clue_window_FormClosed(object sender, FormClosedEventArgs e)
        {
            throw new NotImplementedException();
            clue_window.Hide();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Main_Resize(object sender, EventArgs e)
        {
            

        }
    }
    public class id_cells //piece data from pzl file
    {
        public int x; //the start x of the word
        public int y; //the start y of the word
        public String direction; //direction of the word arrat
        public String number; //number of item
        public String word; //the word to guess
        public String clue; // the question/clue to find out 
        public id_cells(int x, int y, String d, String n, String w, String c) //initialize the objects with these attributes
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

