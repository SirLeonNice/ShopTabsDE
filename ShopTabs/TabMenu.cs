﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTabs
{
    internal class TabMenu : IClickableMenu
    {
        public ShopMenu targetMenu;

        public static List<string> filterTypes = new()
        {
            "Seeds", // type == Seeds, category == -74
            "Saplings", // type == Basic, catagory == -74
            "Cooking", // type == Cooking, catagory == -7
            "Fertillizer", // type == Basic, catagory == -19
            "Crops", // type == Basic, catagory == -75
            "Recipes",
            "Other"
        };

        public List<ClickableTextureComponent> filterTabs = new();

        public TabMenu(ShopMenu menu, Dictionary<ISalable, ItemStockInformation> shopItems)
        {
            this.targetMenu = menu;
            menu.SetChildMenu(this);

            ClickableTextureComponent seedsTab = new ClickableTextureComponent(
                "Seeds", new Rectangle(menu.xPositionOnScreen, menu.yPositionOnScreen - 64, 64, 64), "",
                "Seeds", Game1.mouseCursors, new Rectangle(16, 368, 16, 16), 4f);

            filterTabs.Add(seedsTab);
        }

        public override void draw(SpriteBatch b)
        {
            base.draw(b);
            foreach (ClickableTextureComponent tab in filterTabs)
            {
                Console.WriteLine("drawing tab");
                tab.draw(b);
            }
            drawMouse(b);
        }

        public override void performHoverAction(int x, int y)
        {
            base.performHoverAction(x, y);
            bool hoveredOverAnyTab = false;

            foreach (ClickableTextureComponent tab in filterTabs)
            {
                if (tab.containsPoint(x, y))
                {
                    tab.tryHover(x, y);
                    hoveredOverAnyTab = true;
                }
            }

            if (!hoveredOverAnyTab)
                targetMenu.performHoverAction(x, y); // Forward hover action to ShopMenu if not hovering over any tabs
        }

        public override void receiveScrollWheelAction(int direction)
        {
            targetMenu.receiveScrollWheelAction(direction);
        }

        public override void update(GameTime time)
        {
            targetMenu.update(time);
        }

        public override bool readyToClose()
        {
            return targetMenu.readyToClose();
        }

        public override void snapToDefaultClickableComponent()
        {
            targetMenu.snapToDefaultClickableComponent();
        }

        public override void snapCursorToCurrentSnappedComponent()
        {
            targetMenu.snapCursorToCurrentSnappedComponent();
        }
    }
}