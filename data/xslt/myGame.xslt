<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:mg="www.univ-grenoble-alpes.fr/projetJeuVideo"
                version="1.0">

    <xsl:output method="html" encoding="UTF-8" indent="yes"/>

    <!-- Template principal qui génère la structure HTML -->
    <xsl:template match="/">
        <html>
            <head>
                <title>Game Status</title>
                <style>
                    body { font-family: Arial, sans-serif; }
                    .container { margin: 20px; }
                    .player, .monster, .meteor, .projectile { margin: 10px 0; }
                </style>
            </head>
            <body>
                <div class="container">
                    <h1>Game Status</h1>

                    <!-- Affichage des informations sur le joueur -->
                    <h2>Player</h2>
                    <div class="player">
                        <p><strong>Health:</strong> <xsl:value-of select="mg:myGame/mg:player/mg:health" /> / <xsl:value-of select="mg:myGame/mg:player/mg:maxHealth" /></p>
                        <p><strong>Attack:</strong> <xsl:value-of select="mg:myGame/mg:player/mg:attack" /></p>
                        <p><strong>Velocity:</strong> <xsl:value-of select="mg:myGame/mg:player/mg:velocity" /></p>
                        <p><strong>Position:</strong> X: <xsl:value-of select="mg:myGame/mg:player/mg:position/mg:X" />, Y: <xsl:value-of select="mg:myGame/mg:player/mg:position/mg:Y" /></p>
                    </div>

                    <!-- Affichage des monstres -->
                    <h2>Monsters</h2>
                    <xsl:for-each select="mg:myGame/mg:monsters/mg:monster">
                        <div class="monster">
                            <p><strong>Health:</strong> <xsl:value-of select="mg:health" /></p>
                            <p><strong>Max_Health:</strong> <xsl:value-of select="mg:max_health" /></p>
                            <p><strong>Attack:</strong> <xsl:value-of select="mg:attack" /></p>
                            <p><strong>Velocity:</strong> <xsl:value-of select="mg:velocity" /></p>
                            <p><strong>Position:</strong> X: <xsl:value-of select="mg:rect/mg:X" />, Y: <xsl:value-of select="mg:rect/mg:Y" /></p>
                        </div>
                    </xsl:for-each>

                    <!-- Affichage des météores -->
                    <h2>Meteors</h2>
                    <xsl:for-each select="mg:myGame/mg:meteors/mg:meteor">
                        <div class="meteor">
                            <p><strong>Speed:</strong> <xsl:value-of select="mg:speed" /></p>
                            <p><strong>Position:</strong> X: <xsl:value-of select="mg:rect/mg:X" />, Y: <xsl:value-of select="mg:rect/mg:Y" /></p>
                        </div>
                    </xsl:for-each>

                    <!-- Affichage des projectiles -->
                    <h2>Projectiles</h2>
                    <xsl:for-each select="mg:myGame/mg:projectiles/mg:projectile">
                        <div class="projectile">
                            <p><strong>Size:</strong> <xsl:value-of select="mg:size" /></p>
                            <p><strong>Velocity:</strong> <xsl:value-of select="mg:velocity" /></p>
                            <p><strong>Position:</strong> X: <xsl:value-of select="mg:Rect/mg:X" />, Y: <xsl:value-of select="mg:Rect/mg:Y" /></p>
                        </div>
                    </xsl:for-each>

                    <!-- Affichage du score et du timer -->
                    <h2>Game Info</h2>
                    <p><strong>Score:</strong> <xsl:value-of select="mg:myGame/mg:score" /></p>
                    <p><strong>Timer:</strong> <xsl:value-of select="mg:myGame/mg:second" /> seconds</p>
                </div>
            </body>
        </html>
    </xsl:template>

</xsl:stylesheet>
