using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace YoungCapitalPong
{
    public class PlayScreen : DrawableGameComponent
    {
        // Game is needed for getting and setting the current gameState.
        YoungCapitalPong game;

        // SpriteBatch is needed for drawing.
        SpriteBatch spriteBatch;

        // The font used in the game.
        SpriteFont font;

        // The game's entities: Two bats and a ball.
        // Ball is public so it can be accessed by the CPU bat.
        Bat player1;
        Bat player2;
        public Ball ball;

        // The scores for each player.
        public static int player1Score;
        public static int player2Score;

        /// <summary>
        /// Imports the necessary variables.
        /// </summary>
        /// <param name="game">The parent game</param>
        /// <param name="spriteBatch">The parent game's spriteBatch</param>
        public PlayScreen(YoungCapitalPong game, SpriteBatch spriteBatch) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.font = game.font;
            this.Initialize();
        }

        /// <summary>
        /// Initialises all necessary entities.
        /// Player scores are (re)set to 0.
        /// Depending on the gameState creates two player Bats or one player Bat and one CPU Bat.
        /// Ball is created before bats since CPU bat is dependant on ball.
        /// </summary>
        public override void Initialize()
        {
            player1Score = 0;
            player2Score = 0;

            ball = new Ball(game, spriteBatch);

            if (game.gameState == GameState.Players)
            {
                player1 = new Bat(game, spriteBatch, PlayerType.Player1);
                player2 = new Bat(game, spriteBatch, PlayerType.Player2);
            }
            else if (game.gameState == GameState.CPU)
            {
                player1 = new Bat(game, spriteBatch, PlayerType.Player1);
                player2 = new Bat(game, spriteBatch, PlayerType.CPU);
            }

            base.Initialize();
        }

        /// <summary>
        /// Checks for intersections.
        /// Checks if the game is already over.
        /// Calls for update of bats and of ball.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            CheckIntersect();
            CheckWin();

            player1.Update(gameTime);
            player2.Update(gameTime);
            ball.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Calls for the bats and ball to draw themselves and draws the scores to the screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            player1.Draw(gameTime);
            player2.Draw(gameTime);
            ball.Draw(gameTime);

            spriteBatch.DrawString(font, player1Score.ToString(), new Vector2(GraphicsDevice.Viewport.Width / 2 + 50, 50), game.youngCapitalOrange);
            spriteBatch.DrawString(font, player2Score.ToString(), new Vector2(GraphicsDevice.Viewport.Width / 2 - 50, 50), game.youngCapitalOrange);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Checks if the bounding boxes of the ball and a bat intersect, if so the ball is bounced.
        /// </summary>
        public void CheckIntersect()
        {
            if (ball.intersectionRectangle.Intersects(player1.intersectionRectangle) || ball.intersectionRectangle.Intersects(player2.intersectionRectangle))
            {
                ball.Bounce();
            }

        }

        /// <summary>
        /// The game ends if a player has scored 5 points.
        /// The GameOver screen is created and the game's gameState is set to GameOver.
        /// </summary>
        public void CheckWin()
        {
            if (player1Score == 5 || player2Score == 5)
            {
                game.gameOverScreen = new GameOverScreen(game, spriteBatch, game.gameState);
                game.gameState = GameState.GameOver;
            }
        }
    }
}
