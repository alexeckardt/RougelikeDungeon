using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RougelikeDungeon
{
    internal class Input
    {
        //Singleton
        static Input instance;

        //Store Temporarily
        KeyboardState keyboardState;

        private Input() {}

        //Get
        public static Input Instance
        {
            get { 

                //Singleton Instantiation
                if (instance == null)
                {
                    instance = new Input();
                }

                //Update on Call, so Subsequent on same call do not need to perform the get
                keyboardState = Keyboard.GetState();

                //Return
                return instance; 
            }
        }

        //Passback Values
        public int IsKeyDown(Keys key) => keyboardState.IsKeyDown(key) ? 1 : 0;
    }
}
