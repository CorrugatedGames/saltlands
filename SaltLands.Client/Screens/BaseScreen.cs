using Gum.Wireframe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using RenderingLibrary;
using System.Collections.Generic;
using System.Linq;

namespace SaltLands;

public abstract class BaseScreen : GameScreen
{
    protected SaltLandsGame SaltGame => (SaltLandsGame)Game;
    protected GraphicalUiElement screen;
    protected IEnumerable<GraphicalUiElement> hoverables;

    private bool wasDownLastFrame = false;

    public BaseScreen(SaltLandsGame game) : base(game) { }

    public override void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        int mouseX = mouseState.X;
        int mouseY = mouseState.Y;


        bool isDownThisFrame = mouseState.LeftButton == ButtonState.Pressed;

        foreach (var button in hoverables)
        {
            bool isOver =
                mouseX > button.GetAbsoluteLeft() &&
                mouseX < button.GetAbsoluteRight() &&
                mouseY > button.GetAbsoluteTop() &&
                mouseY < button.GetAbsoluteBottom();

            if (isOver)
            {
                button.ApplyState("Hover");
            }
            else
            {
                button.ApplyState("Normal");
            }

            if (isDownThisFrame && !wasDownLastFrame && isOver)
            {
                HandleButtonClick(button);
            }
        }

        wasDownLastFrame = isDownThisFrame;

    }

    public override void Draw(GameTime gameTime)
    {
    }

    protected void LoadScreenData(string screenName)
    {
        screen = SaltGame.saltUI.LoadScreen(screenName);
        hoverables = screen.ContainedElements.Where(child => child.ElementSave.AllStates.Any(state => state.Name == "Hover"));
    }

    protected abstract void HandleButtonClick(GraphicalUiElement buttonName);
}