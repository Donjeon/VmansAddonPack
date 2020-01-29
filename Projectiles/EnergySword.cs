using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework; //For MathHelper
using Microsoft.Xna.Framework.Graphics;
using System;

namespace VmansAddonPack.Projectiles
{
    public class EnergySword : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Sword");
            Main.projFrames[projectile.type] = 3; //show the projectile has 3 animation frames
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 27; //take the AI of beam sword
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            aiType = 156;

            /*
            drawOffsetX = -30; //Hitbox begins at tip. This seems correct
            drawOriginOffsetX = 16;
            drawOriginOffsetY = 0;
            */

            drawOffsetX = -6; //Corrects offset when being shot. Hitbox begins at the hilt, instead of the tip
            drawOriginOffsetX = -12;
            drawOriginOffsetY = -27;


        }

        public float shootDirection;
        public float actDirection;
        public float shootSpeed = 12;
        public NPC prey;

        public override void AI()
        {
            projectile.velocity.ToRotation();

            for (int i = 0; i < 200; i++) //cycle through the list of all AI that exist
            {
                NPC target = Main.npc[i];

                //(Credit to ExampleMod https://github.com/tModLoader/tModLoader/tree/master/ExampleMod and Qwerty https://github.com/qwerty3-14/QwertysRandomContent for this AI and the relevant methods)
                if (target.CanBeChasedBy()) //if the projectile exists (Prey does not exist and is null)
                {

                    if (UsefulMethods.ClosestNPC(ref prey, 480, projectile.Center)) //number is the range of the homing effect
                    {
                        shootDirection = (projectile.Center - prey.Center).ToRotation() - (float)Math.PI;
                    }
                    else
                    {
                        shootDirection = projectile.ai[1] - (float)Math.PI;
                    }

                    actDirection = UsefulMethods.SlowRotation(actDirection, shootDirection, 12);
                    projectile.velocity.X = (float)Math.Cos(actDirection) * shootSpeed;
                    projectile.velocity.Y = (float)Math.Sin(actDirection) * shootSpeed;
                    projectile.rotation = actDirection + (float)Math.PI / 2;
                    actDirection = projectile.velocity.ToRotation();

                }
                else
                {
                    actDirection = projectile.velocity.ToRotation();
                }

            }
               

            

            projectile.ai[0] += 1f; //how many pixels it has travelled????
            if (projectile.ai[0] < 50f)
            {
                // Fade in
                projectile.alpha -= 25;
                if (projectile.alpha < 100)
                {
                    projectile.alpha = 100;
                }
            }

            if (++projectile.frameCounter >= 5) //loop animation every 5 ticks, there are 60 ticks in a second
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 3) //it has 3 animation frames
                {
                    projectile.frame = 0;
                }
            }


            if (true) //always the case, done so i can see the stuff
            {
                Lighting.AddLight(projectile.Center, 0.6f, 0.2f, 1f); // R G B values from 0 to 1f. This makes purple of #9933ff
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 21, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default(Color), 0.7f);
            }
            
        }
        /*
        public void Kill() //Do something when it dies
        {
            Main.PlaySound(SoundID.Item25, projectile.position);
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 27, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default(Color), 0.7f);
        }
        //TODO: Get a proper sound to play on death and an explosion like jester arrows
        */

        

    }



    /*
    for (int num163 = 0; num163 < 20; num163++) //give the projectile a dust trail of 10 pixels
    {
        float x2 = projectile.position.X - projectile.velocity.X / 10f * (float)num163;
        float y2 = projectile.position.Y - projectile.velocity.Y / 10f * (float)num163;
        int num164 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 21, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default(Color), 0.7f); 
        //Dust.NewDust(new Vector2(x2, y2), 10, 10, 21, 0f, 0f, 0, default(Color), 1f);
        Main.dust[num164].alpha = projectile.alpha;
        Main.dust[num164].position.X = x2 + 5;
        Main.dust[num164].position.Y = y2;
        Main.dust[num164].velocity *= 0f;
        Main.dust[num164].noGravity = true;
    }
    */

    /*
               for (int i = 0; i < 200; i++) //HOming properties
               {
                   NPC target = Main.npc[i];

                   if (target.CanBeChasedBy()) //if the target can be chased by this projectile
                   {
                       //Get the shoot trajectory from the projectile and target
                       float shootToX = target.position.X + (float)target.width * 0.5f - projectile.Center.X;
                       float shootToY = target.position.Y + (float)target.height * 0.5f - projectile.Center.Y;//- projectile.Center.Y; 

                       float Num = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - shootToX) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - shootToY); //AI taken from chlorophyte and adapted, Projectile.cs, line 31413

                       float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));



                       //If the distance between the live targeted npc and the projectile is less than 240 pixels (Credit to ExampleMod https://github.com/tModLoader/tModLoader/tree/master/ExampleMod and Qwerty https://github.com/qwerty3-14/QwertysRandomContent)
                       if (distance < 240f && target.active && Collision.CanHit(new Vector2(projectile.position.X + (float)(projectile.width / 2), projectile.position.Y + (float)(projectile.height / 2)), 1, 1, target.position, target.width, target.height))
                       {

                           //Divide the factor, 3f, which is the desired velocity
                           distance = 3f / distance;

                           //Multiply the distance by a multiplier if you wish the projectile to have go faster
                           shootToX *= distance * 5;
                           shootToY *= distance * 5;

                           //Set the velocities to the shoot values
                           projectile.velocity.X = shootToX; //Go to this X position at the speed of ([the distance] * 5)
                           projectile.velocity.Y = shootToY;

                       }


                   }
               }
               */

}
