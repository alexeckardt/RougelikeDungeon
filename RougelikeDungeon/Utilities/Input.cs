using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RougelikeDungeon.Utilities
{
    public class Input //Static classes can easily be accessed anywhere in our codebase. They always stay in memory so you should only do it for universal things like input.
    {
        private KeyboardState keyboardState = Keyboard.GetState();
        private KeyboardState lastKeyboardState;

        private MouseState mouseState;
        private MouseState lastMouseState;

        private Input() { }

        //Singleton Instantiation
        private static Input instance;
        public static Input Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Input();
                }

                //Update On Singleton Get
                return instance;
            }
        }

        public void Update()
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            lastMouseState = mouseState;
            mouseState = Mouse.GetState();
        }

        /// <summary>
        /// Checks if key is currently pressed.
        /// </summary>
        public bool IsKeyDown(Keys input)
        {
            return keyboardState.IsKeyDown(input);
        }

        //Integer
        public int IsKeyDownInt(Keys input) => IsKeyDown(input) ? 1 : 0;

        /// <summary>
        /// Checks if key is currently up.
        /// </summary>
        public bool IsKeyUp(Keys input)
        {
            return keyboardState.IsKeyUp(input);
        }

        //Integer
        public int IsKeyUpInt(Keys input) => IsKeyUp(input) ? 1 : 0;

        /// <summary>
        /// Checks if key was just pressed.
        /// </summary>
        public bool KeyPressed(Keys input)
        {
            if (keyboardState.IsKeyDown(input) == true && lastKeyboardState.IsKeyDown(input) == false)
                return true;
            else
                return false;
        }

        //Integer
        public int KeyPressedInt(Keys input) => KeyPressed(input) ? 1 : 0;

        /// <summary>
        /// Returns whether or not the left mouse button is being pressed.
        /// </summary>
        public bool MouseLeftDown()
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns whether or not the right mouse button is being pressed.
        /// </summary>
        public bool MouseRightDown()
        {
            if (mouseState.RightButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks if the left mouse button was clicked.
        /// </summary>
        public bool MouseLeftClicked()
        {
            if (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Checks if the right mouse button was clicked.
        /// </summary>
        public bool MouseRightClicked()
        {
            if (mouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton == ButtonState.Released)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Gets mouse coordinates adjusted for virtual resolution and camera position.
        /// </summary>
        public Vector2 MousePositionCamera()
        {
            Vector2 mousePosition = Vector2.Zero;
            mousePosition.X = mouseState.X;
            mousePosition.Y = mouseState.Y;

            return ScreenToWorld(mousePosition);
        }

        /// <summary>
        /// Gets the last mouse coordinates adjusted for virtual resolution and camera position.
        /// </summary>
        public Vector2 LastMousePositionCamera()
        {
            Vector2 mousePosition = Vector2.Zero;
            mousePosition.X = lastMouseState.X;
            mousePosition.Y = lastMouseState.Y;

            return ScreenToWorld(mousePosition);
        }

        /// <summary>
        /// Takes screen coordinates (2D position like where the mouse is on screen) then converts it to world position (where we clicked at in the world). 
        /// </summary>
        private Vector2 ScreenToWorld(Vector2 input)
        {
            input.X -= Resolution.VirtualViewportX;
            input.Y -= Resolution.VirtualViewportY;

            return Vector2.Transform(input, Matrix.Invert(Camera.Instance.GetTransformMatrix()));
        }
    }
}
