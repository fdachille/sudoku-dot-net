namespace Sudoku
{
  partial class MainForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( MainForm ) );
      this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.m_newGameButton = new System.Windows.Forms.ToolStripButton();
      this.m_difficultyComboBox = new System.Windows.Forms.ToolStripComboBox();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.m_pauseButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // toolStripContainer1
      // 
      // 
      // toolStripContainer1.ContentPanel
      // 
      this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size( 382, 366 );
      this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.toolStripContainer1.Location = new System.Drawing.Point( 0, 0 );
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new System.Drawing.Size( 382, 391 );
      this.toolStripContainer1.TabIndex = 0;
      this.toolStripContainer1.Text = "toolStripContainer1";
      // 
      // toolStripContainer1.TopToolStripPanel
      // 
      this.toolStripContainer1.TopToolStripPanel.Controls.Add( this.toolStrip1 );
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem [] {
            this.m_newGameButton,
            this.m_difficultyComboBox,
            this.toolStripSeparator2,
            this.m_pauseButton,
            this.toolStripSeparator1} );
      this.toolStrip1.Location = new System.Drawing.Point( 3, 0 );
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size( 234, 25 );
      this.toolStrip1.TabIndex = 0;
      // 
      // m_newGameButton
      // 
      this.m_newGameButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.m_newGameButton.Image = ( ( System.Drawing.Image ) ( resources.GetObject( "m_newGameButton.Image" ) ) );
      this.m_newGameButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.m_newGameButton.Name = "m_newGameButton";
      this.m_newGameButton.Size = new System.Drawing.Size( 62, 22 );
      this.m_newGameButton.Text = "New Game";
      this.m_newGameButton.Click += new System.EventHandler( this.m_newGameButton_Click );
      // 
      // m_difficultyComboBox
      // 
      this.m_difficultyComboBox.Items.AddRange( new object [] {
            "Easy",
            "Medium",
            "Difficult",
            "Evil"} );
      this.m_difficultyComboBox.Name = "m_difficultyComboBox";
      this.m_difficultyComboBox.Size = new System.Drawing.Size( 75, 25 );
      this.m_difficultyComboBox.Text = "Easy";
      this.m_difficultyComboBox.ToolTipText = "Select difficulty of next generated puzzle";
      this.m_difficultyComboBox.TextUpdate += new System.EventHandler( this.m_difficultyComboBox_TextUpdate );
      this.m_difficultyComboBox.SelectedIndexChanged += new System.EventHandler( this.m_difficultyComboBox_SelectedIndexChanged );
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size( 6, 25 );
      // 
      // m_pauseButton
      // 
      this.m_pauseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.m_pauseButton.Image = ( ( System.Drawing.Image ) ( resources.GetObject( "m_pauseButton.Image" ) ) );
      this.m_pauseButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.m_pauseButton.Name = "m_pauseButton";
      this.m_pauseButton.Size = new System.Drawing.Size( 40, 22 );
      this.m_pauseButton.Text = "Pause";
      this.m_pauseButton.Click += new System.EventHandler( this.m_pauseButton_Click );
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size( 6, 25 );
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size( 382, 391 );
      this.Controls.Add( this.toolStripContainer1 );
      this.Name = "MainForm";
      this.Text = "Sudoku";
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout( false );
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout( false );
      this.toolStripContainer1.PerformLayout();
      this.toolStrip1.ResumeLayout( false );
      this.toolStrip1.PerformLayout();
      this.ResumeLayout( false );

    }

    #endregion

    private System.Windows.Forms.ToolStripContainer toolStripContainer1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton m_newGameButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton m_pauseButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripComboBox m_difficultyComboBox;
  }
}

