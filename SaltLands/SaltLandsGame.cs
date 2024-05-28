using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Sprites;

namespace SaltLands;

public class SaltLandsGame : Nez.Core
{
    protected override void Initialize()
    {
        base.Initialize();

        Scene menuScene = Scene.CreateWithDefaultRenderer(Color.CornflowerBlue);

        Texture2D homeBackground = menuScene.Content.LoadTexture(Nez.Content.Background.Home);

        Entity homeEntity = menuScene.CreateEntity("homeBackground");
        SpriteRenderer renderer = homeEntity.AddComponent<SpriteRenderer>();
        renderer.SetTexture(homeBackground);

        Core.Scene = menuScene;
    }
}
