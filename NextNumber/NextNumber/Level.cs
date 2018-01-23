using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextNumber
{
    class Level
    {
       
       
        private int _widthCount;
        
        public int WidthCount
        {
            get { return _widthCount; }
            set { _widthCount = value; }
        }
        private int _heightCount;

        public int HeightCount
        {
            get { return _heightCount; }
            set { _heightCount = value; }
        }
        public Level(int x, int y)
        {
            this._widthCount = x;
            this._heightCount = y;
           
        }

    }
}
