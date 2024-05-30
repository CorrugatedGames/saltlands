using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace SaltLands;

public class OptionsScreen : GameScreen
{
    private SaltLandsGame SaltGame;

    public OptionsScreen(SaltLandsGame game) : base(game) {
        SaltGame = game;
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(GameTime gameTime)
    {
    }
}