using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjetJeuVideo;

public class Meteor
{
    private Texture2D _texture;
    private Vector2 _position;
    
    private int _speed;
    private Rectangle _rect;
    public Rectangle Rect => _rect;
    public bool IsOffScreen => _position.Y > 800; // Si la météorite sort de l'écran

    public Meteor(Texture2D texture, Vector2 position, int speed){
        _texture = texture;
        _position = position;
        _speed = speed;

        float scale = 0.15f;
        int newWidth = (int)(_texture.Width * scale);
        int newHeight = (int)(_texture.Height * scale);

        // Initialiser la hitbox
        _rect = new Rectangle((int)_position.X, (int)_position.Y, newWidth, newHeight);
    }

    public void Update()
    {
        // Faire tomber la météorite
        _position.Y += _speed;
        _rect.Y = (int)_position.Y; // Mettre à jour la hitbox
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        float scale = 0.15f; // Facteur d'échelle pour correspondre à la hitbox
        spriteBatch.Draw(_texture, _position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }

}