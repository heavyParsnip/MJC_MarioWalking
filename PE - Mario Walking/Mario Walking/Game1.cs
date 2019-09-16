using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Mario_Walking
{

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // The Mario to draw depending on the current state
        Mario mario;

        // Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Sets up the mario location
            Vector2 marioLoc = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

            // Load the single spritesheet and create a new Mario object
            Texture2D spriteSheet = Content.Load<Texture2D>("Mario");

            mario = new Mario(spriteSheet, marioLoc, MarioState.FaceRight);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Handles animation for you
            mario.UpdateAnimation(gameTime);

            // *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
            // PRACTICE EXERCISE: Add your finite state machine code (switch statement) here!
            // - Be sure to check the finite state machine's state first
            // - Then check for specific transitions inside each state (may require keyboard input)
            // - Update Mario's state as needed

            // Step 1: Grab user input

            // Step 2: Change state
            // Step 3: Move Mario only when walking

            // *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
            KeyboardState kbState = Keyboard.GetState();

            switch (mario.State)
            {
                case MarioState.FaceLeft:
                    if (kbState.IsKeyDown(Keys.Left) == true)
                    {
                        mario.State = MarioState.WalkLeft;
                    }
                    else if(kbState.IsKeyDown(Keys.Right) == true)
                    {
                        mario.State = MarioState.FaceRight;
                        break;
                    }
                    else if(kbState.IsKeyDown(Keys.Down) == true)
                    {
                        mario.State = MarioState.CrouchLeft;
                        break;
                    }
                    break;
                case MarioState.WalkLeft:
                    if(kbState.IsKeyDown(Keys.Left) == true)
                    {
                        mario.X -= 3.0f;
                        break;
                    }
                    else if(kbState.IsKeyUp(Keys.Left) == true)
                    {
                        mario.State = MarioState.FaceLeft;
                        break;
                    }
                    break;
                case MarioState.FaceRight:
                    if (kbState.IsKeyDown(Keys.Right) == true)
                    {
                        mario.State = MarioState.WalkRight;
                        break;
                    }
                    else if(kbState.IsKeyDown(Keys.Left) == true)
                    {
                        mario.State = MarioState.FaceLeft;
                        break;
                    }
                    else if(kbState.IsKeyDown(Keys.Down) == true)
                    {
                        mario.State = MarioState.CrouchRight;
                        break;
                    }
                    break;
                case MarioState.WalkRight:
                    if(kbState.IsKeyDown(Keys.Right) == true)
                    {
                        mario.X += 3.0f;
                        break;
                    }
                    else if(kbState.IsKeyUp(Keys.Right) == true)
                    {
                        mario.State = MarioState.FaceRight;
                        break;
                    }
                    break;
                case MarioState.CrouchLeft:
                    if(kbState.IsKeyDown(Keys.Down) == true)
                    {
                        break;
                    }
                    else if(kbState.IsKeyUp(Keys.Down) == true)
                    {
                        mario.State = MarioState.FaceLeft;
                        break;
                    }
                    break;
                case MarioState.CrouchRight:
                    if(kbState.IsKeyDown(Keys.Down) == true)
                    {
                        break;
                    }
                    else if(kbState.IsKeyUp(Keys.Down) == true)
                    {
                        mario.State = MarioState.FaceRight;
                        break;
                    }
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // Begin the sprite batch
            spriteBatch.Begin();

            mario.Draw(spriteBatch);

            // End the sprite batch
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
