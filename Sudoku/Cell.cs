using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sudoku
{
  public partial class Cell : System.Windows.Forms.Control
  {
    public event EventHandler<EventArgs> TextChangedEvent;

    private Color m_color = Color.Blue;
    private bool m_locked = false;
    private Brush m_foreBrush = Brushes.Black;
    private Brush m_backBrush = Brushes.White;
    private bool m_selected = false;
    private string m_tooltipText = string.Empty;

    public bool Locked
    {
      get { return m_locked; }
      set { m_locked = value; }
    }

    public string TooltipText
    {
      set { m_tooltipText = value; }
    }

    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
        Refresh();
      }
    }

    public Cell()
    {
      InitializeComponent();

      this.SetStyle( ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true );
      this.TextChanged += new EventHandler( Cell_TextChanged );
      this.SizeChanged += new EventHandler( Cell_SizeChanged );
      this.ForeColorChanged += new EventHandler( Cell_ForeColorChanged );
      this.BackColorChanged += new EventHandler( Cell_BackColorChanged );
      this.MouseClick += new MouseEventHandler( Cell_MouseClick );
      this.KeyPress += new KeyPressEventHandler( Cell_KeyPress );
      this.LostFocus += new EventHandler( Cell_LostFocus );
    }

    void Cell_LostFocus( object sender, EventArgs e )
    {
      m_selected = false;
      Refresh();
    }

    void Cell_KeyPress( object sender, KeyPressEventArgs e )
    {
      if ( m_locked )
        return;

      if ( e.KeyChar == 8 && this.Text.Length > 0 )
      {
        this.Text = this.Text.Substring( 0, this.Text.Length - 1 );
      }
      else if ( e.KeyChar >= 49 && e.KeyChar <= 57 )
      {
        this.Text += e.KeyChar;
      }
      
      Refresh();

      if ( TextChangedEvent != null )
        TextChangedEvent( this, EventArgs.Empty );
    }

    void Cell_MouseClick( object sender, MouseEventArgs e )
    {
      if ( m_locked )
        return;

      this.Focus();
      m_selected = true;
      Refresh();

      if ( m_tooltipText.Length > 0 && Sudoku.Properties.Settings.Default.ShowPossibleTooltip )
      {
        ToolTip tt = new ToolTip();
        tt.Show( m_tooltipText, this, new Point( this.ClientSize.Width, this.ClientSize.Height / 2 - 10 ), 3000 );
      }
    }

    void Cell_BackColorChanged( object sender, EventArgs e )
    {
      m_backBrush = new SolidBrush( this.BackColor );
    }

    void Cell_ForeColorChanged( object sender, EventArgs e )
    {
      m_foreBrush = new SolidBrush( this.ForeColor );
    }

    void Cell_SizeChanged( object sender, EventArgs e )
    {
      RecalculateFont();
    }

    void Cell_TextChanged( object sender, EventArgs e )
    {
      RecalculateFont();
    }

    private void RecalculateFont()
    {
      float fontSize = Math.Min( this.ClientSize.Width, this.ClientSize.Height ) / 1.8f;
      fontSize = Math.Max( 8.0f, fontSize / Math.Max( 1, Text.Length ) );
      this.Font = new System.Drawing.Font( FontFamily.GenericSansSerif, ( int ) fontSize, FontStyle.Bold );
    }

    protected override void OnPaint( PaintEventArgs pe )
    {
      Rectangle rect = new Rectangle( pe.ClipRectangle.Location, pe.ClipRectangle.Size );

      // fill the background
      pe.Graphics.FillRectangle( m_backBrush, rect );

      // set up antialiasing
      pe.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
      pe.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

      if ( m_selected )
      {
        Rectangle smallerRect = new Rectangle( rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2 );
        pe.Graphics.DrawRectangle( new Pen( Color.LightGreen, 2 ), smallerRect );
      }

      // draw the string
      StringFormat sf = new StringFormat();
      sf.Alignment = StringAlignment.Center;
      sf.LineAlignment = StringAlignment.Center;
      pe.Graphics.DrawString( this.Text, this.Font, m_foreBrush, rect, sf );
    }
  }
}
