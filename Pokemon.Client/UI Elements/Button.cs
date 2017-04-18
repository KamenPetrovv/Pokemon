﻿
namespace Pokemon.Client.UI_Elements
{
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Pokemon.Client.Interfaces;
    using Pokemon.Client.Textures;

    using System;
    using Microsoft.Xna.Framework;

    public enum ButtonState
    {
        None,
        Hovered,
        Clicked
    }

    public class Button : Interfaces.IDrawable, IUpdatable
    {
        public  Texture2D SpriteSheet { get; set; }

        public Vector2 Position { get; set; }

        internal Color DefaultSpriteColour;

        internal Color HoverSpriteColour;

        internal Text Text;

        internal Color DefaultTextColour;

        internal Color HoverTextColour;

        private ButtonState previousButtonState;

        public ButtonState currentButtonState { get; set; }

        private Action onClicked;

        private Action onHovered;

        public int TextureWidth { get; }

        public int TextureHeight { get; }

        internal event Action OnClicked
        {
            add { onClicked += value; }
            remove { onClicked -= value; }
        }

        internal event Action OnHovered
        {
            add { onHovered += value; }
            remove { onHovered -= value; }
        }

        internal void HandleInput ( KeyboardState keyboardState, bool IsHovered )
        {
            if ( IsHovered )
            {
                currentButtonState = ButtonState.Hovered;

                if ( keyboardState.IsKeyDown (Keys.Enter) )
                {
                    currentButtonState = ButtonState.Clicked;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (currentButtonState == ButtonState.Clicked)
            {
                if ( onClicked != null )
                {
                    onClicked( );
                };
                currentButtonState = ButtonState.None;
            }
            if (currentButtonState == ButtonState.Hovered)
            {
                if (onHovered != null)
                {
                    onHovered();
                }
                
            }

            Text.Color = DefaultTextColour;

            if ( currentButtonState == ButtonState.Hovered )
            {
                Text.Color = HoverTextColour;
            }
        }
        //Obsolete
        private void FireEvents ( )
        {
            if ( currentButtonState != previousButtonState )
            {
                switch ( currentButtonState )
                {
                    case ButtonState.Hovered:
                        if ( onHovered != null )
                        {
                            onHovered ( );
                        }
                        break;
                    case ButtonState.Clicked:
                        if ( onClicked != null )
                        {
                            onClicked ( );
                        }
                        break;
                }
            }
        }

        public void Draw ( SpriteBatch spriteBatch)
        {
            
            if ( currentButtonState == ButtonState.Hovered )
            {
                spriteBatch.Draw (this.SpriteSheet, this.Position, new Rectangle (0, 0, 306,111), HoverSpriteColour);
            }
            else
            {
                spriteBatch.Draw (this.SpriteSheet, this.Position, new Rectangle (0, 0, 306, 111), DefaultSpriteColour);
            }
            Text.Position = this.Position +  new Vector2(120f,40f);
            
            Text.Draw (spriteBatch);
        }

    }
}
