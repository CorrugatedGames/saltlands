using Gum.Wireframe;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using MonoGameGum.Input;
using System.Collections.Generic;
using System.Linq;

namespace SaltLands;

public abstract class BaseScreen : GameScreen
{
    protected SaltLandsGame SaltGame => (SaltLandsGame)Game;
    protected GraphicalUiElement screen;
    protected IEnumerable<GueFormControl> hoverables;
    Cursor cursor;

    public BaseScreen(SaltLandsGame game) : base(game) { }

    public override void Initialize()
    {
        base.Initialize();
        cursor = new Cursor();
    }

    public override void Update(GameTime gameTime)
    {
        cursor.Activity(gameTime.TotalGameTime.TotalSeconds);
        screen?.DoUiActivityRecursively(cursor);
    }

    public override void Draw(GameTime gameTime)
    {
    }

    protected void LoadScreenData(string screenName)
    {
        screen = SaltGame.saltUI.LoadScreen(screenName);
        hoverables = screen.ContainedElements.Where(child => child.ElementSave.AllStates.Any(state => state.Name == "Hover")).Select(item => (GueFormControl) item);


        foreach (var button in hoverables)
        {
            button.RollOn += (_, _) =>
            {
                if (button.IsDisabled) return;
                button.ApplyState("Hover");
            };

            button.RollOff += (_, _) =>
            {
                if (button.IsDisabled) return;
                button.ApplyState("Normal");
            };
        }
    }
}