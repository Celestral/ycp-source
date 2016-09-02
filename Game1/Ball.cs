using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace YoungCapitalPong
{
    public class Ball : DrawableGameComponent
    {
        // Basic properties of the ball/puck.
        public Texture2D sprite;
        private Texture2D glowSprite;
        public Vector2 position;
        private Vector2 velocity;
        private int speed;
        public Rectangle intersectionRectangle;

        // Used to generate the starting velocity.
        private Random random;

        // Used in checking for border collision.
        private Viewport viewport;

        // Used for drawing.
        SpriteBatch spriteBatch;

        /// <summary>
        /// All basic properties of the ball/puck are set here, including loading of sprites.
        /// </summary>
        /// <param name="game">The parent game</param>
        /// <param name="spriteBatch">The parent game's spriteBatch</param>
        public Ball(YoungCapitalPong game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.viewport = game.GraphicsDevice.Viewport;
            this.speed = 3;
            this.sprite = game.Content.Load<Texture2D>("ball");
            this.glowSprite = game.Content.Load<Texture2D>("ballglow");

            random = new Random();
            // Spawns the ball
            Spawn();
        }

        /// <summary>
        /// Bounces the ball back in the opposite direction, going slightly faster
        /// </summary>
        public void Bounce()
        {
            velocity = new Vector2(velocity.X * -1.2f, velocity.Y);
        }

        /// <summary>
        /// Sets the ball's position in the middle of the screen and calculates a random starting velocity.
        /// x cannot be 0 since that would leave the ball just going up and down for eternity.
        /// y cannot be 0 since the ball's bounceback does not depend on angle so it would continue in a straight line.
        /// </summary>
        public void Spawn()
        {
            this.position = new Vector2(viewport.Width / 2 - this.sprite.Height / 2, viewport.Height / 2 - this.sprite.Height / 2);

            int x = 0;
            while (x == 0)
            {
                x = random.Next(-speed *2, speed *2);
            }
            int y = 0;
            while (y == 0)
            {
                y = random.Next(-speed, speed);
            }

            velocity = new Vector2(x, y);
        }

        /// <summary>
        /// Checks whether the ball left the screen, if so the opposite player's score is increased and the ball respawns.
        /// Makes the ball bounce back if it touches the upper or lower bounds of the level.
        /// Also updates the bounding box of the ball.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (this.position.X >= viewport.Width)
            {
                PlayScreen.player2Score++;
                Spawn();
            }
            else if (this.position.X < 0)
            {
                PlayScreen.player1Score++;
                this.position = new Vector2(viewport.Width / 2 - this.sprite.Height / 2, viewport.Height / 2 - this.sprite.Height / 2);
                Spawn();
            }
            if (this.position.Y >= viewport.Height - sprite.Height || this.position.Y < 0)
                velocity.Y = velocity.Y * -1;
            this.position = position + velocity;

            intersectionRectangle = new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height);
        }

        /// <summary>
        /// Draws the ball at its current position and draw an underlying sprite for a glowy effect.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(glowSprite, new Vector2(position.X - 3, position.Y - 3), Color.White);
            spriteBatch.Draw(sprite, position, Color.White);
        }

    }
}
