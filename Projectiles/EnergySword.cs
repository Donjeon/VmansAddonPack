﻿using Terraria;
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

        //Fires a sword of chaotic energy that chases after enemies and strikes multiple times

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 27; //take the AI of beam sword
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            projectile.noDropItem = true;

            projectile.penetrate = Main.rand.Next(2, 8); //these 2 lines along with homing make for a very cool effect
            projectile.tileCollide = true; //false


            aiType = 156;

            
            drawOffsetX = -30; //Hitbox begins at tip. This seems correct
            drawOriginOffsetX = 16;
            drawOriginOffsetY = 0;
            
            /*
            drawOffsetX = -6; //Corrects offset when being shot. Hitbox begins at the hilt, instead of the tip
            drawOriginOffsetX = -12;
            drawOriginOffsetY = -27;
            */


        }



        public override void AI()
        {

          


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


            if (projectile.active) //always the case, done so i can see the stuff
            {
                Lighting.AddLight(projectile.Center, 0.6f, 0.2f, 1f); // R G B values from 0 to 1f. This makes purple of #9933ff

                for(int i = 0; i < 2; i++) //i < number of times the particles should be generated per cycle
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 21, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default(Color), 0.7f);
                }
                
            }
            
            if (projectile.active) //if the projectile exists
            {


                for (int i = 0; i < 200; i++) //HOming properties
                {
                    NPC target = Main.npc[i];

                    if (target.CanBeChasedBy() && projectile.alpha > 100) //if the target can be chased by this projectile
                    {
                        //Get the shoot trajectory from the projectile and target
                        float shootToX = target.position.X + (float)target.width * 0.5f - projectile.Center.X;
                        float shootToY = target.position.Y + (float)target.height * 0.5f - projectile.Center.Y;//- projectile.Center.Y; 

                        float Num = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - shootToX) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - shootToY); //AI taken from chlorophyte and adapted, Projectile.cs, line 31413

                        float distance = (float)System.Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));
                        


                        //If the distance between the live targeted npc. Distance is chosen to be a random distance between 50 and 500
                        if (distance < Main.rand.Next(50, 500) && target.active && Collision.CanHit(new Vector2(projectile.position.X + (float)(projectile.width / 2), projectile.position.Y + (float)(projectile.height / 2)), 1, 1, target.position, target.width, target.height))
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

            }

        }

        
        
        public override void Kill(int timeleft) //Do something when it dies
        {
            //Main.PlaySound(SoundID.Item25, projectile.position);
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10); //Main.PlaySound(int listid, int x, int y, int soundid) first number is the list, second is the style.
            Dust.NewDust(projectile.position, projectile.width, projectile.height, 27, projectile.velocity.X * 0.25f, projectile.velocity.Y * 0.25f, 150, default(Color), 0.7f);


        }
        //TODO: Get a proper sound to play on death and an explosion like jester arrows
        
    }
}