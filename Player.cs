

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ProjetJeuVideo;
public class Player {
     private Texture2D _texture;
    public Texture2D Texture { get => _texture; set => _texture = value; }
    private Color _color = Color.White;
    
    //les attribues de la classe Player
    public int Health { get; set; }
    private int Maxhealth { get; init; }
    public int Attack { get; init; }
    private int Velocity { get; init; }
    public Rectangle Rect { get => new Rectangle((int) _position.X,
        600, Size, Size); }
    private Vector2 _position;
    private int Size { get; init; }
    private bool _estToucher;
    
    //Le constructeur de la classe player
   public Player(){}
    public Player(Texture2D texture, Vector2 position, int size) {
        Texture = texture;
        _position = position;
        Size = size;
        Health = 200;
        Maxhealth = 200;
        Attack = 3;
        Velocity = 10;
    }
   
    public void DrawHealthBar(SpriteBatch spriteBatch, GameTime gameTime){
        // Largeur et hauteur de la jauge
        int barWidth = 150;
        int barHeight = 8;

        // Calculer la largeur en fonction de la vie
        float healthPercentage = (float)Health / Maxhealth;
        int currentBarWidth = (int)(barWidth * healthPercentage);

        // Couleurs pour la jauge (rouge pour fond, vert pour la vie)
        Texture2D healthBarTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
        healthBarTexture.SetData(new[] { Color.White });

        // Fond de la jauge (rouge)
        spriteBatch.Draw(healthBarTexture, new Rectangle(Rect.X-80, Rect.Y-80, barWidth, barHeight), Color.Red);

        // Vie restante (vert)
        spriteBatch.Draw(healthBarTexture, new Rectangle(Rect.X-80, Rect.Y-80 , currentBarWidth, barHeight), Color.Green);
    }

    //pour bouger le jouer vers la droite
    public void MoveRight(){
        _position.X += Velocity;
    }

    //pour bouger le joueur vers la gauche
    public void MoveLeft(){
        _position.X -= Velocity;
        
    }

    public void Damage(){
        Health -=1;
        if (Health <= 0){
            _estToucher = true;
        }
    }
    
    
    public void Draw(SpriteBatch spriteBatch) {
        var origin = new Vector2(_texture.Width / 2f, _texture.Height / 2f);
        spriteBatch.Draw(
            _texture, // Texture2D,
            Rect, // Rectangle destinationRectangle,
            null, // Nullable<Rectangle> sourceRectangle,
            _color, // Color,
            0.0f, // float rotation,
            origin, // Vector2 origin,
            SpriteEffects.None, // SpriteEffects effects,
            0f ); // float layerDepth
    }
    
}