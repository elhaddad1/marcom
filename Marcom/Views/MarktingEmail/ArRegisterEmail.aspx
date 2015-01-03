<%@ Page Title="Marcom Trade" Language="C#" Inherits="System.Web.Mvc.ViewPage<Marcom.Models.clsAddToCard>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    
    <style type="text/css">
        /* Client-specific Styles */#outlook a
        {
            padding: 0;
        }
        /* Force Outlook to provide a "view in browser" button. */body
        {
            width: 100% !important;
        }
        .ReadMsgBody
        {
            width: 100%;
        }
        .ExternalClass
        {
            width: 100%;
        }
        /* Force Hotmail to display emails at full width */body
        {
            -webkit-text-size-adjust: none;
        }
        /* Prevent Webkit platforms from changing default text sizes. *//* Reset Styles */body
        {
            margin: 0;
            padding: 0;
        }
        img
        {
            border: 0;
            height: auto;
            line-height: 100%;
            outline: none;
            text-decoration: none;
        }
        table td
        {
            border-collapse: collapse;
        }
        #backgroundTable
        {
            height: 100% !important;
            margin: 0;
            padding: 0;
            width: 100% !important;
        }
        /* Template Styles *//* /\/\/\/\/\/\/\/\/\/\ STANDARD STYLING: COMMON PAGE ELEMENTS /\/\/\/\/\/\/\/\/\/\ *//**
			* @tab Page
			* @section background color
			* @tip Set the background color for your email. You may want to choose one that matches your company's branding.
			* @theme page
			*/body, #backgroundTable
        {
            /*@editable*/
            background-color: #FAFAFA;
        }
        /**
			* @tab Page
			* @section email border
			* @tip Set the border for your email.
			*/#templateContainer
        {
            /*@editable*/
            border: 1px solid #DDDDDD;
        }
        /**
			* @tab Page
			* @section heading 1
			* @tip Set the styling for all first-level headings in your emails. These should be the largest of your headings.
			* @style heading 1
			*/h1, .h1
        {
            /*@editable*/
            color: #202020;
            display: block; /*@editable*/
            font-family: Tahoma; /*@editable*/
            font-size: 20px; /*@editable*/
            font-weight: bold; /*@editable*/
            line-height: 100%;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0; /*@editable*/
        }
        /**
			* @tab Page
			* @section heading 2
			* @tip Set the styling for all second-level headings in your emails.
			* @style heading 2
			*/h2, .h2
        {
            /*@editable*/
            color: #202020;
            display: block; /*@editable*/
            font-family: Tahoma; /*@editable*/
            font-size: 19px; /*@editable*/
            font-weight: bold; /*@editable*/
            line-height: 100%;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0; /*@editable*/
            text-align: right;
        }
        /**
			* @tab Page
			* @section heading 3
			* @tip Set the styling for all third-level headings in your emails.
			* @style heading 3
			*/h3, .h3
        {
            /*@editable*/
            color: #202020;
            display: block; /*@editable*/
            font-family: Tahoma; /*@editable*/
            font-size: 18px; /*@editable*/
            font-weight: bold; /*@editable*/
            line-height: 100%;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0; /*@editable*/
            text-align: right;
        }
        /**
			* @tab Page
			* @section heading 4
			* @tip Set the styling for all fourth-level headings in your emails. These should be the smallest of your headings.
			* @style heading 4
			*/h4, .h4
        {
            /*@editable*/
            color: #202020;
            display: block; /*@editable*/
            font-family: Tahoma; /*@editable*/
            font-size: 16px; /*@editable*/
            font-weight: bold; /*@editable*/
            line-height: 100%;
            margin-top: 0;
            margin-right: 0;
            margin-bottom: 10px;
            margin-left: 0; /*@editable*/
            text-align: right;
        }
        /* /\/\/\/\/\/\/\/\/\/\ STANDARD STYLING: PREHEADER /\/\/\/\/\/\/\/\/\/\ *//**
			* @tab Header
			* @section preheader style
			* @tip Set the background color for your email's preheader area.
			* @theme page
			*/#templatePreheader
        {
            /*@editable*/
            background-color: #FAFAFA;
        }
        /**
			* @tab Header
			* @section preheader text
			* @tip Set the styling for your email's preheader text. Choose a size and color that is easy to read.
			*/.preheaderContent div
        {
            /*@editable*/
            color: #505050; /*@editable*/
            font-family: Tahoma; /*@editable*/
            font-size: 10px; /*@editable*/
            line-height: 100%; /*@editable*/
            text-align: right;
            padding-top: 10px;
        }
        /**
			* @tab Header
			* @section preheader link
			* @tip Set the styling for your email's preheader links. Choose a color that helps them stand out from your text.
			*/.preheaderContent div a:link, .preheaderContent div a:visited, /* Yahoo! Mail Override */ .preheaderContent div a .yshortcuts /* Yahoo! Mail Override */
        {
            /*@editable*/
            color: #336699; /*@editable*/
            font-weight: normal; /*@editable*/
            text-decoration: underline;
        }
        /* /\/\/\/\/\/\/\/\/\/\ STANDARD STYLING: HEADER /\/\/\/\/\/\/\/\/\/\ *//**
			* @tab Header
			* @section header style
			* @tip Set the background color and border for your email's header area.
			* @theme header
			*/#templateHeader
        {
            /*@editable*/
            background-color: #FFFFFF; /*@editable*/
            border-bottom: 0;
        }
        /**
			* @tab Header
			* @section header text
			* @tip Set the styling for your email's header text. Choose a size and color that is easy to read.
			*/.headerContent
        {
            /*@editable*/
            color: #202020; /*@editable*/
            font-family: Tahoma; /*@editable*/
            font-size: 34px; /*@editable*/
            font-weight: bold; /*@editable*/
            line-height: 100%; /*@editable*/
            padding: 0; /*@editable*/
            text-align: center; /*@editable*/
            vertical-align: middle;
        }
        /**
			* @tab Header
			* @section header link
			* @tip Set the styling for your email's header links. Choose a color that helps them stand out from your text.
			*/.headerContent a:link, .headerContent a:visited, /* Yahoo! Mail Override */ .headerContent a .yshortcuts /* Yahoo! Mail Override */
        {
            /*@editable*/
            color: #336699; /*@editable*/
            font-weight: normal; /*@editable*/
            text-decoration: underline;
        }
        #headerImage
        {
            height: auto;
            max-width: 440px !important;
        }
        /* /\/\/\/\/\/\/\/\/\/\ STANDARD STYLING: MAIN BODY /\/\/\/\/\/\/\/\/\/\ *//**
			* @tab Body
			* @section body style
			* @tip Set the background color for your email's body area.
			*/#templateContainer, .bodyContent
        {
            /*@editable*/
            background-color: #FFFFFF;
        }
        /**
			* @tab Body
			* @section body text
			* @tip Set the styling for your email's main content text. Choose a size and color that is easy to read.
			* @theme main
			*/.bodyContent div
        {
            /*@editable*/
            color: #505050; /*@editable*/
            font-family: Tahoma; /*@editable*/
            font-size: 14px; /*@editable*/
            line-height: 150%; /*@editable*/
            text-align: right;
        }
        /**
			* @tab Body
			* @section body link
			* @tip Set the styling for your email's main content links. Choose a color that helps them stand out from your text.
			*/.bodyContent div a:link, .bodyContent div a:visited, /* Yahoo! Mail Override */ .bodyContent div a .yshortcuts /* Yahoo! Mail Override */
        {
            /*@editable*/
            color: #336699; /*@editable*/
            font-weight: normal; /*@editable*/
            text-decoration: underline;
        }
        .bodyContent img
        {
            display: inline;
            height: auto;
        }
        /* /\/\/\/\/\/\/\/\/\/\ STANDARD STYLING: MIDDLE COLUMNS; LEFT, RIGHT /\/\/\/\/\/\/\/\/\/\ *//**
			* @tab Middle Columns
			* @section left column text
			* @tip Set the styling for your email's left column text. Choose a size and color that is easy to read.
			*/.leftMidColumnContent
        {
            /*@editable*/
            background-color: #FFFFFF;
        }
        /**
			* @tab Middle Columns
			* @section left column text
			* @tip Set the styling for your email's left column text. Choose a size and color that is easy to read.
			*/.leftMidColumnContent div
        {
            /*@editable*/
            color: #505050; /*@editable*/
            font-family: Tahoma; /*@editable*/
            font-size: 14px; /*@editable*/
            line-height: 150%; /*@editable*/
            text-align: right;
        }
        /**
			* @tab Middle Columns
			* @section left column link
			* @tip Set the styling for your email's left column links. Choose a color that helps them stand out from your text.
			*/.leftMidColumnContent div a:link, .leftMidColumnContent div a:visited, /* Yahoo! Mail Override */ .leftMidColumnContent div a .yshortcuts /* Yahoo! Mail Override */
        {
            /*@editable*/
            color: #336699; /*@editable*/
            font-weight: normal; /*@editable*/
            text-decoration: underline;
        }
        .leftMidColumnContent img
        {
            display: inline;
            max-width: 200px !important;
        }
        /**
			* @tab Middle Columns
			* @section right column text
			* @tip Set the styling for your email's right column text. Choose a size and color that is easy to read.
			*/.rightMidColumnContent
        {
            /*@editable*/
            background-color: #FFFFFF;
        }
        /**
			* @tab Middle Columns
			* @section right column text
			* @tip Set the styling for your email's right column text. Choose a size and color that is easy to read.
			*/.rightMidColumnContent div
        {
            /*@editable*/
            color: #505050; /*@editable*/
            font-family: Tahoma; /*@editable*/
            font-size: 14px; /*@editable*/
            line-height: 150%; /*@editable*/
            text-align: right;
        }
        /**
			* @tab Middle Columns
			* @section right column link
			* @tip Set the styling for your email's right column links. Choose a color that helps them stand out from your text.
			*/.rightMidColumnContent div a:link, .rightMidColumnContent div a:visited, /* Yahoo! Mail Override */ .rightMidColumnContent div a .yshortcuts /* Yahoo! Mail Override */
        {
            /*@editable*/
            color: #336699; /*@editable*/
            font-weight: normal; /*@editable*/
            text-decoration: underline;
        }
        .rightMidColumnContent img
        {
            display: inline;
            max-width: 200px !important;
        }
        /* /\/\/\/\/\/\/\/\/\/\ STANDARD STYLING: LOWER COLUMNS; LEFT, CENTER, RIGHT /\/\/\/\/\/\/\/\/\/\ *//**
			* @tab Lower Columns
			* @section left column text
			* @tip Set the styling for your email's left column text. Choose a size and color that is easy to read.
			*/.leftLowerColumnContent
        {
            /*@editable*/
            background-color: #FFFFFF;
        }
        /**
			* @tab Lower Columns
			* @section left column text
			* @tip Set the styling for your email's left column text. Choose a size and color that is easy to read.
			*/.leftLowerColumnContent div
        {
            /*@editable*/
            color: #505050; /*@editable*/
            font-family: Tahoma; /*@editable*/
            font-size: 14px; /*@editable*/
            line-height: 150%; /*@editable*/
            text-align: right;
        }
        /**
			* @tab Lower Columns
			* @section left column link
			* @tip Set the styling for your email's left column links. Choose a color that helps them stand out from your text.
			*/.leftLowerColumnContent div a:link, .leftLowerColumnContent div a:visited, /* Yahoo! Mail Override */ .leftLowerColumnContent div a .yshortcuts /* Yahoo! Mail Override */
        {
            /*@editable*/
            color: #336699; /*@editable*/
            font-weight: normal; /*@editable*/
            text-decoration: underline;
        }
        .leftLowerColumnContent img
        {
            display: inline;
            max-width: 200px !important;
        }
        /**
			* @tab Lower Columns
			* @section center column text
			* @tip Set the styling for your email's center column text. Choose a size and color that is easy to read.
			*/.centerLowerColumnContent
        {
            /*@editable*/
            background-color: #FFFFFF;
        }
        /**
			* @tab Lower Columns
			* @section center column text
			* @tip Set the styling for your email's center column text. Choose a size and color that is easy to read.
			*/.centerLowerColumnContent div
        {
            /*@editable*/
            color: #505050; /*@editable*/
            font-family: Tahoma; /*@editable*/
            font-size: 14px; /*@editable*/
            line-height: 150%; /*@editable*/
            text-align: right;
        }
        /**
			* @tab Lower Columns
			* @section center column link
			* @tip Set the styling for your email's center column links. Choose a color that helps them stand out from your text.
			*/.centerLowerColumnContent div a:link, .centerLowerColumnContent div a:visited, /* Yahoo! Mail Override */ .centerLowerColumnContent div a .yshortcuts /* Yahoo! Mail Override */
        {
            /*@editable*/
            color: #336699; /*@editable*/
            font-weight: normal; /*@editable*/
            text-decoration: underline;
        }
        .centerLowerColumnContent img
        {
            display: inline;
            max-width: 200px !important;
        }
        /**
			* @tab Lower Columns
			* @section right column text
			* @tip Set the styling for your email's right column text. Choose a size and color that is easy to read.
			*/.rightLowerColumnContent
        {
            /*@editable*/
            background-color: #FFFFFF;
        }
        /**
			* @tab Lower Columns
			* @section right column text
			* @tip Set the styling for your email's right column text. Choose a size and color that is easy to read.
			*/.rightLowerColumnContent div
        {
            /*@editable*/
            color: #505050; /*@editable*/
            font-family: Tahoma; /*@editable*/
            font-size: 14px; /*@editable*/
            line-height: 150%; /*@editable*/
            text-align: right;
        }
        /**
			* @tab Lower Columns
			* @section right column link
			* @tip Set the styling for your email's right column links. Choose a color that helps them stand out from your text.
			*/.rightLowerColumnContent div a:link, .rightLowerColumnContent div a:visited, /* Yahoo! Mail Override */ .rightLowerColumnContent div a .yshortcuts /* Yahoo! Mail Override */
        {
            /*@editable*/
            color: #336699; /*@editable*/
            font-weight: normal; /*@editable*/
            text-decoration: underline;
        }
        .rightLowerColumnContent img
        {
            display: inline;
            max-width: 300px;
        }
        /* /\/\/\/\/\/\/\/\/\/\ STANDARD STYLING: FOOTER /\/\/\/\/\/\/\/\/\/\ *//**
			* @tab Footer
			* @section footer style
			* @tip Set the background color and top border for your email's footer area.
			* @theme footer
			*/#templateFooter
        {
            /*@editable*/
            background-color: #FFFFFF; /*@editable*/
            border-top: 0;
        }
        /**
			* @tab Footer
			* @section footer text
			* @tip Set the styling for your email's footer text. Choose a size and color that is easy to read.
			* @theme footer
			*/.footerContent div
        {
            /*@editable*/
            color: #707070; /*@editable*/
            font-family: Tahoma; /*@editable*/
            font-size: 12px; /*@editable*/
            line-height: 125%; /*@editable*/
            text-align: right;
        }
        /**
			* @tab Footer
			* @section footer link
			* @tip Set the styling for your email's footer links. Choose a color that helps them stand out from your text.
			*/.footerContent div a:link, .footerContent div a:visited, /* Yahoo! Mail Override */ .footerContent div a .yshortcuts /* Yahoo! Mail Override */
        {
            /*@editable*/
            color: #336699; /*@editable*/
            font-weight: normal; /*@editable*/
            text-decoration: underline;
        }
        .footerContent img
        {
            display: inline;
        }
        /**
			* @tab Footer
			* @section social bar style
			* @tip Set the background color and border for your email's footer social bar.
			* @theme footer
			*/#social
        {
            /*@editable*/
            background-color: #FAFAFA; /*@editable*/
            border: 0;
        }
        /**
			* @tab Footer
			* @section social bar style
			* @tip Set the background color and border for your email's footer social bar.
			*/#social div
        {
            /*@editable*/
            text-align: center;
        }
        /**
			* @tab Footer
			* @section utility bar style
			* @tip Set the background color and border for your email's footer utility bar.
			* @theme footer
			*/#utility
        {
            /*@editable*/
            background-color: #FFFFFF; /*@editable*/
            border: 0;
        }
        /**
			* @tab Footer
			* @section utility bar style
			* @tip Set the background color and border for your email's footer utility bar.
			*/#utility div
        {
            /*@editable*/
            text-align: center;
        }
        #monkeyRewards img
        {
            max-width: 190px;
        }
    </style>
</head>
<body leftmargin="0" marginwidth="0" topmargin="0" marginheight="0" offset="0" dir="rtl">
    <center>
        <table border="0" cellpadding="0" cellspacing="0" height="100%" width="100%" id="backgroundTable"
            dir="rtl">
            <tr>
                <td align="center" valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" width="600" id="templateContainer">
                        <tr>
                            <td align="center" valign="top">
                                <!-- // Begin Template Header \\ -->
                                <table border="0" cellpadding="0" cellspacing="0" width="600" id="templateHeader">
                                    <tr>
                                        <td class="headerContent">
                                            <!-- // Begin Module: Standard Header Image \\ -->
                                            <a href="http://marcom.eg-mt.com">
                                                <img src="http://marcom.eg-mt.com/Resources/Images/Portal/Logo.png" width="440" id="Img1"
                                                    mc:label="header_image" mc:edit="header_image" mc:allowdesigner mc:allowtext /></a>
                                            <!-- // End Module: Standard Header Image \\ -->
                                        </td>
                                    </tr>
                                </table>
                                <!-- // End Template Header \\ -->
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top" style="100%">
                                <!-- // Begin Template Body \\ -->
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="templateBody">
                                    <tr>
                                        <td valign="top">
                                            <table border="0" cellpadding="0" cellspacing="0" width="600">
                                                <tr>
                                                    <td valign="top" class="bodyContent">
                                                        <!-- // Begin Module: Standard Content \\ -->
                                                        <table border="0" cellpadding="20" cellspacing="0" width="100%" dir="rtl">
                                                            <tr>
                                                                <td valign="top" align="right" dir="rtl">
                                                                    <div mc:edit="std_content00" style="text-align: right;">
                                                                        <h1>
                                                                            عزيزى عميل ماركوم تريد</h1>
                                                                        <br />
                                                                        <h3>
                                                                            نشكرك على قيامك بالتسجيل فى ماركوم تريد.</h3>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <!-- // End Module: Standard Content \\ -->
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <!-- // End Template Body \\ -->
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                <!-- // Begin Template Footer \\ -->
                                <table border="0" cellpadding="10" cellspacing="0" width="600" id="templateFooter">
                                    <tr>
                                        <td valign="top" class="footerContent">
                                            <!-- // Begin Module: Standard Footer \\ -->
                                            <table border="0" cellpadding="10" cellspacing="0" width="100%">
                                                <tr>
                                                    <td colspan="2" valign="middle" id="social">
                                                        <div mc:edit="std_social" style="text-align: center;">
                                                            &nbsp;<a href="https://twitter.com/Marcomtrade">أتبعنا على تويتر</a> | <a href="https://www.facebook.com/Marcom.Trade">
                                                                أتبعنا على فيس بوك</a> | <a href="http://www.linkedin.com/company/marcom-trade">أتبعنا
                                                                    على لينكد ان</a>&nbsp;
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" width="100%" dir="rtl">
                                                        <div mc:edit="std_footer" style="text-align: right;">
                                                            <em>جمبع حقوق الطبع محفوظة 2013 &copy Marcom Trade </em>
                                                            <br />
                                                            الفرع الرئيسى :48 ,شارع إسماعيل حلمى ,سموحة -الأسكندرية 21615 , مصر.
                                                            <br />
                                                            <strong>بريدنا الألكترونى:</strong> sales@marcomtrade.com &nbsp; ,&nbsp; info@marcomtrade.com
                                                            <br />
                                                            تليفون: 3 93 93 42 03 002, 4 94 94 42 03 002
                                                            <br />
                                                            فاكس : 4 94 94 42 03 002
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!-- // End Module: Standard Footer \\ -->
                                        </td>
                                    </tr>
                                </table>
                                <!-- // End Template Footer \\ -->
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
        </table>
    </center>
</body>
</html>
