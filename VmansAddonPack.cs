using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace VmansAddonPack
{
	public class VmansAddonPack : Mod
	{
        /*
       public override void HandlePacket(BinaryReader reader, int whoAmI)
       {
           ModMessageType msgType = (ModMessageType)reader.ReadByte();
           switch (msgType)
           {
               case ModMessageType.ProjectileAIUpdate:
                   int identity6 = reader.ReadInt32();
                   byte owner6 = reader.ReadByte();
                   int realIdentity6 = Projectile.GetByUUID(owner6, identity6);
                   float ai0 = reader.ReadSingle();
                   float ai1 = reader.ReadSingle();
                   if (realIdentity6 != -1)
                   {
                       Main.projectile[realIdentity6].ai[0] = ai0;
                       Main.projectile[realIdentity6].ai[1] = ai1;
                       if (Main.netMode == 2)
                       {
                           ProjectileAIUpdate(Main.projectile[realIdentity6]);
                       }
                   }
*/
    }

}