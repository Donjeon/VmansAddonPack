using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework; //For MathHelper

namespace VmansAddonPack.Items
{
	public class HiltCannon : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("BasicSword"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Able fire soul capsules");
		}

		public override void SetDefaults() 
		{
			item.damage = 76;
			item.ranged = true;
			item.width = 50;
			item.height = 26;
			item.useTime = 14;
			item.useAnimation = 13; 
			item.useStyle = 5; //1 for sword, 5 for staves/guns
			item.knockBack = 8; //max 20
			item.value = 500000; //value in copper coins
			item.rare = 6; //its rarity, from -1 to 13
			item.UseSound = SoundID.Item34; //sound it makes on use https://tconfig.fandom.com/wiki/List_of_Sounds (Sound ID + 1)
			item.autoReuse = true; //autoswing
            item.noMelee = true; //Is not a melee weapon so does no contact damage
            item.crit = 12;

            //TODO: Balance damage of HiltCannon
            //TODO: Resprite this
            //TODO: Make ammo for this weapon (The ammo fires the swords?. This becomes a shotgun with normal bullets?)d
            //TODO: Come up with a better name
            //TODO: Sort out git
            //TODO: Get a recipe going


            item.shoot = mod.ProjectileType("EnergySword"); // the projectile that is fired
            item.scale = 1f; //Scale of item in hand
            item.shootSpeed = 12f; //Speed of the fired projectile 12 is pretty good for the swords (156)
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);

			recipe.AddIngredient(ItemID.Shotgun, 1);
            recipe.AddIngredient(ItemID.Excalibur, 1);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofLight, 20);
            recipe.AddIngredient(ItemID.SoulofNight, 20);

            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

        
        
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            //Innacuracy properties
            //Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); //spread of 30 degrees
            //speedX = perturbedSpeed.X;
            //speedY = perturbedSpeed.Y;

            //Swords come out of the muzzle, not where you hold the gun
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            return true;
        }

        //Hold the gun at the trigger
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, -2);
        }
        
    }
}