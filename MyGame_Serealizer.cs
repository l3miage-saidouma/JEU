using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace ProjetJeuVideo
{
    [XmlRoot(ElementName = "myGame", Namespace = "www.univ-grenoble-alpes.fr/projetJeuVideo")]
    [Serializable]
    public class MyGame_Serealizer{
        [XmlAttribute("estGameOver")] public bool EstGameOver { get; set; }

        [XmlAttribute("estMenu")] public bool EstMenu { get; set; }

        [XmlAttribute("estPause")] public bool EstPause { get; set; }

        [XmlElement("player")] public Players Player { get; set; }

        [XmlArray("monsters")]
        [XmlArrayItem("monster")]
        public List<Monsters> _Monsters { get; set; } = new();

        [XmlArray("meteors")]
        [XmlArrayItem("meteor")]
        public List<Meteors> _Meteors { get; set; } = new();

        [XmlArray("projectiles")]
        [XmlArrayItem("projectile")]
        public List<Projectiles> _Projectiles { get; set; } = new();

        [XmlElement("second")] public double Second { get; set; }

        [XmlElement("score")] public int Score { get; set; }

        [XmlElement("meteorTimer")] public double MeteorTimer { get; set; }

        public class Players{
            [XmlAttribute("estToucher")] public bool EstToucher { get; set; }

            [XmlElement("health")] public int Health { get; set; }

            [XmlElement("maxHealth")] public int MaxHealth { get; set; }

            [XmlElement("attack")] public int Attack { get; set; }

            [XmlElement("position")]public Position Position { get; set; }

            [XmlElement("Rect")]public Rect Rect { get; set; }

            [XmlElement("size")]public int Size { get; set; }
        }

        public class Monsters{
            [XmlAttribute("estToucher")] public bool EstToucher { get; set; }

            [XmlElement("health")]public int Health { get; set; }

            [XmlElement("max_health")]public int MaxHealth { get; set; }

            [XmlElement("attack")]public int Attack { get; set; }

            [XmlElement("position")]public Position Position { get; set; }

            [XmlElement("rect")]public Rect Rect { get; set; }

            [XmlElement("size")]public int Size { get; set; }

            [XmlElement("velocity")]public int Velocity { get; set; }
        }

        public class Meteors{
            [XmlAttribute("IsOffScreen")]public bool IsOffScreen { get; set; }

            [XmlElement("speed")]public int Speed { get; set; }

            [XmlElement("rect")]public Rect Rect { get; set; }
        }

        public class Projectiles{
            [XmlAttribute("estToucher")] public bool EstToucher { get; set; }

            [XmlElement("size")] public int Size { get; set; }

            [XmlElement("velocity")] public int Velocity { get; set; }

            [XmlElement("position")] public Position Position { get; set; }

            [XmlElement("Rect")] public Rect Rect { get; set; }
        }

        public class Position{
            [XmlElement("X")] public int X { get; set; }

            [XmlElement("Y")] public int Y { get; set; }
        }

        public class Rect{
            [XmlElement("X")]  public int X { get; set; }
            [XmlElement("Y")] public int Y { get; set; }
            [XmlElement("with")] public int Width { get; set; }
            [XmlElement("height")] public int Height { get; set; }
        }
        
        public void affichage(){
            // Affichage du contenu désérialisé
            Console.WriteLine("=== My Game Details ===");
            Console.WriteLine($"estGameOver: {EstGameOver}");
            Console.WriteLine($"estMenu: {EstMenu}");
            Console.WriteLine($"estPause: {EstPause}");
            Console.WriteLine($"Second: {Second}");
            Console.WriteLine($"Score: {Score}");
            Console.WriteLine($"Meteor Timer: {MeteorTimer}");
            
            Console.WriteLine("\n=== Player Details ===");
            Console.WriteLine($"  estToucher: {Player.EstToucher}");
            Console.WriteLine($"  Health: {Player.Health}");
            Console.WriteLine($"  MaxHealth: {Player.MaxHealth}");
            Console.WriteLine($"  Attack: {Player.Attack}");
            Console.WriteLine($"  Position: X={Player.Position.X}, Y={Player.Position.Y}");
            Console.WriteLine($"  Rect: X={Player.Rect.X}, Y={Player.Rect.Y}, Width={Player.Rect.Width}, Height={Player.Rect.Height}");
            Console.WriteLine($"  Size: {Player.Size}");
            
            Console.WriteLine("\n=== Monsters Details ===");
            foreach (var monster in _Monsters) {
                Console.WriteLine($"  - estToucher: {monster.EstToucher}");
                Console.WriteLine($"    Health: {monster.Health}");
                Console.WriteLine($"    MaxHealth: {monster.MaxHealth}");
                Console.WriteLine($"    Attack: {monster.Attack}");
                Console.WriteLine($"    Position: X={monster.Position.X}, Y={monster.Position.Y}");
                Console.WriteLine($"    Rect: X={monster.Rect.X}, Y={monster.Rect.Y}, Width={monster.Rect.Width}, Height={monster.Rect.Height}");
                Console.WriteLine($"    Size: {monster.Size}");
                Console.WriteLine($"    Velocity: {monster.Velocity}");
            }
            
            Console.WriteLine("\n=== Meteors Details ===");
            foreach (var meteor in _Meteors){
                Console.WriteLine($"  - IsOffScreen: {meteor.IsOffScreen}");
                Console.WriteLine($"    Speed: {meteor.Speed}");
                Console.WriteLine($"    Rect: X={meteor.Rect.X}, Y={meteor.Rect.Y}, Width={meteor.Rect.Width}, Height={meteor.Rect.Height}");
            }
            
            Console.WriteLine("\n=== Projectiles Details ===");
            foreach (var projectile in _Projectiles){
                Console.WriteLine($"  - estToucher: {projectile.EstToucher}");
                Console.WriteLine($"    Size: {projectile.Size}");
                Console.WriteLine($"    Velocity: {projectile.Velocity}");
                Console.WriteLine($"    Position: X={projectile.Position.X}, Y={projectile.Position.Y}");
                Console.WriteLine($"    Rect: X={projectile.Rect.X}, Y={projectile.Rect.Y}, Width={projectile.Rect.Width}, Height={projectile.Rect.Height}");
            }
        }
    }
}
