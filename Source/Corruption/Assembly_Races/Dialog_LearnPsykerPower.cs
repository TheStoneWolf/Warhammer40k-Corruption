﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace Corruption
{
    public class Dialog_LearnPsykerPower : Window                
    {
        private PsykerPowerManager powerManager;

        private PsykerPowerLevel powerLevel;

        private Vector2 scrollPosition = Vector2.zero;

        private PsykerPowerDef selectedPsykerPower;

        public Dialog_LearnPsykerPower(PsykerPowerLevel powerLevel, PsykerPowerManager powerManager)
        {
            this.powerManager = powerManager;
            this.powerLevel = powerLevel;
            this.forcePause = true;
            this.closeOnEscapeKey = true;
            this.absorbInputAroundWindow = true;
        }

        private List<PsykerPowerDef> powerDefs
        {
            get
            {
                return PsykerUtility.GetPowerDefsFor(this.powerLevel);
            }
        }

        public override void DoWindowContents(Rect inRect)
        {
            Rect mainRect = inRect.ContractedBy(2f);
            Rect outRect = new Rect(0f, 35f, mainRect.width, 256f);
            Rect viewRect = new Rect(0f, 0f, outRect.width - 50f, 1024f);

            Widgets.BeginScrollView(outRect, ref this.scrollPosition, viewRect);

            Listing_Standard listing_Standard = new Listing_Standard();
            listing_Standard.Begin(viewRect);
            int index = 0;
            Text.Font = GameFont.Medium;
            foreach (PsykerPowerDef def in this.powerDefs)
            {
                Rect iconRect = new Rect(0f, listing_Standard.CurHeight, Text.LineHeight, Text.LineHeight);
                GUI.DrawTexture(iconRect, def.uiIcon);
                if (listing_Standard.RadioButton(def.label, this.selectedPsykerPower == def, Text.LineHeight + 2f))
                {
                    this.selectedPsykerPower = def;
                }
                index++;
            }
            listing_Standard.End();
            Widgets.EndScrollView();

            Text.Font = GameFont.Small;
            Rect DescriptionRect = new Rect(mainRect.x, outRect.yMax + 16f, inRect.width, 128f);
            string description = selectedPsykerPower == null ? "Select Power" : selectedPsykerPower.description;
            Widgets.Label(DescriptionRect, description);

            Rect confirmRect = new Rect(0f, DescriptionRect.yMax + 4f, 128f, 64f);
            if (Widgets.ButtonText(confirmRect, "OK"))
            {
                this.ConfirmPower();
            }

        }
        
        private void ConfirmPower()
        {
            this.powerManager.LearnPsykerPower(this.selectedPsykerPower);
            this.Close();
           }

        public override Vector2 InitialSize
        {
            get
            {
                return new Vector2(512f, 544f);
            }
        }

        public override void PreOpen()
        {
            base.PreOpen();
        }
    }
}
