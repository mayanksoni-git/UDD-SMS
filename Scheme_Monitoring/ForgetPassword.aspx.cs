using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ForgetPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnForgetPassword_Click(object sender, EventArgs e)
    {
        string userInput = txtUserName.Text.Trim();
        DataSet ds = new DataSet();
        ds = new DataLayer().getPassword(userInput);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            // Assuming there's at least one row and the password is in the first column
            string password = ds.Tables[0].Rows[0]["Login_password"].ToString();
            string Person_EmailId = ds.Tables[0].Rows[0]["Person_EmailId"].ToString();

            // Email sending logic
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("kushwaharamdhyan1382002@gmail.com");
                mail.To.Add(Person_EmailId); // Assuming you have a textbox for the email address
                mail.Subject = "Password Recovery";
                mail.Body = "Dear user, your password is: " + password + "";
                mail.IsBodyHtml = false;

                smtpClient.Port = 587; // Use appropriate port number
                smtpClient.Credentials = new System.Net.NetworkCredential("kushwaharamdhyan1382002@gmail.com", "lvbc chbt qwla rbcm");
                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);

                // Optionally provide feedback to the user
                lblMessage.Text = "Password has been sent to your email address.";
            }
            catch (Exception ex)
            {
                // Handle any errors that occurred during sending
                lblMessage.Text = "An error occurred while sending the email.";
                // Log the exception or handle it as needed
            }
        }
        else
        {
            lblMessage.Text = "User not found or no password available.";
        }
    }
}