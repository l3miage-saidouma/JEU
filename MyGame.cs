using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace ProjetJeuVideo;

public class MyGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private  Player _player;
    private Texture2D mosterTexture;
    private Texture2D menuTexture;
    private Texture2D pauseTexture;
    private Texture2D gameOverTexture;
    private Texture2D baground;
    private Texture2D _projectileTexture;
    private Texture2D _meteorTexture;
    const int backroundheight = 800;
    const int backroundwidth = 1600;
    private Random _random = new Random();
    private Song _backgroundMusic;
    
    private List<Projectile> _ListeProjectile = new List<Projectile>();
    private List<Monster> _ListeMonster = new List<Monster>();
    private List<Meteor> _meteors = new List<Meteor>();
    private double second;
    private bool estMenu = true;
    private bool estPause = false;
    private bool estGameOver = false;
    private int score;
    private int meilleurScore;
    private double _meteorTimer = 0;
    
 
   // public sealed class OpenFileDialog : System.Windows.Forms.FileDialog;
    public MyGame(){
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "./Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferHeight = backroundheight;
        _graphics.PreferredBackBufferWidth = backroundwidth;
    }
    private void ResetGame(){
        // Réinitialise le joueur
       _player = new Player(_player.Texture, new Vector2(150, backroundheight / 2), 150);
        // Vide les listes de projectiles et de monstres
        _ListeProjectile.Clear();
        _ListeMonster.Clear();
        // Réinitialise l'état du menu
        estMenu = false; // On relance directement la partie
    }


    protected override void Initialize(){
        // TODO: Add your initialization logic here
        base.Initialize();
        
        // Activer la musique en boucle
        MediaPlayer.IsRepeating = true;
        // Définir le volume (entre 0.0 et 1.0)
        MediaPlayer.Volume = 0.1f;
        // Jouer la musique
        MediaPlayer.Play(_backgroundMusic);
    }

    protected override void LoadContent(){
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Texture2D playerTexture = Content.Load<Texture2D>("assets/images/dragon");
        baground = Content.Load<Texture2D>("assets/images/bg");
        mosterTexture = Content.Load<Texture2D>("assets/images/ogre");
        _projectileTexture = Content.Load<Texture2D>("assets/images/feu");
        menuTexture = Content.Load<Texture2D>("assets/images/menu");
        pauseTexture = Content.Load<Texture2D>("assets/images/pause");
        gameOverTexture = Content.Load<Texture2D>("assets/images/gameOver");
        _meteorTexture = Content.Load<Texture2D>("assets/images/meteor");
        _backgroundMusic = Content.Load<Song>("assets/sound/song");
        
        _player = new Player(playerTexture, new Vector2(150, 900),150);
        
        // TODO: use this.Content to load your game content here
    }
    
    //Pour savoir si le jouer est en colision avec le premier monstre
    public Boolean listeMonsterVide(){
        Boolean b = false;
        if (_ListeMonster.Count != 0 ){
            b = _player.Rect.Intersects(_ListeMonster[0]._Rect);
        }
        return b;
    }
    //pour gerer les projectiles en colision avec un monstre
    public void Pcolision(GameTime gameTime) {
        if (_ListeMonster.Count != 0 && _ListeProjectile.Count !=0) {
            foreach (var projectile in _ListeProjectile){
                foreach (var monster in _ListeMonster){
                    if (projectile._Rect.Intersects(monster._Rect)){
                        projectile.estToucher = true;
                        monster.degat(_player.Attack);
                    }
                }
            }
        }
    }

    protected override void Update(GameTime gameTime){
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        if (estMenu){
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)){
                estMenu = false;

            }
            MediaPlayer.Pause();
        }else if (estPause){
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)){
                estPause = false;
            }
            MediaPlayer.Pause();
        }else if (estGameOver){
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)){
                if (meilleurScore < score)
                    meilleurScore = score;
                ResetGame();
                estMenu = true;
                estPause = false;
                estGameOver = false;
                score = 0;
            }
            MediaPlayer.Pause();
        }else{
            // TODO: Add your update logic here
            //Faire bouger le joueur
            MediaPlayer.Resume();
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && _player.Rect.X<backroundwidth-80 && !listeMonsterVide()){
                _player.MoveRight();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && _player.Rect.X>80 ){ 
                _player.MoveLeft();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A)){
                _ListeProjectile.Add( new Projectile(_projectileTexture,(int)_player.Rect.X+85,(int)_player.Rect.Y-15,40));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space)){
                estPause = true;
                
                if (_player.Health <=0){
                    estGameOver = true;
                }
            }
            // Mettre à jour les projectiles
            Pcolision(gameTime);
            foreach (var projectile in _ListeProjectile){
                projectile.Update(gameTime);
            }
            _ListeProjectile.RemoveAll(p => p.estToucher);
        
            //mettre a jour les monster -------
            second += gameTime.ElapsedGameTime.TotalSeconds;
            if (second >= 3){
                // Réinitialise le timer
                second = 0;
                _ListeMonster.Add(new Monster(mosterTexture, 140));
            }
            //si le golem touche le joueur
            foreach (var monster in _ListeMonster){
                if (!monster._Rect.Intersects(_player.Rect)){
                    monster.Update(gameTime);
                }else{
                    _player.Damage();
                    if (_player.Health <=0){
                        estGameOver = true;
                        estPause = false;
                        estMenu = false;
                    }
                }
            }
            int i = _ListeMonster.RemoveAll(m => m.estToucher);
            if (i != 0)
                score += 1;
        }
        // Timer pour la pluie de météorites
        _meteorTimer += gameTime.ElapsedGameTime.TotalSeconds;
        
        // Intervalle de 1 seconde
        if (_meteorTimer > 1.0){
            _meteorTimer = 0;

            // Crée une nouvelle météorite à une position aléatoire en haut de l'écran
            int xPosition = _random.Next(0, backroundwidth - 50); // Position aléatoire sur l'axe X
            _meteors.Add(new Meteor(_meteorTexture, new Vector2(xPosition, -50), _random.Next(5, 10)));
        }
        // Mettre à jour les météorites
        foreach (var meteor in _meteors){
            meteor.Update();
            // Vérifier les collisions avec le joueur
            if (meteor.Rect.Intersects(_player.Rect)){
                _player.Damage(); // Inflige des dégâts au joueur
                if (_player.Health <= 0){
                    estGameOver = true;
                }
            }
        }
        // Retirer les météorites qui sont hors écran
        _meteors.RemoveAll(m => m.IsOffScreen);
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime){
        //GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        if (estMenu ){
            _spriteBatch.Draw(menuTexture, GraphicsDevice.Viewport.Bounds, Color.White);
        }else if (estGameOver ){
            _spriteBatch.Draw(gameOverTexture, GraphicsDevice.Viewport.Bounds, Color.White);
            //pour affiche le score 
            string text = "Your Score : " + score + "\n";
            var font = Content.Load<SpriteFont>("File");
            _spriteBatch.DrawString(font, text, new Vector2(630,450) , Color.White);
            //pour afficher le meilleur score
            text = "Your best Score : " + meilleurScore + "\n";
            font = Content.Load<SpriteFont>("File");
            _spriteBatch.DrawString(font, text, new Vector2(630,320) , Color.White);
            text = "Press Enter to replay \n";
            font = Content.Load<SpriteFont>("File");
            _spriteBatch.DrawString(font, text, new Vector2(450,700) , Color.White);
        }else if (estPause){
            _spriteBatch.Draw(pauseTexture, GraphicsDevice.Viewport.Bounds, Color.White);
            string text = "Your Score : " + score + "\n";
            var font = Content.Load<SpriteFont>("File");
            _spriteBatch.DrawString(font, text, new Vector2(630,420) , Color.White);
        }else{
            _spriteBatch.Draw(baground, GraphicsDevice.Viewport.Bounds, Color.White);
            _player.Draw(_spriteBatch); 
            _player.DrawHealthBar(_spriteBatch, gameTime);
            string text = "Game \n" +
                          "Your Score: " + score + "\n";
            var font = Content.Load<SpriteFont>("File");
            _spriteBatch.DrawString(font, text, new Vector2(5,20) , Color.White);
            //affichage des projectiles
            foreach (var projectile in _ListeProjectile){
                projectile.Draw(_spriteBatch);
            }
            //affichage des monstres
            foreach (var monster in _ListeMonster){
                monster.Draw(_spriteBatch);
                monster.DrawHealthBar(_spriteBatch,gameTime);
            }
            // Dessiner les météorites
            foreach (var meteor in _meteors){
                meteor.Draw(_spriteBatch);
            }
        }
        _spriteBatch.End();
        // TODO: Add your drawing code here
        base.Draw(gameTime);
    }
}