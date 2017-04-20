<?php
/*
	* Reference website: http://www.daniweb.com/forums/thread97570.html
	* Reference website: http://www.daniweb.com/forums/thread148350.html
*/

require_once "Mail.php";		// PEAR Mail package
require_once ('Mail/mime.php');	// PEAR Mail_Mime packge

/*
	* header("location: ../contactus.php");
	* This is used for redirection of webpage
*/

$name			= $_REQUEST['fname']." ".$_REQUEST['lname'];
$contactno		= $_REQUEST['contactno'];
$altcontactno	= $_REQUEST['altcontactno'];
$faxno			= $_REQUEST['faxno'];
$emailid		= $_REQUEST['email'];
$altemailid		= $_REQUEST['altemail'];
$comments		= $_REQUEST['comments'];

$attname		= $_FILES['attachment']['name'];
$atttype		= $_FILES['attachment']['type'];
$atttmpname		= $_FILES['attachment']['tmp_name'];

move_uploaded_file($atttmpname,"../temp/".$attname);
$sendAttachment	= '../temp/'.$attname;

$to				= "Shalabh Dixit <shalabh2k4@yahoo.com>";
$subject		= "Email received from ".strtoupper($name);
$message		= "<html><body><p style=\"font-weight: normal; color: red; font-size: 11px; font-family: arial;\">Dear SIGMA Helpdesk,<br/><br/>I am ".strtoupper($name)." and following are my details for the query.<br/><br/><b>Contact Number : </b>".$contactno."<br/><b>Alternate Contact Number : </b>".$altcontactno."<br/><b>Fax Number : </b>".$faxno."<br/><b>Email ID : </b>".strtolower($emailid)."<br/><b>Alternate Email ID : </b>".strtolower($altemailid)."<br/><b>Comments / Query : </b>".$comments."<br/><br/>Please take the appropriate action. Thanks !!!</p>";

$headers		= array ('From' => $emailid,'To' => $to, 'Subject' => $subject);
$crlf			= "\n";

$mime			= new Mail_mime($crlf);
$mime->setHTMLBody($message);
$mime->addAttachment($sendAttachment, $atttype);

$body			= $mime->get();
$headers		= $mime->headers($headers);

$host		= "smtp.gmail.com";
$username	= "shalabh2k6@gmail.com";
$password	= "prabhat";

$smtp = Mail::factory('smtp', array ('host' => $host, 'auth' => true, 'username' => $username,'password' => $password));
$mail = $smtp->send($to, $headers, $body);

if (PEAR::isError($mail)){
	echo("<script language=\"JavaScript\" type=\"text/javascript\">alert('".$mail->getMessage() . "');</script>");
	unlink($sendAttachment);
}
else{
	echo("<script language=\"JavaScript\" type=\"text/javascript\">alert(\"Your Comments / Query has been received. Someone will get back to you ASAP\");</script>");
	unlink($sendAttachment);
}

/*
	* Reference website: http://php.bigresource.com/Track/php-5uFNBOPi/
	* Excerpts from the above website: "This situation is a dreadful solution. For every access to the page server will take two hits, and the user has to load the page twice. No good. There is clearly something else wrong with the way your app is written since you actually need to reload the page."
*/

echo "<meta http-equiv=refresh content=\"0; URL=../contactus.php\">";
?>