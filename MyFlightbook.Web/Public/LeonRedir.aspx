﻿<%@ Page Language="C#" AutoEventWireup="true" Codebehind="LeonRedir.aspx.cs" MasterPageFile="~/MasterPage.master" Async="true" Inherits="MyFlightbook.OAuth.Leon.LeonRedir" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Src="~/Controls/mfbTypeInDate.ascx" TagPrefix="uc1" TagName="mfbTypeInDate" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cpPageTitle" runat="server">
    <asp:Label ID="lblPageHeader" runat="server" Text="<%$ Resources:LogbookEntry, LeonImportHeader %>" />
</asp:Content>
<asp:Content ID="ContentTopForm" ContentPlaceHolderID="cpTopForm" runat="server">
    <div style="float:left; margin-right: 40px; width: 100px;">
        <svg version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"
	                viewBox="0 0 294.504 385.501" xml:space="preserve">
            <g>
	            <path style="fill:#2F4D83;" d="M76.324,170.969c0,0,19.173,1.787,20.313-30.934c1.121-34.02-14.609-35.263-23.492-35.263
		            c-8.882-0.123-19.206,7.746-20.825,35.772C54.105,172.108,76.324,170.969,76.324,170.969z"/>
	            <path style="fill:#2F4D83;" d="M178.548,123.407c0,0,20.691,1.765,20.691-28.679c-3.059-40.943-19.721-41.668-26.682-41.668
		            c-4.497,0-22.726,5.667-17.125,50.57C159.612,122.27,178.548,123.407,178.548,123.407z"/>
	            <path style="fill:#2F4D83;" d="M79.602,185.848c-8.75,18.993,4.465,48.927,18.2,49.327c13.757,0.396,25.376-11.172,29.078-11.203
		            c3.177,0.393,9.457,11.429,22.305,11.668v0.012c0.044,0,0.075-0.012,0.106-0.012c0.063,0,0.095,0.012,0.119,0.012v-0.012
		            c12.854-0.239,19.15-11.275,22.311-11.668c3.712,0.031,15.348,11.599,29.091,11.203c13.73-0.399,26.945-30.334,18.201-49.327
		            c-10.066-11.957-12.075-5.121-19.266-13.294c-7.162-8.176-4.767-16.682-14.355-25.209c-5.395-5.909-10.186-10.214-22.076-10.49
		            c-6.417-0.204-8.392,1.636-14.025,2.229c-5.614-0.594-7.592-2.434-13.991-2.229c-11.894,0.276-16.688,4.581-22.083,10.49
		            c-9.587,8.528-7.19,17.033-14.355,25.209C91.673,180.727,89.667,173.892,79.602,185.848z"/>
	            <path style="fill:#2F4D83;" d="M121.011,126.701c28.327-4.431,22.993-33.461,22.993-33.461s0.646-33.489-21.081-33.618
		            C109.59,59.75,98.271,73.069,97.791,91.946C97.27,111.749,106.283,126.584,121.011,126.701z"/>
	            <path style="fill:#2F4D83;" d="M238.336,128.745c-5.124-8.603-10.964-10.418-17.25-10.418c-6.299,0-20.088,10.672-20.829,31.596
		            c0.502,4.453,3.039,23.458,19.8,24.359c16.754,0.87,21.576-21.813,21.576-21.813S244.176,135.957,238.336,128.745z"/>
	            <polygon style="fill:#2F4D83;" points="81.63,252.654 70.93,252.654 70.93,304.473 104.722,304.473 104.722,296.586 81.63,296.586 
			            "/>
	            <path style="fill:#2F4D83;" d="M125.944,265.414c-5.313-0.028-9.466,1.818-12.624,5.529c-3.155,3.721-4.656,8.449-4.656,14.183
		            v1.438c0,5.501,1.594,10.035,4.926,13.609c3.339,3.568,7.748,5.354,13.324,5.354c3.125,0,5.938-0.422,8.484-1.27
		            c2.543-0.848,4.6-1.918,6.17-3.215l-2.826-6.443c-1.809,1.002-3.479,1.736-5.008,2.194c-1.523,0.465-3.444,0.704-5.749,0.704
		            c-2.6,0-4.653-0.826-6.167-2.462c-1.513-1.633-2.364-3.648-2.553-6.188l0.066-0.145h23.134v-5.824
		            c0-5.356-1.469-9.614-4.345-12.754C135.235,266.984,131.166,265.414,125.944,265.414z M132.324,281.383h-12.917l-0.107-0.034
		            c0.261-2.402,0.955-4.279,2.069-5.755c1.124-1.479,2.65-2.18,4.574-2.18c2.098,0,3.727,0.682,4.76,2.004
		            c1.042,1.318,1.62,3.102,1.62,5.321V281.383z"/>
	            <path style="fill:#2F4D83;" d="M165.122,265.414c-5.689,0-10.128,1.837-13.343,5.517c-3.222,3.677-4.811,8.399-4.811,14.16v0.751
		            c0,5.812,1.576,10.55,4.801,14.204c3.225,3.658,7.655,5.48,13.372,5.48c5.689,0,10.258-1.822,13.489-5.48
		            c3.228-3.654,4.954-8.392,4.954-14.204v-0.751c0-5.78-1.733-10.512-4.967-14.179C175.37,267.241,170.837,265.414,165.122,265.414z
		                M172.884,285.823c0,3.5-0.606,6.323-1.858,8.458c-1.244,2.139-3.206,3.216-5.897,3.216c-2.763,0-4.841-1.068-6.081-3.196
		            c-1.24-2.129-1.938-4.958-1.938-8.478v-0.744c0-3.407,0.71-6.195,1.959-8.365c1.255-2.179,3.265-3.265,5.959-3.265
		            c2.712,0,4.7,1.083,5.962,3.246c1.266,2.167,1.894,4.958,1.894,8.384V285.823z"/>
	            <path style="fill:#2F4D83;" d="M210.938,265.414c-2.336,0-4.478,0.546-6.358,1.639c-1.884,1.1-3.482,2.653-4.77,4.632l-0.483-5.511
		            h-9.552v38.299h10.143V276.67c1.127-1.029,1.717-1.817,2.819-2.376c1.093-0.565,2.36-0.845,3.742-0.845
		            c2.123,0,3.736,0.54,4.785,1.626c1.049,1.087,1.61,2.933,1.61,5.532v23.866h10.701V280.63c0-5.24-1.234-9.087-3.42-11.539
		            C217.959,266.639,214.844,265.414,210.938,265.414z"/>
	            <path style="fill:#2F4D83;" d="M77.798,322.999c-1.438-0.365-2.48-0.785-3.123-1.266c-0.637-0.477-0.962-1.07-0.962-1.793
		            c0-0.807,0.298-1.457,0.892-1.963c0.594-0.502,1.443-0.75,2.548-0.75c1.195,0,2.11,0.189,2.75,0.789
		            c0.638,0.592,0.956,1.098,0.956,2.229h2.127l0.019,0.041c0.041-1.303-0.478-2.398-1.546-3.385c-1.064-0.986-2.5-1.453-4.307-1.453
		            c-1.673,0-3.038,0.439-4.089,1.303c-1.053,0.859-1.576,1.943-1.576,3.225c0,1.23,0.49,2.25,1.463,3.061
		            c0.977,0.813,2.338,1.426,4.082,1.84c1.438,0.342,2.444,0.766,3.009,1.277c0.572,0.51,0.851,1.141,0.851,1.885
		            c0,0.813-0.328,1.451-0.984,1.924c-0.659,0.473-1.549,0.711-2.673,0.711c-1.143,0-2.105-0.211-2.882-0.758
		            c-0.784-0.555-1.167-1.783-1.167-2.342h-2.13h-0.019c-0.041,1.492,0.571,2.652,1.835,3.549c1.262,0.891,2.717,1.32,4.363,1.32
		            c1.75,0,3.171-0.41,4.256-1.221c1.086-0.807,1.628-1.875,1.628-3.209c0-1.234-0.452-2.275-1.353-3.111
		            C80.859,324.065,79.536,323.428,77.798,322.999z"/>
	            <path style="fill:#2F4D83;" d="M96.744,315.471c-1.926,0-3.573,0.67-4.775,2.004c-1.206,1.334-1.887,3.006-1.887,5.018v2.926
		            c0,2.016,0.682,3.689,1.887,5.016c1.203,1.33,2.81,1.99,4.73,1.99c1.996,0,3.696-0.66,4.945-1.99c1.25-1.326,1.955-3,1.955-5.016
		            v-2.926c0-2.021-0.686-3.689-1.931-5.023C100.416,316.137,98.734,315.471,96.744,315.471z M101.345,325.418
		            c0,1.533-0.457,2.779-1.265,3.746c-0.81,0.957-1.933,1.438-3.314,1.438c-1.293,0-2.415-0.48-3.19-1.447
		            c-0.774-0.967-1.242-2.209-1.242-3.736v-2.953c0-1.508,0.457-2.748,1.232-3.715c0.769-0.961,1.846-1.447,3.14-1.447
		            c1.375,0,2.534,0.486,3.354,1.447c0.813,0.967,1.286,2.207,1.286,3.715V325.418z"/>
	            <polygon style="fill:#2F4D83;" points="111.485,332.083 113.74,332.083 113.74,324.752 121.062,324.752 121.062,323.067 
		            113.74,323.067 113.74,317.432 122.186,317.432 122.186,315.743 111.485,315.743 	"/>
	            <polygon style="fill:#2F4D83;" points="128.38,317.432 133.455,317.432 133.455,332.083 135.709,332.083 135.709,317.432 
		            140.777,317.432 140.777,315.743 128.38,315.743 	"/>
	            <polygon style="fill:#2F4D83;" points="161.204,326.549 160.877,328.698 160.802,328.698 160.355,326.549 157.311,315.743 
		            155.342,315.743 152.319,326.502 151.875,328.569 151.81,328.553 151.502,326.502 148.991,315.743 146.77,315.743 150.73,332.083 
		            152.727,332.083 156.03,320.624 156.312,319.114 156.378,319.114 156.673,320.624 159.923,332.083 161.916,332.083 
		            165.889,315.743 163.656,315.743 	"/>
	            <path style="fill:#2F4D83;" d="M177.383,315.743l-6.374,16.34h2.276l1.561-4.51h6.905l1.538,4.51h2.279l-6.273-16.34H177.383z
		                M175.537,325.877l2.766-7.322h0.063l2.719,7.322H175.537z"/>
	            <path style="fill:#2F4D83;" d="M203.861,329.545v-1.555c0-0.977-0.157-1.789-0.55-2.453c-0.398-0.656-1.026-1.133-1.931-1.434
		            c0.841-0.365,1.494-0.867,1.94-1.492c0.437-0.621,0.659-1.363,0.659-2.207c0-1.523-0.471-2.676-1.419-3.477
		            c-0.955-0.797-2.324-1.186-4.132-1.186h-5.834v16.34h2.254v-7.33h3.987c0.876,0,1.526,0.418,2.045,0.961
		            c0.521,0.543,0.725,1.408,0.725,2.295v1.508c0,0.527,0.095,0.982,0.183,1.486c0.088,0.498,0.326,1.08,0.612,1.08h2.019v-0.164
		            c0-0.258-0.377-0.59-0.49-0.998C203.823,330.508,203.861,330.053,203.861,329.545z M198.206,323.067h-3.356v-5.635h3.58
		            c1.143,0,1.978,0.275,2.523,0.781c0.537,0.512,0.808,1.184,0.808,2.107c0,0.982-0.279,1.66-0.832,2.141
		            C200.369,322.938,199.465,323.067,198.206,323.067z"/>
	            <polygon style="fill:#2F4D83;" points="214.562,324.192 222.445,324.192 222.445,322.502 214.562,322.502 214.562,317.432 
		            223.014,317.432 223.014,315.743 212.313,315.743 212.313,332.083 223.576,332.083 223.576,330.385 214.562,330.385 	"/>
            </g>
        </svg>
    </div>
	<div style="margin-top: 30px">
	    <asp:MultiView ID="mvLeonState" runat="server">
			<asp:View ID="vwUnauthenticated" runat="server">
				<asp:Label ID="lblUnauth" runat="server" Text="<%$ Resources:LogbookEntry, LeonMustBeSignedIn %>" />
			</asp:View>
			<asp:View ID="vwNoAuthToken" runat="server">
				<p><asp:Label ID="lblNoAuthToken" runat="server" /></p>
				<p><asp:Label ID="lblSubdomainPrompt" runat="server" Text="<%$ Resources:LogbookEntry, LeonSubDomainPrompt %>" /></p>
				<div>https://<asp:TextBox ID="txtSubDomain" runat="server" Width="50px" />.leon.aero</div>
				<div><asp:LinkButton ID="lnkAuthorize" runat="server" OnClick="lnkAuthorize_Click" /></div>
				<div><asp:RequiredFieldValidator ID="reqSubDomain" runat="server" ErrorMessage="<%$ Resources:LogbookEntry, LeonSubDomainRequired %>" CssClass="error" ControlToValidate="txtSubDomain" Display="Dynamic" /> </div>
				<div><asp:RegularExpressionValidator ID="regSubDomain" runat="server" ErrorMessage="<%$ Resources:LogbookEntry, LeonInvalidSubDomain %>" CssClass="error" ControlToValidate="txtSubDomain" Display="Dynamic"  ValidationExpression="[a-zA-Z0-9]+(\.[a-zA-Z0-9]+)*" /></div>
			</asp:View>
			<asp:View ID="vwAuthorized" runat="server">
				<div><asp:Label ID="lblAuthedHeader" runat="server" /></div>
				<ul>
					<li><asp:HyperLink ID="lnkImport" runat="server" Text="<%$ Resources:LogbookEntry, LeonGoToImport %>" NavigateUrl="~/Member/Import.aspx" /></li>
					<li><asp:LinkButton ID="btnDeAuth" runat="server" OnClick="btnDeAuth_Click" /></li>
				</ul>
			</asp:View>
		</asp:MultiView>
	</div>
</asp:Content>