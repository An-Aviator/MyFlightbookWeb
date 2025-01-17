﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;

/******************************************************
 * 
 * Copyright (c) 2011-2021 MyFlightbook LLC
 * Contact myflightbook-at-gmail.com for more information
 *
*******************************************************/

namespace MyFlightbook.ImportFlights
{
    public partial class ImportTable : Page
    {
        private const int cColumnsOfProps = 4;

        private static void AddProp(CustomPropertyType cpt, TableRow tr)
        {
            string szUnit = string.Empty;
            switch (cpt.Type)
            {
                case CFPPropertyType.cfpBoolean:
                    szUnit = Resources.LogbookEntry.importUnitBoolean;
                    break;
                case CFPPropertyType.cfpCurrency:
                case CFPPropertyType.cfpDecimal:
                case CFPPropertyType.cfpInteger:
                    szUnit = Resources.LogbookEntry.importUnitNumber;
                    break;
                case CFPPropertyType.cfpDate:
                case CFPPropertyType.cfpDateTime:
                    szUnit = Resources.LogbookEntry.importUnitDate;
                    break;
                case CFPPropertyType.cfpString:
                    szUnit = Resources.LogbookEntry.importUnitText;
                    break;
                default:
                    break;
            }

            TableCell tc = new TableCell();
            tr.Cells.Add(tc);
            tc.Style["padding"] = "5px";

            Panel pTitle = new Panel();
            tc.Controls.Add(pTitle);
            Label lTitle = new Label();
            pTitle.Controls.Add(lTitle);
            lTitle.Style["font-weight"] = "bold";
            lTitle.Text = cpt.Title;

            Panel pUnit = new Panel();
            tc.Controls.Add(pUnit);
            pUnit.Controls.Add(new LiteralControl(szUnit));

            Panel pDesc = new Panel();
            tc.Controls.Add(pDesc);
            pDesc.Style["font-size"] = "smaller";
            pDesc.Controls.Add(new LiteralControl(cpt.Description));

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            tblAdditionalProps.Rows.Add(new TableRow());
            int iCurRow = tblAdditionalProps.Rows.Count - 1;
            CustomPropertyType[] rgCpt = CustomPropertyType.GetCustomPropertyTypes(string.Empty);
            int cRows = (rgCpt.Length + cColumnsOfProps - 1) / cColumnsOfProps;
            for (int iRow = 0; iRow < cRows; iRow++)
            {
                for (int iCol = 0; iCol < cColumnsOfProps; iCol++)
                {
                    int iProp = (iCol * cRows) + iRow;
                    if (iProp < rgCpt.Length)
                        AddProp(rgCpt[iProp], tblAdditionalProps.Rows[iCurRow]);
                    else
                    {
                        // add blank cells to pad out the row.
                        tblAdditionalProps.Rows[iCurRow].Cells.Add(new TableCell());
                    }
                }
                tblAdditionalProps.Rows.Add(new TableRow());
                iCurRow = tblAdditionalProps.Rows.Count - 1;
                tblAdditionalProps.Rows[iCurRow].Style["vertical-align"] = "top";
            }

            if (tblAdditionalProps.Rows[iCurRow].Cells.Count == 0)
                tblAdditionalProps.Rows.RemoveAt(iCurRow);

            Master.SelectedTab = tabID.tabLogbook;
            Title = lblImportHeader.Text = String.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.LogbookEntry.importTableHeader, Branding.CurrentBrand.AppName);
            litTableMainFields.Text = Branding.ReBrand(Resources.LogbookEntry.ImportTableDescription);
        }
    }
}