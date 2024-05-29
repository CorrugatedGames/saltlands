

using Microsoft.Xna.Framework;
using Nez.UI;
using Nez.BitmapFonts;
using Nez.Textures;

namespace SaltLands;

public class SaltUI
{
    public Skin skin;
    public BitmapFont font;

    public SaltUI(Nez.Systems.NezContentManager contentManager)
    {
        skin = Skin.CreateDefaultSkin();
        font = contentManager.LoadBitmapFont(Nez.Content.Font.PixelifySansBitmap);

        skin.Add<Sprite>("menu-play-button-down", new Sprite(contentManager.LoadTexture(Nez.Content.Buttons.Home.PlayGameButtonPressed)));
        skin.Add<Sprite>("menu-play-button-up", new Sprite(contentManager.LoadTexture(Nez.Content.Buttons.Home.PlayGameButtonUnpressed)));

        skin.Add<Sprite>("menu-options-button-down", new Sprite(contentManager.LoadTexture(Nez.Content.Buttons.Home.OptionsButtonPressed)));
        skin.Add<Sprite>("menu-options-button-up", new Sprite(contentManager.LoadTexture(Nez.Content.Buttons.Home.OptionsButtonUnpressed)));

        skin.Add<Sprite>("menu-quit-button-down", new Sprite(contentManager.LoadTexture(Nez.Content.Buttons.Home.QuitButtonPressed)));
        skin.Add<Sprite>("menu-quit-button-up", new Sprite(contentManager.LoadTexture(Nez.Content.Buttons.Home.QuitButtonUnpressed)));

        skin.Add("menu-button", new TextButtonStyle
        {
            Up = new PrimitiveDrawable(Color.White, 6, 2),
            Over = new PrimitiveDrawable(Color.Gray),
            Down = new PrimitiveDrawable(Color.DarkGray),
            PressedOffsetX = 1,
            PressedOffsetY = 1,
            FontScaleX = 0.4f,
            FontScaleY = 0.4f,
            Font = font
        });

        skin.Add("menu-play-button", new ImageButtonStyle
        {
            ImageDown = skin.GetDrawable("menu-play-button-down"),
            ImageOver = skin.GetDrawable("menu-play-button-down"),
            ImageUp = skin.GetDrawable("menu-play-button-up"),
        });

        skin.Add("menu-options-button", new ImageButtonStyle
        {
            ImageDown = skin.GetDrawable("menu-options-button-down"),
            ImageOver = skin.GetDrawable("menu-options-button-down"),
            ImageUp = skin.GetDrawable("menu-options-button-up")
        });

        skin.Add("menu-quit-button", new ImageButtonStyle
        {
            ImageDown = skin.GetDrawable("menu-quit-button-down"),
            ImageOver = skin.GetDrawable("menu-quit-button-down"),
            ImageUp = skin.GetDrawable("menu-quit-button-up")
        });
    }
}
