using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Sudoku
{
  public partial class Board : UserControl
  {
    public event EventHandler<EventArgs> Solved;

    private Cell [ , ] m_labels;
    private State m_state = new State();
    private Difficulty m_difficulty = Difficulty.Easy;

    public Difficulty Difficulty
    {
      get { return m_difficulty; }
      set { m_difficulty = value; }
    }

    public Board()
    {
      InitializeComponent();

      this.DoubleBuffered = true;

      this.tableLayoutPanel1.Padding = new Padding( 0 );
      this.tableLayoutPanel1.Margin = new Padding( 2 );
      this.tableLayoutPanel1.BackColor = Color.Black;

      int [] margins = { 2, 1, 1, 1, 1, 2 };

      this.m_labels = new Cell [ 9, 9 ];
      int fontSize = Math.Min( this.ClientSize.Width, this.ClientSize.Height ) / 30;
      Font font = new System.Drawing.Font( FontFamily.GenericSansSerif, fontSize, FontStyle.Bold );
      for ( int x = 0; x < 9; x++ )
      {

        // determine the padding
        int leftPadding = ( x % 3 == 0 ) ? 2 : 1;
        int rightPadding = ( x % 3 == 2 ) ? 2 : 1;

        for ( int y = 0; y < 9; y++ )
        {
          this.m_labels [ x, y ] = new Cell();
          this.tableLayoutPanel1.Controls.Add( this.m_labels [ x, y ], x, y );
          this.m_labels [ x, y ].AutoSize = true;
          this.m_labels [ x, y ].Location = new System.Drawing.Point( 255, 40 );
          this.m_labels [ x, y ].Name = "label" + x.ToString() + y.ToString();
          this.m_labels [ x, y ].Size = new System.Drawing.Size( 35, 35 );
          this.m_labels [ x, y ].TabIndex = 0;
          this.m_labels [ x, y ].Text = "0";
          this.m_labels [ x, y ].Dock = System.Windows.Forms.DockStyle.Fill;
          this.m_labels [ x, y ].Font = font;
          this.m_labels [ x, y ].Tag = new Point( x, y );

          // determine the padding
          int topPadding = ( y % 3 == 0 ) ? 2 : 1;
          int bottomPadding = ( y % 3 == 2 ) ? 2 : 1;
          this.m_labels [ x, y ].Margin = new Padding( leftPadding, topPadding, rightPadding, bottomPadding );

          this.m_labels [ x, y ].BackColor = GetColor( x, y );
          this.m_labels [ x, y ].TextChangedEvent +=new EventHandler<EventArgs>(Board_TextChangedEvent);
        }
      }

      m_state.Initialize( m_difficulty );
      UpdateLabels();
    }

    void Board_TextChangedEvent( object sender, EventArgs e )
    {
      State currentState = GetCurrentState();
      if ( Sudoku.Properties.Settings.Default.ShowErrors )
        CheckForErrors( currentState );
      if ( currentState.IsSolved() )
      {
        if ( Solved != null )
          Solved( this, EventArgs.Empty );
      }
    }

    private Color GetColor( int x, int y )
    {
      bool bAlternateColor = ( ( x / 3 ) + ( y / 3 ) ) % 2 == 0;
      return bAlternateColor ? System.Drawing.Color.Bisque : System.Drawing.Color.AntiqueWhite;
    }

    void CheckForErrors( State state )
    {
      for ( int x = 0; x < 9; x++ )
      {
        for ( int y = 0; y < 9; y++ )
        {
          bool valid = state.IsValid( x, y );
          m_labels [ x, y ].BackColor = valid ? GetColor( x, y ) : Color.Red;
        }
      }
    }

    State GetCurrentState()
    {
      State state = new State();
      for ( int x = 0; x < 9; x++ )
      {
        for ( int y = 0; y < 9; y++ )
        {
          string label = m_labels [ x, y ].Text;
          if ( label.Length == 1 )
          {
            state.Numbers [ x, y ] = int.Parse( label );
          }
        }
      }
      return state;
    }

    public void Initialize()
    {
      m_state = new State();
      m_state.UpdateEvent += new EventHandler<EventArgs>( m_state_UpdateEvent );
      m_state.Initialize( m_difficulty );
      if ( Sudoku.Properties.Settings.Default.ShowErrors )
        CheckForErrors( m_state );
    }

    void m_state_UpdateEvent( object sender, EventArgs e )
    {
      UpdateLabels();
      Application.DoEvents();
    }

    public void UpdateLabels()
    {
      Possibilities possible = GetCurrentState().GetPossibilities();
      for ( int x = 0; x < 9; x++ )
      {
        for ( int y = 0; y < 9; y++ )
        {
          int number = m_state.Numbers [ x, y ];
          Cell cell = this.m_labels [ x, y ];
          if ( number == 0 )
          {
            cell.Locked = false;
            cell.ForeColor = Color.DarkBlue;
            string tooltipText = "Possible values: ";
            foreach ( int i in possible.PossibleList [ x, y ] )
            {
              tooltipText += i.ToString() + " ";
            }
            cell.TooltipText = tooltipText;
            cell.Text = string.Empty;
          }
          else
          {
            cell.Locked = true;
            cell.ForeColor = Color.Black;
            cell.TooltipText = string.Empty;
            cell.Text = number.ToString();
          }
        }
      }
    }
  }
}
