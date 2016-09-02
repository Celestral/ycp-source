using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace YoungCapitalPong
{
    class Bat : DrawableGameComponent
    {
        // Used for the CPU bat to decide whether to go up or down.
        YoungCapitalPong game;
        Ball ball;

        // The basic properties of the bat.
        private Texture2D sprite;
        private Texture2D glowSprite;
        private Vector2 position;
        private int speed;
        private PlayerType playerType;
        public Rectangle intersectionRectangle;

        // Keyboardstate needed for input.
        private KeyboardState keyboardState;

        // Used for lower bounds for bat.
        private Viewport viewport;

        // Used for drawing.
        SpriteBatch spriteBatch;

        /// <summary>
        /// Creates a new bat and sets the position depending on whether the bat is the first player or second player.
        /// </summary>
        /// <param name="game">The parent game</param>
        /// <param name="spriteBatch">The parent game's spriteBatch</param>
        /// <param name="playerType">The playerType (Player1, Player2 or CPU) needed for positioning and input</param>
        public Bat(YoungCapitalPong game, SpriteBatch spriteBatch, PlayerType playerType) : base(game)
        {
            this.game = game;

            this.playerType = playerType;
            this.speed = 4;
            this.sprite = game.Content.Load<Texture2D>("bat");
            this.glowSprite = game.Content.Load<Texture2D>("bat glow");
            this.spriteBatch = spriteBatch;

            viewport = game.GraphicsDevice.Viewport;

            // Bat is placed on the left side if it's Player2 or CPU and on the right side if it's Player1.
            if (this.playerType == PlayerType.Player1)
            {
                this.position = new Vector2(viewport.Width - 40, viewport.Height / 2 - sprite.Height / 2);
            }
            else
            {
                this.position = new Vector2(40 - sprite.Width, viewport.Height / 2 - sprite.Height / 2);
            }
        }

        /// <summary>
        /// Player1 uses Up and Down keys.
        /// Player2 uses W and S keys.
        /// CPU moves automatically without input.
        /// All have an upper bound (0) and lower bound (the lowest pixel minus the height of the sprite).
        /// The bounding box is updated with the new position.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            ball = game.playScreen.ball;
            keyboardState = Keyboard.GetState();
            if (playerType == PlayerType.Player1)
            {
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    position = new Vector2(position.X, Math.Max(0, position.Y - speed));
                }
                else if (keyboardState.IsKeyDown(Keys.Down))
                {
                    position = new Vector2(position.X, Math.Min(position.Y + speed, viewport.Height - sprite.Height));
                }
            }
            else if (playerType == PlayerType.Player2)
            {
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    position = new Vector2(position.X, Math.Max(0, position.Y - speed));
                }
                else if (keyboardState.IsKeyDown(Keys.S))
                {
                    position = new Vector2(position.X, Math.Min(position.Y + speed, viewport.Height - sprite.Height));
                }
            }
            else
            {
                // If the ball is higher than the CPU, the CPU goes up.
                // If the ball is lower than the CPU, the CPU goes down.
                if ((ball.position.Y - ball.sprite.Height / 2) < (this.position.Y))
                {
                    position = new Vector2(position.X, Math.Max(0, position.Y - speed));
                }
                else if ((ball.position.Y - ball.sprite.Height / 2) > (this.position.Y))
                {
                    position = new Vector2(position.X, Math.Min(position.Y + speed, viewport.Height - sprite.Height));
                }
            }

            intersectionRectangle = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the bat sprite in the correct position and an underlying sprite consisting of only a glow effect.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(glowSprite, new Vector2 (position.X - 5, position.Y - 9), Color.White);
            spriteBatch.Draw(sprite, position, Color.White);
            base.Draw(gameTime);
        }
    }
}
