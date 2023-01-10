using ColossalFramework.PlatformServices;
using ColossalFramework.UI;
using Kamikaze;
using Kwytto.Interfaces;
using Kwytto.LiteUI;
using Kwytto.Utils;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using UnityEngine;

[assembly: AssemblyVersion("0.0.0.0")]
namespace Kamikaze
{
    public class ModInstance : BasicIUserMod<ModInstance, MainController>
    {
        public override string SimpleName { get; } = "Fine Road Anarchy - ending";

        public override string Description { get; } = "Remove it!";

        protected override void SetLocaleCulture(CultureInfo culture) { }

        private static readonly Dictionary<ulong, string> ModsToReplace = new Dictionary<ulong, string>
        {
            [2862881785L] = "Network Anarchy"
        };

        public new void OnEnabled()
        {
            if (UIView.GetAView() != null)
            {
                ShowModalUnsub();
            }
        }
        private bool modalShown;
        private void ShowModalUnsub()
        {
            if (modalShown) return;
            modalShown = true;
            KwyttoDialog.ShowModal(new KwyttoDialog.BindProperties
            {
                title = SimpleName,
                message = "This mod is being replaced by the following mods." +
                "You can choose subscribe them or simple unsubscribe this - in both cases, the game will be closed to avoid conflict issues.",
                scrollText = string.Join("\n", ModsToReplace.Select(x => $" - ID {x.Key}: {x.Value}").ToArray()),
                buttons = new KwyttoDialog.ButtonDefinition[]
                {
                    KwyttoDialog.SpaceBtn,
                    new KwyttoDialog.ButtonDefinition
                    {
                        title = "Subscribe them all!",
                        onClick = () =>
                        {
                            foreach(var id in ModsToReplace.Keys)
                            {
                                try {
                                    PlatformService.workshop.Subscribe(new PublishedFileId(id));
                                } catch {
                                }
                            }
                            PlatformService.workshop.Unsubscribe(new PublishedFileId(ModId));
                            Application.Quit();
                            return true;
                        }
                    },
                    new KwyttoDialog.ButtonDefinition
                    {
                        title = "Just unsubscribe this",
                        onClick = () =>
                        {
                            PlatformService.workshop.Unsubscribe(new PublishedFileId(ModId));
                            Application.Quit();
                            return true;
                        }
                    }
                }
            });
        }

        protected override void CreateGroup9(UIHelper helper)
        {
            base.CreateGroup9(helper);
            ShowModalUnsub();
        }
    }

    public class MainController : BaseController<ModInstance, MainController>
    {

    }


}
namespace Kwytto
{
    public static class CommonProperties
    {
        public static bool DebugMode => ModInstance.DebugMode;
        public static string Version => ModInstance.Version;
        public static string FullVersion => ModInstance.FullVersion;
        public static string ModName => ModInstance.Instance.SimpleName;
        public static string Acronym { get; } = "FRA";
        public static string ModRootFolder => "";
        public static string ModIcon => ModInstance.Instance.IconName;
        public static string ModDllRootFolder => ModInstance.RootFolder;

        public static string GitHubRepoPath => null;

        internal static readonly string[] AssetExtraDirectoryNames = new string[0];
        internal static readonly string[] AssetExtraFileNames = new string[] { };

        public static Color ModColor { get; } = ColorExtensions.FromRGB("888888");
        public static float UIScale { get; } = 1f;
    }
}