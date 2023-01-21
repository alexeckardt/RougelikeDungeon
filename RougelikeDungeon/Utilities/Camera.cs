using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RougelikeDungeon.Objects;

//Thanks to David Amador and o KB o. Static since we will only have one camera.

namespace RougelikeDungeon.Utilities
{
    internal class Camera
    {
        private Matrix TransformMatrix; //A transformation matrix containing info on our position, how much we are rotated and zoomed etc.
        

        public float Rotation;
        private float zoom;
        private Rectangle screenRect;
       
        public bool updateYAxis = true; //Should the camera move along on the y axis?
        public bool updateXAxis = true; //Should the camera move along on the x axis?

        //Position
        private Vector2 Position;
        private Vector2 RealPosition;
        private Vector2 GoalPosition;
        private Vector2 GoalPositionOffset = Vector2.Zero;
        private float CameraSmoothness = 12f;

        private Camera()
        {
            zoom = 1.0f;
            Rotation = 0.0f;

            //Start the camera at the center of the screen:
            Position = new Vector2(Resolution.VirtualWidth / 2, Resolution.VirtualHeight / 2);
        }

        private static Camera inst;
        public static Camera Instance
        {
            get
            {
                if (inst == null)
                    inst = new Camera();

                return inst;
            }
        }

        /// <summary>
        /// This rectangle covers the entire screen based on where the camera is, useful if you need to determine what is currently viewable to the player, or the position of the camera.
        /// </summary>
        public Rectangle ScreenRect 
        {
            get { return screenRect; }
        }

        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value; if (zoom < 0.1f) zoom = 0.1f; //Negative zoom will flip image.
            }
        }

        public void Update(Vector2 follow, GameTime gameTime)
        {
            var time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            GoalPosition = follow.Floored();
            RealPosition = Vector2.Lerp(RealPosition, GoalPosition, CameraSmoothness * time);

            //Reset, Floor Positions
            //Position += FractionalPosition;
            //FractionalPosition = new Vector2(Position.X % 1, Position.Y % 1);
            //IntegerPosition = Position - FractionalPosition;
            //Position = IntegerPosition;

            //Calculate Camera Matrix
            Position = RealPosition.Floored();
            CalculateMatrixAndRectangle();
        }


        /// <summary>
        /// Immediately sets the camera to look at the position passed in.
        /// </summary>
        public void LookAt(Vector2 lookAt)
        {
            //Immediately looks at the vector passed in:
            if (updateXAxis == true)
                Position.X = lookAt.X;
            if (updateYAxis == true)
                Position.Y = lookAt.Y;
        }

        private void CalculateMatrixAndRectangle()
        {
            //The math involved with calculated our transformMatrix and screenRect is a little intense, so instead of calling the math whenever we need these variables,
            //we'll calculate them once each frame and store them... when someone needs these variables we will simply return the stored variable instead of re cauclating them every time.

            //Calculate the camera transform matrix:
            TransformMatrix = Matrix.CreateTranslation(new Vector3(-Position, 0)) * Matrix.CreateRotationZ(Rotation) *
                        Matrix.CreateScale(new Vector3(zoom, zoom, 1)) * Matrix.CreateTranslation(new Vector3(Resolution.VirtualWidth
                            * 0.5f, Resolution.VirtualHeight * 0.5f, 0));

            //Now combine the camera's matrix with the Resolution Manager's transform matrix to get our final working matrix:
            TransformMatrix = TransformMatrix * Resolution.getTransformationMatrix();

            //Round the X and Y translation so the camera doesn't jerk as it moves:
            TransformMatrix.M41 = (float)Math.Round(TransformMatrix.M41, 0);
            TransformMatrix.M42 = (float)Math.Round(TransformMatrix.M42, 0);

            //Calculate the rectangle that represents where our camera is at in the world:
            screenRect = VisibleArea();
        }

        /// <summary>
        /// Calculates the screenRect based on where the camera currently is.
        /// </summary>
        private Rectangle VisibleArea()
        {
            Matrix inverseViewMatrix = Matrix.Invert(TransformMatrix);
            Vector2 tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            Vector2 tr = Vector2.Transform(new Vector2(Resolution.VirtualWidth, 0), inverseViewMatrix);
            Vector2 bl = Vector2.Transform(new Vector2(0, Resolution.VirtualHeight), inverseViewMatrix);
            Vector2 br = Vector2.Transform(new Vector2(Resolution.VirtualWidth, Resolution.VirtualHeight), inverseViewMatrix);
            Vector2 min = new Vector2(
                MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
            Vector2 max = new Vector2(
                MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
            return new Rectangle((int)min.X, (int)min.Y, (int)(Resolution.VirtualWidth / zoom), (int)(Resolution.VirtualHeight / zoom));
        }

        public Matrix GetTransformMatrix()
        {
            return TransformMatrix; //Return the transformMatrix we calculated earlier in this frame.
        }
    }
}
