

using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;
using GumRuntime;
using RenderingLibrary;
using System.Linq;

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
            var layers = SystemManagers.Default.Renderer.Layers;
            while (layers.Count > 1)
            {
                SystemManagers.Default.Renderer.RemoveLayer(SystemManagers.Default.Renderer.Layers.LastOrDefault());
            }
        }

        activeScreen = GetScreen(name).ToGraphicalUiElement(SystemManagers.Default, addToManagers: true);

        return activeScreen;
    }
}
