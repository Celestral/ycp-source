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
    class MenuScreen : DrawableGameComponent
    {
        // Game is needed for getting and setting the current gameState.
        YoungCapitalPong game;

        // SpriteBatch is needed for drawing.
        SpriteBatch spriteBatch;

        // The font used in the game.
        SpriteFont font;

        // The pointer for the currently selected menu item
        Texture2D pointer;
        int position;

        // Keyboardstates needed for input.
        KeyboardState keyboardState;
        KeyboardState previousKeyboardState;

        /// <summary>
        /// Imports the necessary variables and loads and sets the pointer.
        /// </summary>
        /// <param name="game">The parent game</param>
        /// <param name="spriteBatch">The parent game's spriteBatch</param>
        public MenuScreen (YoungCapitalPong game, SpriteBatch spriteBatch) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.font = game.font;
            pointer = game.Content.Load<Texture2D>("pointer");
            position = 0;
            previousKeyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// Basic Menu Item selector.
        /// Up and Down keys loop through the menu items.
        /// Enter selects an option, changes the current gameState and creates the new PlayScreen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            // previousKeyboardState prevents the pointer from moving every frame while the key is pressed.
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Down) && !previousKeyboardState.IsKeyDown(Keys.Down))
            {
                position = (position + 1) % 3;
            }
            else if (keyboardState.IsKeyDown(Keys.Up) && !previousKeyboardState.IsKeyDown(Keys.Up))
            {
                position = ((position - 1) + 3) % 3;
            }
            else if (keyboardState.IsKeyDown(Keys.Enter) || keyboardState.IsKeyDown(Keys.Space))
            {
                switch (position)
                {
                    // [One Player] Play against CPU.
                    case 0:
                        game.gameState = GameState.CPU;
                        game.playScreen = new PlayScreen(game, spriteBatch);
                        break;
                    // [Two Player] Play against friend.
                    case 1:
                        game.gameState = GameState.Players;
                        game.playScreen = new PlayScreen(game, spriteBatch);
                        break;
                    case 2:
                        game.Exit();
                        break;
                }
            }
            base.Update(gameTime);

            previousKeyboardState = keyboardState;
        }

        /// <summary>
        /// Draws the text for the menu items and the pointer at the right location.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.DrawString(font, "One Player", new Vector2(75, 50), game.youngCapitalOrange, 0f, new Vector2(0,0), 4f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Two Player", new Vector2(75, 150), game.youngCapitalOrange, 0f, new Vector2(0, 0), 4f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, "Exit", new Vector2(75, 250), game.youngCapitalOrange, 0f, new Vector2(0, 0), 4f, SpriteEffects.None, 0);
            spriteBatch.Draw(pointer, new Vector2(15, 55 + (position * 100)), Color.White);

            base.Draw(gameTime);
        }
    }
}
