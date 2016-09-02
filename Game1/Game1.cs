using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public enum PlayerType { CPU, Player1, Player2 };
    public enum GameState { Menu, Players, CPU, GameOver };
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Menu menu;

        Bat player1;
        Bat player2;

        Ball ball;

        public SpriteFont font;
        public static int player1Score;
        public static int player2Score;

        public GameState gameState;

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
            // TODO: Add your initialization logic here

            player1Score = 0;
            player2Score = 0;

            gameState = GameState.Menu;

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

            font = Content.Load<SpriteFont>("Font");

            menu = new Menu(this, spriteBatch);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // While the menu is active, only menu-input is checked.
            // All game-related methods are only called during actual gameplay.
            switch (gameState)
            {
                case GameState.Menu:
                    menu.Update(gameTime);
                    break;
                case GameState.GameOver:
                    break;
                default:
                    //CheckIntersect();
                    //player1.Update(gameTime);
                    //player2.Update(gameTime);
                    //ball.Update(gameTime);
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.Menu:
                    menu.Draw(gameTime);
                    break;
                case GameState.GameOver:
                    break;
                default:
                    //player1.Draw(gameTime);
                    //player2.Draw(gameTime);
                    //ball.Draw(gameTime);

                    spriteBatch.DrawString(font, player1Score.ToString(), new Vector2(GraphicsDevice.Viewport.Width / 2 + 50, 50), Color.Red);
                    spriteBatch.DrawString(font, player2Score.ToString(), new Vector2(GraphicsDevice.Viewport.Width / 2 - 50, 50), Color.Red);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void CheckIntersect()
        {
            if (ball.intersectionRectangle.Intersects(player1.intersectionRectangle) || ball.intersectionRectangle.Intersects(player2.intersectionRectangle))
            {
                ball.Bounce();
            }

        }
    }
}
