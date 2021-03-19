using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;


namespace Utils {

    class LoadedMap {
        public List<(int, Point)> textures = new List<(int, Point)>();
        public List<Rectangle> collisionRects = new List<Rectangle>();
    }
    static class MapLoader {

        static public LoadedMap Load(string path) {
            var toReturn = new LoadedMap();

            var data = System.IO.File.ReadAllLines(path);
            foreach(var thing in data) {
                var stuff = thing.Split(' ');
                switch (stuff[0]) {
                    case "T":
                        try {
                            var stuff1 = int.Parse(stuff[1]);
                            var stuff2 = int.Parse(stuff[2]);
                            var stuff3 = int.Parse(stuff[3]);
                            toReturn.textures.Add((stuff1, new Point(stuff2, stuff3)));
                        } catch {
                            Console.WriteLine("Type error, couldn't parse map file");
                        }
                        break;
                    case "CB":
                        try {
                            var stuff1 = int.Parse(stuff[1]);
                            var stuff2 = int.Parse(stuff[2]);
                            var stuff3 = int.Parse(stuff[3]);
                            var stuff4 = int.Parse(stuff[4]);
                            toReturn.collisionRects.Add(new Rectangle(stuff1, stuff2, stuff3, stuff4));
                        } catch {
                            Console.WriteLine("Type error, couldn't parse map file");
                        }
                        break;
                }
            }

            return toReturn;
        }
    }
}