using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjetJeuVideo;
public class Projectile  {
    private Texture2D _texture;
    public Texture2D _Texture { get => _texture; init => _texture = value; }
    private Color _color = Color.White;
    
    private Vector2 _position;
    private int _size { get; init; }
    private int velocity { get; init; }
    public Boolean estToucher = false;
    public Rectangle _Rect { get => new Rectangle((int) _position.X,
        (int ) _position.Y, _size, _size); }
    
    //constructeur de projectile
    public Projectile(Texture2D texture, int a,int b,int size) {
        _Texture = texture;
        _position.X = a;
        _position.Y = b;
        _size = size;
        velocity = 3;
    }
    
    //methode pour avancer les projectiles
    public void Update(GameTime gameTime) {
        _position.X += velocity;
        
    }
    
    
    public void Draw(SpriteBatch spriteBatch) {
        var origin = new Vector2(_texture.Width / 2f, _texture.Height / 2f);
        spriteBatch.Draw(
            _texture, // Texture2D,
            _Rect, // Rectangle destinationRectangle,
            null, // Nullable<Rectangle> sourceRectangle,
            _color, // Color,
            0.0f, // float rotation,
            origin, // Vector2 origin,
            SpriteEffects.None, // SpriteEffects effects,
            0f ); // float layerDepth
    }
    
}