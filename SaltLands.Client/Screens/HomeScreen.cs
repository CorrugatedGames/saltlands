using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using RenderingLibrary;
using System.Collections.Generic;
using System.Linq;

namespace SaltLands;

public class HomeScreen : BaseScreen
{

    public HomeScreen(SaltLandsGame game) : base(game) {}

    public override void Initialize()
    {
        base.Initialize();
        LoadScreenData("Home");
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

    }

    public override void Draw(GameTime gameTime)
    {
    }

    protected override void HandleButtonClick(string buttonName)
    {
        switch(buttonName)
        {
            case "PlayButton":
                SaltGame.SaltEmitter.Emit(SaltEvents.LoadPlayGameScreen);
                break;

            case "OptionsButton":
                SaltGame.SaltEmitter.Emit(SaltEvents.LoadOptionsScreen);
                break;

            case "QuitButton":
                SaltGame.SaltEmitter.Emit(SaltEvents.QuitGame);
                break;
        }
    }
}