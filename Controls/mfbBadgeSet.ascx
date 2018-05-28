﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="mfbBadgeSet.ascx.cs" Inherits="Controls_mfbBadgeSet" %>
<asp:Panel ID="pnlBadges" runat="server">
    <h2><asp:Label ID="lblCategory" runat="server" Text=""></asp:Label></h2>
    <asp:Repeater ID="repeaterBadges" runat="server">
        <ItemTemplate>
            <div style="display:inline-block; text-align:center; vertical-align:top; margin-left: 5px; margin-right:5px; width:140px">
                <div style="position:relative; display:inline;">
                    <asp:Image ID="imgBadge" runat="server" Width="70" Height="113" ImageUrl='<%# Eval("BadgeImage") %>' ToolTip='<%# Eval("BadgeImageAltText") %>' AlternateText='<%# Eval("BadgeImageAltText") %>' />
                    <asp:Image ID="imgOverlay" runat="server" Width="70" ImageUrl='<%# Eval("BadgeImageOverlay") %>' ToolTip="" AlternateText="" Visible='<%# Eval("BadgeImageOverlay").ToString().Length > 0 %>' style="position:absolute; bottom: 0; left: 0; z-index:1;" />
                </div>
                <br />
                <asp:Label ID="lblBadgeName" runat="server" Text='<%# Eval("Name") %>' Font-Bold="true"></asp:Label><br />
                <asp:Label ID="lblDateEarned" runat="server" Text='<%# Eval("EarnedDateString") %>'></asp:Label>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Panel>


