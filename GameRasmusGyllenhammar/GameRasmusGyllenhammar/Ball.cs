using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRasmusGyllenhammar
{
    class Ball : Imove
    {
        private double yPosition;
        private double xPosition;

        public void move()
        {
            throw new NotImplementedException();
        }
        public double XPosition { get => xPosition; set => xPosition = value; }
        public double YPosition { get => yPosition; set => yPosition = value; }
        
    }
}
