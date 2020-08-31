﻿using MyFlightbook.Airports;
using MyFlightbook.Currency;
using System;
using System.Collections.ObjectModel;
using System.Globalization;

/******************************************************
 * 
 * Copyright (c) 2013-2020 MyFlightbook LLC
 * Contact myflightbook-at-gmail.com for more information
 *
*******************************************************/

namespace MyFlightbook.RatingsProgress
{

    /// <summary>
    /// IFR milestones
    /// </summary>
    [Serializable]
    public class InstrumentMilestones : MilestoneGroup
    {
        public InstrumentMilestones()
        {
            GroupName = Resources.MilestoneProgress.RatingGroupInstrument;
        }

        public override Collection<MilestoneProgress> Milestones
        {
            get
            {
                return new Collection<MilestoneProgress> {
                    new IFR6165D(),
                    new IFR6165E(),
                    new IFR6165F(),
                    new IFR141CAirplane(),
                    new IFR141CHelicopter(),
                    new IFRCanadaAirplane(),
                    new IFRCanadaHelicopter()
                };
            }
        }
    }

    #region 61.65 - Instrument Ratings
    /// <summary>
    /// 61.65 - Instrument rating
    /// </summary>
    [Serializable]
    public abstract class IFR6165Base : MilestoneProgress
    {
        protected MilestoneItem miMinXCTime { get; set; }
        protected MilestoneItem miMinTimeInCategory { get; set; }
        protected MilestoneItem miMinIMCTime { get; set; }
        protected MilestoneItem miMinIMCTestPrep { get; set; }
        protected MilestoneItem miIMCXC { get; set; }

        protected MilestoneItem miInstrumentTraining { get; set; }

        // Per 61.57(h), can count 20 or 30 hours of FTD time, and per 61.57(i) can count 10 or 20 hours of ATD time
        // We use the smaller amount in both cases because we can't tell if it's performed under part 142 (for 61.57(h)) 
        // nor can we distinguish a BATD from an AATD (for 61.57(i)).
        // These limits seem additive, though (can do both FTD and ATD), and since AddTrainingEvent can only accomodate
        // one limited, we have to manually do the FTD limit here.
        private const int MaxFTDTime = 20;
        protected int MaxATDTime { get; set; } = 10;

        private decimal FTDTimeRemaining { get; set; }

        protected int MinXCDistance { get; set; } = 250;

        /// <summary>
        /// Initializes an instrument rating object
        /// </summary>
        /// <param name="szTitle">Title</param>
        /// <param name="szBaseFAR">Base FAR</param>
        /// <param name="ratingSought">Specific rating being sought</param>
        protected virtual void Init(string szTitle, string szBaseFAR, RatingType ratingSought)
        {
            Title = szTitle;
            BaseFAR = szBaseFAR;
            RatingSought = ratingSought;

            FTDTimeRemaining = MaxFTDTime;

            string szAircraftCategory;

            switch (RatingSought)
            {
                case RatingType.InstrumentAirplane:
                    szAircraftCategory = Resources.MilestoneProgress.ratingAirplane;
                    break;
                case RatingType.InstrumentHelicopter:
                    szAircraftCategory = Resources.MilestoneProgress.ratingHelicopter;
                    break;
                case RatingType.InstrumentPoweredLift:
                    szAircraftCategory = Resources.MilestoneProgress.ratingPoweredLift;
                    break;
                default:
                    throw new MyFlightbookException(String.Format(CultureInfo.InvariantCulture, "Unallowed rating for 61.65: {0}", RatingSought.ToString()));
            }

            // 61.65(def)(1)
            miMinXCTime = new MilestoneItem(Resources.MilestoneProgress.MinInstrumentPICXC, ResolvedFAR("(1)"), Resources.MilestoneProgress.NoteXCTime, MilestoneItem.MilestoneType.Time, 50.0M);
            miMinTimeInCategory = new MilestoneItem(String.Format(CultureInfo.CurrentCulture, Resources.MilestoneProgress.MinInstrumentPICInCategory, szAircraftCategory), ResolvedFAR("(1)"), String.Empty, MilestoneItem.MilestoneType.Time, 10.0M);

            // 61.65(def)(2) - total instrument time
            miMinIMCTime = new MilestoneItem(Resources.MilestoneProgress.MinInstrumentTime, ResolvedFAR("(2)"), Branding.ReBrand(Resources.MilestoneProgress.NoteInstrumentTime), MilestoneItem.MilestoneType.Time, 40.0M);

            // 61.65(def)(2) - Instrument training
            miInstrumentTraining = new MilestoneItem(Resources.MilestoneProgress.MinInstrumentTraining, ResolvedFAR("(2)"), Branding.ReBrand(Resources.MilestoneProgress.MinInstrumentTrainingNote), MilestoneItem.MilestoneType.Time, 15.0M);

            // 61.65(def)(2)(i) - recent test prep
            miMinIMCTestPrep = new MilestoneItem(String.Format(CultureInfo.CurrentCulture, Resources.MilestoneProgress.MinInstrumentTestPrep, szAircraftCategory), ResolvedFAR("(2)(i)"), Branding.ReBrand(Resources.MilestoneProgress.NoteTestPrep), MilestoneItem.MilestoneType.Time, 3.0M);

            // 61.65(def)(2)(ii) - cross-country
            miIMCXC = new MilestoneItem(String.Format(CultureInfo.CurrentCulture, Resources.MilestoneProgress.MinInstrumentXC, szAircraftCategory, MinXCDistance), ResolvedFAR("(2)(ii)"), Branding.ReBrand(Resources.MilestoneProgress.NoteInstrumentXC), MilestoneItem.MilestoneType.AchieveOnce, 1.0M);
        }

        protected IFR6165Base() { }

        public override void ExamineFlight(ExaminerFlightRow cfr)
        {
            if (cfr == null)
                throw new ArgumentNullException(nameof(cfr));

            decimal IMCTime = cfr.IMC + cfr.IMCSim;

            if (cfr.fIsFTD)
            {
                decimal FTDTimeToApply = Math.Min(IMCTime, FTDTimeRemaining);
                miMinIMCTime.AddEvent(FTDTimeToApply);
                FTDTimeRemaining -= FTDTimeToApply;
            }
            else if (cfr.fIsATD)
                miMinIMCTime.AddTrainingEvent(IMCTime, MaxATDTime, true);

            // Everything past this point only applies to real aircraft
            if (!cfr.fIsRealAircraft)
                return;

            bool IsInMatchingCategory = CatClassMatchesRatingSought(cfr.idCatClassOverride);
            decimal XCPICTime = Math.Min(cfr.PIC, cfr.XC);
            decimal IMCXCTime = Math.Min(IMCTime, cfr.XC);

            // 61.65(def)(1) - Look for cross-country time as PIC
            miMinXCTime.AddEvent(XCPICTime);
            if (IsInMatchingCategory)
                miMinTimeInCategory.AddEvent(XCPICTime);

            // 61.65(def)(2) - IMC time (total)
            miMinIMCTime.AddEvent(IMCTime);

            decimal instTrainingTime = Math.Min(cfr.Dual, IMCTime);
            miInstrumentTraining.AddEvent(instTrainingTime);

            // 61.65(def)(2)(i) - recent test prep
            if (IsInMatchingCategory)
            {
                if (DateTime.Now.AddCalendarMonths(-2).CompareTo(cfr.dtFlight) <= 0)
                    miMinIMCTestPrep.AddEvent(instTrainingTime);

                if (cfr.cApproaches >= 3 && IMCXCTime > 0.0M)
                {
                    AirportList al = AirportListOfRoutes.CloneSubset(cfr.Route);
                    if (al.DistanceForRoute() >= MinXCDistance)
                    {
                        miIMCXC.AddEvent(1.0M);
                        miIMCXC.MatchingEventText = String.Format(CultureInfo.CurrentCulture, Resources.MilestoneProgress.MatchingXCFlightTemplate, cfr.dtFlight.ToShortDateString(), cfr.Route);
                        miIMCXC.MatchingEventID = cfr.flightID;
                    }
                }
            }
        }

        public override Collection<MilestoneItem> Milestones
        {
            get { return new Collection<MilestoneItem>() { miMinXCTime, miMinTimeInCategory, miMinIMCTime, miInstrumentTraining, miMinIMCTestPrep, miIMCXC }; }
        }
    }

    #region Concrete IFR Classes
    /// <summary>
    /// 61.65(d) - Instrument Airplane
    /// </summary>
    [Serializable]
    public class IFR6165D : IFR6165Base
    {
        public IFR6165D() { Init(Resources.MilestoneProgress.Title6165D, "61.65(d)", RatingType.InstrumentAirplane); }
    }

    /// <summary>
    /// 61.65(e) - Instrument Helicopter
    /// </summary>
    [Serializable]
    public class IFR6165E : IFR6165Base
    {
        public IFR6165E() { MinXCDistance = 100; Init(Resources.MilestoneProgress.Title6165E, "61.65(e)", RatingType.InstrumentHelicopter); }
    }

    /// <summary>
    /// 61.65(f) - Instrument Powered Lift
    /// </summary>
    [Serializable]
    public class IFR6165F : IFR6165Base
    {
        public IFR6165F() { Init(Resources.MilestoneProgress.Title6165F, "61.65(f)", RatingType.InstrumentPoweredLift); }
    }

    [Serializable]
    public abstract class IFRCanadaBase : IFR6165Base
    {
        protected MilestoneItem miXCPIC { get; set; }
        protected MilestoneItem miXCDualXC { get; set; }

        protected MilestoneItem miIFRTrainingInCategory { get; set; }

        protected override void Init(string szTitle, string szBaseFAR, RatingType ratingSought)
        {
            MaxATDTime = 20;    // can do 20 hours of FTD OR ATD time.
            MinXCDistance = 100;

            base.Init(szTitle, szBaseFAR, ratingSought);

            miXCPIC = new MilestoneItem(Resources.MilestoneProgress.MinInstrumentPICXC, ResolvedFAR("i"), Resources.MilestoneProgress.NoteCanadaIRXC, MilestoneItem.MilestoneType.Time, 50.0M);
            miXCDualXC = new MilestoneItem(String.Format(CultureInfo.InvariantCulture, Resources.MilestoneProgress.MinInstrumentXCCanada, MinXCDistance), ResolvedFAR("(ii)(D)"), Resources.MilestoneProgress.MinInstrumentXCCanadaNote, MilestoneItem.MilestoneType.AchieveOnce, 1.0M);
            miIFRTrainingInCategory = new MilestoneItem(String.Format(CultureInfo.CurrentCulture, Resources.MilestoneProgress.MinInstrumentTrainingInCategoryCanada, ratingSought == RatingType.InstrumentAirplane ? Resources.MilestoneProgress.CAATPAeroplanes : Resources.MilestoneProgress.CAATPHelicopters), ResolvedFAR("(ii)(B)"), string.Empty, MilestoneItem.MilestoneType.Time, 5.0M);

            // fix up FAR references for inherited milestones
            miMinTimeInCategory.FARRef = ResolvedFAR("(i)");
            miMinIMCTime.FARRef = ResolvedFAR("(ii)");
            miInstrumentTraining.FARRef = ResolvedFAR("(ii)(C)");

            this.GeneralDisclaimer = Branding.ReBrand(Resources.MilestoneProgress.InstrumentCanadaGeneralDisclaimer);
        }

        public override void ExamineFlight(ExaminerFlightRow cfr)
        {
            base.ExamineFlight(cfr);

            if (cfr == null)
                throw new ArgumentNullException(nameof(cfr));

            if (!cfr.fIsRealAircraft)
                return;

            switch (cfr.idCatClassOverride)
            {
                case CategoryClass.CatClassID.Helicopter:
                case CategoryClass.CatClassID.ASEL:
                case CategoryClass.CatClassID.ASES:
                case CategoryClass.CatClassID.AMEL:
                case CategoryClass.CatClassID.AMES:
                    miXCPIC.AddEvent(Math.Min(cfr.PIC, cfr.XC));
                    break;
                default:
                    break;
            }

            if (cfr.IMC + cfr.IMCSim > 0 && cfr.Dual > 0 && CatClassMatchesRatingSought(cfr.idCatClassOverride))
                miIFRTrainingInCategory.AddEvent(Math.Min(cfr.Dual, cfr.IMC + cfr.IMCSim));

            if (cfr.cApproaches >= 2 && cfr.Dual > 0 && cfr.IMC + cfr.IMCSim > 0)
            {
                AirportList al = AirportListOfRoutes.CloneSubset(cfr.Route);
                if (al.DistanceForRoute() >= MinXCDistance)
                {
                    miXCDualXC.AddEvent(1.0M);
                    miXCDualXC.MatchingEventText = String.Format(CultureInfo.CurrentCulture, Resources.MilestoneProgress.MatchingXCFlightTemplate, cfr.dtFlight.ToShortDateString(), cfr.Route);
                    miXCDualXC.MatchingEventID = cfr.flightID;
                }
            }
        }

        public override Collection<MilestoneItem> Milestones
        {
            get { return new Collection<MilestoneItem>() { miXCPIC, miMinTimeInCategory, miMinIMCTime, miIFRTrainingInCategory, miInstrumentTraining, miXCDualXC }; }
        }
    }

    /// <summary>
    /// 421.46(2)(b) - Instrument airplane (Canada)
    /// Essentially the same as 61.65, but a few minor differences
    /// </summary>
    [Serializable]
    public class IFRCanadaAirplane : IFRCanadaBase
    {
        public IFRCanadaAirplane() : base()
        {
            Init(Resources.MilestoneProgress.Title42146Airplane, "421.46(2)(b)", RatingType.InstrumentAirplane);
        }
    }

    /// <summary>
    /// 421.46(2)(b) - Instrument airplane (Canada)
    /// Essentially the same as 61.65, but a few minor differences
    /// </summary>
    [Serializable]
    public class IFRCanadaHelicopter : IFRCanadaBase
    {
        public IFRCanadaHelicopter() : base()
        {
            Init(Resources.MilestoneProgress.Title42146Helicopter, "421.46(2)(b)", RatingType.InstrumentHelicopter);
        }
    }

    [Serializable]
    public abstract class IFR141Base : IFR6165Base
    {
        private readonly MilestoneItem miIMCAircraftTime, miIMCFSTime, miIMCFTDTime, miIMCATDTime;

        const decimal totalIMCTime = 35.0M;
        const decimal maxIMCFSTime = totalIMCTime * 0.5M;
        const decimal maxIMCFTDTime = totalIMCTime * 0.4M;
        const decimal maxIMCATDTime = totalIMCTime * 0.25M;
        const decimal maxIMCTrainingDeviceTime = totalIMCTime * 0.5M;

        protected IFR141Base(string szTitle, string szBaseFAR, RatingType ratingSought, int minXCDistance = 250)
        {
            MinXCDistance = minXCDistance;
            Init(szTitle, szBaseFAR, ratingSought);

            miIMCAircraftTime = new MilestoneItem(Resources.MilestoneProgress.MinInstrumentTime141, ResolvedFAR("(a)(1)"), Resources.MilestoneProgress.Note141InstrumentReducedHours, MilestoneItem.MilestoneType.Time, totalIMCTime);
            miIMCFSTime = new MilestoneItem(Resources.MilestoneProgress.MinInstrumentTime141, ResolvedFAR("(b)(1)"), Resources.MilestoneProgress.Note141InstrumentReducedHours, MilestoneItem.MilestoneType.Time, maxIMCFSTime);
            miIMCFTDTime = new MilestoneItem(Resources.MilestoneProgress.MinInstrumentTime141, ResolvedFAR("(b)(2)"), Resources.MilestoneProgress.Note141InstrumentReducedHours, MilestoneItem.MilestoneType.Time, maxIMCFTDTime);
            miIMCATDTime = new MilestoneItem(Resources.MilestoneProgress.MinInstrumentTime141, ResolvedFAR("(b)(3)"), Resources.MilestoneProgress.Note141InstrumentReducedHours, MilestoneItem.MilestoneType.Time, maxIMCATDTime);
        }

        public override void ExamineFlight(ExaminerFlightRow cfr)
        {
            if (cfr == null)
                throw new ArgumentNullException(nameof(cfr));
            base.ExamineFlight(cfr);    // really only picks up the cross-country flight

            if (cfr.fIsCertifiedIFR && CatClassMatchesRatingSought(cfr.idCatClassOverride))
            {
                decimal IMCTime = cfr.IMC + cfr.IMCSim;
                if (cfr.fIsRealAircraft)
                    miIMCAircraftTime.AddEvent(IMCTime);
                else if (cfr.fIsCertifiedIFR)
                {
                    if (cfr.fIsCertifiedLanding)
                        miIMCFSTime.AddEvent(IMCTime);
                    else if (cfr.fIsFTD)
                        miIMCFTDTime.AddEvent(IMCTime);
                    else
                        miIMCATDTime.AddEvent(IMCTime);
                }
            }
        }

        public override Collection<MilestoneItem> Milestones
        {
            get
            {
                // Adjust for maximum training time.
                decimal allowedFSTime = Math.Min(miIMCFSTime.Threshold, miIMCFSTime.Progress);
                decimal allowedFTDTime = Math.Min(miIMCFTDTime.Threshold, miIMCFTDTime.Progress);
                decimal allowedATDTime = Math.Min(miIMCATDTime.Threshold, miIMCATDTime.Progress);
                decimal allowedSimTime = Math.Min(maxIMCTrainingDeviceTime, allowedFSTime + allowedFTDTime + allowedATDTime);
                miIMCAircraftTime.Progress += allowedSimTime;

                return new Collection<MilestoneItem>() { miIMCAircraftTime, miIMCXC };
            }
        }
    }

    [Serializable]
    public class IFR141CAirplane : IFR141Base
    {
        public IFR141CAirplane() : base(Resources.MilestoneProgress.Title141IFRAirplane, "Part 141, Appendix (C)(4)", RatingType.InstrumentAirplane)
        {
            miIMCXC.FARRef = ResolvedFAR("(c)(1)");
        }
    }

    [Serializable]
    public class IFR141CHelicopter : IFR141Base
    {
        public IFR141CHelicopter() : base(Resources.MilestoneProgress.Title141IFRHelicopter, "Part 141, Appendix (C)(4)", RatingType.InstrumentHelicopter, 100)
        {
            miIMCXC.FARRef = ResolvedFAR("(c)(2)");
        }
    }
    #endregion
    #endregion
}