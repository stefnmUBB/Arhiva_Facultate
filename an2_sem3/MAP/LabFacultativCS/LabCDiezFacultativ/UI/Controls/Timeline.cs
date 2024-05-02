using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabCDiezFacultativ.UI.Controls
{
    public partial class Timeline : UserControl
    {
        public Timeline()
        {
            InitializeComponent();
            UpdateSelectedDates();
        }

        private DateTime _StartDate = new DateTime(DateTime.Now.Year, 1, 1);
        private DateTime _EndDate = new DateTime(DateTime.Now.Year + 1, 1, 1);

        public DateTime StartDate 
        { 
            get=>_StartDate; 
            set
            {
                _StartDate = value;
                UpdateSelectedDates();
            }
        } 
        public DateTime EndDate 
        { 
            get=>_EndDate; 
            set
            {
                _EndDate = value;
                UpdateSelectedDates();
            }
        }

        int Cursor1Pos = 0;
        int Cursor2Pos = 100;

        public DateTime SelectedStartDate
        {
            get => StartDate + new TimeSpan((EndDate - StartDate).Ticks * Cursor1Pos / 100);
        }

        public DateTime SelectedEndDate
        {
            get => StartDate + new TimeSpan((EndDate - StartDate).Ticks * Cursor2Pos / 100);
        }

        void UpdateSelectedDates()
        {
            StartDateLabel.Text = SelectedStartDate.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss");
            EndDateLabel.Text = SelectedEndDate.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss");
            SelectedIntervalChanged?.Invoke(this);
        }

        public delegate void OnSelectedIntervalChanged(object sender);
        public event OnSelectedIntervalChanged SelectedIntervalChanged;


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int c1x = Cursor1Pos * Width / 100;
            int c2x = Cursor2Pos * Width / 100;

            e.Graphics.FillRectangle(Brushes.DodgerBlue, c1x, 0, c2x-c1x, Height);
            e.Graphics.FillRectangle(Brushes.White, c1x - 2, 0, 4, Height);
            e.Graphics.FillRectangle(Brushes.White, c2x - 2, 0, 4, Height);

            e.Graphics.DrawRectangle(Pens.Gray, c1x - 2, 0, 4, Height - 1);
            e.Graphics.DrawRectangle(Pens.Gray, c2x - 2, 0, 4, Height - 1);
        }

        void SetCursor(int x)
        {
            int c1x = Cursor1Pos * Width / 100;
            int c2x = Cursor2Pos * Width / 100;

            //this.FindForm().Text = $"{x} {c1x} {c2x}";

            if (Math.Abs(x - c1x) <= 3 || Math.Abs(x - c2x) <= 3)
                this.Cursor = Cursors.SizeWE;
            else
                this.Cursor = Cursors.Default;
        }

        protected override void OnMouseHover(EventArgs e)
        {
            int x = this.PointToClient(Cursor.Position).X;
            SetCursor(x);           
        }

        int under_cursor = -1;

        protected override void OnMouseEnter(EventArgs e)
        {
            int x = this.PointToClient(Cursor.Position).X;
            SetCursor(x);
            under_cursor = -1;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            under_cursor = -1;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            int c1x = Cursor1Pos * Width / 100;
            int c2x = Cursor2Pos * Width / 100;
            int x = this.PointToClient(Cursor.Position).X;
            if (Math.Abs(x - c1x) <= 3)
                under_cursor = 1;
            else if (Math.Abs(x - c2x) <= 3)
                under_cursor = 2;
            else under_cursor = -1;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            under_cursor = -1;
        }

        static int Clamp(int x, int a, int b)
        {
            if (x <= a) return a;
            if (x >= b) return b;
            return x;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            int x = this.PointToClient(Cursor.Position).X;
            SetCursor(x);
            if (under_cursor < 0) return;

            if(under_cursor==1)
            {
                Cursor1Pos = Clamp(x * 100 / Width, 0, 100);
                UpdateSelectedDates();
                Invalidate(true);
            }
            else if(under_cursor==2)
            {
                Cursor2Pos = Clamp(x * 100 / Width, 0, 100);
                UpdateSelectedDates();
                Invalidate(true);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            ToLabel.Left = Width - ToLabel.Width - 3;
            EndDateLabel.Left = Width - EndDateLabel.Width - 3;
            Invalidate(true);
        }
    }
}
