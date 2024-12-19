using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjetJeuVideo;
public class Monster {
    private Texture2D _texture;
    public Texture2D _Texture { get => _texture; init => _texture = value; }
    private Color _color = Color.White;

    private Vector2 _position;
    private int health { get; set; }
    private int maxhealth { get; init; }
    private int attack { get; init; }
    private int velocity { get; init; }
    private int _size { get; init; }
    public Boolean estToucher;
    public Rectangle _Rect { get =>  new Rectangle((int) _position.X,
        (int ) _position.Y, _size, _size); }
    
    //constructeur de Monstre
    public Monster(Texture2D texture, int size) {
        _Texture = texture;
        _position.X = 1500;
        _position.Y = 600;
        _size = size;
        estToucher = false;
        health = 200;
        maxhealth = 200;
        attack = 1;
        velocity = 3;
    }
    //gere la vie du monstre
    public void DrawHealthBar(SpriteBatch spriteBatch, GameTime gameTime){
        // Largeur et hauteur de la jauge
        int barWidth = 150;
        int barHeight = 8;

        // Calculer la largeur en fonction de la vie
        float healthPercentage = (float)health / maxhealth;
        int currentBarWidth = (int)(barWidth * healthPercentage);

        // Couleurs pour la jauge (rouge pour fond, vert pour la vie)
        Texture2D healthBarTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        healthBarTexture.SetData(new[] { Color.White });

        // Fond de la jauge (rouge)
        spriteBatch.Draw(healthBarTexture, new Rectangle(_Rect.X-80, _Rect.Y-80, barWidth, barHeight), Color.Red);

        // Vie restante (vert)
        spriteBatch.Draw(healthBarTexture, new Rectangle(_Rect.X-80, _Rect.Y-80 , currentBarWidth, barHeight), Color.Green);
    }
    
    //methode pour avancer le monstre
    public void Update(GameTime gameTime){
        _position.X -= velocity;
    }
    
    //degat infliger
    public void degat(int attack){
        health -= attack;
        if (health <= 0){
            estToucher = true;
        }
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