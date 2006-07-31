using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sudoku
{
  public partial class MainForm : Form
  {
    private Board m_board = new Board();
    TimeSpan m_elapsed = new TimeSpan( 0 );
    Timer m_timer = new Timer();

    public MainForm()
    {
      InitializeComponent();

      this.toolStripContainer1.ContentPanel.Controls.Add( m_board );
      m_board.Dock = System.Windows.Forms.DockStyle.Fill;
      m_timer.Interval = 1000;
      m_timer.Tick += new EventHandler( m_timer_Tick );
      m_timer.Start();
      m_board.Solved += new EventHandler<EventArgs>( m_board_Solved );
    }

    void m_board_Solved( object sender, EventArgs e )
    {
      m_timer.Stop();
      m_pauseButton.Text.Replace( "Pause", "Solved" );
      System.Windows.Forms.MessageBox.Show( this, "You solved it!" );
      m_board.Refresh();
    }

    void m_timer_Tick( object sender, EventArgs e )
    {
      if ( !m_pauseButton.Checked )
      {
        m_elapsed += new TimeSpan( 0, 0, 1 );
        m_pauseButton.Text = "Pause " +
          ( ( int ) m_elapsed.TotalMinutes ).ToString() + ":" +
          ( m_elapsed.Seconds < 10 ? "0" : "" ) +
          m_elapsed.Seconds.ToString();
      }
    }

    private void m_newGameButton_Click( object sender, EventArgs e )
    {
      m_board.Initialize();
      m_elapsed = new TimeSpan( 0 );
      m_pauseButton.Checked = false;
      m_board.Visible = true;
      m_timer.Start();
    }

    private void m_pauseButton_Click( object sender, EventArgs e )
    {
      if ( m_pauseButton.Checked )
      {
        m_pauseButton.Checked = false;
        m_board.Visible = true;
      }
      else
      {
        m_pauseButton.Checked = true;
        m_board.Visible = false;
      }
    }

    private void m_difficultyComboBox_SelectedIndexChanged( object sender, EventArgs e )
    {
      switch ( m_difficultyComboBox.SelectedIndex )
      {
        case 0:
          m_board.Difficulty = Difficulty.Easy;
          break;
        case 1:
          m_board.Difficulty = Difficulty.Medium;
          break;
        case 2:
          m_board.Difficulty = Difficulty.Difficult;
          break;
        case 3:
          m_board.Difficulty = Difficulty.Evil;
          break;
        default:
          break;
      }
    }

    private void m_difficultyComboBox_TextUpdate( object sender, EventArgs e )
    {
      m_difficultyComboBox.Text = m_board.Difficulty.ToString();
    }

  }
}