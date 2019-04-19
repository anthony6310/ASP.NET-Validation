using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			ImageButtonClearFields_Click(ImageButtonClearFields, null);
		}
    }

	protected void ImageButtonClearFields_Click(object sender, ImageClickEventArgs e)
	{
		//lets clear some fields...

		//reset it to the default text
		TextBoxSec3EmailBody.Text = @"Thanks for including me in the planning of your vacation.  I think a Carnival cruise would be a great choice for your next trip.  Carnival offers a broad range of options for onboard fun, including a Waterworks(tm) water park, a luxury spa, award-winning youth programs, and gourmet and casual dining options.  There truly is something for everyone.

Please let me knmow if you have any questions.  I look forward to helping you plan a fantastic vacation.";
		TextBoxSec3Subject.Text = "Your Carnival cruise eBrochure";

		//clear the rest
		TextBoxOfferPrice.Text =
			TextBoxSec2Recipients.Text =
			TextBoxSec4AgencyName.Text =
			TextBoxSec4AgencyWebsite.Text =
			TextBoxSec4City.Text =
			TextBoxSec4EmailAddress.Text =
			TextBoxSec4FaxNumber.Text =
			TextBoxSec4FirstName.Text =
			TextBoxSec4LastName.Text =
			TextBoxSec4PhoneExt.Text =
			TextBoxSec4PhonePart1.Text =
			TextBoxSec4PhonePart2.Text =
			TextBoxSec4PhonePart3.Text =
			TextBoxSec4State.Text =
			TextBoxSec4Street.Text =
			TextBoxSec4TollFreeNumber.Text =
			TextBoxSec4Zip.Text = String.Empty;

        //Clear labels
        ClearLabels();

		DropDownListDays.ClearSelection();
		DropDownListDestinations.ClearSelection();
		CheckBoxSend.Checked = true;	//defaul is true

	}

    protected void ClearLabels()
    {
        lblSection2Err.Text = String.Empty;
        lblSection3BodyErr.Text = String.Empty;
        lblSection3SubjectErr.Text = String.Empty;
        lblSection4AgencyNameErr.Text = String.Empty;
        lblSection4CityErr.Text = String.Empty;
        lblSection4EmailErr.Text = String.Empty;
        lblSection4NameErr.Text = String.Empty;
        lblSection4StateErr.Text = String.Empty;
        lblSection4StreetErr.Text = String.Empty;
        lblSection4ZipErr.Text = String.Empty;
    }

    protected void ImageButtonNext_Click(object sender, ImageClickEventArgs e)
    {
        Boolean errors = false;

        //regex to check emails from section 2
        Regex rxEmail = new Regex(@"^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;,](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        Regex rxFax = new Regex(@"\d{3}-\d{3}-\d{4}",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        Regex rxTollFree = new Regex(@"(1-800|1-877)-\d{3}-\d{4}",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        Regex rxState = new Regex(@"\D{2}",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        Regex rxZip = new Regex(@"^\d{5}(?:[-\s]\d{4})?$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        Regex rxPrice = new Regex(@"\d+",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        //checking section 2 for errors
        if (TextBoxSec2Recipients.Text == "")
        {
            errors = true;
            lblSection2Err.Text = "You must specify at least one email address.";
        }
        else if (!rxEmail.IsMatch(TextBoxSec2Recipients.Text))
        {
            errors = true;
            lblSection2Err.Text = "Email must be correct format";
        }
        else
        {
            lblSection2Err.Text = "";
        }

        //section 3 check
        if (TextBoxSec3Subject.Text == String.Empty)
        {
            errors = true;
            lblSection3SubjectErr.Text = "You must specify a subject for the email.";
        }
        else
        {
            lblSection3SubjectErr.Text = "";
        }

        if(TextBoxSec3EmailBody.Text == String.Empty)
        {
            errors = true;
            lblSection3BodyErr.Text = "The body of the email may not be empty.";
        }
        else if(TextBoxSec3EmailBody.Text.Length > 500)
        {
            lblSection3BodyErr.Text = "Cannot be more than 500 characters in length, you have " + TextBoxSec3EmailBody.Text.Length;
        }
        else
        {
            lblSection3BodyErr.Text = "";
        }

        //array for error message


        //section 4 check
        //email check
        if(TextBoxSec4EmailAddress.Text == String.Empty)
        {
            errors = true;
            lblSection4EmailErr.Text = "You must specify an email address.";
        }
        else if (!rxEmail.IsMatch(TextBoxSec4EmailAddress.Text))
        {
            errors = true;
            lblSection4EmailErr.Text = "Email must be correct format.";
        }
        else
        {
            lblSection4EmailErr.Text = "";
        }

        //first name check
        if (TextBoxSec4FirstName.Text == String.Empty)
        {
            errors = true;
            lblSection4NameErr.Text = "You must specify your first name.";
        }
        else
        {
            lblSection4NameErr.Text = "";
        }

        //Agency name check
        if (TextBoxSec4AgencyName.Text == String.Empty)
        {
            errors = true;
            lblSection4AgencyNameErr.Text = "You must specify your travel agency name.";
        }
        else
        {
            lblSection4AgencyNameErr.Text = "";
        }

        //street check
        if (TextBoxSec4Street.Text == String.Empty)
        {
            errors = true;
            lblSection4StreetErr.Text = "You must specify your travel agency's mailing address.";
        }
        else
        {
            lblSection4StreetErr.Text = "";
        }

        //city check
        if (TextBoxSec4City.Text == String.Empty)
        {
            errors = true;
            lblSection4CityErr.Text = "You must specify your travelagency's city.";
        }
        else
        {
            lblSection4CityErr.Text = "";
        }

        //state check
        if (TextBoxSec4State.Text == String.Empty)
        {
            errors = true;
            lblSection4StateErr.Text = "You must specify your travel agency's state.";
        }
        else if(!rxState.IsMatch(TextBoxSec4State.Text)) 
        {
            errors = true;
            lblSection4StateErr.Text = "State must be exactly 2 characters.";
        }
        else
        {
            lblSection4StateErr.Text = "";
        }

        //zip check
        if (TextBoxSec4Zip.Text == String.Empty)
        {
            errors = true;
            lblSection4ZipErr.Text = "You must specify your travel agency's zip code.";
        }
        else if (!rxZip.IsMatch(TextBoxSec4Zip.Text))
        {
            errors = true;
            lblSection4ZipErr.Text = "Zip code is not in correct format.";
        }
        else
        {
            lblSection4ZipErr.Text = "";
        }

        //fax check
        if(TextBoxSec4FaxNumber.Text.Length <= 0)
        {
            lblSection4FaxErr.Text = "";
        }
        else if (!rxFax.IsMatch(TextBoxSec4FaxNumber.Text))
        {
            errors = true;
            lblSection4FaxErr.Text = "Fax is not in correct format.";
        }
        else
        {
            lblSection4FaxErr.Text = "";
        }

        //toll free number check
        if (TextBoxSec4TollFreeNumber.Text.Length <= 0)
        {
            lblSection4TollErr.Text = "";
        }
        else if (!rxTollFree.IsMatch(TextBoxSec4TollFreeNumber.Text))
        {
            errors = true;
            lblSection4TollErr.Text = "Toll free number is not in correct format.";
        }
        else
        {
            lblSection4TollErr.Text = "";
        }

        //add offer details section
        //days check
        if(DropDownListDestinations.SelectedItem.Text == "Alaska")
        {
            if(DropDownListDays.SelectedIndex < 6)
            {
                errors = true;
                lblSectionDetailsDaysErr.Text = "Trips to Alaska must be 7 days or more";
            }
            else
            {
                lblSectionDetailsDaysErr.Text = "";
            }
        }
        else
        {
            lblSectionDetailsDaysErr.Text = "";
        }

        //price check
        int intPrice = 0;

        if(TextBoxOfferPrice.Text.Length > 0)
        {
            int.TryParse(TextBoxOfferPrice.Text, out intPrice);
        }

        if (intPrice < 100)
        {
            errors = true;
            lblSectionDetailsPriceRangeErr.Text = "Price needs to be more than $100.";
        }
        else if (intPrice > 999)
        {
            errors = true;
            lblSectionDetailsPriceRangeErr.Text = "Price needs to be less than $999.";
        }
        else
        {
            lblSectionDetailsPriceRangeErr.Text = "";
        }

        if (!rxPrice.IsMatch(TextBoxOfferPrice.Text) || intPrice == 0)
        {
            errors = true;
            lblSectionDetailsPriceFormatErr.Text = "Price is not in correct format.";
        }
        else
        {
            lblSectionDetailsPriceFormatErr.Text = "";
        }

        //agreement
        if(!CheckBoxAgrement.Checked)
        {
            errors = true;
            lblAgreementErr.Text = "You must check the box to continue.";
        }
        else
        {
            lblAgreementErr.Text = "";
        }

        //if there are no errors then load passed.aspx
        if (!errors)
        {
            Server.Transfer("Passed.aspx");
        }
	}

}
