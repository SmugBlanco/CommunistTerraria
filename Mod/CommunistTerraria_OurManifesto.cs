﻿using System.Collections.Generic;
using System.Text;
using CommunistTerraria.UI;
using Terraria;
using Terraria.UI;

namespace CommunistTerraria
{
    public partial class CommunistTerraria
    {
        private string _manifesto;
        private UserInterface _manifestoInterface;

        private void LoadManifesto()
        {
            byte[] manifestoBytes = GetFileBytes("manifesto.txt");
            _manifesto = Encoding.UTF8.GetString(manifestoBytes);

            _manifestoInterface = new UserInterface();
        }

        public void ToggleManifestoUI()
        {
#if DEBUG
            Main.NewText($"Toggling manifesto ui, old state is: {_manifestoInterface.CurrentState}");
#endif
            
            if (_manifestoInterface.CurrentState == null)
                _manifestoInterface.SetState(new ManifestoUI());
            else
                _manifestoInterface.SetState(null);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseLayerIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Mouse Text");
            if (mouseLayerIndex == 1)
                return;
            
            layers.Insert(mouseLayerIndex, new LegacyGameInterfaceLayer(
                "CommunistTerraria: Our Manifesto UI",
                delegate
                {
                    _manifestoInterface.Draw(Main.spriteBatch, null);
                    return true;
                },
                InterfaceScaleType.UI));
        }
    }
}