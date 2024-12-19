using System.Xml;
namespace ProjetJeuVideo;

public class MyGameDomReader{
    //---------------------utilisation de Dom
    private XmlDocument _doc;
    private XmlNode _root;
    private XmlNamespaceManager _nsmng;
    
    //constructeur
    public MyGameDomReader(string filename){
        _doc = new XmlDocument();
        _doc.Load(filename);
        _root = _doc.DocumentElement;
        _nsmng = new XmlNamespaceManager(_doc.NameTable);
        _nsmng.AddNamespace(_root.Prefix,_root.NamespaceURI);
    }
    
    public XmlNodeList GetXPath(string expression, string nsPrefix, string nsUri){
        _nsmng.AddNamespace(nsPrefix,nsUri);
        return _root.SelectNodes(expression, _nsmng);
    }
    
    /*
     * returner le prefix de mon document xml
     */
    public string GetNsPrefix()
    {
        return _root.Prefix;
    }
    
    /*
     * Returner le URI de mon document
     */
    public string GetNsuri(){
        return _root.NamespaceURI;
    }
    
    /*
     * compter le nombre d'element
     */
    public int Count(string elementName){
        XmlElement rootElt = (XmlElement)_root;
        XmlNodeList refnl = rootElt.GetElementsByTagName(elementName);
        return refnl.Count;
    }
    
     public void AddMonster(string health, string maxHealth, string x,string y,string filename) {
        
        XmlElement monster = _doc.CreateElement(_root.Prefix, "monster", _root.NamespaceURI);
        monster.SetAttribute("estToucher", "false");
        XmlElement healthElement = _doc.CreateElement(_root.Prefix, "health", _root.NamespaceURI);
        healthElement.InnerText = health;
        monster.AppendChild(healthElement);
        XmlElement maxHealthElement = _doc.CreateElement(_root.Prefix, "max_health", _root.NamespaceURI);
        maxHealthElement.InnerText = maxHealth;
        monster.AppendChild(maxHealthElement);
        XmlElement attackElement = _doc.CreateElement(_root.Prefix, "attack", _root.NamespaceURI);
        attackElement.InnerText = "1";
        monster.AppendChild(attackElement);
        XmlElement sizeElement = _doc.CreateElement(_root.Prefix, "size", _root.NamespaceURI);
        sizeElement.InnerText = "150";
        monster.AppendChild(sizeElement);
       
        XmlElement positionElement = _doc.CreateElement(_root.Prefix, "position", _root.NamespaceURI);
        XmlElement xElement = _doc.CreateElement(_root.Prefix, "X", _root.NamespaceURI);
        xElement.InnerText = x;
        positionElement.AppendChild(xElement);
        XmlElement yElement = _doc.CreateElement(_root.Prefix, "Y", _root.NamespaceURI);
        yElement.InnerText = y;
        positionElement.AppendChild(yElement);
        monster.AppendChild(positionElement);
        
        //ajout d'un rectangle
        XmlElement rectElement = _doc.CreateElement(_root.Prefix, "rect", _root.NamespaceURI);
        XmlElement rectXElement = _doc.CreateElement(_root.Prefix, "X", _root.NamespaceURI);
        rectXElement.InnerText = x;
        rectElement.AppendChild(rectXElement);
        XmlElement rectYElement = _doc.CreateElement(_root.Prefix, "Y", _root.NamespaceURI);
        rectXElement.InnerText = y;
        rectElement.AppendChild(rectYElement);
        XmlElement rectWithElement = _doc.CreateElement(_root.Prefix, "with", _root.NamespaceURI);
        rectWithElement.InnerText = "150";
        rectElement.AppendChild(rectWithElement);
        XmlElement rectHeightElement = _doc.CreateElement(_root.Prefix, "height", _root.NamespaceURI);
        rectWithElement.InnerText = "150";
        rectElement.AppendChild(rectHeightElement);
        monster.AppendChild(rectElement);
        
        XmlNode monstersNode = ((XmlElement)_root).GetElementsByTagName(_root.Prefix + ":monsters").Item(0);
        monstersNode.AppendChild(monster);

        // Sauvegarde dans le fichier
        _doc.Save(filename);
    }
}