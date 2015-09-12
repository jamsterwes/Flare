using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using ScintillaNET;
using System.Collections.Generic;

namespace WesPythonIDE
{
    public partial class Form1 : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        private string titleText = "Flare";
        private string _filename; 
        private Point mouseOffset;
        private bool isMouseDown = false;
        private void style(int style, Color color)
        {
            scintilla1.Styles[style].BackColor = Color.FromArgb(48, 48, 48);
            scintilla1.Styles[style].ForeColor = color;
        }
        private void changePython()
        {
            style(Style.Python.Default, Color.FromArgb(192, 192, 192));
            style(Style.Python.ClassName, Color.FromArgb(64, 163, 127));
            style(Style.Python.Operator, Color.FromArgb(192, 192, 192));
            style(Style.Python.StringEol, Color.FromArgb(192, 192, 192));
            style(Style.Python.Decorator, Color.FromArgb(192, 192, 192));
            style(Style.Python.DefName, Color.FromArgb(64, 163, 127));
            style(Style.Python.Identifier, Color.FromArgb(192, 192, 192));
            style(Style.Python.Triple, Color.FromArgb(163, 163, 21));
            style(Style.Python.TripleDouble, Color.FromArgb(163, 163, 21));
            style(Style.Python.Character, Color.FromArgb(21, 163, 21));
            style(Style.Python.CommentLine, Color.FromArgb(0, 128, 0));
            style(Style.Python.String, Color.FromArgb(21, 163, 21));
            style(Style.Python.Number, Color.FromArgb(100, 100, 163));
            style(Style.Python.Word, Color.FromArgb(163, 127, 64));
            style(Style.Python.Word2, Color.FromArgb(64, 127, 163));
            scintilla1.Lexer = Lexer.Python; 
            scintilla1.SetKeywords(0, "and as True None False assert break class continue def del elif else except exec finally for from global if import in is lambda not or pass raise return try while with yield");
            scintilla1.SetKeywords(1, "abs all any ascii bin bool bytearray bytes callable chr classmethod compile complex delattr dict dir divmod enumerate eval exec filter float format frozenset getattr globals hasattr hash help hex id input int isinstance issubclass iter len list locals map max memoryview min next object oct open ord pow print property range repr reversed round set setattr slice sorted staticmethod str sum super tuple type vars zip __import__");
        }
        private void changeCSharp()
        {
            style(Style.Cpp.Word, Color.FromArgb(163, 127, 64));
            style(Style.Cpp.Word2, Color.FromArgb(64, 127, 163));
            style(Style.Cpp.String, Color.FromArgb(21, 163, 21));
            style(Style.Cpp.StringEol, Color.FromArgb(192, 192, 192));
            style(Style.Cpp.Character, Color.FromArgb(21, 163, 21));
            style(Style.Cpp.Verbatim, Color.FromArgb(21, 163, 21));
            style(Style.Cpp.Comment, Color.FromArgb(0, 128, 0));
            style(Style.Cpp.CommentLine, Color.FromArgb(0, 128, 0));
            style(Style.Cpp.CommentLineDoc, Color.FromArgb(0, 128, 0));
            style(Style.Cpp.Number, Color.FromArgb(100, 100, 163));
            style(Style.Cpp.Preprocessor, Color.FromArgb(153, 84, 158));

            scintilla1.Lexer = Lexer.Cpp;
            scintilla1.SetKeywords(0, "abstract as base break case catch checked continue default delegate do else event explicit extern false finally fixed for foreach goto if implicit in interface internal is lock namespace new null object operator out override params private protected public readonly ref return sealed sizeof stackalloc switch this throw true try typeof unchecked unsafe using virtual while");
            scintilla1.SetKeywords(1, "bool byte char class const decimal double enum float int long sbyte short static string struct uint ulong ushort void");
        }
        private void changeCpp()
        {
            style(Style.Cpp.Word, Color.FromArgb(163, 127, 64));
            style(Style.Cpp.Word2, Color.FromArgb(64, 127, 163));
            style(Style.Cpp.String, Color.FromArgb(21, 163, 21));
            style(Style.Cpp.StringEol, Color.FromArgb(192, 192, 192));
            style(Style.Cpp.Character, Color.FromArgb(21, 163, 21));
            style(Style.Cpp.Verbatim, Color.FromArgb(21, 163, 21));
            style(Style.Cpp.Comment, Color.FromArgb(0, 128, 0));
            style(Style.Cpp.CommentLine, Color.FromArgb(0, 128, 0));
            style(Style.Cpp.CommentLineDoc, Color.FromArgb(0, 128, 0));
            style(Style.Cpp.Number, Color.FromArgb(100, 100, 163));
            style(Style.Cpp.Preprocessor, Color.FromArgb(153, 84, 158));

            scintilla1.Lexer = Lexer.Cpp;
            scintilla1.SetKeywords(0, "alignas alignof and and_eq asm bitand bitor break case catch class compl concept const constexpr const_cast continue decltype default delete do dynamic_cast else explicit export extern false for friend goto if inline mutable namespace new noexcept not not_eq nullptr operator or or_eq private protected public register reinterpret_cast requires return signed sizeof static static_assert static_cast switch template this thread_local throw true try typedef typeid typename unsigned using virtual volatile while xor xor_eq");
            scintilla1.SetKeywords(1, "auto bool char char16_t char32_t double enum float int long struct void");
        }
        private void changeHTML()
        {
            style(Style.Html.Default, Color.FromArgb(192, 192, 192));
            style(Style.Html.Attribute, Color.FromArgb(64, 163, 127));
            style(Style.Html.Comment, Color.FromArgb(100, 100, 100));
            style(Style.Html.Tag, Color.FromArgb(100, 130, 155));
            style(Style.Html.TagUnknown, Color.FromArgb(192, 192, 192));
            style(Style.Html.Entity, Color.FromArgb(192, 100, 192));
            style(Style.Html.SingleString, Color.FromArgb(192, 192, 0));
            style(Style.Html.Number, Color.FromArgb(192, 192, 192));
            style(Style.Html.Script, Color.FromArgb(192, 192, 192));
            scintilla1.Lexer = Lexer.Html;
        }
        private void changePlaintext()
        {
            style(Style.Python.Default, Color.FromArgb(192, 192, 192));
            style(Style.Python.ClassName, Color.FromArgb(192, 192, 192));
            style(Style.Python.Operator, Color.FromArgb(192, 192, 192));
            style(Style.Python.StringEol, Color.FromArgb(192, 192, 192));
            style(Style.Python.Decorator, Color.FromArgb(192, 192, 192));
            style(Style.Python.DefName, Color.FromArgb(192, 192, 192));
            style(Style.Python.Identifier, Color.FromArgb(192, 192, 192));
            style(Style.Python.Triple, Color.FromArgb(192, 192, 192));
            style(Style.Python.TripleDouble, Color.FromArgb(192, 192, 192));
            style(Style.Python.Character, Color.FromArgb(192, 192, 192));
            style(Style.Python.CommentLine, Color.FromArgb(192, 192, 192));
            style(Style.Python.String, Color.FromArgb(192, 192, 192));
            style(Style.Python.Number, Color.FromArgb(192, 192, 192));
            style(Style.Python.Word, Color.FromArgb(192, 192, 192));
            style(Style.Python.Word2, Color.FromArgb(192, 192, 192));
            scintilla1.Lexer = Lexer.Python;
        }
        private void changeJavascript()
        {
            style(Style.Cpp.Word, Color.FromArgb(163, 127, 64));
            style(Style.Cpp.Word2, Color.FromArgb(64, 127, 163));
            style(Style.Cpp.String, Color.FromArgb(21, 163, 21));
            style(Style.Cpp.StringEol, Color.FromArgb(192, 192, 192));
            style(Style.Cpp.Character, Color.FromArgb(21, 163, 21));
            style(Style.Cpp.Verbatim, Color.FromArgb(21, 163, 21));
            style(Style.Cpp.Comment, Color.FromArgb(0, 128, 0));
            style(Style.Cpp.CommentLine, Color.FromArgb(0, 128, 0));
            style(Style.Cpp.CommentLineDoc, Color.FromArgb(0, 128, 0));
            style(Style.Cpp.Number, Color.FromArgb(100, 100, 163));
            style(Style.Cpp.Preprocessor, Color.FromArgb(153, 84, 158));

            scintilla1.Lexer = Lexer.Cpp;
            scintilla1.SetKeywords(0, "abstract arguments break case catch class const continue debugger default delete do double else enum eval export extends false final finally for goto if implements import in instanceof interface let native new null package private protected public return static super switch synchronized this throw throws transient true try typeof volatile while with yield Array Date eval hasOwnProperty Infinity isFinite isNaN isPrototypeOf length Math NaN name Number Object prototype String toString undefined valueOf");
            scintilla1.SetKeywords(1, "function boolean char var byte void int float long short");
        }
        private void defaultStyle()
        {
            scintilla1.Margins[0].Width = 16;
            scintilla1.Styles[Style.LineNumber].ForeColor = Color.FromArgb(127, 127, 127);
            scintilla1.Styles[Style.LineNumber].BackColor = Color.FromArgb(64, 64, 64);
            scintilla1.Styles[Style.LineNumber].ForeColor = Color.FromArgb(127, 127, 127);

            changePlaintext();

            scintilla1.Styles[Style.Default].Font = "DejaVu Sans Mono";
            scintilla1.Styles[Style.Default].Size = 10;

            style(Style.Default, Color.FromArgb(192, 192, 192));

            style(Style.IndentGuide, Color.FromArgb(127, 127, 127));
        }
        public Form1()
        {
            _filename = "";
            InitializeComponent();

            defaultStyle();

            menuStrip1.BackColor = Color.FromArgb(24, 24, 24);
            menuStrip1.ForeColor = Color.FromArgb(127, 127, 127);
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new MenuColorTable());

            if (Environment.GetCommandLineArgs().Length == 2)
            {
                _filename = Environment.GetCommandLineArgs()[1];
                langAdapt(_filename);
                titleText = _filename + " - Flare";
                this.Invalidate();
                StreamReader sr = new StreamReader(_filename);
                scintilla1.Text = sr.ReadToEnd();
                sr.Close();
            }
        }
        private void langAdapt(String _filename)
        {
            if (_filename.EndsWith(".html") || _filename.EndsWith(".xhtml"))
            {
                changeHTML();
            }
            else if (_filename.EndsWith(".py") || _filename.EndsWith(".pyw"))
            {
                changePython();
            }
            else if (_filename.EndsWith(".cs"))
            {
                changeCSharp();
            }
            else if (_filename.EndsWith(".cc") || _filename.EndsWith(".cxx") || _filename.EndsWith(".cpp") || _filename.EndsWith(".h") || _filename.EndsWith(".hxx") || _filename.EndsWith(".hpp"))
            {
                changeCpp();
            }
            else if (_filename.EndsWith(".js"))
            {
                changeJavascript();
            }
            else
            {
                changePlaintext();
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "" && result == DialogResult.OK)
            {
                _filename = openFileDialog1.FileName;
                langAdapt(_filename);
                titleText = _filename + " - Flare";
                this.Invalidate();
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                scintilla1.Text = sr.ReadToEnd();
                sr.Close();
            }
        }
        private void saveFile(bool forceSaveAs)
        {
            if (_filename == "" || forceSaveAs)
            {
                DialogResult result = saveFileDialog1.ShowDialog();
                if (result == DialogResult.OK && saveFileDialog1.FileName != "")
                {
                    _filename = saveFileDialog1.FileName;
                    langAdapt(_filename);
                    titleText = _filename + " - Flare";
                    this.Invalidate();
                    StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                    sw.Write(scintilla1.Text);
                    sw.Close();
                }
            }
            else
            {
                StreamWriter sw = new StreamWriter(_filename);
                sw.Write(scintilla1.Text);
                sw.Close();
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S))
            {
                saveFile(false);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save your current file?", "Flare", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.No)
            {
                scintilla1.Text = "";
                titleText = "Flare";
                this.Invalidate();
                
            }
            else if (result == DialogResult.Yes)
            {
                saveFile(false);
            }
            else
            {
                /** Do nothing... **/
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile(false);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFile(true);
        }

        private void runCommand(string command)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + command;
            process.StartInfo = startInfo;
            process.Start();
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cToolStripMenuItem_TextChanged(object sender, EventArgs e) {

        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var dropDownMenu = (ToolStripDropDownMenu)this.fileToolStripMenuItem.DropDown;
            dropDownMenu.ShowImageMargin = false;
            dropDownMenu.BackColor = Color.FromArgb(24, 24, 24);
            saveAsToolStripMenuItem.Font = new Font("DejaVu Sans Mono", 8.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            saveToolStripMenuItem.Font = new Font("DejaVu Sans Mono", 8.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            newToolStripMenuItem.Font = new Font("DejaVu Sans Mono", 8.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            openToolStripMenuItem.Font = new Font("DejaVu Sans Mono", 8.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;

            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset = -e.Y - SystemInformation.FrameBorderSize.Height;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            } 
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                this.WindowState = FormWindowState.Maximized;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }
        private int maxLineNumberCharLength;
        private void scintilla1_TextChanged(object sender, EventArgs e)
        {
            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = scintilla1.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == this.maxLineNumberCharLength)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            scintilla1.Margins[0].Width = scintilla1.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            this.maxLineNumberCharLength = maxLineNumberCharLength;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void titlebar_MouseDown(object sender, MouseEventArgs e)
        {
            Form1_MouseDown(sender, e);
        }

        private void titlebar_MouseMove(object sender, MouseEventArgs e)
        {
            Form1_MouseMove(sender, e);
        }

        private void titlebar_MouseUp(object sender, MouseEventArgs e)
        {
            Form1_MouseUp(sender, e);
        }
        private const int cGrip = 16;      // Grip size
        private const int cCaption = 32;   // Caption bar height;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawImage(Flare.Properties.Resources.flareicon, new Rectangle(2, 2, 26, 26));
            e.Graphics.DrawString(titleText, new Font("Segoe UI", 14.0f, FontStyle.Regular, GraphicsUnit.Point, 0), new SolidBrush(Color.FromArgb(255, 116, 0)), new Point(40, 3));
            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.Default;
        }
    }
    class MenuColorTable : ProfessionalColorTable
    {
        public MenuColorTable()
        {
            // see notes
            base.UseSystemColors = false;
        }
        public override Color MenuBorder
        {
            get { return Color.FromArgb(24, 24, 24); }
        }
        public override Color MenuItemBorder
        {
            get { return Color.FromArgb(24, 24, 24); }
        }
        public override Color MenuItemSelected
        {
            get { return Color.FromArgb(48, 48, 48); }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.FromArgb(32, 32, 32); }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.FromArgb(24, 24, 24); }
        }
        public override Color MenuStripGradientBegin
        {
            get { return Color.FromArgb(24, 24, 24); }
        }
        public override Color MenuStripGradientEnd
        {
            get { return Color.FromArgb(24, 24, 24); }
        }
    }
}
