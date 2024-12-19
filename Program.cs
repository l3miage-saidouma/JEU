/*using var game = new ProjetJeuVideo.MyGame();
game.Run();
*/

using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using ProjetJeuVideo;
public class Program : Game
{
    public static void Main(string[] args){
        
        MyGame game = new MyGame();
        game.Run();
        
        Console.WriteLine("****************************************** Deserealyse myGame.xml *******************************************");
        MyGame_Serealizer myGameSerealizer;
        using (TextReader reader = new StreamReader("./data/xml/myGame.xml"))
        {
            var xmlmyGame = new XmlSerializer(typeof(MyGame_Serealizer));
            myGameSerealizer = (MyGame_Serealizer)xmlmyGame.Deserialize(reader);
        }
        myGameSerealizer.affichage();
        
        Console.WriteLine("************************************************** DOM & Reader *********************************************");
        MyGameDomReader myGameDomReader = new MyGameDomReader("./data/xml/myGame.xml");
        string prefix = myGameDomReader.GetNsPrefix();
        string uri = myGameDomReader.GetNsuri();
        XmlNodeList list = myGameDomReader.GetXPath("//my:monsters/my:monster", prefix, uri);
        Console.WriteLine("Nombre de monstre  : "+list.Count);
        list = myGameDomReader.GetXPath("//my:projectiles/my:projectile", prefix, uri);
        Console.WriteLine("Nombre de Projectiles : "+list.Count);
        list = myGameDomReader.GetXPath("//my:meteors/my:meteor", prefix, uri);
        Console.WriteLine("Nombre de meteorite : "+list.Count);
        
        myGameDomReader.AddMonster("200","200","1555","800","./data/xml/ajoutMonster.xml");
    }
}