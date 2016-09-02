using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    // Used to differentiate between input methods.
    public enum PlayerType { CPU, Player1, Player2 };
    // Used to differentiate between the specific screens and modes of the game.
    public enum GameState { Menu, Players, CPU, GameOver };
    public class Game1 : Game
    {
        // Auto generated for Monogame projects.
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // The different types of screens to display.
        MenuScreen menu;
        public PlayScreen playScreen;
        public GameOverScreen gameOverScreen;

        // One font is used throughout the project and YoungCapital's Orange color is used for text.
        public SpriteFont font;
        public Color youngCapitalOrange;

        // The gamestate the game is currently in.
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
            // Game should start on Menu screen.
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

            // Load the standard font and color.
            font = Content.Load<SpriteFont>("Font");
            youngCapitalOrange = new Color(245, 128, 32);

            // Create the menu screen.
            menu = new MenuScreen(this, spriteBatch);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // All updates are delegated to the screen objects of the current gamestate.
            switch (gameState)
            {
                case GameState.Menu:
                    menu.Update(gameTime);
                    break;
                case GameState.GameOver:
                    gameOverScreen.Update(gameTime);
                    break;
                default:
                    playScreen.Update(gameTime);
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
            // Sets the background color to black.
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            // All further drawing is delegated to the screen objects of the current gamestate.
            switch (gameState)
            {
                case GameState.Menu:
                    menu.Draw(gameTime);
                    break;
                case GameState.GameOver:
                    gameOverScreen.Draw(gameTime);
                    break;
                default:
                    playScreen.Draw(gameTime);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
