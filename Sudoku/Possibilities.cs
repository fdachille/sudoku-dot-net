using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
  class Possibilities : ICloneable
  {
    private List<int> [ , ] m_possibilities = null;

    public List<int> [ , ] PossibleList
    {
      get { return m_possibilities; }
    }

    public Possibilities()
    {
      m_possibilities = new List<int> [ 9, 9 ];
      for ( int x = 0; x < 9; x++ )
      {
        for ( int y = 0; y < 9; y++ )
        {
          m_possibilities [ x, y ] = new List<int>();
        }
      }
    }

    public bool AnySimpleEliminations()
    {
      for ( int x = 0; x < 9; x++ )
      {
        for ( int y = 0; y < 9; y++ )
        {
          if ( m_possibilities [ x, y ].Count == 1 )
            return true;
        }
      }
      return false;
    }

    #region ICloneable Members

    public object Clone()
    {
      Possibilities newPossibilities = new Possibilities();
      for ( int x = 0; x < 9; x++ )
      {
        for ( int y = 0; y < 9; y++ )
        {
          newPossibilities.m_possibilities [ x, y ] = ( List<int> ) m_possibilities.Clone();
        }
      }
      return newPossibilities;
    }

    #endregion
  }
}
