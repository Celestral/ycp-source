using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace YoungCapitalPong
{
    public class GameOverScreen : DrawableGameComponent
    {
        // Game is needed for getting and setting the current gameState.
        YoungCapitalPong game;

        // SpriteBatch is needed for drawing.
        SpriteBatch spriteBatch;

        // The font used in the game.
        SpriteFont font;

        // Viewport used for aligning text.
        Viewport viewport;

        // String dependant on winning player.
        string winner;

        // KeyboardState for input
        KeyboardState keyboardState;

        // Needed for reloading the game in either CPU or Players mode.
        GameState previousState;

        /// <summary>
        /// Imports the necessary variables.
        /// </summary>
        /// <param name="game">The parent game</param>
        /// <param name="spriteBatch">The parent game's spriteBatch</param>
        /// <param name="previousState">The playmode from which we came</param>
        public GameOverScreen(YoungCapitalPong game, SpriteBatch spriteBatch, GameState previousState) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.font = game.font;
            this.previousState = previousState;
            this.viewport = game.GraphicsDevice.Viewport;
        }


        /// <summary>
        /// Restart the last game mode (vs player or vs cpu) when pressing Enter
        /// Exits to main menu when pressing Escape.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                game.gameState = previousState;
                game.playScreen = new PlayScreen(game, spriteBatch);
            }
            else if (keyboardState.IsKeyDown(Keys.Escape))
            {
                game.gameState = GameState.Menu;
            }
            base.Update(gameTime);
        }


        /// <summary>
        /// Draws the text for the winning player in the middle of the screen.
        /// Draws instruction text for replaying/returning to menu underneath winning text.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            // String dependant on winner.
            if (PlayScreen.player1Score == 5)
                winner = "Player 1 Wins!";
            else
                winner = "Player 2 Wins!";

            // MeasureString is used to calculate the length of the string in the font so the text can be alligned.
            Vector2 winnerSize = font.MeasureString(winner);

            string instruction = "Press Escape to return to menu or Enter to play again";
            Vector2 instructionSize = font.MeasureString(instruction);

            spriteBatch.DrawString(font, winner, new Vector2(viewport.Width / 2, viewport.Height / 2),
                game.youngCapitalOrange, 0f, new Vector2(winnerSize.X / 2, winnerSize.Y / 2), 3f, SpriteEffects.None, 0);

            spriteBatch.DrawString(font, instruction, new Vector2(viewport.Width / 2, viewport.Height / 2 + (winnerSize.Y * 2)), game.youngCapitalOrange,
                0f, new Vector2(instructionSize.X / 2, instructionSize.Y / 2), 1f, SpriteEffects.None, 0);

            base.Draw(gameTime);
        }
    }
}
