﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameJam.Game
{
    class Nuke
    {
        private GameContext context;
        private Random rnd = new Random();
        public Nuke(GameContext gc)
        {
            context = gc;

            Tile[] tiles = gc.room.GetAllTiles();
            Tile randomTile = tiles[rnd.Next(0, tiles.Length)];
      
            RenderObject nukeSign = new RenderObject()
            {
                frames = gc.spriteMap.GetNukeSignFrames(),
                rectangle = new Rectangle(randomTile.rectangle.X +8, randomTile.rectangle.Y + 8, 35, 35)
            };
            gc.nukeSigns.Add(nukeSign);

            Timer spawnTimer = new Timer();
            spawnTimer.Interval = (2500);
            spawnTimer.Tick += (sender, e) => SpawnNuke(spawnTimer, randomTile.rectangle.X, randomTile.rectangle.Y, nukeSign, gc);
            spawnTimer.Start();
        }

        private void SpawnNuke(Timer spawnTimer, int x, int y, RenderObject nukeSign, GameContext gc)
        {
            gc.nukeSigns.Remove(nukeSign);
            spawnTimer.Dispose();
            RenderObject nuke = new RenderObject()
            {
                frames = context.GetSingeFrameArray('Q'),
                rectangle = new Rectangle(x - 16, y - 16, 80, 82),
            };
            context.nukes.Add(nuke);

            Timer despawnTimer = new Timer();
            despawnTimer.Interval = (2500);
            despawnTimer.Tick += (sender, e) => DespawnNuke(spawnTimer, gc, nuke);
            despawnTimer.Start();
            HandleExplosion(new Vector2(x, y), gc);
        }

        private void HandleExplosion(Vector2 center, GameContext gc)
        {
            center.x += gc.tileSize;
            center.y += gc.tileSize;
            //center segment
            //center
            DestroyTile(GetTileInfo(center, gc), center, gc);
            //mid center
            DestroyTile(GetTileInfo(new Vector2(center.x, center.y - 16), gc), new Vector2(center.x, center.y -16), gc);
            //top center
            DestroyTile(GetTileInfo(new Vector2(center.x, center.y - 32), gc), new Vector2(center.x, center.y - 32), gc);
            //down center mid
            DestroyTile(GetTileInfo(new Vector2(center.x, center.y + 16), gc), new Vector2(center.x, center.y + 16), gc);
            //low center
            DestroyTile(GetTileInfo(new Vector2(center.x, center.y + 32), gc), new Vector2(center.x, center.y + 32), gc);

            //left most segment
            DestroyTile(GetTileInfo(new Vector2(center.x - 32, center.y), gc), new Vector2(center.x - 32, center.y), gc);
            //mid center
            DestroyTile(GetTileInfo(new Vector2(center.x -32, center.y - 16), gc), new Vector2(center.x - 32, center.y - 16), gc);
            //top center
            DestroyTile(GetTileInfo(new Vector2(center.x -32, center.y - 32), gc), new Vector2(center.x -32, center.y - 32), gc);
            //down center mid
            DestroyTile(GetTileInfo(new Vector2(center.x - 32, center.y + 16), gc), new Vector2(center.x -32, center.y + 16), gc);
            //low center
            DestroyTile(GetTileInfo(new Vector2(center.x -32, center.y + 32), gc), new Vector2(center.x -32, center.y + 32), gc);

            //left segment
            DestroyTile(GetTileInfo(new Vector2(center.x - 16, center.y), gc), new Vector2(center.x - 16, center.y), gc);
            //mid center
            DestroyTile(GetTileInfo(new Vector2(center.x - 16, center.y - 16), gc), new Vector2(center.x - 16, center.y - 16), gc);
            //top center
            DestroyTile(GetTileInfo(new Vector2(center.x - 16, center.y - 32), gc), new Vector2(center.x - 16, center.y - 32), gc);
            //down center mid
            DestroyTile(GetTileInfo(new Vector2(center.x - 16, center.y + 16), gc), new Vector2(center.x - 16, center.y + 16), gc);
            //low center
            DestroyTile(GetTileInfo(new Vector2(center.x - 16, center.y + 32), gc), new Vector2(center.x - 16, center.y + 32), gc);

            //center right segment
            //center
            DestroyTile(GetTileInfo(new Vector2(center.x + 16, center.y), gc), new Vector2(center.x + 16, center.y), gc);
            //mid center
            DestroyTile(GetTileInfo(new Vector2(center.x + 16, center.y - 16), gc), new Vector2(center.x + 16, center.y - 16), gc);
            //top center
            DestroyTile(GetTileInfo(new Vector2(center.x + 16, center.y - 32), gc), new Vector2(center.x + 16, center.y - 32), gc);
            //down center mid
            DestroyTile(GetTileInfo(new Vector2(center.x + 16, center.y + 16), gc), new Vector2(center.x + 16, center.y + 16), gc);
            //low center
            DestroyTile(GetTileInfo(new Vector2(center.x + 16, center.y + 32), gc), new Vector2(center.x + 16, center.y + 32), gc);

            //center most right segment
            //center
            DestroyTile(GetTileInfo(new Vector2(center.x + 32, center.y), gc), new Vector2(center.x + 32, center.y), gc);
            //mid center
            DestroyTile(GetTileInfo(new Vector2(center.x + 32, center.y - 16), gc), new Vector2(center.x + 32, center.y - 16), gc);
            //top center
            DestroyTile(GetTileInfo(new Vector2(center.x + 32, center.y - 32), gc), new Vector2(center.x + 32, center.y - 32), gc);
            //down center mid
            DestroyTile(GetTileInfo(new Vector2(center.x + 32, center.y + 16), gc), new Vector2(center.x + 32, center.y + 16), gc);
            //low center
            DestroyTile(GetTileInfo(new Vector2(center.x + 32, center.y + 32), gc), new Vector2(center.x + 32, center.y + 32), gc);

        }
        private void DestroyTile(Tile tile, Vector2 position, GameContext gc)
        {
            if (tile == null) return;
            if (tile.graphic != 'W')
            {
                tile.sprite = gc.spriteMap.GetSprite('.');
                tile.graphic = '.';
            }
        }
        private Tile GetTileInfo(Vector2 position, GameContext gc)
        {
            return gc.room.tiles.SelectMany(ty => ty.Where(tx => tx.rectangle.Contains((int)position.x, (int)position.y))).FirstOrDefault();
        }
        private void DespawnNuke(Timer despawnTimer, GameContext gc, RenderObject nuke)
        {
            despawnTimer.Dispose();
            gc.nukes.Remove(nuke);
        }
    }
}
