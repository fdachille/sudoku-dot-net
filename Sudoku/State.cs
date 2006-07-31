using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
  public enum Difficulty { Easy, Medium, Difficult, Evil };

  class State : ICloneable
  {
    public event EventHandler<EventArgs> UpdateEvent;
    private int [ , ] m_numbers = new int [ 9, 9 ];
    private static Random m_random = new Random();

    public int [ , ] Numbers
    {
      get { return m_numbers; }
    }

    public State()
    {
      Clear();
    }

    public void Clear()
    {
      for ( int x = 0; x < 9; x++ )
      {
        for ( int y = 0; y < 9; y++ )
        {
          m_numbers [ x, y ] = 0;
        }
      }
    }

    public void Initialize( Difficulty difficulty )
    {
      Clear();
      for ( int i = 1; i < 10; i++ )
      {
        for ( int x = 0; x < 3; x++ )
        {
          for ( int y = 0; y < 3; y++ )
          {
            int testPositionX, testPositionY;
            bool duplicate;
            int count = 0;
            do
            {
              duplicate = false;
              testPositionX = 3 * x + m_random.Next( 0, 3 );
              testPositionY = 3 * y + m_random.Next( 0, 3 );
              duplicate =
                m_numbers [ testPositionX, testPositionY ] != 0 ||
                InGroup( i, testPositionX, testPositionY ) ||
                InRow( i, testPositionY ) ||
                InColumn( i, testPositionX );
            } while ( count++ < 1000 && duplicate );

            if ( duplicate )
            {
              Clear();
              i = 0;
              x = y = 3;
            }
            else
            {
              m_numbers [ testPositionX, testPositionY ] = i;
            }
          }
        }
      }
      if ( !IsValid() )
      {
        System.Windows.Forms.MessageBox.Show( "Invalid...contact support!" );
      }
      MakeHarder( difficulty );
      RaiseUpdateBoard();
    }

    public Possibilities GetPossibilities()
    {
      Possibilities possibilities = new Possibilities();
      for ( int x = 0; x < 9; x++ )
      {
        for ( int y = 0; y < 9; y++ )
        {
          for ( int i = 1; i < 10; i++ )
          {
            if ( m_numbers [ x, y ] == 0 && !InGroup( i, x, y ) && !InRow( i, y ) && !InColumn( i, x ) )
            {
              possibilities.PossibleList [ x, y ].Add( i );
            }
          }
        }
      }
      return possibilities;
    }

    public bool IsSolvable()
    {
      State tempState = ( State ) Clone();

      bool solvedAny = false;
      do
      {
        Possibilities possibilities = tempState.GetPossibilities();
        solvedAny = false;
        for ( int x = 0; x < 9; x++ )
        {
          for ( int y = 0; y < 9; y++ )
          {
            if ( possibilities.PossibleList [ x, y ].Count == 1 )
            {
              tempState.m_numbers [ x, y ] = possibilities.PossibleList [ x, y ] [ 0 ];
              solvedAny = true;
            }
          }
        }
      } while ( solvedAny );

      return tempState.IsSolved();
    }

    public bool IsSolved()
    {
      for ( int x = 0; x < 9; x++ )
      {
        for ( int y = 0; y < 9; y++ )
        {
          int number = m_numbers [ x, y ];
          if ( number == 0 )
            return false;
          m_numbers [ x, y ] = 0;
          bool duplicate =
            InGroup( number, x, y ) ||
            InRow( number, y ) ||
            InColumn( number, x );
          m_numbers [ x, y ] = number;
          if ( duplicate )
            return false;
        }
      }
      return true;
    }

    public bool IsValid()
    {
      for ( int x = 0; x < 9; x++ )
      {
        for ( int y = 0; y < 9; y++ )
        {
          if ( !IsValid( x, y ) )
            return false;
        }
      }
      return true;
    }

    public bool IsValid( int x, int y )
    {
      int number = m_numbers [ x, y ];
      if ( number == 0 )
        return true;
      m_numbers [ x, y ] = 0;

      bool duplicate =
        InGroup( number, x, y ) ||
        InRow( number, y ) ||
        InColumn( number, x );
      m_numbers [ x, y ] = number;

      return !duplicate;
    }

    public bool InGroup( int testNumber, int x, int y )
    {
      x = 3 * ( int ) ( x / 3 );
      y = 3 * ( int ) ( y / 3 );

      return
        m_numbers [ x + 0, y + 0 ] == testNumber ||
        m_numbers [ x + 1, y + 0 ] == testNumber ||
        m_numbers [ x + 2, y + 0 ] == testNumber ||
        m_numbers [ x + 0, y + 1 ] == testNumber ||
        m_numbers [ x + 1, y + 1 ] == testNumber ||
        m_numbers [ x + 2, y + 1 ] == testNumber ||
        m_numbers [ x + 0, y + 2 ] == testNumber ||
        m_numbers [ x + 1, y + 2 ] == testNumber ||
        m_numbers [ x + 2, y + 2 ] == testNumber;
    }

    public bool InRow( int testNumber, int row )
    {
      return
        m_numbers [ 0, row ] == testNumber ||
        m_numbers [ 1, row ] == testNumber ||
        m_numbers [ 2, row ] == testNumber ||
        m_numbers [ 3, row ] == testNumber ||
        m_numbers [ 4, row ] == testNumber ||
        m_numbers [ 5, row ] == testNumber ||
        m_numbers [ 6, row ] == testNumber ||
        m_numbers [ 7, row ] == testNumber ||
        m_numbers [ 8, row ] == testNumber;
    }

    public bool InColumn( int testNumber, int column )
    {
      return
        m_numbers [ column, 0 ] == testNumber ||
        m_numbers [ column, 1 ] == testNumber ||
        m_numbers [ column, 2 ] == testNumber ||
        m_numbers [ column, 3 ] == testNumber ||
        m_numbers [ column, 4 ] == testNumber ||
        m_numbers [ column, 5 ] == testNumber ||
        m_numbers [ column, 6 ] == testNumber ||
        m_numbers [ column, 7 ] == testNumber ||
        m_numbers [ column, 8 ] == testNumber;
    }

    public void RaiseUpdateBoard()
    {
      EventHandler<EventArgs> temp = UpdateEvent;
      if ( temp != null )
        temp( this, new EventArgs() );
    }

    public void MakeHarder( Difficulty difficulty )
    {
      int tries = 0;
      int removeLimit;
      switch ( difficulty )
      {
        case Difficulty.Easy:
          removeLimit = 57;
          break;
        case Difficulty.Medium:
          removeLimit = 62;
          break;
        case Difficulty.Difficult:
          removeLimit = 70;
          break;
        case Difficulty.Evil:
          removeLimit = 81;
          break;
        default:
          removeLimit = 81;
          break;
      }
      int removed = 0;
      while ( tries < 5 && removed < removeLimit )
      {
        int testPositionX = m_random.Next( 0, 9 );
        int testPositionY = m_random.Next( 0, 9 );
        int number = m_numbers [ testPositionX, testPositionY ];
        m_numbers [ testPositionX, testPositionY ] = 0;
        if ( IsSolvable() )
        {
          tries = 0;
          //RaiseUpdateBoard();
          removed++;
        }
        else
        {
          tries++;
          m_numbers [ testPositionX, testPositionY ] = number;
        }
      }
    }

    #region ICloneable Members

    public object Clone()
    {
      State newState = new State();
      newState.m_numbers = ( int [ , ] ) m_numbers.Clone();
      return newState;
    }

    #endregion
  }
}
