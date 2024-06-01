

using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using RenderingLibrary;

namespace SaltLands;

public class SaltUI
{
    public GumProjectSave gum;

    private GraphicalUiElement activeScreen;

    public SaltUI()
    {
        gum = GumProjectSave.Load("UI.gumx");
        ObjectFinder.Self.GumProjectSave = gum;
        gum.Initialize();
    }

    private ScreenSave GetScreen(string name)
    {
        return gum.Screens.Find(screen => screen.Name == name);
    }

    public GraphicalUiElement LoadScreen(string name)
    {
        if(activeScreen != null)
        {
            activeScreen.RemoveFromManagers();
        }

        activeScreen = GetScreen(name).ToGraphicalUiElement(SystemManagers.Default, addToManagers: true);

        return activeScreen;
    }

    public void UpdateLayout()
    {
        activeScreen?.UpdateLayout();
    }
}
